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
    /// Special game object made for handling collectible items (ex: coins)
    /// </summary>
    class Collectible:GameObject
    {
        private Texture2D texture;
        private bool collected;
        private bool removeColliders = false;
        private Collider collider;
        private new Rectangle position;

        public Collectible(Rectangle position, Texture2D texture):base(new Vector2(position.X, position.Y))
        {
            collected = false;
            this.texture = texture;
            this.position = position;
            collider = new Collider(position.Width, position.Height, true);
            this.AddComponent<Collider>(collider);
        }

        /// <summary>
        /// When the player collides with collectible, adds to coin count
        /// </summary>
        /// <param name="otherCollider"></param>
        public override void OnCollision(Collision otherCollider)
        {
            if (otherCollider.CallingCollider != null)
            {
                // If the colliding object is the player and the player object exists, then add to coins collected count
                if (otherCollider.CallingCollider.GameObject is Player p && p != null)
                {
                    removeColliders = true;
                    p.CoinsCollected++;
                    collected = true;
                }
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (collected)
            {
                return;
            }
            else
            {
                base.Draw(sb);
            }
        }

        public override void LateUpdate()
        {
            if (removeColliders)
            {
                this.RemoveComponent<Collider>(this.GetComponent<Collider>());
                CollisionManager.GetInstance().RemoveStaticCollider(this.GetComponent<Collider>());
                removeColliders = false;
            }
        }
    }
}
