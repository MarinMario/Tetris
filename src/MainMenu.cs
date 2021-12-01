using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Tetris
{
    class MainMenu : Scene
    {

        Button start;
        Texture2D whiteBlock;

        public MainMenu()
        {
            whiteBlock = Global.content.Load<Texture2D>("WhiteBlock");
            var x = Global.resolution.X / 2 - whiteBlock.Width / 2;
            var y = Global.resolution.Y / 2 - whiteBlock.Height / 2;
            start = new Button(whiteBlock, new Rectangle(x, y, whiteBlock.Width, whiteBlock.Height));
        }

        public void Update(float delta)
        {
            if (start.Pressed())
                Global.scene = new TetrisGame();
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(whiteBlock, new Rectangle(0, 0, Global.resolution.X, Global.resolution.Y), Color.Gray);
            start.Draw(sb);
        }
    }
}