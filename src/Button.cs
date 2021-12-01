using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Tetris
{
    class Button
    {
        Texture2D texture;
        Rectangle rect;

        public Button(Texture2D texture, Rectangle rect)
        {
            this.texture = texture;
            this.rect = rect;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rect, Color.White);
        }

        public bool Pressed()
        {
            var ms = Mouse.GetState();
            return rect.Contains(Global.mousePosition) && ms.LeftButton == ButtonState.Pressed;
        }
    }
}