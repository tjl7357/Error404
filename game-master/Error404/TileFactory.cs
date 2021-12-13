using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

// KillerCows

/// <summary>
/// Loads in a level file, create the game objects required for the level, and to be able to draw the level to the screen.
/// </summary>

namespace Error404
{
    class TileFactory
    {
        // Fields
        private List<Texture2D> loadTextureList;

        // Fields for the player object
        private int spriteWidth;
        private int spriteHeight;
        private Dictionary<string, Texture2D[]> startingSprites;
        private Player character;

        Texture2D[] glitchAnimation;
        double activeTime; // Holds the values for glitch timing
        double inactiveTime; // for now the default will be 3 seconds of each

        // Constructor

        /// <summary>
        /// Constructor for the TileFactory.  Initializes the array and loads in the textures
        /// </summary>
        /// <param name="textureList">A list of textures to be used when loading</param>
        /// <param name="spriteWidth">The width of the player sprite</param>
        /// <param name="spriteHeight">The height of the player sprite</param>
        /// <param name="startingSprite">The starting sprite of the player</param>
        public TileFactory(List<Texture2D> textureList, int spriteWidth, int spriteHeight, Dictionary<string, Texture2D[]> startingSprite, Texture2D[] glitchAnimation)
        {
            // takes in a list and sets the internal list equal to it
            loadTextureList = textureList;

            // Initializes player fields
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
            this.startingSprites = startingSprite;
            character = null;

            // Disappearing object related fields
            this.glitchAnimation = glitchAnimation;
            activeTime = 3;
            inactiveTime = 3;
        }

        // Pulic Methods

        /// <summary>
        /// Loads in a text document and converts the files data into an array of gameObjects
        /// </summary>
        /// <param name="level">The file being loaded in</param>
        /// <returns>Returns the player object so that other methods can manipulate it</returns>
        public void Load(string level, out Player p1)
        {
            // Creates the filestream and stream reader for use in the try/catch statement
            FileStream readStream = null;
            StreamReader input = null;

            // Tries to open a read stream and read in the data contained in a text file
            try
            {
                // Sets of the stream
                readStream = File.OpenRead(level);
                input = new StreamReader(readStream);

                // Creates a string and array for reading the file, as well as an int to id what
                //   index we are at
                string row = null;
                string[] stringArray = null;

                // Loops until it has read through the entire file and adds them to the tileArray.
                //   Increments the index
                for (int index = 0; index < 24; index++)
                {
                    row = input.ReadLine();
                    // If the row returns a string, then proceed
                    if (row != null)
                    {
                        stringArray = row.Split(' ');
                        CreateTiles(stringArray, index);
                    }
                }
            }
            // If any errors occur, then notifies the developer
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            // Finally, if the stream is still open, then close it
            finally
            {
                if (input != null)
                {
                    input.Close();
                }
            }

            // Returns the player so it can be used in other methods
            p1 = character;
        }

        // Helper Methods

        /// <summary>
        /// Helper method that converts the string Data to gameObjects
        /// </summary>
        /// <param name="stringTiles">The string data being converted</param>
        /// <returns>An array of the converted data</returns>
        private void CreateTiles(string[] stringTiles, int row)
        {
            // Loops through each element in the stringTiles array and converts them to GameObjects
            for (int i = 0; i < stringTiles.Length; i++)
            {
                // Creates a tempGameObject for use in and out of the switch statement below
                GameObject tempGameObj = null;

                // Switch case based on what the identifier is
                switch (stringTiles[i])
                {
                    // A turquoise tile that looks like a motherboard
                    // Has the Texture and Collider components
                    case "0":
                        //TempGameObject is created and has a textureComponent attached
                        tempGameObj = new GameObject(new Vector2(i * 32, row * 32));
                        tempGameObj.AddComponent<TextureComponent>(new TextureComponent(loadTextureList[0], 32, 32));
                        //Makes it a Collider
                        tempGameObj.AddComponent<Collider>(new Collider(32, 32, false));
                        break;

                    // Is the same as 0 but is a glitched version
                    case ")":
                        tempGameObj = new DisappearingObject(new Rectangle(i * 32, row * 32, 32, 32),
                            loadTextureList[0], glitchAnimation, activeTime, inactiveTime);
                        break;

                    // A maroon tile that looks like a crossbeam
                    // Has the Texture and Collider components
                    case "1":
                        //TempGameObject is created and has a textureComponent attached
                        tempGameObj = new GameObject(new Vector2(i * 32, row * 32));
                        tempGameObj.AddComponent<TextureComponent>(new TextureComponent(loadTextureList[1], 32, 32));
                        //Makes it a Collider
                        tempGameObj.AddComponent<Collider>(new Collider(32, 32, false));
                        break;

                    // Is the same as 1 but is a glitched version
                    case "!":
                        tempGameObj = new DisappearingObject(new Rectangle(i * 32, row * 32, 32, 32),
                            loadTextureList[1], glitchAnimation, activeTime, inactiveTime);
                        break;

                    // A green Tile that looks like a motherboard
                    // Has the Texture and Collider components
                    case "2":
                        // Creates a tempGameObj and adds its texture component
                        tempGameObj = new GameObject(new Vector2(i * 32, row * 32));
                        tempGameObj.AddComponent<TextureComponent>(new TextureComponent(loadTextureList[2], 32, 32));
                        //Makes it a Collider
                        tempGameObj.AddComponent<Collider>(new Collider(32, 32, false));
                        break;

                    // Is the same as 2 but is a glitched version
                    case "@":
                        tempGameObj = new DisappearingObject(new Rectangle(i * 32, row * 32, 32, 32),
                            loadTextureList[2], glitchAnimation, activeTime, inactiveTime);
                        break;

                    // A green tile that looks like a different section of a motherboard
                    // Has the Texture and Collider components
                    case "3":
                        // Creates a tempGameObj and adds its texture component
                        tempGameObj = new GameObject(new Vector2(i * 32, row * 32));
                        tempGameObj.AddComponent<TextureComponent>(new TextureComponent(loadTextureList[3], 32, 32));
                        //Makes it a Collider
                        tempGameObj.AddComponent<Collider>(new Collider(32, 32, false));
                        break;

                    // Is the same as 3 but is a glitched version
                    case "#":
                        tempGameObj = new DisappearingObject(new Rectangle(i * 32, row * 32, 32, 32),
                            loadTextureList[3], glitchAnimation, activeTime, inactiveTime);
                        break;

                    // A blue tile that looks like a vent grate
                    // Has the Texture and Collider components
                    case "4":
                        // Creates a tempGameObj and adds its texture component
                        tempGameObj = new GameObject(new Vector2(i * 32, row * 32));
                        tempGameObj.AddComponent<TextureComponent>(new TextureComponent(loadTextureList[4], 32, 32));
                        //Makes it a Collider
                        tempGameObj.AddComponent<Collider>(new Collider(32, 32, false));
                        break;

                    // Is the same as 4 but is a glitched version
                    case "$":
                        tempGameObj = new DisappearingObject(new Rectangle(i * 32, row * 32, 32, 32),
                            loadTextureList[4], glitchAnimation, activeTime, inactiveTime);
                        break;

                    // A silver tile that looks like a wall panel
                    // Has the Texture and Collider components
                    case "5":
                        // Creates a tempGameObj and adds its texture component
                        tempGameObj = new GameObject(new Vector2(i * 32, row * 32));
                        tempGameObj.AddComponent<TextureComponent>(new TextureComponent(loadTextureList[9], 32, 32));
                        //Makes it a Collider
                        tempGameObj.AddComponent<Collider>(new Collider(32, 32, false));
                        break;

                    // Is the same as 5 but is a glitched version
                    case "%":
                        tempGameObj = new DisappearingObject(new Rectangle(i * 32, row * 32, 32, 32), 
                            loadTextureList[9], glitchAnimation, activeTime, inactiveTime);
                        break;

                    // A silver tile that looks like a vent grate
                    // Has the Texture and Collider components
                    case "6":
                        // Creates a tempGameObj and adds its texture component
                        tempGameObj = new GameObject(new Vector2(i * 32, row * 32));
                        tempGameObj.AddComponent<TextureComponent>(new TextureComponent(loadTextureList[5], 32, 32));
                        //Makes it a Collider
                        tempGameObj.AddComponent<Collider>(new Collider(32, 32, false));
                        break;

                    // Is the same as 6 but is a glitched version
                    case "^":
                        tempGameObj = new DisappearingObject(new Rectangle(i * 32, row * 32, 32, 32),
                            loadTextureList[5], glitchAnimation, activeTime, inactiveTime);
                        break;

                    // A blue tile that looks like a wall panel
                    // Has the Texture and Collider components
                    case "7":
                        // Creates a tempGameObj and adds its texture component
                        tempGameObj = new GameObject(new Vector2(i * 32, row * 32));
                        tempGameObj.AddComponent<TextureComponent>(new TextureComponent(loadTextureList[6], 32, 32));
                        //Makes it a Collider
                        tempGameObj.AddComponent<Collider>(new Collider(32, 32, false));
                        break;

                    // Is the same as 7 but is a glitched version
                    case "&":
                        tempGameObj = new DisappearingObject(new Rectangle(i * 32, row * 32, 32, 32),
                            loadTextureList[6], glitchAnimation, activeTime, inactiveTime);
                        break;

                    // A black tile that looks like a cross beam
                    // Has the Texture and Collider components
                    case "8":
                        // Creates a tempGameObj and adds its texture component
                        tempGameObj = new GameObject(new Vector2(i * 32, row * 32));
                        tempGameObj.AddComponent<TextureComponent>(new TextureComponent(loadTextureList[7], 32, 32));
                        //Makes it a Collider
                        tempGameObj.AddComponent<Collider>(new Collider(32, 32, false));
                        break;

                    // Is the same as 8 but is a glitched version
                    case "*":
                        tempGameObj = new DisappearingObject(new Rectangle(i * 32, row * 32, 32, 32),
                            loadTextureList[7], glitchAnimation, activeTime, inactiveTime);
                        break;

                    // A gray tile that looks like a cross beam
                    // Has the Texture and Collider components
                    case "9":
                        // Creates a tempGameObj and adds its texture component
                        tempGameObj = new GameObject(new Vector2(i * 32, row * 32));
                        tempGameObj.AddComponent<TextureComponent>(new TextureComponent(loadTextureList[8], 32, 32));
                        //Makes it a Collider
                        tempGameObj.AddComponent<Collider>(new Collider(32, 32, false));
                        break;

                    // Is the same as 9 but is a glitched version
                    case "(":
                        tempGameObj = new DisappearingObject(new Rectangle(i * 32, row * 32, 32, 32),
                            loadTextureList[8], glitchAnimation, activeTime, inactiveTime);
                        break;

                    // Creates the player object and sets its start location
                    case "P":
                        // Creates a new player and adds a texture and collision component to it
                        character = new Player(new Rectangle(i * 32, row * 32, 32, 32), startingSprites);
                        character.AddComponent<Collider>(new Collider(24, 32, true, new Vector2(4, 0)));
                        character.Size = new Vector2(24, 32);
                        character.Offset = new Vector2(4, 0);
                        break;

                    // Creates a spike tile
                    // Has the Texture, LethalObject, and Collider components
                    case "S":
                        // Creates a tempGameObj and adds its texture component
                        tempGameObj = new GameObject(new Vector2(i * 32, row * 32));
                        tempGameObj.AddComponent<TextureComponent>(new TextureComponent(loadTextureList[10], 32, 32));
                        tempGameObj.AddComponent<LethalObject>(new LethalObject());
                        tempGameObj.AddComponent<Collider>(new Collider(16, 16, false, new Vector2(8, 16)));
                        break;

                    // Is a glitchy spike tile
                    case "s":
                        tempGameObj = new DisappearingObject(new Rectangle(i * 32, row * 32, 32, 32),
                            loadTextureList[10], glitchAnimation, activeTime, inactiveTime);
                        tempGameObj.AddComponent<LethalObject>(new LethalObject());
                        break;

                    // Creates a coin collectible
                    // Has a texture component attached to it
                    case "c":
                        // Creates a tempGameObj and adds its texture component and collectible component
                        tempGameObj = new Collectible(new Rectangle(i * 32, row * 32, 32, 32), loadTextureList[11]);
                        tempGameObj.AddComponent<TextureComponent>(new TextureComponent(loadTextureList[11], 32, 32));
                        tempGameObj.AddComponent<Collider>(new Collider(32, 32, false));
                        break;

                    // Catches other tiles that aren't covered above, but does nothing with that data
                    default:
                        break;
                }

                // If tempGameObj is not null, Adds the object to the array
                if (tempGameObj != null)
                {
                    ObjectManager.GetInstance().AddObject(tempGameObj);
                }
            }
        }
    }
}
