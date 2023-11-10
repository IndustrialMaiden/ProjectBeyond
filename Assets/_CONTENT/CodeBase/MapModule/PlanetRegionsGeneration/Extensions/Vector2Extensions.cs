using System.Linq;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 Interpolate(Vector2 pt1, Vector2 pt2, float f)
        {
            var x = f * pt1.x + (1 - f) * pt2.x;
            var y = f * pt1.y + (1 - f) * pt2.y;

            return new Vector2(x, y);
        }
    }
}


