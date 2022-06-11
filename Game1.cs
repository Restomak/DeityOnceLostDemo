using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DeityOnceLost
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public enum gameState
        {
            title,
            combat,
            dungeon,
            happening
        }

        //Framework variables
        private static Game1 _currentGame;
        private static SpriteBatch _spriteBatch;
        private static GraphicsDeviceManager _graphics;
        private static RenderTarget2D _renderTarget;
        private static Random rand = new Random();
        private static Input.WindowControl _windowControl;
        private static Input.InputController _inputController;
        private static gameState _gameState, _previousGameState;
        private static bool _gameInitialized = false; //first loop through Update will initialize the game then set to true
        private double timeSinceLastAnimationUpdate = 0; //used to make sure animation ticks are constrained
        private int millisecondsPerAnimationUpdate = 16; //slightly over 60 per second

        //User Interface variables
        private static List<UserInterface.UserInterface> _activeUI;
        private static List<UserInterface.UserInterface> _previousUI;
        private static UserInterface.Clickable _currentHover;
        private static UserInterface.TopBarUI _topBar;

        //Game structure variables
        private static Drawing.DrawHandler _drawHandler;
        private static DeckBuilder.CardCollection _cardCollection_All;
        private static Combat.CombatHandler _combatHandler;
        private static Dungeon.DungeonHandler _dungeonHandler;
        private static Events.EventHandler _eventHandler;
        private static Characters.Hero _hero; //Will probably be initialized elsewhere later post demo
        private static Characters.Champion _champ; //Will probably be initialized elsewhere later post demo

        //Logs
        public static List<String> errorLog;
        public static List<String> debugLog;
        public static bool showErrorLog = false, showDebugLog = false;


        //Framework constants
        public const double VIRTUAL_SCREEN_RATIO_X = 16;
        public const double VIRTUAL_SCREEN_RATIO_Y = 9;
        public const int VIRTUAL_WINDOW_WIDTH = (int)VIRTUAL_SCREEN_RATIO_X * 100;
        public const int VIRTUAL_WINDOW_HEIGHT = (int)VIRTUAL_SCREEN_RATIO_Y * 100;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;
            _currentGame = this;

            _windowControl = new Input.WindowControl();
            _inputController = new Input.InputController();
        }

        //Framework getters
        public GraphicsDeviceManager getGraphics()
        {
            return _graphics;
        }
        public SpriteBatch getSpriteBatch()
        {
            return _spriteBatch;
        }
        public GameWindow getWindow()
        {
            return Window;
        }
        public static Game1 getGame()
        {
            return _currentGame;
        }
        public static Input.InputController getInputController()
        {
            return _inputController;
        }
        public static Combat.CombatHandler getCombatHandler()
        {
            return _combatHandler;
        }
        public static Dungeon.DungeonHandler getDungeonHandler()
        {
            return _dungeonHandler;
        }
        public static Events.EventHandler getEventHandler()
        {
            return _eventHandler;
        }

        
        //User Interface setters
        public static void setHoveredClickable(UserInterface.Clickable clickable)
        {
            _currentHover = clickable;
        }

        //User Interface getters
        public static UserInterface.Clickable getHoveredClickable()
        {
            return _currentHover;
        }

        //Other useful getters
        public static Characters.Champion getChamp()
        {
            return _champ;
        }

        public static Point getPlayerLocationOnMap()
        {
            return _dungeonHandler.getPlayerLocation();
        }



        public static void enterNewCombat(Combat.Encounter newEncounter, Dungeon.Room currentRoom)
        {
            _gameState = gameState.combat;
            _combatHandler.setNewEncounter(newEncounter);
            _combatHandler.setCurrentRoom(currentRoom);
            _combatHandler.combatStart(_activeUI);
        }

        public static void returnToDungeon()
        {
            _gameState = gameState.dungeon;
            _dungeonHandler.returnToDungeon(_activeUI);
            _drawHandler.setEventTextBox(null);
        }

        public static void startEvent(Dungeon.Room room)
        {
            startEvent(room.getRoomEvent());
            _eventHandler.setCurrentRoom(room);
        }
        public static void startEvent(Events.Happening newEvent)
        {
            if (_gameState != gameState.happening)
            {
                _previousGameState = _gameState;
                _previousUI = _activeUI;
            }
            _gameState = gameState.happening;
            _eventHandler.setupNewEvent(_activeUI, newEvent);
            _drawHandler.setEventTextBox(new Drawing.EventTextBox(newEvent.getWriting()));
        }

        public static void eventComplete()
        {
            _gameState = _previousGameState;
            _activeUI = _previousUI;
        }

        public static void addTopBar()
        {
            _topBar.addToActiveUI(_activeUI);
        }

        public static void updateTopBar()
        {
            _topBar.updateUI();
        }

        public static void setupRandomFirstChampion()
        {
            _hero = new Characters.Hero();
            _champ = new Characters.Champion(_hero);
            _topBar.setupUI();
        }



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _windowControl.Initialize();
            _renderTarget = new RenderTarget2D(GraphicsDevice, VIRTUAL_WINDOW_WIDTH, VIRTUAL_WINDOW_HEIGHT, false, GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
            this.IsMouseVisible = true;

            _activeUI = new List<UserInterface.UserInterface>();
            _topBar = new UserInterface.TopBarUI();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //functionality art
            pic_functionality_uiSketch = Content.Load<Texture2D>("functionality art/UI Sketch");
            pic_functionality_endTurnButton = Content.Load<Texture2D>("functionality art/End Turn Button");
            pic_functionality_cardDown = Content.Load<Texture2D>("functionality art/Card Down");
            pic_functionality_bar = Content.Load<Texture2D>("functionality art/Bar");
            pic_functionality_intent_AoE = Content.Load<Texture2D>("functionality art/Intent AoE");
            pic_functionality_intent_Attack = Content.Load<Texture2D>("functionality art/Intent Attack");
            pic_functionality_intent_Buff = Content.Load<Texture2D>("functionality art/Intent Buff");
            pic_functionality_intent_Debuff = Content.Load<Texture2D>("functionality art/Intent Debuff");
            pic_functionality_intent_Defend = Content.Load<Texture2D>("functionality art/Intent Defend");
            pic_functionality_defenseIcon = Content.Load<Texture2D>("functionality art/Defense");
            pic_functionality_championSilhouette = Content.Load<Texture2D>("functionality art/silhouette");
            pic_functionality_targeting_faded_TL = Content.Load<Texture2D>("functionality art/targeting faded - top left");
            pic_functionality_targeting_faded_TR = Content.Load<Texture2D>("functionality art/targeting faded - top right");
            pic_functionality_targeting_faded_BR = Content.Load<Texture2D>("functionality art/targeting faded - bottom right");
            pic_functionality_targeting_faded_BL = Content.Load<Texture2D>("functionality art/targeting faded - bottom left");
            pic_functionality_targeting_back_TL = Content.Load<Texture2D>("functionality art/targeting back - top left");
            pic_functionality_targeting_back_TR = Content.Load<Texture2D>("functionality art/targeting back - top right");
            pic_functionality_targeting_back_BR = Content.Load<Texture2D>("functionality art/targeting back - bottom right");
            pic_functionality_targeting_back_BL = Content.Load<Texture2D>("functionality art/targeting back - bottom left");
            pic_functionality_mapRoom = Content.Load<Texture2D>("functionality art/Map Room");
            pic_functionality_mapConnectorH = Content.Load<Texture2D>("functionality art/Map Open Connector H");
            pic_functionality_mapConnectorV = Content.Load<Texture2D>("functionality art/Map Open Connector V");
            pic_functionality_mapChampLoc = Content.Load<Texture2D>("functionality art/Map Champion Location");
            pic_functionality_mapStoryIcon = Content.Load<Texture2D>("functionality art/Map Story Icon");
            pic_functionality_mapCombatIcon = Content.Load<Texture2D>("functionality art/Map Combat Icon");
            pic_functionality_mapExitIcon = Content.Load<Texture2D>("functionality art/Map Exit Icon");
            pic_functionality_mapConnectorWindowH = Content.Load<Texture2D>("functionality art/Map Window Connector H");
            pic_functionality_mapConnectorWindowV = Content.Load<Texture2D>("functionality art/Map Window Connector V");

            //fonts
            roboto_regular_8 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-8");
            roboto_medium_8 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-8");
            roboto_bold_8 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-8");
            roboto_black_8 = Content.Load<SpriteFont>("Fonts/Roboto-Black-8");
            roboto_regular_10 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-10");
            roboto_medium_10 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-10");
            roboto_bold_10 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-10");
            roboto_black_10 = Content.Load<SpriteFont>("Fonts/Roboto-Black-10");
            roboto_regular_11 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-11");
            roboto_medium_11 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-11");
            roboto_bold_11 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-11");
            roboto_black_11 = Content.Load<SpriteFont>("Fonts/Roboto-Black-11");
            roboto_regular_12 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-12");
            roboto_medium_12 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-12");
            roboto_bold_12 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-12");
            roboto_black_12 = Content.Load<SpriteFont>("Fonts/Roboto-Black-12");
            roboto_regular_14 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-14");
            roboto_medium_14 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-14");
            roboto_bold_14 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-14");
            roboto_black_14 = Content.Load<SpriteFont>("Fonts/Roboto-Black-14");
            roboto_regular_16 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-16");
            roboto_medium_16 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-16");
            roboto_bold_16 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-16");
            roboto_black_16 = Content.Load<SpriteFont>("Fonts/Roboto-Black-16");
            roboto_regular_20 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-20");
            roboto_medium_20 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-20");
            roboto_bold_20 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-20");
            roboto_black_20 = Content.Load<SpriteFont>("Fonts/Roboto-Black-20");
            roboto_regular_24 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-24");
            roboto_medium_24 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-24");
            roboto_bold_24 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-24");
            roboto_black_24 = Content.Load<SpriteFont>("Fonts/Roboto-Black-24");

            //cards
            pic_card_front_default = Content.Load<Texture2D>("Cards/Card Default");
            pic_card_front_common = Content.Load<Texture2D>("Cards/Card Common");
            pic_card_front_rare = Content.Load<Texture2D>("Cards/Card Rare");
            pic_card_front_epic = Content.Load<Texture2D>("Cards/Card Epic");
            pic_card_front_godly = Content.Load<Texture2D>("Cards/Card Godly");
            pic_card_front_void = Content.Load<Texture2D>("Cards/Card Void");

            //enemies
            pic_enemy_fanbladeGuard = Content.Load<Texture2D>("Enemies/Fanblade Guard");

            //shaders
            shader_Regular = Content.Load<Effect>("Shaders/Regular");
            shader_CardGlow = Content.Load<Effect>("Shaders/Card Glow");
            shader_DeckGlow = Content.Load<Effect>("Shaders/Deck Glow");
            shader_Experimental = Content.Load<Effect>("Shaders/Experimental");

            //backgrounds
            pic_background_map = Content.Load<Texture2D>("Backgrounds/Map Background");
            pic_background_event = Content.Load<Texture2D>("Backgrounds/Event Background");
        }

        /*____________________.Content Variables._____________________*/
        //Functionality Art
        public static Texture2D pic_functionality_uiSketch, pic_functionality_endTurnButton, pic_functionality_cardDown, pic_functionality_bar,
            pic_functionality_intent_AoE, pic_functionality_intent_Attack, pic_functionality_intent_Buff, pic_functionality_intent_Debuff, pic_functionality_intent_Defend,
            pic_functionality_defenseIcon, pic_functionality_championSilhouette,
            pic_functionality_targeting_faded_TL, pic_functionality_targeting_faded_TR, pic_functionality_targeting_faded_BR, pic_functionality_targeting_faded_BL,
            pic_functionality_targeting_back_TL, pic_functionality_targeting_back_TR, pic_functionality_targeting_back_BR, pic_functionality_targeting_back_BL,
            pic_functionality_mapRoom, pic_functionality_mapConnectorH, pic_functionality_mapConnectorV, pic_functionality_mapChampLoc, pic_functionality_mapStoryIcon,
            pic_functionality_mapCombatIcon, pic_functionality_mapExitIcon, pic_functionality_mapConnectorWindowH, pic_functionality_mapConnectorWindowV;

        //Fonts
        public static SpriteFont roboto_regular_8, roboto_medium_8, roboto_bold_8, roboto_black_8,
            roboto_regular_10, roboto_medium_10, roboto_bold_10, roboto_black_10,
            roboto_regular_11, roboto_medium_11, roboto_bold_11, roboto_black_11,
            roboto_regular_12, roboto_medium_12, roboto_bold_12, roboto_black_12,
            roboto_regular_14, roboto_medium_14, roboto_bold_14, roboto_black_14,
            roboto_regular_16, roboto_medium_16, roboto_bold_16, roboto_black_16,
            roboto_regular_20, roboto_medium_20, roboto_bold_20, roboto_black_20,
            roboto_regular_24, roboto_medium_24, roboto_bold_24, roboto_black_24;

        //Cards
        public static Texture2D pic_card_front_default, pic_card_front_common, pic_card_front_rare,
            pic_card_front_epic, pic_card_front_godly, pic_card_front_void;

        //Enemies
        public static Texture2D pic_enemy_fanbladeGuard;
        
        //Shaders
        public static Effect shader_Regular, shader_CardGlow, shader_DeckGlow, shader_Experimental;

        //Backgrounds
        public static Texture2D pic_background_map, pic_background_event;


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        
        /// <summary>
        /// Works very similarly to the turing function "randint(variable, min, max)" except you use this one
        /// by doing "variable = Game1.randint(min, max)" instead.
        /// </summary>
        public static int randint(int min, int max)
        {
            return rand.Next(min, max + 1);
        }
        
        /// <summary>
        /// Should return a list of one of every single card in the game, no more or less. Currently all cards
        /// are manually initialized in CardCollection.cs - needs a better instantiation method
        /// </summary>
        public static DeckBuilder.CardCollection getAllCards()
        {
            return _cardCollection_All;
        }

        /// <summary>
        /// Runs on the first loop of the game. Initialize everything here, much like in a constructor
        /// </summary>
        public static void InitializeGame()
        {
            //Logs
            errorLog = new List<string>();
            debugLog = new List<string>();
            errorLog.Add("ERROR LOG");
            debugLog.Add("DEBUG LOG");

            //Structure stuff
            _drawHandler = new Drawing.DrawHandler();

            //Game stuff
            _cardCollection_All = new DeckBuilder.CardCollection();
            Characters.Names.initializeNameList();
            _combatHandler = new Combat.CombatHandler();
            _dungeonHandler = new Dungeon.DungeonHandler();
            _eventHandler = new Events.EventHandler();
            _gameState = gameState.title;

            //Temporary testing stuff
            _dungeonHandler.setupDungeon(_activeUI, new Dungeon.Locations.FirstDungeon());
            
            _gameInitialized = true;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Window stuff first
            _windowControl.setWindowPosX(Window.Position.X);
            _windowControl.setWindowPosY(Window.Position.Y);
            
            //Make sure game is initialized
            if (!_gameInitialized)
            {
                InitializeGame();
            }



            //Input
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _inputController.updateInput(_windowControl, _activeUI, _gameState);

            //Debug log on screen
            if (Keyboard.GetState().IsKeyDown(Keys.OemTilde) && Keyboard.GetState().IsKeyDown(Keys.D))
            {
                showDebugLog = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.OemTilde) && Keyboard.GetState().IsKeyDown(Keys.E))
            {
                showErrorLog = true;
            }
            else
            {
                showDebugLog = false;
                showErrorLog = false;
            }



            //Game Logic
            switch (_gameState)
            {
                case gameState.combat:
                    _combatHandler.handleCombat();
                    break;
                case gameState.dungeon:
                    _dungeonHandler.handleDungeonLogic();
                    break;
                case gameState.happening:
                    _eventHandler.handleEventLogic();
                    break;
            }


            /*____________________.Temporary testing input._____________________*/
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && _gameState == gameState.title)
            {
                _gameState = gameState.dungeon;
            }



            //end of loop
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget); //draw to texture first so screen resizing doesn't require resolution changes
            GraphicsDevice.Clear(new Color(36, 0, 72)); //dark indigo
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);
            shader_Regular.CurrentTechnique.Passes[0].Apply();



            //Animation
            timeSinceLastAnimationUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastAnimationUpdate >= millisecondsPerAnimationUpdate)
            {
                timeSinceLastAnimationUpdate = 0;

                //Animation logic goes here
                _drawHandler.pulse();
            }



            /*____________________.Draw Logic._____________________*/


            //Draw backgrounds
            if (_gameState == gameState.title)
            {
                Drawing.DrawHandler.drawTitle_Background(_spriteBatch);
            }
            else if (_gameState == gameState.combat || _gameState == gameState.happening && _previousGameState == gameState.combat)
            {
                Drawing.DrawHandler.drawCombat_Background(_spriteBatch);
            }
            else if (_gameState == gameState.dungeon || _gameState == gameState.happening && _previousGameState == gameState.dungeon)
            {
                Drawing.DrawHandler.drawMap_Background(_spriteBatch);
            }

            if (_gameState == gameState.happening) //Draw over other backgrounds
            {
                _drawHandler.drawEvent_Background(_spriteBatch);
            }

            Drawing.DrawHandler.drawTopBar_Background(_spriteBatch); //Draw the top bar's background


            //Draw UIs
            if (_gameState == gameState.title) //demo stuff, will be removed later
            {
                Drawing.DrawHandler.drawTitle_Background(_spriteBatch);
            }
            else if (_gameState == gameState.combat || _gameState == gameState.happening && _previousGameState == gameState.combat)
            {
                _drawHandler.drawUI(_spriteBatch, _activeUI, _champ);

                if (_combatHandler.getCombatUI().getActiveCard() != null)
                {
                    //Change up the shader for the glow
                    shader_CardGlow.CurrentTechnique.Passes[0].Apply();
                    Drawing.DrawHandler.drawCombat_HandCard(_spriteBatch, _combatHandler.getCombatUI().getActiveCard(), _champ, true);

                    //Set it back to the regular shader
                    shader_Regular.CurrentTechnique.Passes[0].Apply();
                    Drawing.DrawHandler.drawCombat_HandCard(_spriteBatch, _combatHandler.getCombatUI().getActiveCard(), _champ, false);
                }
                if (_currentHover != null)
                {
                    if (_currentHover.GetType() == typeof(UserInterface.Clickables.Button))
                    {
                        //Doesn't glow using a shader atm
                        Drawing.DrawHandler.drawUI_glowButton(_spriteBatch, (UserInterface.Clickables.Button)_currentHover);
                    }
                    else if (_currentHover.GetType() == typeof(UserInterface.Clickables.DeckOfCards) &&
                        ((UserInterface.Clickables.DeckOfCards)_currentHover).getDeckType() != UserInterface.Clickables.DeckOfCards.typeOfDeck.WHOLECOLLECTION &&
                        ((UserInterface.Clickables.DeckOfCards)_currentHover).getDeckType() != UserInterface.Clickables.DeckOfCards.typeOfDeck.DECK)
                    {
                        //Change up the shader for the glow
                        shader_DeckGlow.CurrentTechnique.Passes[0].Apply();
                        Drawing.DrawHandler.drawUI_GlowCardPile(_spriteBatch, (UserInterface.Clickables.DeckOfCards)_currentHover, _champ);
                    }

                    //Set it back to the regular shader in case it was changed
                    shader_Regular.CurrentTechnique.Passes[0].Apply();
                    _drawHandler.drawInterface(_spriteBatch, _currentHover, _champ);
                }
            }
            else if (_gameState == gameState.dungeon || _gameState == gameState.happening && _previousGameState == gameState.dungeon)
            {
                _drawHandler.drawUI(_spriteBatch, _activeUI, _champ);
            }



            //Change the render target back to null
            _spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);



            //Drawing render target to screen
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_renderTarget, _windowControl.getScreenRect(), Color.White);
            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
