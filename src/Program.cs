using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Tetris
{
    class GameLoop : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Window window;

        public GameLoop()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            window = new Window(
               Global.resolution,
               WindowAspect.Keep,
               new RenderTarget2D(GraphicsDevice, Global.resolution.X, Global.resolution.Y)
           );

            Global.content = Content;
            Global.scene = new MainMenu();
        }

        protected override void Update(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Global.scene.Update(delta);
            window.UpdateSize(GraphicsDevice);
            window.resolution = Global.resolution;
            window.cameraPosition = new Point(-1) * Global.cameraPosition;
            Global.mousePosition = window.MousePosition();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            window.Draw(spriteBatch, GraphicsDevice, () => Global.scene.Draw(spriteBatch));

            base.Draw(gameTime);
        }
    }

    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameLoop())
                game.Run();
        }
    }

    public interface Scene
    {
        void Update(float delta);
        void Draw(SpriteBatch sb);
    }
}
