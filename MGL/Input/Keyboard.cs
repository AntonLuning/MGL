using Microsoft.Xna.Framework.Input;
using System;

namespace MGL.Input
{
    public sealed class Keyboard
    {
        private static readonly Lazy<Keyboard> _lazy = new Lazy<Keyboard>(() => new Keyboard());
        public static Keyboard Instance { get { return _lazy.Value; } }

        private KeyboardState _state;
        private KeyboardState _previousState;

        public Keyboard()
        {
            _previousState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            _state = _previousState;
        }

        public void Update()
        {
            _previousState = _state;
            _state = Microsoft.Xna.Framework.Input.Keyboard.GetState();
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
