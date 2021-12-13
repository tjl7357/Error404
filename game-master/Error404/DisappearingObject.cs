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
    public enum ObjectState
    {
        active,
        inactive,
        dissapearing,
        reappearing
    }

    class DisappearingObject : GameObject
    {
        private Dictionary<string, Texture2D[]> animations;
        // must have: Glitch & Default
        // Default will just be a single index array holding
        // the texture we want it to show when not glitching away

        private double deadTime;
        private double activeTime;
        private ObjectState state;
        private Collider collider;
        private Animation animator;
        private double timeCounter;
        private new Rectangle position;
        private Texture2D defaultTexture;

        public DisappearingObject(Rectangle position, Texture2D regular, Texture2D[] textures, double activeTime, double deadTime) : base(new Vector2(position.X, position.Y))
        {
            animations = new Dictionary<string, Texture2D[]>();
            animations.Add("Glitch", textures);
            animations.Add("Default", new Texture2D[] { regular });
            animator = new Animation(animations, 0.5);
            this.position = position;
            this.deadTime = deadTime;
            this.activeTime = activeTime;
            collider = new Collider(position.Width, position.Height, false);
            this.AddComponent<Collider>(collider);
            state = ObjectState.active;
            defaultTexture = regular;
        }

        /* Need to create ability for the collider to dissappear and reappear
         * Also a time so we can set the intervals where it dissappears and reappears 
         * Ex: every 5 seconds the platform dissappears for 3 seconds
         * And a state machine to handle the animations and any other fields we may want to have
         */

        public override void Update()
        {
            if (state.Equals(ObjectState.active))
            {
                timeCounter += TimeManager.GetInstance().Time.ElapsedGameTime.TotalSeconds;
                if (timeCounter >= activeTime)
                {
                    collider.Enabled = false;
                    state = ObjectState.inactive;
                    timeCounter = 0;
                }
            }
            else if (state.Equals(ObjectState.inactive))
            {
                timeCounter += TimeManager.GetInstance().Time.ElapsedGameTime.TotalSeconds;
                if (timeCounter >= deadTime)
                {
                
                    collider.Enabled = true;
                    state = ObjectState.active;
                    timeCounter = 0;
                }
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (state.Equals(ObjectState.active))
            {
                sb.Draw(defaultTexture, position, Color.White);
                //animator.Play("Default");
            }
            else if (state.Equals(ObjectState.inactive))
            {
                //animator.Stop();
            }
            else if (state.Equals(ObjectState.dissapearing))
            {
                //animator.Play("Glitch");
            }
            else if (state.Equals(ObjectState.reappearing))
            {
                //animator.Play("Glitch");
            }
        }

        //private void TimeKeeper()
        //{
        //    if (state.Equals(ObjectState.active))
        //    {
        //        timeCounter += TimeManager.GetInstance().Time.ElapsedGameTime.TotalSeconds;
        //        if (timeCounter >= activeTime)
        //        {
        //            state = ObjectState.dissapearing;
        //        }
        //    }
        //    else if (state.Equals(ObjectState.inactive))
        //    {
        //        timeCounter += TimeManager.GetInstance().Time.ElapsedGameTime.TotalSeconds;
        //        if (timeCounter >= deadTime)
        //        {
        //            state = ObjectState.reappearing;
        //        }
        //    }
        //    else if (state.Equals(ObjectState.dissapearing))
        //    {
        //        this.RemoveComponent<Collider>(collider);
        //        timeCounter += TimeManager.GetInstance().Time.ElapsedGameTime.TotalSeconds;
        //        if (timeCounter >= 2)
        //        {
        //            state = ObjectState.inactive;
        //        }
        //    }
        //    else if (state.Equals(ObjectState.reappearing))
        //    {
        //        timeCounter += TimeManager.GetInstance().Time.ElapsedGameTime.TotalSeconds;
        //        if (timeCounter >= 2)
        //        {
        //            state = ObjectState.active;
        //            // this.AddComponent<Collider>(collider);
        //        }
        //    }
        //}

    }
}
