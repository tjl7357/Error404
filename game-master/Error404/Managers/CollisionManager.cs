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
    /// Purpose: Handles all the Colliders in Game
    /// </summary>
    public class CollisionManager
    {
        private static CollisionManager Instance;

        /// <summary>
        /// Holds all Colliders that should not actively be searching for being hit
        /// </summary>
        List<Collider> StaticColliders = new List<Collider>();

        /// <summary>
        /// Holds all Colliders that should be searching for being hit
        /// </summary>
        List<Collider> ActiveColliders = new List<Collider>();

        private CollisionManager()
        {

        }

        /// <summary>
        /// gives access to the CollisionManager
        /// </summary>
        /// <returns></returns>
        public static CollisionManager GetInstance()
        {
            if(Instance == null)
            {
                Instance = new CollisionManager();
            }
            return Instance;
        }

        /// <summary>
        /// adds a collider that does not handle collisions
        /// </summary>
        /// <param name="collider"></param>
        public void AddStaticCollider(Collider collider)
        {
            StaticColliders.Add(collider);
        }

        /// <summary>
        /// adds a collider that handles collisions, checking against all the static colliders
        /// </summary>
        /// <param name="collider"></param>
        public void AddActiveCollider(Collider collider)
        {
            ActiveColliders.Add(collider);
        }

        /// <summary>
        /// Checks for collisions between active and static colliders.
        /// </summary>
        public void CheckForCollisions()
        {
            foreach(Collider activeCollider in ActiveColliders)
            {
                foreach(Collider staticCollider in StaticColliders)
                {
                    Collision check = activeCollider.SweepIntersects(staticCollider);

                    if (check.collided && staticCollider.Enabled)
                    {
                        activeCollider.GameObject.OnCollision(check);
                        staticCollider.GameObject.OnCollision(check);
                    }
                }
            }
        }

        public void ClearStaticColliders()
        {
            StaticColliders.Clear();
        }

        public void RemoveStaticCollider(Collider c)
        {
            StaticColliders.Remove(c);
        }

        public bool BoxCast(Rectangle Check)
        {
            foreach(Collider staticCollider in StaticColliders)
            {
                if(staticCollider.Enabled && staticCollider.Bounds.Intersects(Check)) return true;
            }                                                           
            return false;
        }
    }
}
