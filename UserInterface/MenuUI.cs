using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface
{
    public abstract class MenuUI
    {
        protected List<UserInterface> _wholeUI;
        public int _x, _y, _width, _height;
        public Texture2D _backgroundTexture;
        protected Color _backgroundColor, _titleColor, _titleShadowColor;
        protected String _titleString;
        protected SpriteFont _titleFont;
        public int _titleX, _titleY, _titleFontHeight;
        protected bool _scrollable;
        public int _scrollY;

        public MenuUI(int x, int y, int width, int height, Texture2D backgroundTexture, Color backgroundColor,
            String titleString, int titleX, int titleY, SpriteFont titleFont, int titleFontHeight, Color titleColor, Color titleShadowColor)
        {
            _wholeUI = new List<UserInterface>();

            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _backgroundTexture = backgroundTexture;
            _backgroundColor = backgroundColor;
            _titleString = titleString;
            _titleX = titleX;
            _titleY = titleY;
            _titleFont = titleFont;
            _titleFontHeight = titleFontHeight;
            _titleColor = titleColor;
            _titleShadowColor = titleShadowColor;

            _scrollable = false;
            _scrollY = 0;
        }

        //Getters
        public List<UserInterface> getUI()
        {
            return _wholeUI;
        }
        public Color getBackgroundColor()
        {
            return _backgroundColor;
        }
        public String getTitle()
        {
            return _titleString;
        }
        public SpriteFont getTitleFont()
        {
            return _titleFont;
        }
        public Color getTitleColor()
        {
            return _titleColor;
        }
        public Color getTitleShadowColor()
        {
            return _titleShadowColor;
        }
        public bool isScrollable()
        {
            return _scrollable;
        }



        /// <summary>
        /// Will return whether or not the top bar should be present when this menu is active (in most cases it should be)
        /// </summary>
        public abstract bool addTopBar();

        public abstract void updateUI();
    }
}
