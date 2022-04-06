using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Input
{
    class WindowControl
    {
        public const int DEFAULT_SCREENSIZE_X = Game1.VIRTUAL_WINDOW_WIDTH;
        public const int DEFAULT_SCREENSIZE_Y = Game1.VIRTUAL_WINDOW_HEIGHT;

        int _screenSize_X, _screenSize_Y, _windowPos_X, _windowPos_Y;

        //Black bars for when the window is stretched too wide or too tall
        bool _blackBars_X, _blackBars_Y;
        double _blackBars_Width, _blackBars_Height;

        public WindowControl()
        {
            _screenSize_X = DEFAULT_SCREENSIZE_X;
            _screenSize_Y = DEFAULT_SCREENSIZE_Y;
            _blackBars_X = false;
            _blackBars_Y = false;
            _blackBars_Width = 0;
            _blackBars_Height = 0;
        }
        
        //Setters
        public void setWindowPosX(int pos)
        {
            _windowPos_X = pos;
        }
        public void setWindowPosY(int pos)
        {
            _windowPos_Y = pos;
        }

        /// <summary>
        /// Initialize the game window. Normally done in Game1 but I want that class less cluttered
        /// </summary>
        public void Initialize()
        {
            GraphicsDeviceManager graphics = Game1.getGame().getGraphics();
            GameWindow window = Game1.getGame().getWindow();

            graphics.IsFullScreen = false;
            graphics.HardwareModeSwitch = false;
            graphics.PreferredBackBufferWidth = _screenSize_X;
            graphics.PreferredBackBufferHeight = _screenSize_Y;
            window.AllowUserResizing = true;
            window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);

            if (_windowPos_X == 0 && _windowPos_Y == 0)
            {
                /*window.Position = new Microsoft.Xna.Framework.Point(
                    (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - _screenSize_X) / 2 - 8,
                    (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - _screenSize_Y) / 2 - 16);*/
            }
            else
            {
                window.Position = new Microsoft.Xna.Framework.Point(_windowPos_X, _windowPos_Y);
            }
            
            AdjustGraphics();
        }

        /// <summary>
        /// Automatically called when the window gets resized
        /// </summary>
        public void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            GraphicsDeviceManager graphics = Game1.getGame().getGraphics();
            GameWindow window = Game1.getGame().getWindow();

            graphics.PreferredBackBufferWidth = window.ClientBounds.Width;
            graphics.PreferredBackBufferHeight = window.ClientBounds.Height;
            _screenSize_X = window.ClientBounds.Width;
            _screenSize_Y = window.ClientBounds.Height;

            ResizeWindow();
        }

        /// <summary>
        /// Used to adjust the window manually when graphics options get changed
        /// </summary>
        public void AdjustGraphics()
        {
            GraphicsDeviceManager graphics = Game1.getGame().getGraphics();
            
            //FIXIT add graphics options later, this is where things change after they've been adjusted



            graphics.ApplyChanges();
            ResizeWindow();
        }

        /// <summary>
        /// Reposition the virtual window & size the black bars in case the screen is stretched too wide or too tall
        /// </summary>
        public void ResizeWindow()
        {
            GraphicsDeviceManager graphics = Game1.getGame().getGraphics();

            _blackBars_X = false;
            _blackBars_Y = false;
            _blackBars_Width = 0;
            _blackBars_Height = 0;
            if ((int)((double)_screenSize_X / Game1.VIRTUAL_SCREEN_RATIO_X) != (int)((double)_screenSize_Y / Game1.VIRTUAL_SCREEN_RATIO_Y))
            {
                if ((int)((double)_screenSize_X / Game1.VIRTUAL_SCREEN_RATIO_X) > (int)((double)_screenSize_Y / Game1.VIRTUAL_SCREEN_RATIO_Y))
                {
                    _screenSize_X = (int)(Game1.VIRTUAL_SCREEN_RATIO_X * (double)_screenSize_Y / Game1.VIRTUAL_SCREEN_RATIO_Y);
                    _blackBars_X = true;
                    _blackBars_Width = (graphics.PreferredBackBufferWidth - _screenSize_X) / 2.0;
                }
                else
                {
                    _screenSize_Y = (int)(Game1.VIRTUAL_SCREEN_RATIO_Y * (double)_screenSize_X / Game1.VIRTUAL_SCREEN_RATIO_X);
                    _blackBars_Y = true;
                    _blackBars_Height = (graphics.PreferredBackBufferHeight - _screenSize_Y) / 2.0;
                }
            }
        }

        /// <summary>
        /// Returns the rectangle for where the virtual window will be drawn on the real window
        /// </summary>
        public Rectangle getScreenRect()
        {
            return new Rectangle((int)_blackBars_Width, (int)_blackBars_Height, _screenSize_X, _screenSize_Y);
        }
    }
}
