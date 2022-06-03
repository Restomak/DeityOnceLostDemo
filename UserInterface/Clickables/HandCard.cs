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
            int handSize = hand.Count();

            for (int i = 0; i < handSize; i++)
            {
                HandCard clickableCard = new HandCard(hand[i], i);

                //FIXIT set up card locations/sizes based on number of cards in hand. also make functions for the math

                //the first card is furthest to the right, and cards display from left to right as front to back, so add them to front each time
                ui.addClickableToFront(clickableCard); 
            }
        }
    }
}
