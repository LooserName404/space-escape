using System;
using UnityEngine;

namespace SpaceEscape.Utils
{
    public static class Vector2Extensions
    {
        public static Vector2 Abs(this Vector2 self)
        {
            return new Vector2(Math.Abs(self.x), Math.Abs(self.y));
        }

        public static void Deconstruct(this Vector2 self, out float x, out float y)
        {
            x = self.x;
            y = self.y;
        }
    }
}