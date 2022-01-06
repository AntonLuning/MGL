using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MGL.Graphics
{
    public sealed partial class Shapes : IDisposable
    {
        private const int MAX_VERTEX_COUNT = 1024;
        private const int MAX_INDEX_COUNT = 3072;   // 3 * MAX_VERTEX_COUNT

        private const float MIN_LINE_THICKNESS = 2f;
        private const float MAX_LINE_THICKNESS = 10f;

        private bool _isDisposed;
        private Game _game;
        private Camera _camera;
        private BasicEffect _effect;

        private VertexPositionColor[] _vertices;
        private int[] _indices;
        private int _shapeCount;
        private int _vertexCount;
        private int _indexCount;

        private bool _isStarted;

        public Shapes(Game game)
        {
            _game = game ?? throw new ArgumentNullException("game");
            _isDisposed = false;

            _camera = null;

            _effect = new BasicEffect(_game.GraphicsDevice)
            {
                FogEnabled = false,
                LightingEnabled = false,
                TextureEnabled = false,
                VertexColorEnabled = true,
                World = Matrix.Identity,
                Projection = Matrix.Identity,
                View = Matrix.Identity
            };

            _vertices = new VertexPositionColor[MAX_VERTEX_COUNT];
            _indices = new int[MAX_INDEX_COUNT];
            _shapeCount = 0;
            _vertexCount = 0;
            _indexCount = 0;

            _isStarted = false;
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _effect?.Dispose();
            _isDisposed = true;
        }

        public void Begin(Camera camera)
        {
            if (_isStarted)
                throw new InvalidOperationException("Batching already started.");

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

            _camera = camera;

            _isStarted = true;
        }

        public void End()
        {
            if (!_isStarted)
                throw new InvalidOperationException("Batching not started.");

            Flush();
            _isStarted = false;
        }

        private void Flush()
        {
            if (_shapeCount == 0)
                return;

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _game.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, _vertices, 0, _vertexCount, _indices, 0, _indexCount / 3);
            }

            _shapeCount = 0;
            _vertexCount = 0;
            _indexCount = 0;
        }

        private void EnsureSpace(int shapeVertexCount, int shapeIndexCount)
        {
            if(shapeVertexCount > MAX_VERTEX_COUNT || shapeIndexCount > MAX_INDEX_COUNT)
                throw new ArgumentOutOfRangeException("Max vertex or index count is not high enough.");

            if (_vertexCount + shapeVertexCount > MAX_VERTEX_COUNT || _indexCount + shapeIndexCount > MAX_INDEX_COUNT)
                Flush();
        }
    }
}
