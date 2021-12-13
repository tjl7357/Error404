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
    public class ControlsManager
    {
        private static ControlsManager Instance;

        private MouseState currentMState;
        private MouseState lastMState;
        private KeyboardState lastKBState;
        private KeyboardState currentKBState;

        public MouseState CurrentMouseState
        {
            get
            {
                return currentMState;
            }
        }

        public MouseState LastMouseState
        {
            get
            {
                return lastMState;
            }
        }

        public KeyboardState CurrentKeyboardState
        {
            get
            {
                return currentKBState;
            }
        }

        public KeyboardState LastKeyboardState
        {
            get
            {
                return lastKBState;
            }
        }
        private ControlsManager()
        {
            currentMState = Mouse.GetState();
            currentKBState = Keyboard.GetState();
        }

        public void Update()
        {
            lastKBState = currentKBState;
            lastMState = currentMState;
            currentMState = Mouse.GetState();
            currentKBState = Keyboard.GetState();
        }

        public bool MouseClicked()
        {
            return currentMState.LeftButton == ButtonState.Pressed && lastMState.LeftButton == ButtonState.Released;
        }

        public bool KeyPressed(Keys key)
        {
            return currentKBState.IsKeyDown(key) && lastKBState.IsKeyUp(key);
        }

        public static ControlsManager GetInstance()
        {
            if(Instance == null)
            {
                Instance = new ControlsManager();
            }
            return Instance;
        }
    }
}
