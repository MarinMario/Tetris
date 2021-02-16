using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris {
    class GameLoop : Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        TetrisGame tetrisGame;

        public GameLoop() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var block = Content.Load<Texture2D>("WhiteBlock");
            var border = Content.Load<Texture2D>("WhiteBorder");
            tetrisGame = new TetrisGame(block, border);
        }

        protected override void Update(GameTime gameTime) {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            tetrisGame.Update(delta);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            tetrisGame.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
    
    public static class Program {
        [STAThread]
        static void Main() {
            using (var game = new GameLoop())
                game.Run();
        }
    }
}
