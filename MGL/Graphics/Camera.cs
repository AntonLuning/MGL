using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MGL.Graphics
{
    public sealed class Camera
    {
        private const float MIN_Z = 1f;
        private const float MAX_Z = 2048f;
        private const int MIN_ZOOM = 1;
        private const int MAX_ZOOM = 20;

        private float _aspectRatio;
        private float _fieldOfView;
        private Vector2 _position;
        private float _baseZ;
        private float _z;
        private float _zoom;
        
        private Matrix _view;
        private Matrix _projection;

        public Vector2 Position { get { return _position; } }
        public float BaseZ { get { return _baseZ; } }
        public float Z { get { return _z; } }
        public Matrix View { get { return _view; } }
        public Matrix Projection { get { return _projection; } }

        public Camera(Screen screen)
        {
            if (screen == null)
                throw new ArgumentNullException("screen");

            _aspectRatio = (float)screen.Width / screen.Height;
            _fieldOfView = MathHelper.PiOver2;
            _position = new Vector2(0, 0);
            _baseZ = GetZFromHeight(screen.Height);
            _z = _baseZ;
            _zoom = 1;

            UpdateMatrices();
        }

        public void UpdateMatrices()
        {
            _view = Matrix.CreateLookAt(new Vector3(0, 0, _z), Vector3.Zero, Vector3.Up);
            _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOfView, _aspectRatio, MIN_Z, MAX_Z);
        }

        public void Move(Vector2 amount)
        {
            _position += amount;
        }

        public void MoveTo(Vector2 position)
        {
            _position = position;
        }

        public float GetZFromHeight(float height)
        {
            return (0.5f * height) / MathF.Tan(0.5f * _fieldOfView);
        }

        public float GetHeightFromZ()
        {
            return _z * MathF.Tan(0.5f * _fieldOfView) * 2f;
        }

        public void MoveZ(float amount)
        {
            _z = Util.Clamp(_z + amount, MIN_Z, MAX_Z);
        }

        public void MoveZTo(float value)
        {
            _z = Util.Clamp(value, MIN_Z, MAX_Z);
        }

        public void ResestZ()
        {
            _z = _baseZ;
        }

        public void IncZoom()
        {
            _zoom = Util.Clamp(++_zoom, MIN_ZOOM, MAX_ZOOM);
            _z = _baseZ / _zoom;
        }

        public void DecZoom()
        {
            _zoom = Util.Clamp(--_zoom, MIN_ZOOM, MAX_ZOOM);
            _z = _baseZ / _zoom;
        }

        public void SetZoom(int amount)
        {
            _zoom = Util.Clamp(amount, MIN_ZOOM, MAX_ZOOM);
            _z = _baseZ / _zoom;
        }

        public void GetExtents(out float width, out float height)
        {
            height = GetHeightFromZ();
            width = height * _aspectRatio;
        }

        public void GetExtents(out float left, out float right, out float bottom, out float top)
        {
            GetExtents(out float width, out float height);

            left = _position.X - 0.5f * width;
            right = left + width;
            bottom = _position.Y - 0.5f * height;
            top = bottom + height;
        }

        public void GetExtents(out Vector2 min, out Vector2 max)
        {
            GetExtents(out float left, out float right, out float bottom, out float top);

            min = new Vector2(left, bottom);
            max = new Vector2(right, top);
        }
    }
}
