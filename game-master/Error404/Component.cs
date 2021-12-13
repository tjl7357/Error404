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
    /// Purpose: Component describes an abstract attached to 
    /// </summary>
    public abstract class Component
    {
        private GameObject gameObject = null;
        private bool enabled = true;

        /// <summary>
        /// Returns the GameObject of the Component, by default null, once set cannot be changed
        /// </summary>
        public GameObject GameObject
        {
            get
            {
                return gameObject;
            }
            set
            {
                if (GameObject == null)
                {
                    gameObject = value;
                }
            }
        }

        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
            }
        }

        /// <summary>
        /// Called upon Component being added to a GameObject
        /// </summary>
        public virtual void Start()
        {

        }

        /// <summary>
        /// Called upon Component being updated
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// Called upon Component being updated after collision is handled
        /// </summary>
        public virtual void LateUpdate()
        {

        }
        /// <summary>
        /// Called upon Component being Drawn
        /// </summary>
        /// <param name="sb"></param>
        public virtual void Draw(SpriteBatch sb)
        {

        }

        /// <summary>
        /// Called when a gameobject collides with a collider
        /// </summary>
        /// <param name="otherCollider"></param>
        public virtual void OnCollision(Collision collision)
        {

        }
    }
}
