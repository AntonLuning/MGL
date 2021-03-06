using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MGL.Graphics
{
    public sealed partial class Shapes : IDisposable
    {
        // TODO: Implement DrawEllipse()

        public void DrawCircleFilled(float x, float y, float radius, int points, Color color)
        {
            DrawCircleFilled(x, y, radius, points, Transform.Identity, color);
        }

        public void DrawCircleFilled(Vector2 center, float radius, int points, Color color)
        {
            DrawCircleFilled(center.X, center.Y, radius, points, Transform.Identity, color);
        }

        public void DrawCircleFilled(float x, float y, float radius, int points, Transform transform, Color color)
        {
            if (!_isStarted)
                throw new Exception("Batching not started.");

            const int MIN_POINTS = 3;
            const int MAX_POINTS = 256;

            int shapeVertexCount = Util.Clamp(points, MIN_POINTS, MAX_POINTS);
            int shapeTriangleCount = shapeVertexCount - 2;
            int shapeIndexCount = shapeTriangleCount * 3;
            EnsureSpace(shapeVertexCount, shapeIndexCount);

            float angle = MathHelper.TwoPi / shapeVertexCount;
            float sin = MathF.Sin(angle);
            float cos = MathF.Cos(angle);

            int index = 1;

            for (int i = 0; i < shapeTriangleCount; i++)
            {
                _indices[_indexCount++] = 0 + _vertexCount;
                _indices[_indexCount++] = index + _vertexCount;
                _indices[_indexCount++] = index + 1 + _vertexCount;

                index++;
            }

            float startX = radius;
            float startY = 0f;

            for (int i = 0; i < shapeVertexCount; i++)
            {
                Vector2 pt = Util.Transform(new Vector2(startX + x, startY + y), transform);

                _vertices[_vertexCount++] = new VertexPositionColor(new Vector3(pt, 0f), color);

                float endX = startX * cos - startY * sin;
                float endY = startX * sin + startY * cos;

                startX = endX;
                startY = endY;
            }

            _shapeCount++;
        }

        public void DrawCircle(float x, float y, float radius, int points, float thickness, Color color)
        {
            DrawCircle(x, y, radius, points, thickness, Transform.Identity, color);
        }

        public void DrawCircle(Vector2 center, float radius, int points, float thickness, Color color)
        {
            DrawCircle(center.X, center.Y, radius, points, thickness, Transform.Identity, color);
        }

        public void DrawCircle(float x, float y, float radius, int points, float thickness, Transform transform, Color color)
        {
            const int MIN_POINTS = 3;
            const int MAX_POINTS = 256;

            points = Util.Clamp(points, MIN_POINTS, MAX_POINTS);

            float angle = MathHelper.TwoPi / points;
            float sin = MathF.Sin(angle);
            float cos = MathF.Cos(angle);

            float startX = radius;
            float startY = 0;

            for (int i = 0; i < points; i++)
            {
                float endX = startX * cos - startY * sin;
                float endY = startX * sin + startY * cos;

                Vector2 startPt = Util.Transform(new Vector2(startX + x, startY+ y), transform);
                Vector2 endPt = Util.Transform(new Vector2(endX + x, endY + y), transform);

                DrawLine(startPt, endPt, thickness, color);

                startX = endX;
                startY = endY;
            }
        }
    }
}
