using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Clickables
{
    class HandCard : Clickable
    {
        DeckBuilder.Card _card;
        int _positionInHand;

        public HandCard(DeckBuilder.Card card, int positionInHand)
        {
            _card = card;
            _positionInHand = positionInHand;
        }

        //Getters & Setters
        public void setCard(DeckBuilder.Card card)
        {
            _card = card;
        }
        public DeckBuilder.Card getCard()
        {
            return _card;
        }
        public void setPositionInHand(int positionInHand)
        {
            _positionInHand = positionInHand;
        }
        public int getPositionInHand()
        {
            return _positionInHand;
        }

        /// <summary>
        /// Handles what happens in logic when the user hovers over one of the cards in-hand.
        /// The card will grow larger so the player can better read the text without clicking.
        /// </summary>
        public override void onHover()
        {
            _hovered = true;
            Game1.setHoveredClickable(this);
            //FIXIT implement
        }

        /// <summary>
        /// Handles what happens in logic when the user has clicked one of the cards in-hand.
        /// </summary>
        public override void onClick()
        {
            //FIXIT implement
        }

        /// <summary>
        /// Handles what happens in logic when the user is holding down the mouse button on one of the cards in-hand.
        /// </summary>
        public override void whileHeld()
        {
            //FIXIT implement
        }

        /// <summary>
        /// Sets up a list of cards (the player's hand) as a UserInterface so they're interactable
        /// </summary>
        public static void setupHandUI(UserInterface ui, List<DeckBuilder.Card> hand)
        {
            ui.resetClickables();

            for (int i = 0; i < hand.Count; i++)
            {
                HandCard clickableCard = new HandCard(hand[i], i);

                clickableCard._x = Game1.VIRTUAL_WINDOW_WIDTH / 2 - Drawing.DrawConstants.COMBAT_HANDCARDS_X_FROMMID_LEFT
                    + i * ((Drawing.DrawConstants.COMBAT_HANDCARDS_AREAWIDTH - Drawing.DrawConstants.COMBAT_HANDCARD_WIDTH) / (hand.Count - 1));
                clickableCard._y = Drawing.DrawConstants.COMBAT_HANDCARDS_Y;
                clickableCard._width = Drawing.DrawConstants.COMBAT_HANDCARD_WIDTH;
                clickableCard._height = Drawing.DrawConstants.COMBAT_HANDCARD_HEIGHT;

                //the first card is furthest to the right, and cards display from left to right as front to back, so add them to front each time
                ui.addClickableToFront(clickableCard);
            }
        }
    }
}
