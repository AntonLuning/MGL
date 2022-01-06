using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MGL.Graphics
{
    public sealed partial class Shapes : IDisposable
    {
        private bool IsPolygonSelfIntersecting(Vector2[] vertices)
        {
            // TODO: Check if any edges cross (loop through them in order)
            return false;
        }

        private float CalculatePolygonArea(Vector2[] vertices)
        {
            float area = 0f;

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 startPt = vertices[i];
                Vector2 endPt = vertices[(i + 1) % vertices.Length];

                float dx = endPt.X - startPt.X;
                float dy = startPt.Y + endPt.Y;

                area += 0.5f * dy * dx;
            }

            return area;
        }

        private bool IsPointInTriangle(Vector2 pt, Vector2 trianglePt1, Vector2 trianglePt2, Vector2 trianglePt3)
        {
            Vector2 ab = trianglePt2 - trianglePt1;
            Vector2 bc = trianglePt3 - trianglePt2;
            Vector2 ca = trianglePt1 - trianglePt3;

            Vector2 ap = pt - trianglePt1;
            Vector2 bp = pt - trianglePt2;
            Vector2 cp = pt - trianglePt3;

            if (Util.Cross(ab, ap) > 0f || Util.Cross(bc, bp) > 0f || Util.Cross(ca, cp) > 0f)
                return false;

            return true;
        }

        public void DrawPolygonFilled(Vector2[] vertices, Transform transform, Color color)
        {
            if (!_isStarted)
                throw new InvalidOperationException("Batching not started.");

            if (vertices == null || vertices.Length < 3 || vertices.Length > MAX_VERTEX_COUNT)
                throw new ArgumentException($"Must be between 3 and {MAX_VERTEX_COUNT} number of vertices.");

            if (IsPolygonSelfIntersecting(vertices))
                throw new ArgumentException("Polygon is self-intersecting");
            
            if (CalculatePolygonArea(vertices) < 0f)
                Array.Reverse(vertices);

            List<int> indexList = new List<int>();
            for (int i = 0; i < vertices.Length; i++)
            {
                indexList.Add(i);
            }

            int shapeVertexCount = vertices.Length;
            int shapeTriangleCount = shapeVertexCount - 2;
            int shapeIndexCount = shapeTriangleCount * 3;
            EnsureSpace(shapeVertexCount, shapeIndexCount);

            while (indexList.Count > 3)
            {
                for (int i = 0; i < indexList.Count; i++)
                {
                    int index1 = indexList[i];
                    int index2 = Util.GetItem(indexList, i - 1);
                    int index3 = Util.GetItem(indexList, i + 1);

                    Vector2 pt1 = vertices[index1];
                    Vector2 pt2 = vertices[index2];
                    Vector2 pt3 = vertices[index3];

                    if (Util.Cross(pt2 - pt1, pt3 - pt1) < 0f)
                        continue;

                    bool triangleOK = true;

                    for (int j = 0; j < vertices.Length; j++)
                    {
                        if (j == index1 || j == index2 || j == index3)
                            continue;

                        if (IsPointInTriangle(vertices[j], pt2, pt1, pt3))
                        {
                            triangleOK = false;
                            break;
                        }
                    }

                    if (triangleOK)
                    {
                        _indices[_indexCount++] = index2 + _vertexCount;
                        _indices[_indexCount++] = index1 + _vertexCount;
                        _indices[_indexCount++] = index3 + _vertexCount;

                        indexList.RemoveAt(i);
                        break;
                    }
                }
            }

            _indices[_indexCount++] = indexList[0] + _vertexCount;
            _indices[_indexCount++] = indexList[1] + _vertexCount;
            _indices[_indexCount++] = indexList[2] + _vertexCount;

            for (int i = 0; i < shapeVertexCount; i++)
            {
                Vector2 pt = Util.Transform(vertices[i], transform);

                _vertices[_vertexCount++] = new VertexPositionColor(new Vector3(pt, 0f), color);
            }

            _shapeCount++;
        }

        public void DrawPolygonFilled(Vector2[] vertices, Color color)
        {
            DrawPolygonFilled(vertices, Transform.Identity, color);
        }

        public void DrawPolygon(Vector2[] vertices, Transform transform, float thickness, Color color)
        {
            if (vertices == null || vertices.Length < 3 || vertices.Length > MAX_VERTEX_COUNT)
                throw new ArgumentException($"Must be between 3 and {MAX_VERTEX_COUNT} number of vertices.");

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 startPt = Util.Transform(vertices[i], transform);
                Vector2 endPt = Util.Transform(vertices[(i + 1) % vertices.Length], transform);

                DrawLine(startPt, endPt, thickness, color);
            }
        }

        public void DrawPolygon(Vector2[] vertices, float thickness, Color color)
        {
            DrawPolygon(vertices, Transform.Identity, thickness, color);
        }
    }
}
