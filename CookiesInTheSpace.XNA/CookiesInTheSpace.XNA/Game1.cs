using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Box2D.XNA;
using C3.XNA;

namespace CookiesInTheSpace.XNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Space space;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1000;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1000;
            
            // TODO: Add your initialization logic here
            space = new Space(1000);
            Vector2[] shape = { new Vector2(50, 0) };
            space.createObject(shape, 123000000000, new Vector2(400, 400), typeof(Planet));
            space.createObject(shape, 123000000000, new Vector2(700, 700), typeof(Planet));
            space.createObject(shape, 123, new Vector2(400, 700), typeof(Planet));
            space.createObject(shape, 123000000000, new Vector2(700, 400), typeof(Planet));

            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            space.update((float)gameTime.ElapsedGameTime.TotalSeconds);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private SpaceTreeIterationCallbackResult drawNodesCallback(SpaceTreeNode node, object userData) 
        {
            spriteBatch.DrawRectangle(node.Position, new Vector2(node.Size, node.Size), Color.Gray, 2);
            return SpaceTreeIterationCallbackResult.CONTINUE_STEP_INTO;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            space.STree.iterateTree(drawNodesCallback, null);


            foreach (SpaceObject so in space.STree.queryObjects(new Vector2(0,0), new Vector2(1000, 1000))) 
            {

                if (so is Planet)
                {
                    spriteBatch.FillRectangle(new Rectangle((int)so.Position.X - 2, (int)so.Position.Y - 2, 4, 4), Color.AliceBlue);
                    spriteBatch.DrawCircle(so.Position, so.ShapeDefinition[0].X, 100, Color.AliceBlue);
                }
                else
                {
                    Vector2 op = new Vector2(-1, -1);
                    foreach (Vector2 p in so.ShapeDefinition)
                    {
                        if (op.X != -1)
                        {
                            spriteBatch.DrawLine(op + so.Position, p + so.Position, Color.White);

                        }

                        op = p;
                    }

                    spriteBatch.DrawLine(op + so.Position, so.ShapeDefinition[0] + so.Position, Color.White);
                }
                
                //Vector2 v = so.Position - new Vector2(5, 5);
                //spriteBatch.FillRectangle(v, new Vector2(10, 10), Color.Wheat);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
