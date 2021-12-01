using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Tetris
{
    static class Global
    {
        public static Scene scene;
        public static Point resolution = new Point(1280, 720);
        public static Point mousePosition;
        public static Point cameraPosition;
        public static ContentManager content;
    }
}