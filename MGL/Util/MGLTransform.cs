﻿using Microsoft.Xna.Framework;
using System;

namespace MGL
{
    public struct MGLTransform
    {
        public float PosX;
        public float PosY;
        public float Sin;
        public float Cos;
        public float ScaleX;
        public float ScaleY;

        public static readonly MGLTransform Identity = new MGLTransform(Vector2.Zero, 0f, 1f);

        public MGLTransform(Vector2 position, float angle, Vector2 scale)
        {
            PosX = position.X;
            PosY = position.Y;
            Sin = MathF.Sin(angle);
            Cos = MathF.Cos(angle);
            ScaleX = scale.X;
            ScaleY = scale.Y;
        }

        public MGLTransform(Vector2 position, float angle, float scale)
        {
            PosX = position.X;
            PosY = position.Y;
            Sin = MathF.Sin(angle);
            Cos = MathF.Cos(angle);
            ScaleX = scale;
            ScaleY = scale;
        }
    }
}
