using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MGL
{
    public static partial class Util
    {
        public enum ScreenOriginPosition
        {
            TopLeft, BottomLeft, Middle
        }

        public static void ToggleFullScreen(GraphicsDeviceManager graphics)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            graphics.HardwareModeSwitch = false;
            graphics.ToggleFullScreen();
        }

        public static void SetFullScreen(GraphicsDeviceManager graphics)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            if (graphics.IsFullScreen)
                return;

            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }

        public static void UnsetFullScreen(GraphicsDeviceManager graphics)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            if (!graphics.IsFullScreen)
                return;

            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
        }

        public static void SetRelativeBackBufferSize(GraphicsDeviceManager graphics, float ratio)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            ratio = Clamp(ratio, 0.25f, 1f);

            DisplayMode dm = graphics.GraphicsDevice.DisplayMode;

            graphics.PreferredBackBufferWidth = (int)MathF.Round(dm.Width * ratio);
            graphics.PreferredBackBufferHeight = (int)MathF.Round(dm.Height * ratio);
            graphics.ApplyChanges();
        }

        public static void SetRelativeBackBufferSize(GraphicsDeviceManager graphics, float ratio, float aspectRatio)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            ratio = Clamp(ratio, 0.25f, 1f);

            DisplayMode dm = graphics.GraphicsDevice.DisplayMode;

            int height = (int)MathF.Round(dm.Height * ratio);
            int width = (int)MathF.Round(height * aspectRatio);

            if (aspectRatio < 1f)
            {
                width = (int)MathF.Round(dm.Width / ratio);
                height = (int)MathF.Round(width / aspectRatio);
            }

            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.ApplyChanges();
        }
    }
}
