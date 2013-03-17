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
        Camera camera;

        Texture2D myTexture;


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
            IsFixedTimeStep = false;
            float massFactor = 10000000;

            float earthRadius = 6.3f;
            float earthDensity = 5.513e6f * massFactor;
            float earthOrbitRadius = 152000;

            float moonRadius = 1.737f;
            float moonDensity = 3.45e6f * massFactor;
            float moonOrbitRadius = 385f;

            float venusRadius = 5f;
            float venusDensity = 5.513e6f * massFactor;
            float venusOrbitRadius = 100000;

            float jupiterRadius = 30f;
            float jupiterDensity = 5.513e6f * massFactor;
            float jupiterOrbitRadius = 250000;

            float sunRadius = 109 * earthRadius;
            float sunDensity = 1.408e6f * 100 * massFactor;

            SpaceObject sun;
            SpaceObject earth;
            SpaceObject moon;
            SpaceObject venus;
            SpaceObject jupiter;
            
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1000;
            camera = new Camera()
            {
                Position = new Vector2(0, 0),
                UnitsPerPixel = 4
            };
            space = new Space(524288);


            //Vector2[] shape = { new Vector2(50, 0) };
            //space.createObject(shape, 123000000000, new Vector2(400, 400), typeof(Planet));
            //space.createObject(shape, 123000000000, new Vector2(700, 700), typeof(Planet));
            //space.createObject(shape, 123, new Vector2(400, 700), typeof(Planet));
            //space.createObject(shape, 123000000000, new Vector2(700, 400), typeof(Planet));

            sun = space.createObject( new Vector2[] { new Vector2(sunRadius, 0) }, sunDensity, new Vector2(200000, 200000), typeof(Planet));
            
            float earthVel = -(float)Math.Sqrt(Space.G * sun.Mass / earthOrbitRadius);
            earth = space.createObject(new Vector2[] { new Vector2(earthRadius, 0) }, earthDensity, new Vector2(200000 - earthOrbitRadius, 200000), typeof(Planet));
            earth.Velocity = new Vector2(0, earthVel);
            

            float venusVel = -(float)Math.Sqrt(Space.G * sun.Mass / venusOrbitRadius);
            venus = space.createObject(new Vector2[] { new Vector2(venusRadius, 0) }, venusDensity, new Vector2(200000 - venusOrbitRadius, 200000), typeof(Planet));
            venus.Velocity = new Vector2(0, venusVel);
            

            float jupiterVel = -(float)Math.Sqrt(Space.G * sun.Mass / jupiterOrbitRadius);
            jupiter = space.createObject(new Vector2[] { new Vector2(jupiterRadius, 0) }, jupiterDensity, new Vector2(200000 - jupiterOrbitRadius, 200000), typeof(Planet));
            jupiter.Velocity = new Vector2(0, jupiterVel);

            moon = space.createObject(new Vector2[] { new Vector2(moonRadius, 0) }, moonDensity, new Vector2(200000 - earthOrbitRadius - moonOrbitRadius, 200000), typeof(Planet));
            moon.Velocity = new Vector2(0, earthVel - (float)Math.Sqrt(Space.G * earth.Mass / moonOrbitRadius));

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
            myTexture = Content.Load<Texture2D>("planet");

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

            if (Keyboard.GetState().IsKeyDown(Keys.Subtract))
            {
                camera.Position.X -= (float)(0.03 * camera.UnitsPerPixel * 1000 / 2);
                camera.Position.Y -= (float)(0.03 * camera.UnitsPerPixel * 1000 / 2);
                camera.UnitsPerPixel *= 1.03f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Add))
            {
                camera.Position.X += (float)(0.03f * camera.UnitsPerPixel * 1000 / 2);
                camera.Position.Y += (float)(0.03f * camera.UnitsPerPixel * 1000 / 2);

                camera.UnitsPerPixel *= 0.97f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                camera.Position.Y -= camera.UnitsPerPixel * 3;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                camera.Position.Y += camera.UnitsPerPixel * 3;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                camera.Position.X -= camera.UnitsPerPixel * 3;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                camera.Position.X += camera.UnitsPerPixel * 3;
            }

            space.update((float)gameTime.ElapsedGameTime.TotalSeconds);

            // TODO: Add your update logic here

            GC.Collect();
            base.Update(gameTime);
        }

        private SpaceTreeIterationCallbackResult drawNodesCallback(SpaceTreeNode node, object userData) 
        {
            Vector2 screenPos = (node.Position - camera.Position) * camera.PixelsPerUnit;

            spriteBatch.DrawRectangle(screenPos, new Vector2(node.Size * camera.PixelsPerUnit, node.Size * camera.PixelsPerUnit), Color.Gray, 2);
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
            //space.STree.iterateTree(drawNodesCallback, null);

            Vector2 v1 = camera.Position;
            Vector2 v2 = new Vector2(camera.Position.X + 1000 * camera.UnitsPerPixel, camera.Position.X + 1000 * camera.UnitsPerPixel);

            //foreach (SpaceObject so in space.STree.queryObjects(v1, v2))
            foreach (SpaceObject so in space.SpaceObjects)
            {
                Vector2 screenPos = (so.Position - camera.Position)*camera.PixelsPerUnit;
                
                if (so is Planet)
                {

                    screenPos = so.Position - camera.Position;
                    screenPos.X -= so.ShapeDefinition[0].X;
                    screenPos.Y -= so.ShapeDefinition[0].X;

                    screenPos /= camera.UnitsPerPixel;
                    float scale = so.ShapeDefinition[0].X / (24f * camera.UnitsPerPixel);
                    if (scale < 0.1)
                        scale = 0.1f;
                    spriteBatch.Draw(myTexture, screenPos, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    //spriteBatch.FillRectangle(new Rectangle((int)screenPos.X - 2, (int)screenPos.Y - 2, 4, 4), Color.AliceBlue);
                    //spriteBatch.DrawCircle(screenPos, so.ShapeDefinition[0].X * camera.PixelsPerUnit, 100, Color.AliceBlue);
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
