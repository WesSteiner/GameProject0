using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject0.Collisions
{
    public static class CollisionHelper
    {
        /// <summary>
        /// detects circle collisions
        /// </summary>
        /// <param name="a">bounding circle 1</param>
        /// <param name="b">bounding circle 2</param>
        /// <returns>true for collisions</returns>
        public static bool Collides(BoundingCircle a, BoundingCircle b)
        {
            return Math.Pow(a.Radius + b.Radius, 2) >= Math.Pow(a.Center.X - b.Center.X, 2) + Math.Pow(a.Center.Y - b.Center.Y, 2);
        }

        /// <summary>
        /// Detects collision between two rectangles
        /// </summary>
        /// <param name="a">Rectangle 1</param>
        /// <param name="b">Rectangle 2</param>
        /// <returns>True if colliding</returns>
        public static bool Collides(BoundingRectangle a, BoundingRectangle b)
        {
            return !(a.Right < b.Left || a.Left > b.Right || a.Top > b.Bottom || a.Bottom < b.Top);
        }

        /// <summary>
        /// detects collision between rectangle and circle
        /// </summary>
        /// <param name="c">circle</param>
        /// <param name="r">rectangle</param>
        /// <returns>true if colliding</returns>
        public static bool Collides(BoundingCircle c, BoundingRectangle r)
        {
            float nearestX = MathHelper.Clamp(c.Center.X, r.Left, r.Right);
            float nearestY = MathHelper.Clamp(c.Center.Y, r.Top, r.Bottom);
            return Math.Pow(c.Radius, 2) >= Math.Pow(c.Center.X - nearestX, 2) + Math.Pow(c.Center.Y - nearestY, 2);

        }
    }
}
