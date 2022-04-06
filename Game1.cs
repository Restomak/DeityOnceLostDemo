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
        //Framework variables
        public static Game1 currentGame;
        public SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;
        Input.WindowControl windowControl;
        RenderTarget2D renderTarget;
        private static bool _gameInitialized = false; //first loop through Update will initialize the game then set to true
        private static Random rand = new Random();

        //Game structure variables
        private static Draw.DrawHandler _drawHandler;
        private static DeckBuilder.CardCollection _cardCollection_All;
        private static Characters.Hero _hero; //Will probably be initialized elsewhere later post demo
        private static Characters.Champion _champ; //Will probably be initialized elsewhere later post demo
        bool test1 = false, test2 = false, test3 = false, test4 = false, test5 = false, test6 = false; //initial demo variables

        //Logs
        public static List<String> errorLog;
        public static List<String> debugLog;
        

        //Framework constants
        public const double VIRTUAL_SCREEN_RATIO_X = 16;
        public const double VIRTUAL_SCREEN_RATIO_Y = 9;
        public const int VIRTUAL_WINDOW_WIDTH = (int)VIRTUAL_SCREEN_RATIO_X * 100;
        public const int VIRTUAL_WINDOW_HEIGHT = (int)VIRTUAL_SCREEN_RATIO_Y * 100;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;
            currentGame = this;

            windowControl = new Input.WindowControl();
        }

        //Framework getters
        public GraphicsDeviceManager getGraphics()
        {
            return graphics;
        }
        public SpriteBatch getSpriteBatch()
        {
            return spriteBatch;
        }
        public GameWindow getWindow()
        {
            return Window;
        }
        public static Game1 getGame()
        {
            return currentGame;
        }
        


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            windowControl.Initialize();
            renderTarget = new RenderTarget2D(GraphicsDevice, VIRTUAL_WINDOW_WIDTH, VIRTUAL_WINDOW_HEIGHT, false, GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //functionality art
            pic_functionality_uiSketch = Content.Load<Texture2D>("functionality art/UI Sketch");
            pic_functionality_endTurnButton = Content.Load<Texture2D>("functionality art/End Turn Button");
            pic_functionality_cardDown = Content.Load<Texture2D>("functionality art/Card Down");

            //fonts
            roboto_regular_8 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-8");
            roboto_medium_8 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-8");
            roboto_bold_8 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-8");
            roboto_black_8 = Content.Load<SpriteFont>("Fonts/Roboto-Black-8");
            roboto_regular_10 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-10");
            roboto_medium_10 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-10");
            roboto_bold_10 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-10");
            roboto_black_10 = Content.Load<SpriteFont>("Fonts/Roboto-Black-10");
            roboto_regular_12 = Content.Load<SpriteFont>("Fonts/Roboto-Regular-12");
            roboto_medium_12 = Content.Load<SpriteFont>("Fonts/Roboto-Medium-12");
            roboto_bold_12 = Content.Load<SpriteFont>("Fonts/Roboto-Bold-12");
            roboto_black_12 = Content.Load<SpriteFont>("Fonts/Roboto-Black-12");
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
        }

        /*____________________.Content Variables._____________________*/
        //Functionality Art
        public static Texture2D pic_functionality_uiSketch, pic_functionality_endTurnButton, pic_functionality_cardDown;

        //Fonts
        public static SpriteFont roboto_regular_8, roboto_medium_8, roboto_bold_8, roboto_black_8,
            roboto_regular_10, roboto_medium_10, roboto_bold_10, roboto_black_10,
            roboto_regular_12, roboto_medium_12, roboto_bold_12, roboto_black_12,
            roboto_regular_24, roboto_medium_24, roboto_bold_24, roboto_black_24;

        //Cards
        public static Texture2D pic_card_front_default, pic_card_front_common, pic_card_front_rare,
            pic_card_front_epic, pic_card_front_godly, pic_card_front_void;

        //Enemies
        public static Texture2D pic_enemy_fanbladeGuard;



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

            //Framework stuff
            _drawHandler = new Draw.DrawHandler();

            //Game stuff
            _cardCollection_All = new DeckBuilder.CardCollection();
            Characters.Names.initializeNameList();

            //Temporary testing stuff
            _hero = new Characters.Hero();
            _champ = new Characters.Champion(_hero);
            _champ.getDeck().start();


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
            windowControl.setWindowPosX(Window.Position.X);
            windowControl.setWindowPosY(Window.Position.Y);
            
            //Make sure game is initialized
            if (!_gameInitialized)
            {
                InitializeGame();
            }



            //Input
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            /*____________________.Temporary testing input._____________________*/
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !test1)
            {
                test1 = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && !test2)
            {
                test2 = true;
                _champ.getDeck().drawNumCards(DeckBuilder.Deck.DEFAULT_DRAW_ON_TURN_START);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.B) && !test3)
            {
                test3 = true;
                _champ.getDeck().turnEndDiscardAll();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.C) && !test4)
            {
                test4 = true;
                _champ.getDeck().drawNumCards(DeckBuilder.Deck.DEFAULT_DRAW_ON_TURN_START);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && !test5)
            {
                test5 = true;
                test6 = false;
                _champ.getDeck().turnEndDiscardAll();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E) && !test6)
            {
                test6 = true;
                test5 = false;
                _champ.getDeck().drawNumCards(DeckBuilder.Deck.DEFAULT_DRAW_ON_TURN_START);
            }



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
            GraphicsDevice.SetRenderTarget(renderTarget); //draw to texture first so screen resizing doesn't require resolution changes
            GraphicsDevice.Clear(new Color(36, 0, 72)); //dark indigo
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);



            //Draw logic goes here
            if (!test1) //demo stuff, will be removed later
            {
                _drawHandler.drawCombat_Background(spriteBatch);
            }
            else
            {
                _drawHandler.drawCombat_UI(spriteBatch, _champ);
            }



            //Change the render target back to null
            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);



            //Drawing render target to screen
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(renderTarget, windowControl.getScreenRect(), Color.White);
            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
