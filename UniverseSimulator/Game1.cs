using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using UniverseSimulator.UniverseSimulatorStuff;

namespace UniverseSimulator
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const string window_title_base = "UniverseSimulator";

        Effect simpleShader;
        //BlendState blendState;

        //BlendState gradientMix;

        Texture2D gradientText;

        ParticlePlayground playground;
        Camera2d camera;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            playground = new ParticlePlayground(new Vector2(-200, -200), 400, 400, 40, 40, 2, -.05f, 16f, Color.Red);
            //playground = new ParticlePlayground(600, 600, 50, 50, 2, -12.5f, 13f, Color.Red);

            //gradientMix = new BlendState();
            //gradientMix.AlphaBlendFunction = BlendFunction.Add;
            //gradientMix.AlphaDestinationBlend = Blend.

            IsMouseVisible = true;
            IsFixedTimeStep = false;

            graphics.SynchronizeWithVerticalRetrace = false;

            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 600;

            graphics.ApplyChanges();

            camera = new Camera2d();

            gradientText = playground.particleGradient.GetTexture(10);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            simpleShader = Content.Load<Effect>("GradientApplyShader");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            playground.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            int fps = (int)Math.Round(1000 / gameTime.ElapsedGameTime.TotalMilliseconds);
            Window.Title = window_title_base + $" FPS: {fps}";

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, effect: simpleShader, transformMatrix: camera.get_transformation(graphics));
            simpleShader.Parameters["gradient"].SetValue(gradientText);
            //simpleShader.Parameters["width"].SetValue(gradientText.Width);
            playground.Draw(spriteBatch);
            //spriteBatch.Draw(gradientText, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 20f, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
