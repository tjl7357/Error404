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
    /// Purpose: GameObject is an object with a position that manages its components
    /// </summary>
    public class GameObject
    {
        #region fields
        // Fields
        protected bool enabled = true;
        protected Vector2 position;
        #endregion

        #region properties
        // Properties

        /// <summary>
        /// Property for the position of the game object
        /// Get returns the location of the top left corner of the game object / No Set
        /// </summary>
        public Vector2 Position { get { return position; } }

        /// <summary>
        /// Property for the X-coordinate of the top left corner of the game object
        /// Get returns the X-coordinate / Set changes the X-coordinate to the given value
        /// </summary>
        public int X { get { return (int)position.X; } set { position.X = value; } }

        /// <summary>
        /// Property for the Y-coordinate of the top left corner of the game object
        /// Get returns the Y-coordinate / Set changes the Y-coordinate to the given value
        /// </summary>
        public int Y { get { return (int)position.Y; } set { position.Y = value; } }

        /// <summary>
        /// Property for whether the game object is enabled or not
        /// Get returns true of the object is enabled / No Set
        /// </summary>
        public bool Enabled { get { return enabled; } }
        #endregion

        // Component Data Structures
        //A Dictionary that contains Lists of components that are of the same type
        Dictionary<Type, List<Component>> ComponentDictionary = new Dictionary<Type, List<Component>>();
        //A list of all existing components for quick running of virtual functions
        List<Component> ComponentList = new List<Component>();

        #region Constructor
        // Constructors

        /// <summary>
        /// Constructor that creates a game object, sets its location to (0,0), and does NOT add it to the object manager
        /// </summary>
        public GameObject() : this(Vector2.Zero)
        {

        }

        /// <summary>
        /// Parameterized Constructor for a game object that assigns a position and adds it to the object manager.
        /// </summary>
        /// <param name="position">The coordinates of the top left corner of the game object</param>
        public GameObject(Vector2 position)
        {
            this.position = position;
            ObjectManager.GetInstance().AddObject(this);
        }
        #endregion

        #region Methods
        // Methods

        /// <summary>
        /// Adds a component to the GameObject
        /// </summary>
        /// <typeparam name="T">The type of component being added</typeparam>
        /// <param name="component">The name of the component being added</param>
        public void AddComponent<T>(T component) where T : Component
        {
            //Adds component to component list for quick access
            ComponentList.Add(component);
            //Adds component to component dictionary, creates new list if it doesnt exist
            if (ComponentDictionary.ContainsKey(typeof(T)))
            {
                ComponentDictionary[typeof(T)].Add(component);
            }
            else
            {
                ComponentDictionary.Add(typeof(T), new List<Component>() { component });
            }
            //Sets components gameobject to this for easy access
            component.GameObject = this;
            //Runs components start method
            component.Start();
        }

        public void RemoveComponent<T>(T component) where T : Component
        {
            ComponentDictionary[typeof(T)].Remove(component);
            ComponentList.Remove(component);
        }

        /// <summary>
        /// informs you whether a component of type exists in the gameobject
        /// </summary>
        /// <typeparam name="ComponentType">The type of the component being searched for</typeparam>
        /// <returns>Returns true if a compnent of that type exists</returns>
        public bool HasComponent<ComponentType>()
        {
            return ComponentDictionary.ContainsKey(typeof(ComponentType));
        }


        /// <summary>
        /// gets the number of components of a type in the gameobject
        /// </summary>
        /// <typeparam name="ComponentType">The type of the component being counted</typeparam>
        /// <returns>Returns the number of components of the given type are attached to the object</returns>
        public int GetComponentCount<ComponentType>()
        {
            return HasComponent<ComponentType>() ? ComponentDictionary[typeof(ComponentType)].Count : 0;
        }

        /// <summary>
        /// gets a reference to the first available component of the gameobject
        /// </summary>
        /// <typeparam name="ComponentType">The type of component being called</typeparam>
        /// <returns>Returns a reference to the first available component</returns>
        public ComponentType GetComponent<ComponentType>() where ComponentType : Component
        {
            if (ComponentDictionary.ContainsKey(typeof(ComponentType)))
            {
                return (ComponentType)ComponentDictionary[typeof(ComponentType)][0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// gets a reference to all the components of a type in the gameobject
        /// </summary>
        /// <typeparam name="ComponentType">The type of component being called</typeparam>
        /// <returns>Returns an array of references to all components of the given type attached to the game object</returns>
        public ComponentType[] GetComponents<ComponentType>() where ComponentType : Component
        {
            if (ComponentDictionary.ContainsKey(typeof(ComponentType)))
            {
                return (ComponentType[])ComponentDictionary[typeof(ComponentType)].ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Updates all the components of the gameobject
        /// </summary>
        public virtual void Update()
        {
            if (enabled)
            {
                foreach (Component c in ComponentList)
                {
                    if (c.Enabled) c.Update();
                }
            }
        }

        /// <summary>
        /// Updates all the components of the gameobject after collisions have been handled.
        /// </summary>
        public virtual void LateUpdate()
        {
            if (enabled)
            {
                foreach (Component c in ComponentList)
                {
                    if (c.Enabled) c.LateUpdate();
                }
            }
        }

        /// <summary>
        /// draws all the components of the gameobject
        /// </summary>
        /// <param name="sb">The spritebatch reference</param>
        public virtual void Draw(SpriteBatch sb)
        {
            if (enabled)
            {
                foreach (Component c in ComponentList)
                {
                    if (c.Enabled)
                        c.Draw(sb);
                }
            }
        }

        /// <summary>
        /// sends a notice of a collision to all the gameobjects
        /// </summary>
        /// <param name="col">A collision object</param>
        public virtual void OnCollision(Collision col)
        {
            if (enabled)
            {
                foreach (Component c in ComponentList)
                {
                    if (c.Enabled) c.OnCollision(col);
                }
            }
        }
        #endregion

    }
}