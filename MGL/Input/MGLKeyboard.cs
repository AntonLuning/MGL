using Microsoft.Xna.Framework.Input;
using System;

namespace MGL.Input
{
    public sealed class MGLKeyboard
    {
        private static readonly Lazy<MGLKeyboard> _lazy = new Lazy<MGLKeyboard>(() => new MGLKeyboard());
        public static MGLKeyboard Instance { get { return _lazy.Value; } }

        private KeyboardState _state;
        private KeyboardState _previousState;

        public MGLKeyboard()
        {
            _previousState = Keyboard.GetState();
            _state = _previousState;
        }

        public void Update()
        {
            _previousState = _state;
            _state = Keyboard.GetState();
        }

        public bool IsKeyDown(Keys key)
        {
            return _state.IsKeyDown(key);
        }
        public bool IsKeyClicked(Keys key)
        {
            return _state.IsKeyDown(key) && !_previousState.IsKeyDown(key);
        }

        public bool IsKeyClicked(Keys key1, Keys key2)
        {
            return _state.IsKeyDown(key1) && _state.IsKeyDown(key2) && !_previousState.IsKeyDown(key2);
        }
    }
}
