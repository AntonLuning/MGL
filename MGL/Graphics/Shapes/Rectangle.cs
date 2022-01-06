using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MGL.Graphics
{
    public sealed partial class Shapes : IDisposable
    {
        private void AddRectangleData(float pt1X, float pt1Y, float pt2X, float pt2Y, float pt3X, float pt3Y, float pt4X, float pt4Y, Color color)
        {
            if (!_isStarted)
                throw new Exception("Batching not started.");

            int shapeVertexCount = 4;
            int shapeIndexCount = 6;
            EnsureSpace(shapeVertexCount, shapeIndexCount);

            _indices[_indexCount++] = 0 + _vertexCount;
            _indices[_indexCount++] = 1 + _vertexCount;
            _indices[_indexCount++] = 2 + _vertexCount;
            _indices[_indexCount++] = 0 + _vertexCount;
            _indices[_indexCount++] = 2 + _vertexCount;
            _indices[_indexCount++] = 3 + _vertexCount;

            _vertices[_vertexCount++] = new VertexPositionColor(new Vector3(pt1X, pt1Y, 0f), color);
            _vertices[_vertexCount++] = new VertexPositionColor(new Vector3(pt2X, pt2Y, 0f), color);
            _vertices[_vertexCount++] = new VertexPositionColor(new Vector3(pt3X, pt3Y, 0f), color);
            _vertices[_vertexCount++] = new VertexPositionColor(new Vector3(pt4X, pt4Y, 0f), color);

            _shapeCount++;
        }

        public void DrawRectangleFilled(float x, float y, float width, float height, Color color)
        {
            float p1X = x;
            float p1Y = y + height;
            float p2X = x + width;
            float p2Y = y + height;
            float p3X = x + width;
            float p3Y = y;
            float p4X = x;
            float p4Y = y;

            AddRectangleData(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, color);
        }

        public void DrawRectangleFilled(Vector2 min, Vector2 max, Color color)
        {
            DrawRectangleFilled(min.X, min.Y, max.X - min.X, max.Y - min.Y, color);
        }

        public void DrawRectangleFilled(Vector2 center, float width, float height, Color color)
        {
            DrawRectangleFilled(center.X - 0.5f * width, center.Y - 0.5f * height, width, height, color);
        }

        public void DrawRectangleFilled(Vector2 center, float width, float height, float rotation, Color color)
        {
            float aX = -0.5f * width;
            float aY = 0.5f * height;
            float bX = 0.5f * width;
            float bY = 0.5f * height;
            float cX = 0.5f * width;
            float cY = -0.5f * height;
            float dX = -0.5f * width;
            float dY = -0.5f * height;

            float sin = MathF.Sin(rotation);
            float cos = MathF.Cos(rotation);

            float p1X = aX * cos - aY * sin + center.X;
            float p1Y = aX * sin + aY * cos + center.Y;
            float p2X = bX * cos - bY * sin + center.X;
            float p2Y = bX * sin + bY * cos + center.Y;
            float p3X = cX * cos - cY * sin + center.X;
            float p3Y = cX * sin + cY * cos + center.Y;
            float p4X = dX * cos - dY * sin + center.X;
            float p4Y = dX * sin + dY * cos + center.Y;

            AddRectangleData(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, color);
        }

        public void DrawRectangleFilled(float x, float y, float width, float height, MGLTransform transform, Color color)
        {
            float p1X = x;
            float p1Y = y + height;
            float p2X = x + width;
            float p2Y = y + height;
            float p3X = x + width;
            float p3Y = y;
            float p4X = x;
            float p4Y = y;

            Vector2 pt1 = MGLUtil.Transform(p1X, p1Y, transform);
            Vector2 pt2 = MGLUtil.Transform(p2X, p2Y, transform);
            Vector2 pt3 = MGLUtil.Transform(p3X, p3Y, transform);
            Vector2 pt4 = MGLUtil.Transform(p4X, p4Y, transform);

            AddRectangleData(pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y, color);
        }

        public void DrawRectangle(float x, float y, float width, float height, float thickness, Color color)
        {
            float p1X = x;
            float p1Y = y + height;
            float p2X = x + width;
            float p2Y = y + height;
            float p3X = x + width;
            float p3Y = y;
            float p4X = x;
            float p4Y = y;

            DrawLine(p1X, p1Y, p2X, p2Y, thickness, color);
            DrawLine(p2X, p2Y, p3X, p3Y, thickness, color);
            DrawLine(p3X, p3Y, p4X, p4Y, thickness, color);
            DrawLine(p4X, p4Y, p1X, p1Y, thickness, color);
        }

        public void DrawRectangle(Vector2 min, Vector2 max, float thickness, Color color)
        {
            DrawRectangle(min.X, min.Y, max.X - min.X, max.Y - min.Y, thickness, color);
        }

        public void DrawRectangle(Vector2 center, float width, float height, float thickness, Color color)
        {
            DrawRectangle(center.X - 0.5f * width, center.Y - 0.5f * height, width, height, thickness, color);
        }

        public void DrawRectangle(Vector2 center, float width, float height, float rotation, float thickness, Color color)
        {
            float aX = - 0.5f * width;
            float aY = 0.5f * height;
            float bX = 0.5f * width;
            float bY = 0.5f * height;
            float cX = 0.5f * width;
            float cY = - 0.5f * height;
            float dX = - 0.5f * width;
            float dY = - 0.5f * height;
            
            float sin = MathF.Sin(rotation);
            float cos = MathF.Cos(rotation);

            float p1X = aX * cos - aY * sin + center.X;
            float p1Y = aX * sin + aY * cos + center.Y;
            float p2X = bX * cos - bY * sin + center.X;
            float p2Y = bX * sin + bY * cos + center.Y;
            float p3X = cX * cos - cY * sin + center.X;
            float p3Y = cX * sin + cY * cos + center.Y;
            float p4X = dX * cos - dY * sin + center.X;
            float p4Y = dX * sin + dY * cos + center.Y;

            DrawLine(p1X, p1Y, p2X, p2Y, thickness, color);
            DrawLine(p2X, p2Y, p3X, p3Y, thickness, color);
            DrawLine(p3X, p3Y, p4X, p4Y, thickness, color);
            DrawLine(p4X, p4Y, p1X, p1Y, thickness, color);
        }

        public void DrawRectangle(float x, float y, float width, float height, float thickness, MGLTransform transform, Color color)
        {
            float p1X = x;
            float p1Y = y + height;
            float p2X = x + width;
            float p2Y = y + height;
            float p3X = x + width;
            float p3Y = y;
            float p4X = x;
            float p4Y = y;

            Vector2 pt1 = MGLUtil.Transform(p1X, p1Y, transform);
            Vector2 pt2 = MGLUtil.Transform(p2X, p2Y, transform);
            Vector2 pt3 = MGLUtil.Transform(p3X, p3Y, transform);
            Vector2 pt4 = MGLUtil.Transform(p4X, p4Y, transform);

            DrawLine(pt1.X, pt1.Y, pt2.X, pt2.Y, thickness, color);
            DrawLine(pt2.X, pt2.Y, pt3.X, pt3.Y, thickness, color);
            DrawLine(pt3.X, pt3.Y, pt4.X, pt4.Y, thickness, color);
            DrawLine(pt4.X, pt4.Y, pt1.X, pt1.Y, thickness, color);
        }
    }
}
