using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Tetris
{
    enum WindowAspect { Keep, Expand }
    class Window
    {

        public Point resolution;
        public WindowAspect windowAspect;
        public RenderTarget2D renderTarget;
        Point scaledWindowSize = Point.Zero;
        Point windowPosition = Point.Zero;
        public Point cameraPosition = Point.Zero;


        public Window(Point resolution, WindowAspect windowAspect, RenderTarget2D renderTarget)
        {
            this.resolution = resolution;
            this.windowAspect = windowAspect;
            this.renderTarget = renderTarget;
        }

        public Point MousePosition()
        {
            var m = Mouse.GetState().Position;
            var mScaled = (m - windowPosition - cameraPosition) * resolution / scaledWindowSize;

            return mScaled;
        }

        public void UpdateSize(GraphicsDevice graphicsDevice)
        {
            var size = graphicsDevice.Viewport.Bounds.Size;
            switch (windowAspect)
            {
                case WindowAspect.Expand:
                    scaledWindowSize = size;
                    windowPosition = Point.Zero;
                    break;
                case WindowAspect.Keep:
                    {
                        var resRatio = (float)resolution.X / (float)resolution.Y;
                        var scaledRes = new Point((int)(size.Y * resRatio), (int)(size.X / resRatio));
                        scaledWindowSize =
                            size.X > size.Y && size.X > scaledRes.X
                                ? new Point(scaledRes.X, size.Y)
                                : new Point(size.X, scaledRes.Y);
                        windowPosition = (size - scaledWindowSize) / new Point(2);
                    }
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Action draw)
        {
            graphicsDevice.SetRenderTarget(renderTarget);
            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                transformMatrix: Matrix.CreateTranslation(cameraPosition.X, cameraPosition.Y, 0f)
            );
            draw();
            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(renderTarget, new Rectangle(windowPosition, scaledWindowSize), Color.White);
            spriteBatch.End();
        }
    }
}