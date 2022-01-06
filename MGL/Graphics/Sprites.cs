using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MGL.Graphics
{
    public sealed class Sprites : IDisposable
    {
        private bool _isDisposed;
        private Game _game;
        private SpriteBatch _sprites;
        private BasicEffect _effect;

        public Sprites(Game game)
        {
            _game = game ?? throw new ArgumentNullException("game");
            _isDisposed = false;
            
            _sprites = new SpriteBatch(_game.GraphicsDevice);

            _effect = new BasicEffect(_game.GraphicsDevice)
            {
                FogEnabled = false,
                LightingEnabled = false,
                TextureEnabled = true,
                VertexColorEnabled = true,
                World = Matrix.Identity,
                Projection = Matrix.Identity,
                View = Matrix.Identity
            };
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _effect?.Dispose();
            _sprites?.Dispose();
            _isDisposed = true;
        }

        public void Begin(MGLCamera camera, bool isTextureFilteringEnabled)
        {
            SamplerState samplerState = isTextureFilteringEnabled ? SamplerState.LinearClamp : SamplerState.PointClamp;

            if (camera == null)
            {
                Viewport viewPort = _game.GraphicsDevice.Viewport;
                _effect.View = Matrix.Identity;
                _effect.Projection = Matrix.CreateOrthographicOffCenter(0f, viewPort.Width, 0f, viewPort.Height, 0f, 1f);
            }
            else
            {
                camera.UpdateMatrices();
                _effect.View = camera.View;
                _effect.Projection = camera.Projection;
            }
            
            _sprites.Begin(blendState: BlendState.AlphaBlend, samplerState: samplerState, rasterizerState: RasterizerState.CullNone, effect: _effect);
        }

        public void End()
        {
            _sprites.End();
        }

        public void Draw(Texture2D texture, Vector2 position)
        {
            _sprites.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipVertically, 0f);
        }

        public void Draw(Texture2D texture, Vector2 origin, Vector2 position, Color color)
        {
            _sprites.Draw(texture, position, null, color, 0f, origin, 1f, SpriteEffects.FlipVertically, 0f);
        }

        public void Draw(Texture2D texture, Rectangle? sourceRectangle, Vector2 position)
        {
            _sprites.Draw(texture, position, sourceRectangle, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.FlipVertically, 0f);
        }

        public void Draw(Texture2D texture, Rectangle? sourceRectangle, Vector2 origin, Vector2 position, float rotation, Vector2 scale, Color color)
        {
            _sprites.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, SpriteEffects.FlipVertically, 0f);
        }

        public void Draw(Texture2D texture, Rectangle? sourceRectangle, Rectangle destinationRectangle)
        {
            _sprites.Draw(texture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipVertically, 0f);
        }

        public void Draw(Texture2D texture, Rectangle? sourceRectangle, Rectangle destinationRectangle, Vector2 origin, float rotation, Color color)
        {
            _sprites.Draw(texture, destinationRectangle, sourceRectangle, color, rotation, origin, SpriteEffects.FlipVertically, 0f);
        }
    }
}
