using Microsoft.Xna.Framework;
using System;

namespace MGL.Graphics
{
    public sealed partial class MGLShapes : IDisposable
    {
        public enum PointShape
        {
            Circle, Square, Diamond,
            FilledCircle, FilledSquare, FilledDiamond,
            Plus, Cross
        }

        public void DrawPoint(Vector2 position, Color color)
        {
            float radius = 3f;
            if (_camera != null)
                radius *= _camera.Z / _camera.BaseZ;           

            DrawCircleFilled(position, radius, 24, color);
        }

        public void DrawPoint(Vector2 position, float size, Color color)
        {
            float radius = MGLUtil.Clamp(0.5f * size, 2f, 10f);
            if (_camera != null)
                radius *= _camera.Z / _camera.BaseZ;

            DrawCircleFilled(position, radius, Math.Max((int)radius * 4, 24), color);
        }

        public void DrawPoint(Vector2 position, PointShape? shape, Color color)
        {
            DrawPoint(position, 6f, shape, color);
        }

        public void DrawPoint(Vector2 position, float size, PointShape? shape, Color color)
        {
            float radius = MGLUtil.Clamp(0.5f * size, 2f, 10f);
            if (_camera != null)
            { 
                size *= _camera.Z / _camera.BaseZ;
                radius *= _camera.Z / _camera.BaseZ;
            }

            float lineThickness = 1f;

            if (shape == null || shape == PointShape.FilledCircle)
                DrawCircleFilled(position, radius, Math.Max((int)radius * 4, 24), color);

            else if (shape == PointShape.Circle)
                DrawCircle(position, radius, Math.Max((int)radius * 4, 24), lineThickness, color);

            else if (shape == PointShape.Square)
                DrawRectangle(position, size, size, lineThickness, color);

            else if (shape == PointShape.FilledSquare)
                DrawRectangleFilled(position, size, size, color);

            else if (shape == PointShape.Diamond)
                DrawRectangle(position, size, size, MathHelper.PiOver4, lineThickness, color);

            else if (shape == PointShape.FilledDiamond)
                DrawRectangleFilled(position, size, size, MathHelper.PiOver4, color);

            else if (shape == PointShape.Plus)
            {
                float offset = 0.5f * size;
                DrawLine(position.X - offset, position.Y, position.X + offset, position.Y, lineThickness, color);
                DrawLine(position.X, position.Y - offset, position.X, position.Y + offset, lineThickness, color);
            }

            else if (shape == PointShape.Cross)
            {
                float offset = 0.5f * size;
                DrawLine(position.X - offset, position.Y - offset, position.X + offset, position.Y + offset, lineThickness, color);
                DrawLine(position.X - offset, position.Y + offset, position.X + offset, position.Y - offset, lineThickness, color);
            }
        }
    }
}
