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
    /// Purpose: Object Manager has every gameObject in the game and lets them all get run at the same time.
    /// </summary>
    public class ObjectManager
    {
        private static ObjectManager Instance;

        List<GameObject> gameObjects = new List<GameObject>();
        private ObjectManager()
        {

        }

        /// <summary>
        /// gives access to the ObjectManager
        /// </summary>
        /// <returns></returns>
        public static ObjectManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ObjectManager();
            }
            return Instance;
        }

        /// <summary>
        /// Adds a gameobject to be managed
        /// </summary>
        /// <param name="gameObject"></param>
        public void AddObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }
        /// <summary>
        /// updates all the gameobjects
        /// </summary>
        public void Update()
        {
            foreach (GameObject g in gameObjects)
            {
                g.Update();
            }
        }
        /// <summary>
        /// draws all the gameobjects
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb)
        {
            foreach (GameObject g in gameObjects)
            {
                g.Draw(sb);
            }
        }
        /// <summary>
        /// updates all gameobjects after collision has been handled
        /// </summary>
        public void LateUpdate()
        {
            foreach (GameObject g in gameObjects)
            {
                g.LateUpdate();
            }
        }

        public void Clear()
        {
            gameObjects.Clear();
        }
    }
}
