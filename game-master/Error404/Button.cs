using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
//Jakob Ingersoll
//2-19-2020
//Button class for menu buttons

namespace Error404
{
    class Button
    {
        #region Fields
        // Fields
        private Rectangle box;
        private Texture2D texture;
        private Texture2D hoveredTexture;
        private bool hoveredOver;
        #endregion

        #region Properties
        // Properties

        /// <summary>
        /// The x-position of the button.
        /// Get returns the X-coordinate of the button / Set changes the X-coordinate.
        /// </summary>
        public int X
        {
            get { return box.X; }
            set { box.X = value; }
        }

        /// <summary>
        /// The y-position of the button.
        /// Get returns the Y-coordinate of the button / Set changes the Y-coordinate.
        /// </summary>
        public int Y
        {
            get { return box.Y; }
            set { box.Y = value; }
        }

        /// <summary>
        /// If the mouse is hovering over the button.
        /// Get returns true if the mouse is hovering over a button / Set sets whether the mouse is hovering over the button
        /// </summary>
        public bool HoveredOver
        {
            get { return hoveredOver; }
            set { hoveredOver = value; }
        }
        #endregion

        #region Constructors
        // Constructors

        /// <summary>
        /// Creates a menu button with the specified position, dimensions, and textures
        /// </summary>
        /// <param name="x">Horizontal position of the button</param>
        /// <param name="y">Vertical position of the button</param>
        /// <param name="width">Width of the button</param>
        /// <param name="height">Height of the button</param>
        /// <param name="texture">Texture the button uses when the mouse is not hovering over it</param>
        /// <param name="hoverTexture">Texture the button uses when the mouse is not hovering over it</param>
        public Button(int x, int y, int width, int height, Texture2D texture, Texture2D hoveredTexture)
        {
            this.box = new Rectangle(x, y, width, height);
            this.texture = texture;
            hoveredOver = false;
            this.hoveredTexture = hoveredTexture;
        }
        #endregion

        #region Methods
        // Methods

        /// <summary>
        /// Returns whether or not the mouse is currently hovering over the button
        /// </summary>
        /// <returns>whether or not the mouse is currently hovering over the button</returns>
        public bool MouseOver(MouseState mState)
        {
            return mState.X >= this.X && mState.X <= this.X + this.box.Width && mState.Y >= this.Y && mState.Y <= this.Y + this.box.Height;
        }

        /// <summary>
        /// Draws the button to the screen
        /// </summary>
        /// <param name="sb">SpriteBatch object used by Game1</param>
        public void Draw(SpriteBatch sb)
        {
            if (hoveredOver)
                sb.Draw(hoveredTexture, box, Color.White);
            else
                sb.Draw(texture, box, Color.White);
        }
        #endregion
    }
}
