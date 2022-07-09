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

        private static bool _demoFinished = false;

        //User Interface variables
        private static List<UserInterface.UserInterface> _activeUI;
        private static List<UserInterface.UserInterface> _previousUI;
        private static UserInterface.Clickable _currentHover;
        private static UserInterface.Clickable _currentHeld;
        private static UserInterface.TopBarUI _topBar;
        private static List<UserInterface.MenuUI> _menus;

        //Game structure variables
        private static Drawing.DrawHandler _drawHandler;
        private static DeckBuilder.CardCollection _cardCollection_All;
        private static Combat.CombatHandler _combatHandler;
        private static Dungeon.DungeonHandler _dungeonHandler;
        private static Events.EventHandler _eventHandler;
        private static Characters.Champion _champ;
        private static List<Characters.Hero> _party;
        private static Treasury.Equipment.Inventory _inventory;

        //Logs
        public static List<String> errorLog;
        public static List<String> debugLog;
        public static bool showErrorLog = false, showDebugLog = false;
        public static void addToErrorLog(String log)
        {
            if (errorLog[errorLog.Count - 1] != log)
            {
                errorLog.Add(log);
            }
        }

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
        public static DeckBuilder.CardCollection getCardCollection()
        {
            return _cardCollection_All;
        }
        public static gameState getGameState()
        {
            return _gameState;
        }

        //User Interface getters
        public static UserInterface.Clickable getHoveredClickable()
        {
            return _currentHover;
        }
        public static UserInterface.Clickable getHeldClickable()
        {
            return _currentHeld;
        }
        public static UserInterface.Menus.InventoryMenu getInventoryMenuIfOnTop()
        {
            if (_menus.Count > 0 && _menus[_menus.Count - 1].GetType() == typeof(UserInterface.Menus.InventoryMenu))
            {
                return (UserInterface.Menus.InventoryMenu)_menus[_menus.Count - 1];
            }

            return null;
        }
        public static List<UserInterface.UserInterface> getActiveUI()
        {
            return _activeUI;
        }

        //Other useful getters
        public static Characters.Champion getChamp()
        {
            return _champ;
        }

        public static List<Characters.Hero> getPartyMembers()
        {
            return _party;
        }

        public static Point getPlayerLocationOnMap()
        {
            return _dungeonHandler.getPlayerLocation();
        }

        public static Treasury.Equipment.Inventory getInventory()
        {
            return _inventory;
        }

        

        //User Interface setters
        public static void setHoveredClickable(UserInterface.Clickable clickable)
        {
            _currentHover = clickable;
        }
        public static void setHeldClickable(UserInterface.Clickable clickable)
        {
            _currentHeld = clickable;
        }



        /// <summary>
        /// Called when the engine needs to switch over to combat, and sets up the gameState and UI
        /// in order to do so. Since most combats are triggered by room contents, the current room
        /// is passed so that the CombatHandler is able to tell it when combat is complete.
        /// </summary>
        public static void enterNewCombat(Combat.Encounter newEncounter, Dungeon.Room currentRoom)
        {
            if (newEncounter != null)
            {
                _gameState = gameState.combat;
                _combatHandler.setNewEncounter(newEncounter);
                _combatHandler.setCurrentRoom(currentRoom);
                _combatHandler.combatStart(_activeUI);
            }
        }

        /// <summary>
        /// Called when the engine needs to set up dungeon controls once again (after combat or an
        /// event, or when a new floor is reached). Sets up the gameState and UI in order to do so.
        /// </summary>
        public static void returnToDungeon()
        {
            if (!_demoFinished)
            {
                _gameState = gameState.dungeon;
                _dungeonHandler.returnToDungeon(_activeUI);
                _drawHandler.setEventTextBox(null);
            }
        }

        /// <summary>
        /// Called when the engine needs to prioritize an event. Sets up the gameState and UI in
        /// order to do so, and tells the DrawHandler what to display as the event text.
        /// </summary>
        public static void startEvent(Events.Happening newEvent, bool fromRoom = true)
        {
            if (_gameState != gameState.happening)
            {
                _previousGameState = _gameState;
                _previousUI = _activeUI;
            }
            _gameState = gameState.happening;
            _eventHandler.setupNewEvent(_activeUI, newEvent, fromRoom);
            _drawHandler.setEventTextBox(new Drawing.EventTextBox(newEvent.getWriting()));
        }
        /// <summary>
        /// Called by the DungeonHandler to set up an event from room contents.
        /// </summary>
        public static void startEvent(Dungeon.Room room)
        {
            startEvent(room.getRoomEvent());
            _eventHandler.setCurrentRoom(room);
        }

        /// <summary>
        /// Tells the engine when an event is complete so that the gameState and UI can switch
        /// back to what it was previously.
        /// </summary>
        public static void eventComplete()
        {
            _gameState = _previousGameState;
            _activeUI = _previousUI;
        }

        /// <summary>
        /// Called by the three main UIs (Combat, Event and Map) in order to ensure the top bar
        /// UI is added to them so that it's accessible by the player.
        /// </summary>
        public static void addTopBar()
        {
            _topBar.addToActiveUI(_activeUI);
        }

        /// <summary>
        /// Tells the TopBarUI that it needs to update.
        /// </summary>
        public static void updateTopBar()
        {
            _topBar.updateUI();
        }

        /// <summary>
        /// Used for setting up a new menu on the screen. Will not allow duplicates of menus, as
        /// that would be confusing to the player. The menu is placed on top of the stack so that
        /// it is at the forefront of the screen.
        /// </summary>
        public static void addToMenus(UserInterface.MenuUI newMenu)
        {
            bool duplicateFound = false;
            for (int i = 0; i < _menus.Count; i++)
            {
                //Multiple CardCollectionMenus are allowed since they might not be of the same thing (the only new menu they can open inside is deck from top bar, and that's already handled)
                if (_menus.GetType() != typeof(UserInterface.Menus.CardCollectionMenu) && _menus[i].GetType() == newMenu.GetType())
                {
                    duplicateFound = true;
                    break;
                }
            }

            if (!duplicateFound)
            {
                _menus.Add(newMenu);
                updateMenus();
            }
        }

        /// <summary>
        /// Closes the specified menu. Should usually be called by the menu itself.
        /// </summary>
        public static void closeMenu(UserInterface.MenuUI menu)
        {
            _currentHover = null;
            _currentHeld = null;
            _menus.Remove(menu);
            updateMenus();
        }

        /// <summary>
        /// Used for pressing a card pile or the deck button along the top bar. It is less
        /// confusing to the player if only one card menu can be opened at a time, so opening
        /// one closes all the others.
        /// </summary>
        public static void closeCardCollectionMenus()
        {
            for (int i = 0; i < _menus.Count; i++)
            {
                if (_menus[i].GetType() == typeof(UserInterface.Menus.CardCollectionMenu))
                {
                    closeMenu(_menus[i]);
                }
            }
        }

        /// <summary>
        /// Tells every menu to update their UI.
        /// </summary>
        public static void updateMenus()
        {
            for (int i = 0; i < _menus.Count; i++)
            {
                _menus[i].updateUI();
            }
        }

        /// <summary>
        /// Returns whether or not there are any menus on the stack.
        /// </summary>
        public static bool menuActive()
        {
            return (_menus.Count > 0);
        }

        /// <summary>
        /// Returns the top menu of the stack, if there are any; otherwise null.
        /// </summary>
        public static UserInterface.MenuUI getTopMenu()
        {
            if (menuActive())
            {
                return _menus[_menus.Count - 1];
            }

            return null;
        }

        /// <summary>
        /// This is meant as a story-based function, called only once in the entire game. It
        /// sets up the player's first champion and the top bar to display her. This occurs
        /// during the first event of the game.
        /// </summary>
        public static void setupRandomFirstChampion()
        {
            _champ = new Characters.Champion(new Characters.Hero(new Characters.PartyBuffs.Fighter(), true));
            _topBar.setupUI();
        }

        /// <summary>
        /// This is meant as a story-based function, called only once in the entire game. It
        /// sets up the first-ever party members the player receives. This occurs during an
        /// event on the second floor of the first/tutorial dungeon.
        /// </summary>
        public static void setupRandomFirstPartyMembers()
        {
            _party = new List<Characters.Hero>();

            _party.Add(new Characters.Hero(new Characters.PartyBuffs.Fighter()));
            _party.Add(new Characters.Hero(new Characters.PartyBuffs.Healer()));

            _combatHandler.setNewPartyMembers(_party);

            _inventory = new Treasury.Equipment.Inventory();

            _topBar.setupUI();

            _cardCollection_All.addCardsAfterParty();
        }

        /// <summary>
        /// Called when a new champion is chosen (either in town or during combat when the
        /// previous champion is slain. In the case of the latter, it tells the engine to
        /// splinter the champion's deck and to remove the party member that is replacing
        /// the champion.
        /// </summary>
        public static void setupNewChampion(Characters.Hero newChampion, bool died = false)
        {
            if (died)
            {
                List<DeckBuilder.Card> prevChampDeck = DeckBuilder.Deck.getSplinterDeck(_champ.getDeck().getDeck());

                _champ = new Characters.Champion(newChampion);
                _party.Remove(newChampion);
                _combatHandler.setNewPartyMembers(_party);

                for (int i = 0; i < prevChampDeck.Count; i++)
                {
                    _champ.getDeck().permanentlyAddToDeck(prevChampDeck[i]);
                }

                _combatHandler.combatResetTurn_ChampDied(_activeUI);
            }
            else
            {
                _champ = new Characters.Champion(newChampion);
            }

            _topBar.setupUI();
        }


        
        public static void testingSetupSkippedStuff()
        {
            setupRandomFirstChampion();
            setupRandomFirstPartyMembers();
            _dungeonHandler.addRelic(new Treasury.Treasures.Blessings.ScarletCloth());
            _dungeonHandler.addRelic(new Treasury.Treasures.Blessings.HolySymbol());
            _dungeonHandler.addRelic(new Treasury.Treasures.Blessings.Rest.FoxsCunning());
            _champ.getDeck().permanentlyAddToDeck(new DeckBuilder.Cards.EmpoweredCards.CommonCards.FirstStrike_Empowered());
            _champ.getDeck().permanentlyAddToDeck(new DeckBuilder.Cards.EmpoweredCards.CommonCards.FirstStrike_Empowered());
            _champ.getDeck().permanentlyAddToDeck(new DeckBuilder.Cards.EmpoweredCards.CommonCards.FirstStrike_Empowered());
            _champ.getDeck().permanentlyAddToDeck(new DeckBuilder.Cards.EmpoweredCards.CommonCards.FirstStrike_Empowered());
            _champ.getDeck().permanentlyAddToDeck(new DeckBuilder.Cards.EmpoweredCards.CommonCards.FirstStrike_Empowered());
            _champ.getDeck().permanentlyAddToDeck(new DeckBuilder.Cards.EmpoweredCards.CommonCards.FirstStrike_Empowered());
            _champ.getDeck().permanentlyAddToDeck(new DeckBuilder.Cards.EmpoweredCards.CommonCards.FirstStrike_Empowered());
            _champ.getDeck().permanentlyAddToDeck(new DeckBuilder.Cards.EmpoweredCards.CommonCards.FirstStrike_Empowered());
            _topBar.setupUI();
        }

        public static void endDemo()
        {
            _gameState = gameState.title;
            _demoFinished = true;
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
            _menus = new List<UserInterface.MenuUI>();

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
            pic_functionality_demoIntro = Content.Load<Texture2D>("functionality art/demo intro screen");
            pic_functionality_demoOutro = Content.Load<Texture2D>("functionality art/demo outro screen");
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
            pic_functionality_mapConnectorH = Content.Load<Texture2D>("functionality art/Map Connector H");
            pic_functionality_mapConnectorV = Content.Load<Texture2D>("functionality art/Map Connector V");
            pic_functionality_mapOpenConnectorH = Content.Load<Texture2D>("functionality art/Map Open Connector H");
            pic_functionality_mapOpenConnectorV = Content.Load<Texture2D>("functionality art/Map Open Connector V");
            pic_functionality_mapChampLoc = Content.Load<Texture2D>("functionality art/Map Champion Location");
            pic_functionality_mapStoryIcon = Content.Load<Texture2D>("functionality art/Map Story Icon");
            pic_functionality_mapCombatIcon = Content.Load<Texture2D>("functionality art/Map Combat Icon");
            pic_functionality_mapExitIcon = Content.Load<Texture2D>("functionality art/Map Exit Icon");
            pic_functionality_mapConnectorWindowH = Content.Load<Texture2D>("functionality art/Map Window Connector H");
            pic_functionality_mapConnectorWindowV = Content.Load<Texture2D>("functionality art/Map Window Connector V");
            pic_functionality_mapConnectorDoorH = Content.Load<Texture2D>("functionality art/Map Door Connector H");
            pic_functionality_mapConnectorDoorV = Content.Load<Texture2D>("functionality art/Map Door Connector V");
            pic_functionality_skipButton = Content.Load<Texture2D>("functionality art/Skip Button");
            pic_functionality_cardDivinityIcon = Content.Load<Texture2D>("functionality art/Card Divinity Icon");
            pic_functionality_cardBloodIcon = Content.Load<Texture2D>("functionality art/Card Blood Icon");
            pic_functionality_exitButton = Content.Load<Texture2D>("functionality art/Exit Button");
            pic_functionality_topBarDeckIcon = Content.Load<Texture2D>("functionality art/Top Bar Deck Icon");
            pic_functionality_mapTreasureIcon = Content.Load<Texture2D>("functionality art/Map Treasure Icon");
            pic_functionality_mapKeyIcon = Content.Load<Texture2D>("functionality art/Map Key Icon");
            pic_functionality_mapConnectorKeyH = Content.Load<Texture2D>("functionality art/Map Key Connector H");
            pic_functionality_mapConnectorKeyV = Content.Load<Texture2D>("functionality art/Map Key Connector V");
            pic_functionality_topBarInventoryIcon = Content.Load<Texture2D>("functionality art/Top Bar Inventory Icon");
            pic_functionality_combatInventoryIcon = Content.Load<Texture2D>("functionality art/Combat Inventory Icon");
            pic_functionality_confirmButton = Content.Load<Texture2D>("functionality art/Confirm Button");
            pic_functionality_cardBackLootIcon = Content.Load<Texture2D>("functionality art/Card Back Loot Icon");
            pic_functionality_mapMinibossIcon = Content.Load<Texture2D>("functionality art/Map Miniboss Icon");
            pic_functionality_goldLootIcon = Content.Load<Texture2D>("functionality art/Gold Loot Icon");
            pic_functionality_checkmark = Content.Load<Texture2D>("functionality art/checkmark");
            pic_functionality_empowerArrow = Content.Load<Texture2D>("functionality art/Empower Arrow");
            pic_functionality_handCardTargetDot = Content.Load<Texture2D>("functionality art/Circle");

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
            roboto_regular_15 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-15");
            roboto_medium_15 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-15");
            roboto_bold_15 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-15");
            roboto_black_15 = Content.Load<SpriteFont>("Fonts/Roboto-Black-15");
            roboto_regular_16 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-16");
            roboto_medium_16 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-16");
            roboto_bold_16 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-16");
            roboto_black_16 = Content.Load<SpriteFont>("Fonts/Roboto-Black-16");
            roboto_regular_18 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-18");
            roboto_medium_18 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-18");
            roboto_bold_18 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-18");
            roboto_black_18 = Content.Load<SpriteFont>("Fonts/Roboto-Black-18");
            roboto_regular_20 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-20");
            roboto_medium_20 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-20");
            roboto_bold_20 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-20");
            roboto_black_20 = Content.Load<SpriteFont>("Fonts/Roboto-Black-20");
            roboto_regular_21 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-21");
            roboto_medium_21 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-21");
            roboto_bold_21 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-21");
            roboto_black_21 = Content.Load<SpriteFont>("Fonts/Roboto-Black-21");
            roboto_regular_24 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-24");
            roboto_medium_24 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-24");
            roboto_bold_24 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-24");
            roboto_black_24 = Content.Load<SpriteFont>("Fonts/Roboto-Black-24");
            roboto_regular_28 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-28");
            roboto_medium_28 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-28");
            roboto_bold_28 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-28");
            roboto_black_28 = Content.Load<SpriteFont>("Fonts/Roboto-Black-28");

            //cards
            pic_card_front_default = Content.Load<Texture2D>("Cards/Card Default");
            pic_card_front_common = Content.Load<Texture2D>("Cards/Card Common");
            pic_card_front_rare = Content.Load<Texture2D>("Cards/Card Rare");
            pic_card_front_epic = Content.Load<Texture2D>("Cards/Card Epic");
            pic_card_front_godly = Content.Load<Texture2D>("Cards/Card Godly");
            pic_card_front_void = Content.Load<Texture2D>("Cards/Card Void");
            pic_card_art_blank = Content.Load<Texture2D>("Cards/Art/Blank");

            //enemies
            pic_enemy_fanbladeGuard = Content.Load<Texture2D>("Enemies/Fanblade Guard");
            pic_enemy_labTestSlime = Content.Load<Texture2D>("Enemies/Lab Test Slime");
            pic_enemy_crawler = Content.Load<Texture2D>("Enemies/Crawler");

            //items
            pic_item_key = Content.Load<Texture2D>("Items/Key");
            pic_item_wirecutters = Content.Load<Texture2D>("Items/Wirecutters");
            pic_item_trident = Content.Load<Texture2D>("Items/Trident");
            pic_item_deployableCover = Content.Load<Texture2D>("Items/Deployable Cover");
            pic_item_firewood = Content.Load<Texture2D>("Items/Firewood");
            pic_item_soulStone = Content.Load<Texture2D>("Items/Soul Stone");

            //relics
            pic_relic_boarsEndurance = Content.Load<Texture2D>("Relics/Boar's Endurance");
            pic_relic_bullsStrength = Content.Load<Texture2D>("Relics/Bull's Strength");
            pic_relic_catsGrace = Content.Load<Texture2D>("Relics/Cat's Grace");
            pic_relic_eaglesSplendor = Content.Load<Texture2D>("Relics/Eagle's Splendor");
            pic_relic_foxsCunning = Content.Load<Texture2D>("Relics/Fox's Cunning");
            pic_relic_owlsWisdom = Content.Load<Texture2D>("Relics/Owl's Wisdom");
            pic_relic_holySymbol = Content.Load<Texture2D>("Relics/Holy Symbol");
            pic_relic_scarletCloth = Content.Load<Texture2D>("Relics/Scarlet Cloth");
            pic_relic_weakened = Content.Load<Texture2D>("Relics/Weakened");

            //buffs
            pic_buff_feeble = Content.Load<Texture2D>("Buffs/Feeble");
            pic_buff_sluggish = Content.Load<Texture2D>("Buffs/Sluggish");
            pic_buff_vulnerable = Content.Load<Texture2D>("Buffs/Vulnerable");
            pic_buff_strength = Content.Load<Texture2D>("Buffs/Strength");
            pic_buff_dexterity = Content.Load<Texture2D>("Buffs/Dexterity");
            pic_buff_resilience = Content.Load<Texture2D>("Buffs/Resilience");

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
        public static Texture2D pic_functionality_demoIntro, pic_functionality_demoOutro, pic_functionality_endTurnButton, pic_functionality_cardDown, pic_functionality_bar,
            pic_functionality_intent_AoE, pic_functionality_intent_Attack, pic_functionality_intent_Buff, pic_functionality_intent_Debuff, pic_functionality_intent_Defend,
            pic_functionality_defenseIcon, pic_functionality_championSilhouette, pic_functionality_skipButton, pic_functionality_cardDivinityIcon, pic_functionality_cardBloodIcon,
            pic_functionality_targeting_faded_TL, pic_functionality_targeting_faded_TR, pic_functionality_targeting_faded_BR, pic_functionality_targeting_faded_BL,
            pic_functionality_targeting_back_TL, pic_functionality_targeting_back_TR, pic_functionality_targeting_back_BR, pic_functionality_targeting_back_BL,
            pic_functionality_mapRoom, pic_functionality_mapConnectorH, pic_functionality_mapConnectorV, pic_functionality_mapChampLoc, pic_functionality_mapStoryIcon,
            pic_functionality_mapCombatIcon, pic_functionality_mapExitIcon, pic_functionality_mapConnectorWindowH, pic_functionality_mapConnectorWindowV,
            pic_functionality_exitButton, pic_functionality_topBarDeckIcon, pic_functionality_mapConnectorDoorH, pic_functionality_mapConnectorDoorV,
            pic_functionality_mapOpenConnectorH, pic_functionality_mapOpenConnectorV, pic_functionality_mapTreasureIcon, pic_functionality_mapKeyIcon,
            pic_functionality_mapConnectorKeyH, pic_functionality_mapConnectorKeyV, pic_functionality_topBarInventoryIcon, pic_functionality_combatInventoryIcon,
            pic_functionality_confirmButton, pic_functionality_cardBackLootIcon, pic_functionality_mapMinibossIcon, pic_functionality_goldLootIcon,
            pic_functionality_checkmark, pic_functionality_empowerArrow, pic_functionality_handCardTargetDot;

        //Fonts
        public static SpriteFont roboto_regular_8, roboto_medium_8, roboto_bold_8, roboto_black_8,
            roboto_regular_10, roboto_medium_10, roboto_bold_10, roboto_black_10,
            roboto_regular_11, roboto_medium_11, roboto_bold_11, roboto_black_11,
            roboto_regular_12, roboto_medium_12, roboto_bold_12, roboto_black_12,
            roboto_regular_14, roboto_medium_14, roboto_bold_14, roboto_black_14,
            roboto_regular_15, roboto_medium_15, roboto_bold_15, roboto_black_15,
            roboto_regular_16, roboto_medium_16, roboto_bold_16, roboto_black_16,
            roboto_regular_18, roboto_medium_18, roboto_bold_18, roboto_black_18,
            roboto_regular_20, roboto_medium_20, roboto_bold_20, roboto_black_20,
            roboto_regular_21, roboto_medium_21, roboto_bold_21, roboto_black_21,
            roboto_regular_24, roboto_medium_24, roboto_bold_24, roboto_black_24,
            roboto_regular_28, roboto_medium_28, roboto_bold_28, roboto_black_28;

        //Cards
        public static Texture2D pic_card_front_default, pic_card_front_common, pic_card_front_rare,
            pic_card_front_epic, pic_card_front_godly, pic_card_front_void;
        public static Texture2D pic_card_art_blank;

        //Enemies
        public static Texture2D pic_enemy_fanbladeGuard, pic_enemy_labTestSlime, pic_enemy_crawler;

        //Items
        public static Texture2D pic_item_key, pic_item_wirecutters, pic_item_trident, pic_item_deployableCover, pic_item_firewood, pic_item_soulStone;

        //Relics
        public static Texture2D pic_relic_boarsEndurance, pic_relic_bullsStrength, pic_relic_catsGrace, pic_relic_eaglesSplendor, pic_relic_foxsCunning, pic_relic_owlsWisdom,
            pic_relic_holySymbol, pic_relic_scarletCloth, pic_relic_weakened;

        //Buffs
        public static Texture2D pic_buff_feeble, pic_buff_sluggish, pic_buff_vulnerable, pic_buff_strength, pic_buff_dexterity, pic_buff_resilience;

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
        /// Supply the chance of something happening, and the function will return whether or not it should.
        /// </summary>
        public static bool randChance(double chance)
        {
            return (rand.NextDouble() <= chance);
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
            Treasury.Treasures.RestBlessing.setupRestBlessings();
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
            List<UserInterface.UserInterface> inputUI = _activeUI;
            if (_menus.Count > 0) //make sure the active UI is the most recent menu, if one exists
            {
                inputUI = _menus[_menus.Count - 1].getUI();

                if (_menus[_menus.Count - 1].addTopBar())
                {
                    _topBar.addToActiveUI(inputUI);
                }
            }

            _inputController.updateInput(_windowControl, inputUI, _gameState);



            //Dev exit
            if (Keyboard.GetState().IsKeyDown(Keys.OemTilde) && Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }



            //Game logs display input
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
            if (_menus.Count == 0)
            {
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
            }


            /*____________________.Temporary testing input._____________________*/
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && _gameState == gameState.title && !_demoFinished)
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
                Drawing.DrawHandler.drawTitle_Background(_spriteBatch, _demoFinished);
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


            //Draw UIs
            if (_gameState == gameState.title)
            {

            }
            else if (_gameState == gameState.combat || _gameState == gameState.happening && _previousGameState == gameState.combat)
            {
                _drawHandler.drawUI(_spriteBatch, _activeUI, _champ);

                if (_currentHover != null)
                {
                    if (_currentHover.GetType() == typeof(UserInterface.Clickables.Button))
                    {
                        //Doesn't glow using a shader atm
                        Drawing.DrawHandler.drawUI_glowButton(_spriteBatch, (UserInterface.Clickables.Button)_currentHover);
                    }
                    else if (_currentHover.GetType() == typeof(UserInterface.Clickables.DeckOfCards) &&
                        ((UserInterface.Clickables.DeckOfCards)_currentHover).getDeckType() != UserInterface.Clickables.DeckOfCards.typeOfDeck.WHOLECOLLECTION)
                    {
                        //Change up the shader for the glow
                        shader_DeckGlow.CurrentTechnique.Passes[0].Apply();
                        Drawing.DrawHandler.drawUI_GlowCardPile(_spriteBatch, (UserInterface.Clickables.DeckOfCards)_currentHover, _champ);
                    }
                    else if (_currentHover.GetType() == typeof(UserInterface.Clickables.CardChoice))
                    {
                        //Change up the shader for the glow
                        shader_CardGlow.CurrentTechnique.Passes[0].Apply();
                        Drawing.DrawHandler.drawCardChoice(_spriteBatch, (UserInterface.Clickables.CardChoice)_currentHover, _champ, true);
                    }

                    //Set it back to the regular shader in case it was changed
                    shader_Regular.CurrentTechnique.Passes[0].Apply();
                    _drawHandler.drawInterface(_spriteBatch, _currentHover, _champ);
                }

                if (_combatHandler.getCombatUI().getActiveCard() != null)
                {
                    //Draw targeting reticle if the card is held
                    if (_currentHeld != null)
                    {
                        Drawing.DrawHandler.drawCombat_TargetingReticle(_spriteBatch, _combatHandler.getCombatUI().getActiveCard(), _inputController.getMousePos());
                    }

                    //Change up the shader for the glow
                    shader_CardGlow.CurrentTechnique.Passes[0].Apply();
                    Drawing.DrawHandler.drawCombat_HandCard(_spriteBatch, _combatHandler.getCombatUI().getActiveCard(), _champ, true);

                    //Set it back to the regular shader
                    shader_Regular.CurrentTechnique.Passes[0].Apply();
                    Drawing.DrawHandler.drawCombat_HandCard(_spriteBatch, _combatHandler.getCombatUI().getActiveCard(), _champ, false);
                }
            }
            else if (_gameState == gameState.dungeon || _gameState == gameState.happening && _previousGameState == gameState.dungeon)
            {
                _drawHandler.drawUI(_spriteBatch, _activeUI, _champ);

                if (_currentHover != null)
                {
                    if (_currentHover.GetType() == typeof(UserInterface.Clickables.DeckOfCards) &&
                        ((UserInterface.Clickables.DeckOfCards)_currentHover).getDeckType() == UserInterface.Clickables.DeckOfCards.typeOfDeck.DECK)
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



            //Draw menus on top
            for (int i = 0; i < _menus.Count; i++)
            {
                Drawing.DrawHandler.drawMenuBackground(_spriteBatch, _menus[i]);
                _drawHandler.drawUI(_spriteBatch, _menus[i].getUI(), _champ);
            }

            if (_menus.Count > 0)
            {
                //Redraw top bar in case a large (scrollable) menu was over the top bar, which is technically on top of the rest of the UI where input is concerned
                _drawHandler.drawUI(_spriteBatch, _topBar.getUIForLateDraw(), _champ);

                //Draw card choice menu clicked cards (top menu only)
                List<UserInterface.Clickables.CardChoice> selectedChoices = null;
                if (_menus[_menus.Count - 1].GetType() == typeof(UserInterface.Menus.NewCardChoiceMenu) &&
                    ((UserInterface.Menus.NewCardChoiceMenu)_menus[_menus.Count - 1]).multipleChoice())
                {
                    selectedChoices = ((UserInterface.Menus.NewCardChoiceMenu)_menus[_menus.Count - 1]).getClickedChoices();
                }
                else if (_menus[_menus.Count - 1].GetType() == typeof(UserInterface.Menus.CombatCardChoiceMenu) &&
                    ((UserInterface.Menus.CombatCardChoiceMenu)_menus[_menus.Count - 1]).multipleChoice())
                {
                    selectedChoices = ((UserInterface.Menus.CombatCardChoiceMenu)_menus[_menus.Count - 1]).getClickedChoices();
                }
                if (selectedChoices != null)
                {
                    //Change up the shader for the glow
                    shader_CardGlow.CurrentTechnique.Passes[0].Apply();
                    for (int i = 0; i < selectedChoices.Count; i++)
                    {
                        Drawing.DrawHandler.drawCardChoice(_spriteBatch, selectedChoices[i], _champ, true);
                    }

                    //Set it back to the regular shader
                    shader_Regular.CurrentTechnique.Passes[0].Apply();
                    for (int i = 0; i < selectedChoices.Count; i++)
                    {
                        Drawing.DrawHandler.drawCardChoice(_spriteBatch, selectedChoices[i], _champ);
                    }
                }

                //Draw menu current hover last
                if (_currentHover != null)
                {
                    if (_currentHover.GetType() == typeof(UserInterface.Clickables.Button))
                    {
                        //Doesn't glow using a shader atm
                        Drawing.DrawHandler.drawUI_glowButton(_spriteBatch, (UserInterface.Clickables.Button)_currentHover);
                    }
                    else if (_currentHover.GetType() == typeof(UserInterface.Clickables.CardChoice))
                    {
                        //Change up the shader for the glow
                        shader_CardGlow.CurrentTechnique.Passes[0].Apply();
                        Drawing.DrawHandler.drawCardChoice(_spriteBatch, (UserInterface.Clickables.CardChoice)_currentHover, _champ, true);
                    }
                    else if (_currentHover.GetType() == typeof(UserInterface.Clickables.MenuCard) ||
                        _currentHover.GetType() == typeof(UserInterface.Clickables.MenuCard_ForUpgrade))
                    {
                        //Change up the shader for the glow
                        shader_CardGlow.CurrentTechnique.Passes[0].Apply();
                        Drawing.DrawHandler.drawMenuCard(_spriteBatch, (UserInterface.Clickables.MenuCard)_currentHover, _champ, true);
                    }
                    else if (_currentHover.GetType() == typeof(UserInterface.Clickables.DeckOfCards) &&
                        ((UserInterface.Clickables.DeckOfCards)_currentHover).getDeckType() == UserInterface.Clickables.DeckOfCards.typeOfDeck.DECK)
                    {
                        //Change up the shader for the glow
                        shader_DeckGlow.CurrentTechnique.Passes[0].Apply();
                        Drawing.DrawHandler.drawUI_GlowCardPile(_spriteBatch, (UserInterface.Clickables.DeckOfCards)_currentHover, _champ);
                    }
                    else if (_currentHover.GetType() == typeof(UserInterface.Clickables.InventoryItem))
                    {
                        //Change up the shader for the glow
                        shader_CardGlow.CurrentTechnique.Passes[0].Apply();
                        Drawing.DrawHandler.drawGlowInventoryItem(_spriteBatch, (UserInterface.Clickables.InventoryItem)_currentHover);
                    }

                    //Set it back to the regular shader in case it was changed
                    shader_Regular.CurrentTechnique.Passes[0].Apply();

                    //Draw it on top so it draws normally over any glow
                    _drawHandler.drawInterface(_spriteBatch, _currentHover, _champ);
                }
            }

            //Draw inventory held item if there is one
            UserInterface.Menus.InventoryMenu inventoryOnTop = getInventoryMenuIfOnTop();
            if (inventoryOnTop != null && inventoryOnTop.getHeldItem() != null)
            {
                Drawing.DrawHandler.drawInventoryItem(_spriteBatch, inventoryOnTop.getHeldItem());
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
