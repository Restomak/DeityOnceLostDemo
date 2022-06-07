﻿using Microsoft.Xna.Framework;
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
        //Framework variables
        private static Game1 _currentGame;
        private static SpriteBatch _spriteBatch;
        private static GraphicsDeviceManager _graphics;
        private static RenderTarget2D _renderTarget;
        private static Random rand = new Random();
        private static Input.WindowControl _windowControl;
        private static Input.InputController _inputController;
        private static bool _gameInitialized = false; //first loop through Update will initialize the game then set to true

        //User Interface variables
        private static List<UserInterface.UserInterface> _activeUI;
        private static List<UserInterface.UserInterface> _battleUI;
        private static UserInterface.UserInterface _handCards;
        private static UserInterface.UserInterface _cardPiles;
        private static UserInterface.UserInterface _combatUIButtons;
        private static UserInterface.UserInterface _enemies;
        private static UserInterface.UserInterface _enemyHovers;
        private static UserInterface.Clickables.Avatar _championUI;
        private static UserInterface.UserInterface _championHovers;
        private static UserInterface.Clickable _currentHover;
        private static UserInterface.Clickables.HandCard _activeCard;
        private static UserInterface.UserInterface _targets;
        private static UserInterface.UserInterface _resources;

        //Game structure variables
        private static Drawing.DrawHandler _drawHandler;
        private static DeckBuilder.CardCollection _cardCollection_All;
        private static Combat.CombatHandler _combatHandler;
        private static Characters.Hero _hero; //Will probably be initialized elsewhere later post demo
        private static Characters.Champion _champ; //Will probably be initialized elsewhere later post demo
        bool test1 = false, test2 = false, test3 = false, test4 = false, test5 = false, test6 = false; //initial demo variables

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
            _battleUI = new List<UserInterface.UserInterface>();
            _handCards = new UserInterface.UserInterface();
            _cardPiles = new UserInterface.UserInterface();
            _combatUIButtons = new UserInterface.UserInterface();
            _enemies = new UserInterface.UserInterface();
            _enemyHovers = new UserInterface.UserInterface();
            _championUI = new UserInterface.Clickables.Avatar();
            _championHovers = new UserInterface.UserInterface();
            _targets = new UserInterface.UserInterface();
            _resources = new UserInterface.UserInterface();
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

        
        //User Interface setters
        public static void setHoveredClickable(UserInterface.Clickable clickable)
        {
            _currentHover = clickable;
        }
        public static void setActiveCard(UserInterface.Clickables.HandCard card)
        {
            _activeCard = card;

            _targets.resetClickables(); //do it here instead of in setupTargets because it needs to reset even when deseslecting

            //If selecting (and not unselecting), setup targets for that card
            if (_activeCard != null)
            {
                UserInterface.Clickables.Target.setupTargets(_targets, _enemies/*, _party*/, _championUI, card.getCard().getTargetType());
            }
        }
        public static void setEnemiesAsUI(UserInterface.UserInterface enemies)
        {
            _enemies = enemies;
        }
        public static void setTargets(UserInterface.UserInterface targets)
        {
            _targets = targets;
        }

        //User Interface getters
        public static UserInterface.Clickable getHoveredClickable()
        {
            return _currentHover;
        }
        public static UserInterface.Clickables.HandCard getActiveCard()
        {
            return _activeCard;
        }
        public static UserInterface.UserInterface getTargets()
        {
            return _targets;
        }

        //Other useful getters
        public static Characters.Champion getChamp()
        {
            return _champ;
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

            //added in sorted fashion, top to bottom is front of the screen to back
            _battleUI.Add(_targets);
            _battleUI.Add(_handCards);
            _battleUI.Add(_enemyHovers);
            _battleUI.Add(_enemies);
            _battleUI.Add(_championHovers);
            _championHovers.addClickableToFront(_championUI); //FIXIT temp until I add the party UserInterface
            _battleUI.Add(_cardPiles);
            _battleUI.Add(_combatUIButtons);
            _battleUI.Add(_resources);

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
        }

        /*____________________.Content Variables._____________________*/
        //Functionality Art
        public static Texture2D pic_functionality_uiSketch, pic_functionality_endTurnButton, pic_functionality_cardDown, pic_functionality_bar,
            pic_functionality_intent_AoE, pic_functionality_intent_Attack, pic_functionality_intent_Buff, pic_functionality_intent_Debuff, pic_functionality_intent_Defend,
            pic_functionality_defenseIcon, pic_functionality_championSilhouette,
            pic_functionality_targeting_faded_TL, pic_functionality_targeting_faded_TR, pic_functionality_targeting_faded_BR, pic_functionality_targeting_faded_BL,
            pic_functionality_targeting_back_TL, pic_functionality_targeting_back_TR, pic_functionality_targeting_back_BR, pic_functionality_targeting_back_BL;

        //Fonts
        public static SpriteFont roboto_regular_8, roboto_medium_8, roboto_bold_8, roboto_black_8,
            roboto_regular_10, roboto_medium_10, roboto_bold_10, roboto_black_10,
            roboto_regular_11, roboto_medium_11, roboto_bold_11, roboto_black_11,
            roboto_regular_12, roboto_medium_12, roboto_bold_12, roboto_black_12,
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

            //Structure stuff
            _drawHandler = new Drawing.DrawHandler();

            //Game stuff
            _cardCollection_All = new DeckBuilder.CardCollection();
            Characters.Names.initializeNameList();

            //UI stuff
            initializeCombatButtons();

            //Temporary testing stuff
            _hero = new Characters.Hero();
            _champ = new Characters.Champion(_hero);
            _combatHandler = new Combat.CombatHandler(_champ, null); //will have to adjust this later when doing it properly
            _combatHandler.setNewEncounter(new Combat.Encounters.SingleFanblade());
            _combatHandler.combatStart();
            _activeUI = _battleUI;

            _gameInitialized = true;
        }

        public static void initializeCombatButtons()
        {
            //End Turn button
            UserInterface.Clickables.Button endTurnButton = new UserInterface.Clickables.Button(pic_functionality_endTurnButton,
                new Point(VIRTUAL_WINDOW_WIDTH - Drawing.DrawConstants.COMBAT_ENDTURNBUTTON_X_FROMRIGHT - Drawing.DrawConstants.COMBAT_ENDTURNBUTTON_WIDTH, Drawing.DrawConstants.COMBAT_ENDTURNBUTTON_Y),
                Drawing.DrawConstants.COMBAT_ENDTURNBUTTON_WIDTH, Drawing.DrawConstants.COMBAT_ENDTURNBUTTON_HEIGHT, () =>
                {
                    //Make sure it's your turn
                    if (_combatHandler.getTurn() == Combat.CombatHandler.combatTurn.CHAMPION)
                    {
                        _champ.getDeck().turnEndDiscardAll();
                        _combatHandler.nextTurn();
                    }
                }, new List<String>() { "Ends your turn." });

            _combatUIButtons.addClickableToFront(endTurnButton);
        }
        
        public static void updateBattleUI()
        {
            UserInterface.Clickables.HandCard.setupHandUI(_handCards, _champ.getDeck().getHand());
            
            _cardPiles.resetClickables();
            _cardPiles.addClickableToFront(new UserInterface.Clickables.DeckOfCards(UserInterface.Clickables.DeckOfCards.typeOfDeck.REMOVEDPILE, _champ));
            _cardPiles.addClickableToFront(new UserInterface.Clickables.DeckOfCards(UserInterface.Clickables.DeckOfCards.typeOfDeck.DISCARDPILE, _champ));
            _cardPiles.addClickableToFront(new UserInterface.Clickables.DeckOfCards(UserInterface.Clickables.DeckOfCards.typeOfDeck.DRAWPILE, _champ));

            UserInterface.Clickables.Opponent.setupEnemyUI(_enemies, _combatHandler.getCurrentEncounter(), _enemyHovers);

            UserInterface.Clickables.Avatar.setupChampionUI(_championUI, _champ, _championHovers);
            _championHovers.addClickableToFront(_championUI); //FIXIT temp until I add the party UserInterface

            _resources.resetClickables();
            _resources.addClickableToBack(new UserInterface.Clickables.Hovers.Resource(new Point(Drawing.DrawConstants.COMBAT_DIVINITY_X, Drawing.DrawConstants.COMBAT_DIVINITY_Y),
                (int)roboto_bold_16.MeasureString(UserInterface.Clickables.Hovers.Resource.DIVINITY_SAMPLE_STRING).X, Drawing.DrawConstants.TEXT_16_HEIGHT, DeckBuilder.CardEnums.CostType.DIVINITY));
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
            _inputController.updateInput(_windowControl, _activeUI);

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
            _combatHandler.handleCombat();


            /*____________________.Temporary testing input._____________________*/
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !test1)
            {
                test1 = true;

                //_champ.getDeck().drawNumCards(DeckBuilder.Deck.DEFAULT_DRAW_ON_TURN_START);
                //updateBattleUI();
            }
            /*if (Keyboard.GetState().IsKeyDown(Keys.A) && !test2)
            {
                test2 = true;
                _champ.getDeck().drawNumCards(DeckBuilder.Deck.DEFAULT_DRAW_ON_TURN_START);
                updateBattleUI();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.B) && !test3)
            {
                test3 = true;
                _champ.getDeck().turnEndDiscardAll();
                updateBattleUI();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.C) && !test4)
            {
                test4 = true;
                _champ.getDeck().drawNumCards(DeckBuilder.Deck.DEFAULT_DRAW_ON_TURN_START);
                updateBattleUI();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && !test5)
            {
                test5 = true;
                test6 = false;
                _champ.getDeck().turnEndDiscardAll();
                updateBattleUI();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E) && !test6)
            {
                test6 = true;
                test5 = false;
                _champ.getDeck().drawNumCards(DeckBuilder.Deck.DEFAULT_DRAW_ON_TURN_START);
                updateBattleUI();
            }*/



            //Logic



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
            


            //Most draw logic goes here
            if (!test1) //demo stuff, will be removed later
            {
                _drawHandler.drawCombat_Background(_spriteBatch);
            }
            else
            {
                _drawHandler.drawUI(_spriteBatch, _activeUI, _champ);
            }

            if (_activeCard != null)
            {
                //Change up the shader for the glow
                shader_CardGlow.CurrentTechnique.Passes[0].Apply();
                _drawHandler.drawUI_LateActiveCard(_spriteBatch, _champ, true);

                //Set it back to the regular shader
                shader_Regular.CurrentTechnique.Passes[0].Apply();
                _drawHandler.drawUI_LateActiveCard(_spriteBatch, _champ, false);
            }
            if (_currentHover != null)
            {
                if (_currentHover.GetType() == typeof(UserInterface.Clickables.Button))
                {
                    //Doesn't glow using a shader atm
                    _drawHandler.drawUI_glowButton(_spriteBatch, (UserInterface.Clickables.Button)_currentHover);
                }
                else if (_currentHover.GetType() == typeof(UserInterface.Clickables.DeckOfCards) &&
                    ((UserInterface.Clickables.DeckOfCards)_currentHover).getDeckType() != UserInterface.Clickables.DeckOfCards.typeOfDeck.WHOLECOLLECTION &&
                    ((UserInterface.Clickables.DeckOfCards)_currentHover).getDeckType() != UserInterface.Clickables.DeckOfCards.typeOfDeck.DECK)
                {
                    //Change up the shader for the glow
                    shader_DeckGlow.CurrentTechnique.Passes[0].Apply();
                    _drawHandler.drawUI_GlowCardPile(_spriteBatch, (UserInterface.Clickables.DeckOfCards)_currentHover, _champ);
                }

                //Set it back to the regular shader in case it was changed
                shader_Regular.CurrentTechnique.Passes[0].Apply();
                _drawHandler.drawUI_LateHover(_spriteBatch, _champ);
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
