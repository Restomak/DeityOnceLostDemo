using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Menus
{
    /// <summary>
    /// Base class for all card menus, including those that don't have a selection involved.
    /// Includes several functions for setting up the display of the cards on the screen, as
    /// it needs to be able to determine whether or not the menu can be scrolled (when more
    /// cards need to be displayed than can be done at once).
    /// </summary>
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
        UserInterface _scrollBar;

        public CardMenu(List<DeckBuilder.Card> cards, String title) : base(Drawing.DrawConstants.CARDSELECTIONMENU_X, Drawing.DrawConstants.CARDSELECTIONMENU_Y,
            Drawing.DrawConstants.CARDSELECTIONMENU_WIDTH, 0, Game1.pic_functionality_bar, Color.DarkSlateGray * Drawing.DrawConstants.CARDCHOICE_BACKGROUND_FADE, title,
            Game1.VIRTUAL_WINDOW_WIDTH / 2 - (int)(Game1.roboto_black_24.MeasureString(title).X / 2), 0, Game1.roboto_black_24, Drawing.DrawConstants.TEXT_24_HEIGHT,
            Color.Gold, Color.Black)  //no height nor titleY are given because they get calculated later
        {
            _cards = cards;

            _cardsAsClickables = new UserInterface();
            _scrollBar = new UserInterface();
            _wholeUI.Add(_cardsAsClickables);
            _wholeUI.Add(_scrollBar);
        }

        public override bool addTopBar() { return true; }



        /// <summary>
        /// Determines based on the number of cards to display whether or not to also set up
        /// the menu as scrollable (and how much it can scroll if so). Also handles the
        /// initialization of the scroll bar Clickables if needed.
        /// </summary>
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


                //Setup scroll bar - only do it if there's nothing held (if there is, it's this, and that's redundant)
                if (Game1.getHeldClickable() == null)
                {
                    _scrollBar.resetClickables();
                    if (_scrollable)
                    {
                        Clickables.SpecialButtons.ClickAndDrag scrollBarButton = null; //done this way so that I can use scrollBarButton in its own passed held action

                        //Calculate scrollBarButton's initial Y
                        int scrollBarButtonY = calculateScrollBarY();

                        scrollBarButton = new Clickables.SpecialButtons.ClickAndDrag(Game1.pic_functionality_bar,
                            new Point(Drawing.DrawConstants.MENU_SCROLLBAR_CARDMENUS_X + 1, scrollBarButtonY),
                            Drawing.DrawConstants.MENU_SCROLLBAR_BUTTON_WIDTH, Drawing.DrawConstants.MENU_SCROLLBAR_BUTTON_HEIGHT, () =>
                            {
                                scrollBarButton._y = Game1.getInputController().getMousePos().Y;
                                if (scrollBarButton._y > Drawing.DrawConstants.MENU_SCROLLBAR_Y + Drawing.DrawConstants.MENU_SCROLLBAR_HEIGHT - Drawing.DrawConstants.MENU_SCROLLBAR_BUTTON_HEIGHT)
                                {
                                    scrollBarButton._y = Drawing.DrawConstants.MENU_SCROLLBAR_Y + Drawing.DrawConstants.MENU_SCROLLBAR_HEIGHT - Drawing.DrawConstants.MENU_SCROLLBAR_BUTTON_HEIGHT;
                                }
                                else if (scrollBarButton._y < Drawing.DrawConstants.MENU_SCROLLBAR_Y)
                                {
                                    scrollBarButton._y = Drawing.DrawConstants.MENU_SCROLLBAR_Y;
                                }

                                //calculate scrollY based on button position
                                double percentOfMaxScroll = (double)(scrollBarButton._y - Drawing.DrawConstants.MENU_SCROLLBAR_Y) /
                                    (double)((Drawing.DrawConstants.MENU_SCROLLBAR_HEIGHT - Drawing.DrawConstants.MENU_SCROLLBAR_BUTTON_HEIGHT - Drawing.DrawConstants.MENU_SCROLLBAR_Y));

                                _scrollY = (-_height + Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT) -
                                    (int)((double)(-_height + Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT) * percentOfMaxScroll);

                                Game1.updateMenus();
                            });
                        _scrollBar.addClickableToFront(scrollBarButton);

                        Clickables.SpecialButtons.CannotHover scrollbar = new Clickables.SpecialButtons.CannotHover(Game1.pic_functionality_bar,
                            new Point(Drawing.DrawConstants.MENU_SCROLLBAR_CARDMENUS_X, Drawing.DrawConstants.MENU_SCROLLBAR_Y), Drawing.DrawConstants.MENU_SCROLLBAR_WIDTH,
                            Drawing.DrawConstants.MENU_SCROLLBAR_HEIGHT, () =>
                            {
                                //Move scroll bar button to location clicked on the bar
                                scrollBarButton.onHeld();
                                scrollBarButton.whileHeld();
                                Game1.updateMenus();
                            });
                        scrollbar.setColor(Color.Gray);
                        _scrollBar.addClickableToBack(scrollbar); //add it in behind so we know the first index is the button
                    }
                }
            }
        }

        private int calculateScrollBarY()
        {
            double percentOfMaxScroll = (double)_scrollY / (double)(-_height + Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT); //scrollY divided by its maximum
            return Drawing.DrawConstants.MENU_SCROLLBAR_Y + Drawing.DrawConstants.MENU_SCROLLBAR_HEIGHT - Drawing.DrawConstants.MENU_SCROLLBAR_BUTTON_HEIGHT -
                (int)((double)(Drawing.DrawConstants.MENU_SCROLLBAR_HEIGHT - Drawing.DrawConstants.MENU_SCROLLBAR_BUTTON_HEIGHT) * percentOfMaxScroll);
        }

        /// <summary>
        /// Update's the scroll bar button's location on the bar when scrollY is adjusted.
        /// </summary>
        public override void updateScrollBar()
        {
            _scrollBar.getClickables()[0]._y = calculateScrollBarY();
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
