using Microsoft.Xna.Framework;
using System;

namespace MGL.Graphics
{
    public sealed partial class MGLShapes : IDisposable
    {
        public void DrawLine(float x1, float y1, float x2, float y2, float thickness, Color color)
        {
            thickness = MGLUtil.Clamp(thickness, MIN_LINE_THICKNESS, MAX_LINE_THICKNESS);
            if (_camera != null)
                thickness *= _camera.Z / _camera.BaseZ;

            float e1X = x2 - x1;
            float e1Y = y2 - y1;
            MGLUtil.Normalize(ref e1X, ref e1Y);
            e1X *= thickness / 2f;
            e1Y *= thickness / 2f;
            float e2X = -e1X;
            float e2Y = -e1Y;
            float n1X = -e1Y;
            float n1Y = e1X;
            float n2X = -n1X;
            float n2Y = -n1Y;

            float p1X = x1 + n1X + e2X;
            float p1Y = y1 + n1Y + e2Y;
            float p2X = x2 + n1X + e1X;
            float p2Y = y2 + n1Y + e1Y;
            float p3X = x2 + n2X + e1X;
            float p3Y = y2 + n2Y + e1Y;
            float p4X = x1 + n2X + e2X;
            float p4Y = y1 + n2Y + e2Y;

            AddRectangleData(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, color);
        }

        public void DrawLine(Vector2 startPt, Vector2 endPt, float thickness, Color color)
        {
            DrawLine(startPt.X, startPt.Y, endPt.X, endPt.Y, thickness, color);
        }
    }
}
