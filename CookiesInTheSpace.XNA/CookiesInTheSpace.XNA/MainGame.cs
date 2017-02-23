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
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Space space;
        Camera camera;
        float spaceSize;
        int universalCounter = 0;

        // FOR MASS AMPLITUDE TESTS
        //float minMass, maxMass = 0;

        Texture2D myTexture;


        public MainGame()
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
            spaceSize = 4096;
            space = new Space(spaceSize);
            IsFixedTimeStep = false;
            float massFactor = 100000;
            float SU = 0.125f;

            float centerPos1 = spaceSize / 8;
            float centerPos2 = (spaceSize / 16) * 14;
            float centerPos3 = (spaceSize / 16) * 10;
            float centerPos4 = (spaceSize / 16) * 7;
            float earthRadius = SU;
            float earthDensity = 8e3f * massFactor * (3 / 2) * SU;
            float earthOrbitRadius = 4096f * SU / 4;

            float moonRadius = 0.2f * SU;
            float moonDensity = 4e3f * massFactor * (3 / 2) * moonRadius;
            float moonOrbitRadius = 8f * SU;

            //float moonMRadius = 0.03125f * SU;
            //float moonMDensity = 2e3f * massFactor * (3 / 2) * moonMRadius;
            //float moonMOrbitRadius = 0.625f * SU;

            float moonMRadius = 0.125f * SU;
            float moonMDensity = 2e3f * massFactor * (3 / 2) * moonMRadius;
            float moonMOrbitRadius = 5f * SU;

            float venusRadius = 0.8f * SU;
            float venusDensity = 6e3f * massFactor * (3 / 2) * venusRadius;
            float venusOrbitRadius = 3072f * SU / 4;

            float jupiterRadius = 8f * SU;
            float jupiterDensity = 2e3f * massFactor * (3 / 2) * jupiterRadius;
            float jupiterOrbitRadius = 8192f * SU / 4;

            float sunRadius = 64 * earthRadius;
            float sunDensity = 2.6e3f * massFactor * (3 / 2) * sunRadius;

            SpaceObject sun;
            SpaceObject earth;
            SpaceObject moon;
            SpaceObject venus;
            SpaceObject jupiter;
            SpaceObject moonM;

            //Vector2[] shape = { new Vector2(50, 0) };
            //space.createObject(shape, 123000000000, new Vector2(400, 400), typeof(Planet));
            //space.createObject(shape, 123000000000, new Vector2(700, 700), typeof(Planet));
            //space.createObject(shape, 123, new Vector2(400, 700), typeof(Planet));
            //space.createObject(shape, 123000000000, new Vector2(700, 400), typeof(Planet));

            sun = space.createObject(new Vector2[] { new Vector2(sunRadius, 0) }, sunDensity, new Vector2(centerPos1, centerPos1), typeof(Planet));

            float earthVel = -(float)Math.Sqrt(Space.G * sun.Mass / earthOrbitRadius);
            earth = space.createObject(new Vector2[] { new Vector2(earthRadius, 0) }, earthDensity, new Vector2(centerPos1 - earthOrbitRadius, centerPos1), typeof(Planet));
            earth.Velocity = new Vector2(0, earthVel);


            float venusVel = -(float)Math.Sqrt(Space.G * sun.Mass / venusOrbitRadius);
            venus = space.createObject(new Vector2[] { new Vector2(venusRadius, 0) }, venusDensity, new Vector2(centerPos1 - venusOrbitRadius, centerPos1), typeof(Planet));
            venus.Velocity = new Vector2(0, venusVel);


            float jupiterVel = -(float)Math.Sqrt(Space.G * sun.Mass / jupiterOrbitRadius);
            jupiter = space.createObject(new Vector2[] { new Vector2(jupiterRadius, 0) }, jupiterDensity, new Vector2(centerPos1 - jupiterOrbitRadius, centerPos1), typeof(Planet));
            jupiter.Velocity = new Vector2(0, jupiterVel);

            moon = space.createObject(new Vector2[] { new Vector2(moonRadius, 0) }, moonDensity, new Vector2(centerPos1 - earthOrbitRadius - moonOrbitRadius, centerPos1), typeof(Planet));
            moon.Velocity = new Vector2(0, earthVel - (float)Math.Sqrt(Space.G * earth.Mass / moonOrbitRadius));

            //moonM = space.createObject(new Vector2[] { new Vector2(moonMRadius, 0) }, moonMDensity, new Vector2(moon.Position.X - moonMOrbitRadius, centerPos1), typeof(Planet));
            //moonM.Velocity = new Vector2(0, moon.Velocity.Y - (float)Math.Sqrt(Space.G * moon.Mass / moonMOrbitRadius));

            moonM = space.createObject(new Vector2[] { new Vector2(moonMRadius, 0) }, moonMDensity, new Vector2(centerPos1 - earthOrbitRadius + moonMOrbitRadius, centerPos1), typeof(Planet));
            moonM.Velocity = new Vector2(0, earthVel - (float)Math.Sqrt(Space.G * earth.Mass / moonMOrbitRadius));

            // 2ND SYSTEM
            sun = space.createObject(new Vector2[] { new Vector2(sunRadius, 0) }, sunDensity, new Vector2(centerPos2, centerPos2), typeof(Planet));

            earth = space.createObject(new Vector2[] { new Vector2(earthRadius, 0) }, earthDensity, new Vector2(centerPos2 - earthOrbitRadius, centerPos2), typeof(Planet));
            earth.Velocity = new Vector2(0, earthVel);

            venus = space.createObject(new Vector2[] { new Vector2(venusRadius, 0) }, venusDensity, new Vector2(centerPos2 - venusOrbitRadius, centerPos2), typeof(Planet));
            venus.Velocity = new Vector2(0, venusVel);

            jupiter = space.createObject(new Vector2[] { new Vector2(jupiterRadius, 0) }, jupiterDensity, new Vector2(centerPos2 - jupiterOrbitRadius, centerPos2), typeof(Planet));
            jupiter.Velocity = new Vector2(0, jupiterVel);

            moon = space.createObject(new Vector2[] { new Vector2(moonRadius, 0) }, moonDensity, new Vector2(centerPos2 - earthOrbitRadius - moonOrbitRadius, centerPos2), typeof(Planet));
            moon.Velocity = new Vector2(0, earthVel - (float)Math.Sqrt(Space.G * earth.Mass / moonOrbitRadius));

            //moonM = space.createObject(new Vector2[] { new Vector2(moonMRadius, 0) }, moonMDensity, new Vector2(moon.Position.X - moonMOrbitRadius, centerPos2), typeof(Planet));
            //moonM.Velocity = new Vector2(0, moon.Velocity.Y - (float)Math.Sqrt(Space.G * moon.Mass / moonMOrbitRadius));

            moonM = space.createObject(new Vector2[] { new Vector2(moonMRadius, 0) }, moonMDensity, new Vector2(centerPos2 - earthOrbitRadius + moonMOrbitRadius, centerPos2), typeof(Planet));
            moonM.Velocity = new Vector2(0, earthVel - (float)Math.Sqrt(Space.G * earth.Mass / moonMOrbitRadius));

            // 3RD SYSTEM
            sun = space.createObject(new Vector2[] { new Vector2(sunRadius, 0) }, sunDensity, new Vector2(centerPos3, centerPos3), typeof(Planet));

            earth = space.createObject(new Vector2[] { new Vector2(earthRadius, 0) }, earthDensity, new Vector2(centerPos3 - earthOrbitRadius, centerPos3), typeof(Planet));
            earth.Velocity = new Vector2(0, earthVel);

            venus = space.createObject(new Vector2[] { new Vector2(venusRadius, 0) }, venusDensity, new Vector2(centerPos3 - venusOrbitRadius, centerPos3), typeof(Planet));
            venus.Velocity = new Vector2(0, venusVel);

            jupiter = space.createObject(new Vector2[] { new Vector2(jupiterRadius, 0) }, jupiterDensity, new Vector2(centerPos3 - jupiterOrbitRadius, centerPos3), typeof(Planet));
            jupiter.Velocity = new Vector2(0, jupiterVel);

            moon = space.createObject(new Vector2[] { new Vector2(moonRadius, 0) }, moonDensity, new Vector2(centerPos3 - earthOrbitRadius - moonOrbitRadius, centerPos3), typeof(Planet));
            moon.Velocity = new Vector2(0, earthVel - (float)Math.Sqrt(Space.G * earth.Mass / moonOrbitRadius));

            //moonM = space.createObject(new Vector2[] { new Vector2(moonMRadius, 0) }, moonMDensity, new Vector2(moon.Position.X - moonMOrbitRadius, centerPos3), typeof(Planet));
            //moonM.Velocity = new Vector2(0, moon.Velocity.Y - (float)Math.Sqrt(Space.G * moon.Mass / moonMOrbitRadius));

            moonM = space.createObject(new Vector2[] { new Vector2(moonMRadius, 0) }, moonMDensity, new Vector2(centerPos3 - earthOrbitRadius + moonMOrbitRadius, centerPos3), typeof(Planet));
            moonM.Velocity = new Vector2(0, earthVel - (float)Math.Sqrt(Space.G * earth.Mass / moonMOrbitRadius));

            // 4TH SYSTEM
            sun = space.createObject(new Vector2[] { new Vector2(sunRadius, 0) }, sunDensity, new Vector2(centerPos4, centerPos4), typeof(Planet));

            earth = space.createObject(new Vector2[] { new Vector2(earthRadius, 0) }, earthDensity, new Vector2(centerPos4 - earthOrbitRadius, centerPos4), typeof(Planet));
            earth.Velocity = new Vector2(0, earthVel);

            venus = space.createObject(new Vector2[] { new Vector2(venusRadius, 0) }, venusDensity, new Vector2(centerPos4 - venusOrbitRadius, centerPos4), typeof(Planet));
            venus.Velocity = new Vector2(0, venusVel);

            jupiter = space.createObject(new Vector2[] { new Vector2(jupiterRadius, 0) }, jupiterDensity, new Vector2(centerPos4 - jupiterOrbitRadius, centerPos4), typeof(Planet));
            jupiter.Velocity = new Vector2(0, jupiterVel);

            moon = space.createObject(new Vector2[] { new Vector2(moonRadius, 0) }, moonDensity, new Vector2(centerPos4 - earthOrbitRadius - moonOrbitRadius, centerPos4), typeof(Planet));
            moon.Velocity = new Vector2(0, earthVel - (float)Math.Sqrt(Space.G * earth.Mass / moonOrbitRadius));

            //moonM = space.createObject(new Vector2[] { new Vector2(moonMRadius, 0) }, moonMDensity, new Vector2(moon.Position.X - moonMOrbitRadius, centerPos4), typeof(Planet));
            //moonM.Velocity = new Vector2(0, moon.Velocity.Y - (float)Math.Sqrt(Space.G * moon.Mass / moonMOrbitRadius));

            moonM = space.createObject(new Vector2[] { new Vector2(moonMRadius, 0) }, moonMDensity, new Vector2(centerPos4 - earthOrbitRadius + moonMOrbitRadius, centerPos4), typeof(Planet));
            moonM.Velocity = new Vector2(0, earthVel - (float)Math.Sqrt(Space.G * earth.Mass / moonMOrbitRadius));

            float centerPos = spaceSize / 2;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1000;
            camera = new Camera(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight)
            {
                OldPosition = new Vector2(centerPos, centerPos),
                UnitsPerPixel = spaceSize / (float)graphics.PreferredBackBufferHeight
            };

            // FOR MASS AMPLITUDE TESTS
            //foreach (SpaceObject so in space.SpaceObjects)
            //{
            //    minMass += so.Mass;
            //}
            //maxMass = minMass;
            // END

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
                if (camera.UnitsPerPixel < this.spaceSize / this.graphics.PreferredBackBufferHeight)
                    camera.Zoom(-1);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Add))
            {
                camera.Zoom(1);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                camera.Update(0, -10);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                camera.Update(0, 10);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                camera.Update(-10, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                camera.Update(10, 0);
            }


            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                if (universalCounter > 0) universalCounter -= 1;
                camera.Update(space.SpaceObjects[universalCounter/5]);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                if(universalCounter < space.SpaceObjects.Count*5-1) universalCounter += 1;
                camera.Update(space.SpaceObjects[universalCounter/5]);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                camera.FreeLock();
            }

            space.update(50*(float)gameTime.ElapsedGameTime.TotalSeconds);
            camera.Update();

            //if (space.STree.totalMass() > maxMass) maxMass = space.STree.totalMass();
            //else if (space.STree.totalMass() < minMass) minMass = space.STree.totalMass();
            //Window.Title = "Zoom: " + camera.PixelsPerUnit;
            //Window.Title = "Total mass: " + (space.STree.totalMass()) + ", min " + minMass + ", max " + maxMass;
            //Window.Title = "Camera position: ( " + (camera.Position.X) + " , " + (camera.Position.Y) + " )";
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
