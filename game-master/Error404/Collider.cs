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
    public struct Collision
    {
        public bool collided;
        /// <summary>
        /// The Collider that called the search for collisions
        /// </summary>
        public Collider CallingCollider;
        public Collider OtherCollider;
        /// <summary>
        /// The active collider being checked
        /// </summary>
        public Vector2 ActiveColliderPos;
        /// <summary>
        /// The static collider being checked
        /// </summary>
        public Vector2 StaticColliderPos;
        /// <summary>
        /// Rectangle representing the overlap between the two colliders
        /// </summary>
        public Rectangle Intersect;
    }
    /// <summary>
    /// Purpose: A Collider is a Component that detects and reports collisions
    /// </summary>
    public class Collider : Component
    {
        private Vector2 previousPos = Vector2.Zero;
        private Vector2 offset = Vector2.Zero;

        private int width, height;

        private bool active;
        private bool HasAccuratePreviousPos;

        public bool IsActive
        {
            get
            {
                return active;
            }
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)this.GameObject.Position.X + (int)Offset.X, (int)this.GameObject.Position.Y + (int)Offset.Y, width, height);
            }
        }

        public Vector2 Offset
        {
            get
            {
                return offset;
            }
        }
        /// <summary>
        /// Main Constructor
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="active">Describes whether this collider should get sent Collision events</param>
        public Collider(int width, int height, bool active = false, Vector2 Offset = default(Vector2))
        {
            this.width = Math.Abs(width);
            this.height = Math.Abs(height);
            this.active = active;
            this.offset = Offset;
            HasAccuratePreviousPos = false;
        }
        /// <summary>
        /// Called upon being attached to gameObject
        /// </summary>
        public override void Start()
        {
            if (active)
            {
                CollisionManager.GetInstance().AddActiveCollider(this);
            }
            else
            {
                CollisionManager.GetInstance().AddStaticCollider(this);
            }
            previousPos = GameObject.Position;
        }

        /// <summary>
        /// sets the width of the collider
        /// </summary>
        /// <param name="width"></param>
        public void SetWidth(int width)
        {
            this.width = Math.Abs(width);
        }

        /// <summary>
        /// sets the height of the collider
        /// </summary>
        /// <param name="height"></param>
        public void SetHeight(int height)
        {
            this.height = Math.Abs(height);
        }
        /// <summary>
        /// checks to see if a collider is intersecting with another collider
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Collision Intersects(Collider other)
        {
            return new Collision()
            {
                collided = Bounds.Intersects(other.Bounds),
                CallingCollider = this,
                OtherCollider = other
            };
        }
        /// <summary>
        /// Checks to see if a collider wouldve intersected with another collider
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Collision SweepIntersects(Collider other)
        {
            if (this.Enabled && other.Enabled)
            {
                bool collide = this.Bounds.Intersects(other.Bounds);

                if (collide)
                {
                    return new Collision()
                    {
                        collided = true,
                        CallingCollider = this,
                        OtherCollider = other,
                        ActiveColliderPos = new Vector2(this.Bounds.X, this.Bounds.Y),
                        StaticColliderPos = new Vector2(other.Bounds.X, other.Bounds.Y),
                        Intersect = Rectangle.Intersect(this.Bounds, other.Bounds),
                    };
                }
                else
                {
                    return new Collision()
                    {
                        collided = false,
                    };
                }
            }
            else
            {
                return new Collision()
                {
                    collided = false,
                };
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        public override void LateUpdate()
        {
            previousPos = GameObject.Position;
            HasAccuratePreviousPos = true;
        }
    }
}
