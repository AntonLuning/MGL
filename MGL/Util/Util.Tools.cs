using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MGL
{
    public static partial class Util
    {
        public static int Clamp(int value, int min, int max)
        {
            if (min > max)
            {
                var tempMin = min;
                min = max;
                max = tempMin;
            }

            if (value < min)
                return min;
            else if (value > max)
                return max;

            return value;
        }

        public static float Clamp(float value, float min, float max)
        {
            if (min > max)
            {
                var tempMin = min;
                min = max;
                max = tempMin;
            }

            if (value < min)
                return min;
            else if (value > max)
                return max;

            return value;
        }

        public static void Normalize(ref float x, ref float y)
        {
            float invLength = 1f / MathF.Sqrt(x * x + y * y);
            x *= invLength;
            y *= invLength;
        }

        public static Vector2 Normalize(float x, float y)
        {
            float invLength = 1f / MathF.Sqrt(x * x + y * y);
            return new Vector2(x * invLength, y * invLength);
        }

        public static Vector2 Normalize(Vector2 vector)
        {
            float invLength = 1f / MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            return new Vector2(vector.X * invLength, vector.Y * invLength);
        }

        public static Vector2 Transform(Vector2 vector, Transform transform)
        {
            float scaleX = vector.X * transform.ScaleX;
            float scaleY = vector.Y * transform.ScaleY;

            float rotationX = scaleX * transform.Cos - scaleY * transform.Sin;
            float rotationY = scaleX * transform.Sin + scaleY * transform.Cos;

            float translateX = rotationX + transform.PosX;
            float translateY = rotationY + transform.PosY;

            return new Vector2(translateX, translateY);
        }

        public static Vector2 Transform(float x, float y, Transform transform)
        {
            return Transform(new Vector2(x, y), transform);
        }

        public static float Distance(Vector2 vector1, Vector2 vector2)
        {
            float dx = vector2.X - vector1.X;
            float dy = vector2.Y - vector1.Y;
            return MathF.Sqrt(dx * dx + dy * dy);
        }

        public static T GetItem<T>(T[] array, int index)
        {
            if (index < 0)
                return array[index % array.Length + array.Length];

            return array[index % array.Length];
        }

        public static T GetItem<T>(List<T> list, int index)
        {
            if (index < 0)
                return list[index % list.Count + list.Count];

            return list[index % list.Count];
        }

        public static bool IsPointInTriangle(Vector2 pt, Vector2 trianglePt1, Vector2 trianglePt2, Vector2 trianglePt3)
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
    }
}
