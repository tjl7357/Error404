using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Error404
{
    /// <summary>
    /// A Rectangle that works with Floats and Explicitly converts to regular Rectangle
    /// </summary>
    public struct FloatRectangle : IEquatable<FloatRectangle>, IEquatable<Rectangle> //IEquatable provides functionality for .Equals(Type Other)
    {
        #region Fields
        public float X;
        public float Y;
        public float Width;
        public float Height;
        #endregion

        #region Properties
        public float Left
        {
            get
            {
                return this.X;
            }
        }

        public float Right
        {
            get
            {
                return this.X + this.Width;
            }
        }

        public float Top
        {
            get
            {
                return this.Y;
            }
        }

        public float Bottom
        {
            get
            {
                return this.Y + this.Height;
            }
        }
        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public FloatRectangle(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        #region Operators
        public static explicit operator Rectangle(FloatRectangle rect)
        {
            return new Rectangle((int)rect.X,(int)rect.Y,(int)rect.Width,(int)rect.Height);
        }

        public static implicit operator FloatRectangle(Rectangle rect)
        {
            return new FloatRectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static bool operator ==(FloatRectangle a, FloatRectangle b)
        {
            return ((a.X == b.X) && (a.Y == b.Y) && (a.Width == b.Width) && (a.Height == b.Height));
        }

        public static bool operator !=(FloatRectangle a, FloatRectangle b)
        {
            return !(a == b);
        }
        #endregion

        #region Functions
        /// <summary>
        /// Returns whether an X and a Y are between the Left and Right, Top and Bottom
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Contains(float x, float y)
        {
            return (this.Left <= x) && (x < this.Right) && (this.Top <= y) && (y < this.Bottom);
        }
        /// <summary>
        /// Calls Contains with conversion from Vector2
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public bool Contains(Vector2 vec)
        {
            return Contains(vec.X, vec.Y);
        }
        /// <summary>
        /// Calls Contains with conversion from Point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Contains(Point point)
        {
            return Contains(point.X, point.Y);
        }
        /// <summary>
        /// Returns Equivalence
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(FloatRectangle other)
        {
            return this == other;
        }
        /// <summary>
        /// Returns Equivalence with another object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj is Rectangle) ? this == (FloatRectangle)obj : false;
        }
        /// <summary>
        /// Gets a Hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Width.GetHashCode() ^ this.Height.GetHashCode();
        }
        /// <summary>
        /// Returns whether two rectangles intersect
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Intersects(FloatRectangle other)
        {
            return !(other.Left > Right
                     || other.Right < Left
                     || other.Top > Bottom
                     || other.Bottom < Top
                    );
        }

        public bool Equals(Rectangle other)
        {
            return this == other;
        }
        #endregion

    }
}
