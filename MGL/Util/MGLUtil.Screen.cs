using Microsoft.Xna.Framework;
using System;

namespace MGL
{
    public static partial class MGLUtil
    {
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
    }
}
