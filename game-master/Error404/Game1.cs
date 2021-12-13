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
    /// Tracks which menu/state-of-gameplay the game is currently in
    /// </summary>
    enum GameState
    {
        MainMenu,
        CharSelect,
        LevelSelect,
        GamePlay,
        GameWin,
        Pause,
    }
    //Moved PlayerState Enum to Player.cs

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region field creation
        private GameState gameState;
        private SpriteFont VT323_Size50;
        private SpriteFont VT323_Size28;
        private SpriteFont VT323_Size20;
        private SpriteFont VT323_Size12;
        private Player mainCharacter;

        private Texture2D menu;
        private Texture2D pauseScreen;
        private Texture2D levelCompleted;
        private Texture2D start;
        private Texture2D levels;
        private Texture2D options;
        private Texture2D startHover;
        private Texture2D levelsHover;
        private Texture2D optionsHover;
        private Texture2D UI_Bar;

        //need to add ability to switch player textures based on user's selection
        //different sprites for the player
        private Texture2D sprite1;
        private Texture2D sprite2;
        private Texture2D sprite3;

        private Texture2D currentSprite;
        private Texture2D monIdle;
        private Texture2D monDie1;
        private Texture2D monDie2;
        private Texture2D monWalk1;
        private Texture2D monWalk2;
        private Texture2D monWalk3;
        private Texture2D monWalk4;
        private Texture2D monWalk5;
        private Texture2D monJump1;
        private Texture2D monJump2;
        private Texture2D monJump3;

        private Dictionary<string, Texture2D[]> currentAnimationDictionary;

        private int widthOfSingleSprite;
        private int heightOfSingleSprite;

        // Tile Fields
        private Texture2D tile1;
        private Texture2D tile2;
        private Texture2D tile3;
        private Texture2D tile4;
        private Texture2D tile5;
        private Texture2D spikeTile;
        private Texture2D tile6;
        private Texture2D tile7;
        private Texture2D tile8;
        private Texture2D tile9;
        private Texture2D tile10;
        private Texture2D levelEndMarker;
        private Texture2D glitch1;
        private Texture2D glitch2;
        private Texture2D glitch3;
        private Texture2D glitch4;

        // List for access in all other class
        private List<Texture2D> loadTextureList;

        //Fields for menus and menu states
        private Button buttonStart;
        private Button buttonLevels;
        private Button buttonOptions;
        private Button Level1, Level2, Level3, Level4, Level5, Level6, Level7, Level8, Level9, Level10;
        private Button[] LevelButtons;

        // Fields for the tile factory
        private List<string> levelList;
        private int levelIndex;
        private Texture2D tempTexture;
        private TileFactory levelBuilder;

        //Fields for the UI
        private Texture2D coinTexture;
        private double timer;
        private double completionTime;

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 720;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            gameState = GameState.MainMenu;
            /* Offloaded Controls to ControlsManager
            currentMState = Mouse.GetState();
            currentKBState = Keyboard.GetState();
            */


            loadTextureList = new List<Texture2D>();

            timer = 0;

            // Initializes the list of levels
            levelList = new List<string>();
            levelList.Add("../../../../Levels/Level1");
            levelList.Add("../../../../Levels/Level2");
            levelList.Add("../../../../Levels/Level3");
            levelList.Add("../../../../Levels/Level4");
            levelList.Add("../../../../Levels/Level5");
            levelList.Add("../../../../Levels/Level6");
            levelList.Add("../../../../Levels/Level7");
            levelList.Add("../../../../Levels/Level8");
            levelList.Add("../../../../Levels/Level9");
            levelList.Add("../../../../Levels/Level10");

            LevelButtons = new Button[] { Level1, Level2, Level3, Level4, Level5, Level6, Level7, Level8, Level9, Level10 };



            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Texture Loading
            // Content.Load<Texture2D>("FileName");   This makes it easy to load in textures by copying and pasting the statement
            VT323_Size12 = Content.Load<SpriteFont>("VT323-12");
            VT323_Size20 = Content.Load<SpriteFont>("VT323-20");
            VT323_Size28 = Content.Load<SpriteFont>("VT323-28");
            VT323_Size50 = Content.Load<SpriteFont>("VT323-50");
            sprite1 = Content.Load<Texture2D>("Sprite");
            menu = Content.Load<Texture2D>("MainMenu");
            pauseScreen = Content.Load<Texture2D>("PauseScreen");
            levelCompleted = Content.Load<Texture2D>("LevelEnd");
            UI_Bar = Content.Load<Texture2D>("UI-Bar");
            start = Content.Load<Texture2D>("StartButton");
            options = Content.Load<Texture2D>("OptionsButton");
            levels = Content.Load<Texture2D>("LevelsButton");
            startHover = Content.Load<Texture2D>("StartButtonHover");
            optionsHover = Content.Load<Texture2D>("OptionsButtonHover");
            levelsHover = Content.Load<Texture2D>("LevelsButtonHover");

            monIdle = Content.Load<Texture2D>("monIdle");
            monDie1 = Content.Load<Texture2D>("monDie1");
            monDie2 = Content.Load<Texture2D>("monDie2");
            monWalk1 = Content.Load<Texture2D>("monWalk1");
            monWalk2 = Content.Load<Texture2D>("monWalk2");
            monWalk3 = Content.Load<Texture2D>("monWalk3");
            monWalk4 = Content.Load<Texture2D>("monWalk4");
            monWalk5 = Content.Load<Texture2D>("monWalk5");
            monJump1 = Content.Load<Texture2D>("monJump1");
            monJump2 = Content.Load<Texture2D>("monJump2");
            monJump3 = Content.Load<Texture2D>("monJump3");

            // Loading in tile textures
            tile1 = Content.Load<Texture2D>("Tile1");
            tile2 = Content.Load<Texture2D>("Tile2");
            tile3 = Content.Load<Texture2D>("Tile3");
            tile4 = Content.Load<Texture2D>("Tile4");
            tile5 = Content.Load<Texture2D>("Tile5");
            tile6 = Content.Load<Texture2D>("Tile7");
            tile7 = Content.Load<Texture2D>("Tile8");
            tile8 = Content.Load<Texture2D>("Tile9");
            tile9 = Content.Load<Texture2D>("Tile10");
            tile10 = Content.Load<Texture2D>("Tile11");
            spikeTile = Content.Load<Texture2D>("Tile6");
            coinTexture = Content.Load<Texture2D>("Coin");
            levelEndMarker = Content.Load<Texture2D>("ControlPanel");

            // Putting all of these textures into a public list for loading file data
            loadTextureList.Add(tile1);
            loadTextureList.Add(tile2);
            loadTextureList.Add(tile3);
            loadTextureList.Add(tile4);
            loadTextureList.Add(tile5);
            loadTextureList.Add(tile6);
            loadTextureList.Add(tile7);
            loadTextureList.Add(tile8);
            loadTextureList.Add(tile9);
            loadTextureList.Add(tile10);
            loadTextureList.Add(spikeTile);
            loadTextureList.Add(coinTexture);
            loadTextureList.Add(levelEndMarker);

            glitch1 = Content.Load<Texture2D>("Glitch1");
            glitch2 = Content.Load<Texture2D>("Glitch2");
            glitch3 = Content.Load<Texture2D>("Glitch3");
            glitch4 = Content.Load<Texture2D>("Glitch4");
            Texture2D[] glitchAnimation = new Texture2D[] { glitch1, glitch2, glitch3, glitch4 };

            Dictionary<string, Texture2D[]> monsterAnimations = new Dictionary<string, Texture2D[]>();
            monsterAnimations.Add("Idle", new Texture2D[] { monIdle });
            monsterAnimations.Add("Dying", new Texture2D[] { monDie1, monDie2 });
            monsterAnimations.Add("Walking", new Texture2D[] { monWalk1, monWalk2, monWalk3, monWalk4, monWalk5 });
            monsterAnimations.Add("Jumping", new Texture2D[] { monJump1, monJump2 });
            monsterAnimations.Add("Falling", new Texture2D[] { monJump3 });
            widthOfSingleSprite = monIdle.Width;
            heightOfSingleSprite = monIdle.Height;
            currentSprite = monIdle;
            currentAnimationDictionary = monsterAnimations;
            #endregion

            currentSprite = sprite1;

            //Creates buttons
            buttonStart = new Button(715, 367, 233, 53, start, startHover);
            buttonOptions = new Button(715, 461, 233, 53, options, optionsHover);
            buttonLevels = new Button(715, 555, 233, 53, levels, levelsHover);

            for (int i = 0; i < 10; i++)
            {
                LevelButtons[i] = new Button(25 + 250*(i%4), 360 + 100*(int)(i/4), 233, 53, start, startHover);
            }

            // Loading the file
            levelBuilder = new TileFactory(loadTextureList, widthOfSingleSprite, heightOfSingleSprite, monsterAnimations, glitchAnimation);
            // may need to change the constructor here to support our Animation class
            levelIndex = 0;
            levelBuilder.Load(levelList[levelIndex], out mainCharacter);//extra property to use Tile1 to represent hitbox
            //mainCharacter.HitboxTile = tile1;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            TimeManager.GetInstance().UpdateTimeManager(gameTime);
            ControlsManager.GetInstance().Update();

            switch (gameState)
            {
                case GameState.MainMenu:
                    if (buttonStart.MouseOver(ControlsManager.GetInstance().CurrentMouseState))
                    {
                        buttonStart.HoveredOver = true;
                    }
                    else if (!buttonStart.MouseOver(ControlsManager.GetInstance().CurrentMouseState))
                    {
                        buttonStart.HoveredOver = false;
                    }
                    if (buttonOptions.MouseOver(ControlsManager.GetInstance().CurrentMouseState))
                    {
                        buttonOptions.HoveredOver = true;
                    }
                    else if (!buttonOptions.MouseOver(ControlsManager.GetInstance().CurrentMouseState))
                    {
                        buttonOptions.HoveredOver = false;
                    }
                    if (buttonLevels.MouseOver(ControlsManager.GetInstance().CurrentMouseState))
                    {
                        buttonLevels.HoveredOver = true;
                    }
                    else if (!buttonLevels.MouseOver(ControlsManager.GetInstance().CurrentMouseState))
                    {
                        buttonLevels.HoveredOver = false;
                    }
                    if (ControlsManager.GetInstance().MouseClicked())
                    {
                        if (buttonStart.MouseOver(ControlsManager.GetInstance().CurrentMouseState))
                        {//starts gameplay
                            gameState = GameState.GamePlay;
                        }
                        else if (buttonOptions.MouseOver(ControlsManager.GetInstance().CurrentMouseState))
                        {//opens options menu/placeholder
                            gameState = GameState.CharSelect;
                        }
                        else if (buttonLevels.MouseOver(ControlsManager.GetInstance().CurrentMouseState))
                        {//opens level select
                            gameState = GameState.LevelSelect;
                        }
                    }
                    break;

                case GameState.CharSelect:
                    //Mostly placeholder stuff right now
                    if (ControlsManager.GetInstance().MouseClicked())
                    {
                        gameState = GameState.MainMenu;
                    }
                    break;

                case GameState.LevelSelect:
                    //Mostly placeholder stuff right now
                    if (ControlsManager.GetInstance().KeyPressed(Keys.Back))
                    {
                        gameState = GameState.MainMenu;
                    }

                    for (int i = 0; i < 10; i++) {
                        //Level1 for level select
                        if (LevelButtons[i].MouseOver(ControlsManager.GetInstance().CurrentMouseState))
                        {
                            LevelButtons[i].HoveredOver = true;
                            if (ControlsManager.GetInstance().MouseClicked())
                            {
                                UnloadLevel();
                                levelIndex = i;
                                levelBuilder.Load(levelList[levelIndex], out Player newMainCharacter);
                                if(mainCharacter == null)
                                {
                                    mainCharacter = newMainCharacter;
                                }
                                else
                                {
                                    mainCharacter.SetSpawnPoint(newMainCharacter.Position.Location.ToVector2());
                                }
                                gameState = GameState.GamePlay;
                            }
                        }
                        else if (!LevelButtons[i].MouseOver(ControlsManager.GetInstance().CurrentMouseState))
                        {
                            LevelButtons[i].HoveredOver = false;
                        }
                    }
                    break;

                case GameState.GamePlay:
                    if (ControlsManager.GetInstance().KeyPressed(Keys.Enter))
                    {
                        gameState = GameState.Pause;
                    }
                    else
                    {
                        //Updates Every GameObject
                        ObjectManager.GetInstance().Update();
                        CollisionManager.GetInstance().CheckForCollisions();
                        ObjectManager.GetInstance().LateUpdate();

                        // Checks the players location
                        CheckPlayer();

                        timer += gameTime.ElapsedGameTime.TotalSeconds;

                        //Offloaded to Player Class
                        //mainCharacter.MovePlayer(Keyboard.GetState(), gameTime);
                        //mainCharacter.UpdateAnimation(gameTime);

                        base.Update(gameTime);
                    }
                    break;


                case GameState.GameWin:
                    // If the user presses backspace, returns to the main menu
                    // If the user presses enter, proceed to the level select screen
                    if (ControlsManager.GetInstance().KeyPressed(Keys.Escape))
                    {
                        gameState = GameState.MainMenu;
                    }
                    else if (ControlsManager.GetInstance().KeyPressed(Keys.Enter))
                    {
                        if (levelIndex < 9)
                        {
                            // Updates the stat bar
                            timer = 0;
                            mainCharacter.CoinsCollected = 0;
                            mainCharacter.DeathCount = 0;

                            UnloadLevel();
                            // Goes to the next level and starts it
                            levelIndex++;
                            levelBuilder.Load(levelList[levelIndex], out Player newMainCharacter);

                            mainCharacter.SetSpawnPoint(newMainCharacter.Position.Location.ToVector2());
                            // Resets gamestate
                            gameState = GameState.GamePlay;
                        }
                        else
                        {
                            levelIndex = 0;
                            levelBuilder.Load(levelList[levelIndex], out mainCharacter);
                            gameState = GameState.MainMenu;
                        }
                    }
                    else if (ControlsManager.GetInstance().KeyPressed(Keys.Back))
                        gameState = GameState.LevelSelect;
                    break;

                case GameState.Pause:
                    //Mostly placeholder stuff right now
                    if (ControlsManager.GetInstance().KeyPressed(Keys.Enter))
                    {
                        gameState = GameState.GamePlay;
                    }
                    else if (ControlsManager.GetInstance().KeyPressed(Keys.Back))
                        gameState = GameState.MainMenu;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            IsMouseVisible = true;
            switch (gameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(menu, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    buttonStart.Draw(spriteBatch);
                    buttonOptions.Draw(spriteBatch);
                    buttonLevels.Draw(spriteBatch);
                    break;

                case GameState.CharSelect:
                    spriteBatch.Draw(menu, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    spriteBatch.DrawString(VT323_Size20, "Currently just a placeholder pending addition of character selection;\n click anywhere to return to the main menu",
                        new Vector2(100, 180), Color.White);
                    break;

                case GameState.LevelSelect:
                    spriteBatch.Draw(menu, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    spriteBatch.DrawString(VT323_Size20, "Press Backspace to Return to Menu",
                        new Vector2(100, 180), Color.White);

                    for(int i = 0; i < 10; i++)
                    {
                        LevelButtons[i].Draw(spriteBatch);
                    }
                    break;

                case GameState.GamePlay:
                    //Draws Every GameObject
                    ObjectManager.GetInstance().Draw(spriteBatch);
                    DrawUI();
                    break;

                case GameState.GameWin:
                    spriteBatch.Draw(levelCompleted, new Vector2(0), Color.White);
                    double seconds = completionTime % 60;
                    int hours = (int)(completionTime - seconds) / 60;
                    int minutes = hours % 60;
                    hours = (hours - minutes) / 60;
                    spriteBatch.DrawString(VT323_Size50, String.Format("{0:00}:{1:00}:{2:00.00}", hours, minutes, seconds), new Vector2(484, 557), Color.Green);
                    spriteBatch.DrawString(VT323_Size50, mainCharacter.CoinsCollected.ToString(), new Vector2(581, 121), Color.Green);
                    break;

                case GameState.Pause:
                    ObjectManager.GetInstance().Draw(spriteBatch);
                    spriteBatch.Draw(pauseScreen, new Vector2(0), Color.White);
                    break;

                default:
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Helper method: Draws the UI
        /// </summary>
        public void DrawUI()
        {
            spriteBatch.Draw(UI_Bar, new Vector2(0), Color.White);
            //Timer
            //Prepares ints for timer display; simplifies string formatting
            double seconds = timer % 60;
            int hours = (int)(timer - seconds) / 60;
            int minutes = hours % 60;
            hours = (hours - minutes) / 60;
            spriteBatch.DrawString(VT323_Size28, String.Format("{0:00}:{1:00}:{2:00.00}", hours, minutes, seconds), new Vector2(131, 10), Color.White);

            //BitCoin counter
            spriteBatch.DrawString(VT323_Size28, mainCharacter.CoinsCollected.ToString(), new Vector2(626, 10), Color.White);

            //Death counter
            spriteBatch.DrawString(VT323_Size28, mainCharacter.DeathCount.ToString(), new Vector2(920, 10), Color.White);
        }


        public void CheckPlayer()
        {
            // If the player goes below the screen, respawn
            if (mainCharacter.Position.Y >= graphics.PreferredBackBufferHeight)
            {
                mainCharacter.Respawn();
            }
            // If the player has left the right side of the screen, then you win!
            if (mainCharacter.Position.X >= graphics.PreferredBackBufferWidth)
            {
                gameState = GameState.GameWin;
                completionTime = timer;
            }
        }

        public void UnloadLevel()
        {
            ObjectManager.GetInstance().Clear();
            ObjectManager.GetInstance().AddObject(mainCharacter);
            CollisionManager.GetInstance().ClearStaticColliders();
        }
    }
}