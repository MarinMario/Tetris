using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Tetris {
    class MainMenu {

        Button start;

        public void Start(ContentManager content) {
            var texture = content.Load<Texture2D>("WhiteBlock");
            start = new Button(texture, new Rectangle(100, 100, 50, 50));
        }

        public void Update(float delta) {
            if (start.Pressed())
                Global.scene = 1;
        }

        public void Draw(SpriteBatch sb) {
            start.Draw(sb);
        }
    }
}