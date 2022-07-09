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
    /// Menu for displaying a single card on screen as well as its empowered upgrade version
    /// if the card is un-upgraded. Used alongside the EmpowerCardChoiceMenu for empowering
    /// cards, this menu is card-specific and includes a confirm button (when applicable) to
    /// let the player make the decision or not.
    /// </summary>
    class CardInspectionMenu_Empower : MenuUI
    {
        DeckBuilder.Card _inspectedCard;
        UserInterface _buttons;
        UserInterface _cardsAndArrow;
        Action _onConfirm;

        public CardInspectionMenu_Empower(DeckBuilder.Card inspectedCard, Action onConfirm) : base(Drawing.DrawConstants.CARDUPGRADEMENU_X, Drawing.DrawConstants.CARDUPGRADEMENU_BACKGROUND_Y,
            Drawing.DrawConstants.CARDUPGRADEMENU_WIDTH, Drawing.DrawConstants.CARDUPGRADEMENU_BACKGROUND_HEIGHT, Game1.pic_functionality_bar,
            Color.DarkSlateGray * Drawing.DrawConstants.CARDUPGRADEMENU_BACKGROUND_FADE, "Card Empowerment",
            Game1.VIRTUAL_WINDOW_WIDTH / 2 - (int)(Game1.roboto_black_24.MeasureString("Card Empowerment").X / 2), Drawing.DrawConstants.CARDUPGRADEMENU_TITLE_Y, Game1.roboto_black_24,
            Drawing.DrawConstants.TEXT_24_HEIGHT, Color.PowderBlue, Color.Black)
        {
            _inspectedCard = inspectedCard;
            _onConfirm = onConfirm;

            _buttons = new UserInterface();
            _cardsAndArrow = new UserInterface();

            _wholeUI.Add(_buttons);
            _wholeUI.Add(_cardsAndArrow);

            initializeButtons();
        }

        public override bool addTopBar() { return true; }

        public override void updateUI()
        {
            updateCards();
        }

        public override void onEscapePressed()
        {
            cancel();
        }



        private void cancel()
        {
            Game1.closeMenu(this);
        }

        /// <summary>
        /// Called on confirmation that the player wants to empower the selected card. Makes
        /// sure the card can be found in the deck, and if so, empowers the card. Then, calls
        /// the _onConfirm action passed to the menu's constructor to let the engine know that
        /// the card empowerment has been completed.
        /// </summary>
        private void confirm()
        {
            if (Game1.getChamp().getDeck().getDeck().Contains(_inspectedCard))
            {
                int cardIndex = Game1.getChamp().getDeck().getDeck().IndexOf(_inspectedCard);
                Game1.getChamp().getDeck().getDeck().RemoveAt(cardIndex);
                Game1.getChamp().getDeck().getDeck().Insert(cardIndex, _inspectedCard.getEmpoweredCard());
            }
            else
            {
                Game1.addToErrorLog("Chose a card to empower, but can't find it in the deck: " + _inspectedCard.getName());
            }

            _onConfirm();
            Game1.closeMenu(this);
        }



        /// <summary>
        /// Does not initialize the confirm button if the card is already an upgraded version,
        /// even if the card isn't specifically an empowered card (currently, cards cannot be
        /// upgraded in more than one manner).
        /// </summary>
        private void initializeButtons()
        {
            //Exit button
            Clickables.Button exitButton = new Clickables.Button(Game1.pic_functionality_exitButton,
                new Point(Game1.VIRTUAL_WINDOW_WIDTH / 2 - Drawing.DrawConstants.CARDUPGRADEMENU_SPACE_BETWEEN / 2 - Drawing.DrawConstants.CARDUPGRADEMENU_EXIT_BUTTON_WIDTH,
                Drawing.DrawConstants.CARDUPGRADEMENU_Y + Drawing.DrawConstants.CARDUPGRADEMENU_BOTTOM_BUFFER),
                Drawing.DrawConstants.CARDUPGRADEMENU_EXIT_BUTTON_WIDTH, Drawing.DrawConstants.CARDUPGRADEMENU_EXIT_BUTTON_HEIGHT, () =>
                {
                    cancel();
                }, new List<String>());
            _buttons.addClickableToBack(exitButton); //order doesn't matter

            if (!_inspectedCard.isEmpowered() && !_inspectedCard.isBloodstained())
            {
                //Confirm button
                Clickables.Button confirmButton = new Clickables.Button(Game1.pic_functionality_confirmButton,
                    new Point(Game1.VIRTUAL_WINDOW_WIDTH / 2 + Drawing.DrawConstants.CARDUPGRADEMENU_SPACE_BETWEEN / 2,
                    Drawing.DrawConstants.CARDUPGRADEMENU_Y + Drawing.DrawConstants.CARDUPGRADEMENU_BOTTOM_BUFFER),
                    Drawing.DrawConstants.CARDUPGRADEMENU_CONFIRM_BUTTON_WIDTH, Drawing.DrawConstants.CARDUPGRADEMENU_CONFIRM_BUTTON_HEIGHT, () =>
                    {
                        confirm();
                    }, new List<String>());
                _buttons.addClickableToBack(confirmButton); //order doesn't matter
            }
            else
            {
                //No confirm button, so center the exit button
                exitButton._x = Game1.VIRTUAL_WINDOW_WIDTH / 2 - Drawing.DrawConstants.CARDUPGRADEMENU_EXIT_BUTTON_WIDTH / 2;
            }
        }

        /// <summary>
        /// Does not initialize the second card to be drawn if the card is already an upgraded
        /// version, even if the card isn't specifically an empowered card (currently, cards
        /// cannot be upgraded in more than one manner).
        /// </summary>
        private void updateCards()
        {
            _cardsAndArrow.resetClickables();

            if (_inspectedCard.isEmpowered() || _inspectedCard.isBloodstained())
            {
                //Only draw the one card in the middle
                Point xy = new Point(Game1.VIRTUAL_WINDOW_WIDTH / 2 - Drawing.DrawConstants.CARDUPGRADEMENU_CARD_WIDTH / 2,
                    Drawing.DrawConstants.CARDUPGRADEMENU_Y + Drawing.DrawConstants.CARDUPGRADEMENU_BOTTOM_BUFFER +
                    Drawing.DrawConstants.CARDUPGRADEMENU_EXIT_BUTTON_HEIGHT + Drawing.DrawConstants.CARDUPGRADEMENU_SPACE_BETWEEN);

                Clickables.AestheticOnly_InspectedCard card = new Clickables.AestheticOnly_InspectedCard(_inspectedCard.getNewCard(), xy,
                    Drawing.DrawConstants.CARDUPGRADEMENU_CARD_WIDTH, Drawing.DrawConstants.CARDUPGRADEMENU_CARD_HEIGHT);
                _cardsAndArrow.addClickableToBack(card);
            }
            else
            {
                //Draw the regular card on the left, the empowered card on the right, and the arrow in between

                Point xyRegularCard = new Point(Game1.VIRTUAL_WINDOW_WIDTH / 2 - Drawing.DrawConstants.CARDUPGRADEMENU_CARD_WIDTH -
                    Drawing.DrawConstants.CARDUPGRADEMENU_SPACE_BETWEEN - Drawing.DrawConstants.CARDUPGRADEMENU_UPGRADE_ARROW_SIZE / 2,
                    Drawing.DrawConstants.CARDUPGRADEMENU_Y + Drawing.DrawConstants.CARDUPGRADEMENU_BOTTOM_BUFFER +
                    Drawing.DrawConstants.CARDUPGRADEMENU_EXIT_BUTTON_HEIGHT + Drawing.DrawConstants.CARDUPGRADEMENU_SPACE_BETWEEN);

                Clickables.AestheticOnly_InspectedCard regularCard = new Clickables.AestheticOnly_InspectedCard(_inspectedCard.getNewCard(), xyRegularCard,
                    Drawing.DrawConstants.CARDUPGRADEMENU_CARD_WIDTH, Drawing.DrawConstants.CARDUPGRADEMENU_CARD_HEIGHT);
                _cardsAndArrow.addClickableToBack(regularCard);


                Point xyEmpoweredCard = new Point(Game1.VIRTUAL_WINDOW_WIDTH / 2 +
                    Drawing.DrawConstants.CARDUPGRADEMENU_SPACE_BETWEEN + Drawing.DrawConstants.CARDUPGRADEMENU_UPGRADE_ARROW_SIZE / 2,
                    Drawing.DrawConstants.CARDUPGRADEMENU_Y + Drawing.DrawConstants.CARDUPGRADEMENU_BOTTOM_BUFFER +
                    Drawing.DrawConstants.CARDUPGRADEMENU_EXIT_BUTTON_HEIGHT + Drawing.DrawConstants.CARDUPGRADEMENU_SPACE_BETWEEN);

                Clickables.AestheticOnly_InspectedCard empoweredCard = new Clickables.AestheticOnly_InspectedCard(_inspectedCard.getEmpoweredCard(), xyEmpoweredCard,
                    Drawing.DrawConstants.CARDUPGRADEMENU_CARD_WIDTH, Drawing.DrawConstants.CARDUPGRADEMENU_CARD_HEIGHT);
                _cardsAndArrow.addClickableToBack(empoweredCard);


                Point xyArrow = new Point(Game1.VIRTUAL_WINDOW_WIDTH / 2 - Drawing.DrawConstants.CARDUPGRADEMENU_UPGRADE_ARROW_SIZE / 2,
                    Drawing.DrawConstants.CARDUPGRADEMENU_Y + Drawing.DrawConstants.CARDUPGRADEMENU_BOTTOM_BUFFER +
                    Drawing.DrawConstants.CARDUPGRADEMENU_EXIT_BUTTON_HEIGHT + Drawing.DrawConstants.CARDUPGRADEMENU_SPACE_BETWEEN +
                    Drawing.DrawConstants.CARDUPGRADEMENU_CARD_HEIGHT / 2 - Drawing.DrawConstants.CARDUPGRADEMENU_UPGRADE_ARROW_SIZE / 2);

                Clickables.AestheticOnly arrow = new Clickables.AestheticOnly(Game1.pic_functionality_empowerArrow, xyArrow, Drawing.DrawConstants.CARDUPGRADEMENU_UPGRADE_ARROW_SIZE,
                    Drawing.DrawConstants.CARDUPGRADEMENU_UPGRADE_ARROW_SIZE, Color.White);
                _cardsAndArrow.addClickableToBack(arrow);
            }
        }
    }
}
