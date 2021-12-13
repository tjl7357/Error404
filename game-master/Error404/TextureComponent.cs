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
    /// A Component that holds and Draws a Texture
    /// </summary>
    public class TextureComponent : Component
    {
        /// <summary>
        /// The Texture that the texture component draws
        /// </summary>
        public Texture2D texture;

        /// <summary>
        /// Nullable Color: it can be a color or null, allows for optional in constructor
        /// </summary>
        public Color? color;

        private int width, height;

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)this.GameObject.Position.X, (int)this.GameObject.Position.Y, width, height);
            }
        }

        public TextureComponent(Texture2D texture, int width = 0, int height = 0, Color? color = null)
        {
            this.texture = texture;
            this.width = width;
            this.height = height;
            this.color = color;
        }

        /// <summary>
        /// Draws the texture component
        /// </summary>
        /// <param name="sb"></param>
        public override void Draw(SpriteBatch sb)
        {
            // ?? means if left is null, then put right as argument
            if (texture != null)
                sb.Draw(texture, new Rectangle((int)this.GameObject.Position.X, (int)this.GameObject.Position.Y, width, height), color ?? Color.White);
        }

        /// <summary>
        /// Sets the height of the textureComponent
        /// </summary>
        /// <param name="width"></param>
        public void SetWidth(int width)
        {
            this.width = Math.Abs(width);
        }

        /// <summary>
        /// Sets the width of the textureComponent
        /// </summary>
        /// <param name="height"></param>
        public void SetHeight(int height)
        {
            this.height = Math.Abs(height);
        }
    }
}
