using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Menus
{
    public class NewCardChoiceMenu : MenuUI
    {
        List<DeckBuilder.Card> _choices;
        Treasury.Treasures.AddCardToDeck _addCardToDeck;
        UserInterface _choicesAsClickables;

        public NewCardChoiceMenu(List<DeckBuilder.Card> choices, Treasury.Treasures.AddCardToDeck addCardToDeck) : base(Drawing.DrawConstants.CARDSELECTIONMENU_X, Drawing.DrawConstants.CARDSELECTIONMENU_Y,
            Drawing.DrawConstants.CARDSELECTIONMENU_WIDTH, 0, Game1.pic_functionality_bar, Color.DarkSlateGray * Drawing.DrawConstants.CARDCHOICE_BACKGROUND_FADE, "Choose a Card",
            Game1.VIRTUAL_WINDOW_WIDTH / 2 - (int)(Game1.roboto_black_24.MeasureString("Choose a Card").X / 2), 0, Game1.roboto_black_24, Drawing.DrawConstants.TEXT_24_HEIGHT,
            Color.Gold, Color.Black)  //no height nor titleY are given because they get calculated later
        {
            _choices = choices;
            _addCardToDeck = addCardToDeck;

            _choicesAsClickables = new UserInterface();
            _wholeUI.Add(_choicesAsClickables);
        }

        public override bool addTopBar() { return true; }

        public override void updateUI()
        {
            setupChoicesAsClickables();
        }

        public void chooseCard(Clickables.CardChoice chosenCard)
        {
            if (chosenCard != null)
            {
                Game1.debugLog.Add("Adding " + chosenCard.getCard().getName() + " to deck.");

                Game1.getChamp().getDeck().permanentlyAddToDeck(chosenCard.getCard());
                _addCardToDeck.setTaken();
            }

            Game1.closeMenu(this);
        }



        public void setupChoicesAsClickables()
        {
            _choicesAsClickables.resetClickables();

            if (_choices.Count <= 5)
            {
                _height = Drawing.DrawConstants.CARDSELECTIONMENU_Y_BUFFER * 3 + Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_HEIGHT + Drawing.DrawConstants.CARDSELECTIONMENU_CARD_HEIGHT +
                    Drawing.DrawConstants.CARDSELECTIONMENU_TITLE_Y_FROM_TOP + _titleFontHeight;
                if (_height < Drawing.DrawConstants.CARDSELECTIONMENU_MIN_HEIGHT)
                {
                    _height = Drawing.DrawConstants.CARDSELECTIONMENU_MIN_HEIGHT;
                }
                _y = (Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT) / 2 - _height / 2; //Center on the screen with the top bar in mind //FIXIT more things need to center with it in mind
                _titleY = _y + _height - Drawing.DrawConstants.CARDSELECTIONMENU_TITLE_Y_FROM_TOP - _titleFontHeight;

                int startX = 0;
                switch (_choices.Count)
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

                for (int i = 0; i < _choices.Count; i++)
                {
                    Clickables.CardChoice cardChoice = new Clickables.CardChoice(_choices[i], this);

                    cardChoice._x = startX + i * (Drawing.DrawConstants.CARDSELECTIONMENU_CARD_SPACE_BETWEEN + Drawing.DrawConstants.CARDSELECTIONMENU_CARD_WIDTH);
                    //cardChoice._y = _y + Drawing.DrawConstants.CARDSELECTIONMENU_Y_BUFFER * 2 + Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_HEIGHT;
                    cardChoice._y = (Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT) / 2 - Drawing.DrawConstants.CARDSELECTIONMENU_CARD_HEIGHT / 2;
                    cardChoice._width = Drawing.DrawConstants.CARDSELECTIONMENU_CARD_WIDTH;
                    cardChoice._height = Drawing.DrawConstants.CARDSELECTIONMENU_CARD_HEIGHT;

                    _choicesAsClickables.addClickableToBack(cardChoice); //order doesn't matter
                }
            }
            else
            {
                Game1.errorLog.Add("I haven't finished setupChoicesAsClickables for NewCardChoiceMenu with more than 5 cards yet!");

                //FIXIT setup height and y of background
                //FIXIT setup card spread
                //FIXIT setup scrolling functionality
            }

            Clickables.Button skipButton = new Clickables.Button(Game1.pic_functionality_skipButton,
                new Point(Game1.VIRTUAL_WINDOW_WIDTH / 2 - Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_WIDTH / 2, _y + Drawing.DrawConstants.CARDSELECTIONMENU_Y_BUFFER),
                Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_WIDTH, Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_HEIGHT, () =>
                {
                    chooseCard(null);
                }, new List<String>());
            _choicesAsClickables.addClickableToBack(skipButton); //order doesn't matter
        }
    }
}
