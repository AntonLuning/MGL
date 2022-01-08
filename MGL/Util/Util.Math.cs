using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MGL
{
    public static partial class Util
    {
        public static float Dot(Vector2 vector1, Vector2 vector2)
        {
            return vector1.X * vector2.X + vector1.Y * vector2.Y;
        }

        public static float Cross(Vector2 vector1, Vector2 vector2)
        {
            return vector1.X * vector2.Y - vector1.Y * vector2.X;
        }
    }
}
