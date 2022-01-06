using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MGL.Graphics
{
    public sealed class MGLScreen : IDisposable
    {
        private const int MIN_DIM = 128;
        private const int MAX_DIM = 4096;

        private bool _isDisposed;
        private Game _game;
        private RenderTarget2D _renderTarget;
        private bool _isSet;

        public int Width { get { return _renderTarget.Width; } }
        public int Height { get { return _renderTarget.Height; } }

        public MGLScreen(Game game, int width, int height)
        {
            _game = game ?? throw new ArgumentNullException("game");
            _isDisposed = false;

            width = MGLUtil.Clamp(width, MIN_DIM, MAX_DIM);
            height = MGLUtil.Clamp(height, MIN_DIM, MAX_DIM);
            _renderTarget = new RenderTarget2D(_game.GraphicsDevice, width, height);
            _isSet = false;
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _renderTarget?.Dispose();
            _isDisposed = true;
        }

        public void Set()
        {
            if (_isSet)
                throw new InvalidOperationException("Render target is already set.");

            _game.GraphicsDevice.SetRenderTarget(_renderTarget);
            _isSet = true;
        }

        public void UnSet()
        {
            if (!_isSet)
                throw new InvalidOperationException("Render target is not set.");

            _game.GraphicsDevice.SetRenderTarget(null);
            _isSet = false;
        }

        public void Present(Sprites sprites, bool textureFiltering = true)
        {
            if (sprites == null)
                throw new ArgumentNullException("sprites");

#if DEBUG
            _game.GraphicsDevice.Clear(Color.Pink);
#else
            _game.GraphicsDevice.Clear(Color.Black);
#endif

            Rectangle destinationRectangle = CalculateDestinationRectangle();

            sprites.Begin(null, textureFiltering);
            sprites.Draw(_renderTarget, null, destinationRectangle, Vector2.Zero, 0f, Color.White);
            sprites.End();
        }

        internal Rectangle CalculateDestinationRectangle()
        {
            Rectangle backBufferBounds = _game.GraphicsDevice.PresentationParameters.Bounds;
            float backBufferAR = (float)backBufferBounds.Width / backBufferBounds.Height;
            float screenAR = (float)Width / Height;

            float rX = 0f;
            float rY = 0f;
            float rW = backBufferBounds.Width;
            float rH = backBufferBounds.Height;

            if (backBufferAR > screenAR)
            {
                rW = rH * screenAR;
                rX = (backBufferBounds.Width - rW) / 2f;
            }
            else if (backBufferAR < screenAR)
            {
                rH = rW / screenAR;
                rY = (backBufferBounds.Height - rH) / 2f;
            }

            return new Rectangle((int)rX, (int)rY, (int)rW, (int)rH);
        }

        // TODO: Option to determine if the gameplay screen should be clipped or extended accordingly to the backBufferSize.
        // Option in the constructor -> If statement in the Present() method
    }
}
