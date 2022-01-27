using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject0.Collisions
{
    /// <summary>
    /// Struct for circular bounds
    /// </summary>
    public class BoundingCircle
    {
        /// <summary>
        /// Cneter of bounding circle
        /// </summary>
        public Vector2 Center;

        /// <summary>
        /// Radius of bounding circle
        /// </summary>
        public float Radius;

        /// <summary>
        /// Constructs new bounding circle
        /// </summary>
        /// <param name="center">center</param>
        /// <param name="radius">radius</param>
        public BoundingCircle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius; 
        }

        /// <summary>
        /// Tests for collsions between this and another circle 
        /// </summary>
        /// <param name="other">other circle</param>
        /// <returns>ture if collides</returns>
        public bool CollidesWith(BoundingCircle other)
        {
            return CollisionHelper.Collides(this, other);
        }

        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this, other);
        }
    }
}
