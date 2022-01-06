using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MGL.Graphics;
using System;

namespace MGL.Input
{
    public sealed class Mouse
    {
        private static readonly Lazy<Mouse> _lazy = new Lazy<Mouse>(() => new Mouse());
        public static Mouse Instance { get { return _lazy.Value; } }

        private MouseState _state;
        private MouseState _previousState;
        public Point WindowPosition { get { return _state.Position; } }

        public Mouse()
        {
            _previousState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            _state = _previousState;
        }

        public void Update()
        {         
            _previousState = _state;
            _state = Microsoft.Xna.Framework.Input.Mouse.GetState();
        }

        public bool IsLeftButtonDown()
        {
            return _state.LeftButton == ButtonState.Pressed;
        }

        public bool IsRightButtonDown()
        {
            return _state.RightButton == ButtonState.Pressed;
        }

        public bool IsMiddleButtonDown()
        {
            return _state.MiddleButton == ButtonState.Pressed;
        }

        public bool IsLeftButtonClicked()
        {
            return _state.LeftButton == ButtonState.Pressed && _previousState.LeftButton != ButtonState.Pressed;
        }

        public bool IsRightButtonClicked()
        {
            return _state.RightButton == ButtonState.Pressed && _previousState.RightButton != ButtonState.Pressed;
        }

        public bool IsMiddleButtonClicked()
        {
            return _state.MiddleButton == ButtonState.Pressed && _previousState.MiddleButton != ButtonState.Pressed;
        }

        public bool IsScrolledUp()
        {
            return _previousState.ScrollWheelValue < _state.ScrollWheelValue;
        }

        public bool IsScrolledDown()
        {
            return _previousState.ScrollWheelValue > _state.ScrollWheelValue;
        }

        public Vector2 GetScreenPosition(Screen screen)
        {
            Rectangle screenDestinationRectangle = screen.CalculateDestinationRectangle();

            float sX = WindowPosition.X - screenDestinationRectangle.X;
            float sY = WindowPosition.Y - screenDestinationRectangle.Y;

            sX /= screenDestinationRectangle.Width;
            sY /= screenDestinationRectangle.Height;

            sX *= screen.Width;
            sY *= screen.Height;

            sY = screen.Height - sY;

            return new Vector2(sX, sY);
        }
    }
}
