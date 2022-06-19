using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Menus
{
    public abstract class CardMenu : MenuUI
    {
        protected enum setupParameter
        {
            x,
            y,
            width,
            height
        }

        protected List<DeckBuilder.Card> _cards;
        protected UserInterface _cardsAsClickables;

        public CardMenu(List<DeckBuilder.Card> cards, String title) : base(Drawing.DrawConstants.CARDSELECTIONMENU_X, Drawing.DrawConstants.CARDSELECTIONMENU_Y,
            Drawing.DrawConstants.CARDSELECTIONMENU_WIDTH, 0, Game1.pic_functionality_bar, Color.DarkSlateGray * Drawing.DrawConstants.CARDCHOICE_BACKGROUND_FADE, title,
            Game1.VIRTUAL_WINDOW_WIDTH / 2 - (int)(Game1.roboto_black_24.MeasureString(title).X / 2), 0, Game1.roboto_black_24, Drawing.DrawConstants.TEXT_24_HEIGHT,
            Color.Gold, Color.Black)  //no height nor titleY are given because they get calculated later
        {
            _cards = cards;

            _cardsAsClickables = new UserInterface();
            _wholeUI.Add(_cardsAsClickables);
        }

        public override bool addTopBar() { return true; }



        protected void setupClickables(int totalCardCount)
        {
            if (totalCardCount <= Drawing.DrawConstants.CARDSELECTIONMENU_MAX_CARDS_PER_ROW)
            {
                _scrollable = false;
                _scrollY = 0;

                _height = Drawing.DrawConstants.CARDSELECTIONMENU_Y_BUFFER * 3 + Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_HEIGHT +
                    Drawing.DrawConstants.CARDSELECTIONMENU_CARD_HEIGHT + Drawing.DrawConstants.CARDSELECTIONMENU_TITLE_Y_FROM_TOP + _titleFontHeight;
                if (_height < Drawing.DrawConstants.CARDSELECTIONMENU_MIN_HEIGHT)
                {
                    _height = Drawing.DrawConstants.CARDSELECTIONMENU_MIN_HEIGHT;
                }
                _y = (Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT) / 2 - _height / 2 - _scrollY; //Center on the screen with the top bar in mind //FIXIT more things need to center with it in mind
                _titleY = _y + _height - Drawing.DrawConstants.CARDSELECTIONMENU_TITLE_Y_FROM_TOP - _titleFontHeight;
            }
            else
            {
                int numCardRows = (int)Math.Ceiling((double)totalCardCount / 5.0);

                if (numCardRows > 2)
                {
                    _scrollable = true;

                    if (_scrollY > 0)
                    {
                        _scrollY = 0;
                    }
                    else if (_scrollY < 0 - _height + Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT)
                    {
                        _scrollY = 0 - _height + Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT;
                    }
                }

                _height = Drawing.DrawConstants.CARDSELECTIONMENU_Y_BUFFER * (2 + numCardRows) + Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_HEIGHT +
                    Drawing.DrawConstants.CARDSELECTIONMENU_CARD_HEIGHT * numCardRows + Drawing.DrawConstants.CARDSELECTIONMENU_TITLE_Y_FROM_TOP + _titleFontHeight;
                if (_height < Drawing.DrawConstants.CARDSELECTIONMENU_MIN_HEIGHT)
                {
                    _height = Drawing.DrawConstants.CARDSELECTIONMENU_MIN_HEIGHT;
                }
                
                if (numCardRows > 2)
                {
                    _y = 0 - _height + Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT - _scrollY; //Not centered because it's too big for the screen
                    _titleY = _y + _height - Drawing.DrawConstants.CARDSELECTIONMENU_TITLE_Y_FROM_TOP - _titleFontHeight;
                }
                else
                {
                    _y = (Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT) / 2 - _height / 2 - _scrollY; //Center on the screen with the top bar in mind
                    _titleY = _y + _height - Drawing.DrawConstants.CARDSELECTIONMENU_TITLE_Y_FROM_TOP - _titleFontHeight;
                }
            }
        }



        protected int setupClickableParamter(int totalCardCount, int cardIndex, setupParameter param)
        {
            if (totalCardCount <= Drawing.DrawConstants.CARDSELECTIONMENU_MAX_CARDS_PER_ROW)
            {
                switch (param)
                {
                    case setupParameter.y:
                        return (Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT) / 2 - Drawing.DrawConstants.CARDSELECTIONMENU_CARD_HEIGHT / 2 - _scrollY;
                    case setupParameter.width:
                        return Drawing.DrawConstants.CARDSELECTIONMENU_CARD_WIDTH;
                    case setupParameter.height:
                        return Drawing.DrawConstants.CARDSELECTIONMENU_CARD_HEIGHT;
                }

                //setupParameter.x:
                int startX = 0;
                switch (totalCardCount)
                {
                    case 1:
                        startX = Drawing.DrawConstants.CARDSELECTIONMENU_CARDS_START_X_1CARD;
                        break;
                    case 2:
                        startX = Drawing.DrawConstants.CARDSELECTIONMENU_CARDS_START_X_2CARDS;
                        break;
                    case 3:
                        startX = Drawing.DrawConstants.CARDSELECTIONMENU_CARDS_START_X_3CARDS;
                        break;
                    case 4:
                        startX = Drawing.DrawConstants.CARDSELECTIONMENU_CARDS_START_X_4CARDS;
                        break;
                    case 5:
                        startX = Drawing.DrawConstants.CARDSELECTIONMENU_CARDS_START_X_5CARDS;
                        break;
                }

                if (totalCardCount < 4)
                {
                    return startX + cardIndex * (Drawing.DrawConstants.CARDSELECTIONMENU_CARD_SPACE_BETWEEN_UNDER4 + Drawing.DrawConstants.CARDSELECTIONMENU_CARD_WIDTH);
                }

                return startX + cardIndex * (Drawing.DrawConstants.CARDSELECTIONMENU_CARD_SPACE_BETWEEN + Drawing.DrawConstants.CARDSELECTIONMENU_CARD_WIDTH);
            }
            else
            {
                int numCardRows = (int)Math.Ceiling((double)totalCardCount / 5.0);
                int cardRow = (int)Math.Floor((double)cardIndex / 5.0);

                switch (param)
                {
                    case setupParameter.y:
                        return _y + Drawing.DrawConstants.CARDSELECTIONMENU_Y_BUFFER * 2 + Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_HEIGHT +
                            (numCardRows - cardRow - 1) * (Drawing.DrawConstants.CARDSELECTIONMENU_CARD_HEIGHT + Drawing.DrawConstants.CARDSELECTIONMENU_Y_BUFFER);
                    case setupParameter.width:
                        return Drawing.DrawConstants.CARDSELECTIONMENU_CARD_WIDTH;
                    case setupParameter.height:
                        return Drawing.DrawConstants.CARDSELECTIONMENU_CARD_HEIGHT;
                }

                //setupParameter.x:
                return Drawing.DrawConstants.CARDSELECTIONMENU_CARDS_START_X_5CARDS +
                    (cardIndex - cardRow * 5) * (Drawing.DrawConstants.CARDSELECTIONMENU_CARD_SPACE_BETWEEN + Drawing.DrawConstants.CARDSELECTIONMENU_CARD_WIDTH);
            }
        }
    }
}
