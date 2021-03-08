using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris {
    class GameLoop : Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        TetrisGame tetrisGame;
        MainMenu mainMenu = new MainMenu();

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
            switch(Global.scene) {
                case 0:
                    mainMenu.Start(Content);
                    break;
                case 1:
                    break;
            }
        }

        protected override void Update(GameTime gameTime) {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch(Global.scene) {
                case 0:
                    mainMenu.Update(delta);
                    break;
                case 1:
                    tetrisGame.Update(delta);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            switch(Global.scene) {
                case 0:
                    mainMenu.Draw(spriteBatch);
                    break;
                case 1:
                    tetrisGame.Draw(spriteBatch);
                    break;
            }

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
