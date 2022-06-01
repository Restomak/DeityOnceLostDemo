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

        public HandCard(DeckBuilder.Card card)
        {
            _card = card;
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

        /// <summary>
        /// Handles what happens in logic when the user has clicked one of the cards in-hand.
        /// </summary>
        public override void clickLogic()
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
                HandCard clickableCard = new HandCard(hand[i]);

                //FIXIT set up card locations/sizes based on number of cards in hand. also make functions for the math

                //the first card is furthest to the right, and cards display from left to right as front to back, so add them to front each time
                ui.addClickableToFront(clickableCard); 
            }
        }
    }
}
