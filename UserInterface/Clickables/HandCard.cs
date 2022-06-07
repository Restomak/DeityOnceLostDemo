using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Clickables
{
    public class HandCard : Clickable
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
            //Cannot become hovered if another card is active
            if (Game1.getActiveCard() == null)
            {
                _hovered = true;
                Game1.setHoveredClickable(this);

                _x += Drawing.DrawConstants.COMBAT_HANDCARD_WIDTH / 2 - Drawing.DrawConstants.COMBAT_HANDCARD_GROW_WIDTH / 2;
                _y = Drawing.DrawConstants.COMBAT_HANDCARDS_GROW_Y;
                _width = Drawing.DrawConstants.COMBAT_HANDCARD_GROW_WIDTH;
                _height = Drawing.DrawConstants.COMBAT_HANDCARD_GROW_HEIGHT;
            }
        }

        /// <summary>
        /// Handles what happens when the user is no longer hovering over this object. Will
        /// return to its previous size.
        /// </summary>
        public override void onHoverEnd()
        {
            _hovered = false;
            Game1.setHoveredClickable(null);

            if (Game1.getActiveCard() != this)
            {
                deactivate();
            }
        }

        /// <summary>
        /// Handles what happens in logic when the user has clicked one of the cards in-hand.
        /// </summary>
        public override void onClick()
        {
            Game1.debugLog.Add("HandCard " + _positionInHand + " (" + _card.getName() + ") now active.");

            //Deactivate current active card first
            if (Game1.getActiveCard() != null)
            {
                if (Game1.getActiveCard() != this)
                {
                    Game1.getActiveCard().deactivate();
                    onHover();
                }
            }

            if (Game1.getChamp().getDivinity() >= _card.getPlayCost(DeckBuilder.CardEnums.CostType.DIVINITY) && //Make sure there's enough Divinity to play the card
                Game1.getChamp().getCurrentHP() >= _card.getPlayCost(DeckBuilder.CardEnums.CostType.BLOOD)) //Make sure there's enough HP to play the card
            {
                Game1.setActiveCard(this);
            }
        }

        /// <summary>
        /// Used when a card that is currently active ceases being so. Sets the sizes back
        /// to normal, similarly to onHoverEnd if the card were not active.
        /// </summary>
        public void deactivate()
        {
            Game1.setActiveCard(null);

            if (Game1.getHoveredClickable() == this)
            {
                onHoverEnd();
            }

            _x -= Drawing.DrawConstants.COMBAT_HANDCARD_WIDTH / 2 - Drawing.DrawConstants.COMBAT_HANDCARD_GROW_WIDTH / 2;
            _y = Drawing.DrawConstants.COMBAT_HANDCARDS_Y;
            _width = Drawing.DrawConstants.COMBAT_HANDCARD_WIDTH;
            _height = Drawing.DrawConstants.COMBAT_HANDCARD_HEIGHT;
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

                if (hand.Count == 1)
                {
                    clickableCard._x = Game1.VIRTUAL_WINDOW_WIDTH / 2 - Drawing.DrawConstants.COMBAT_HANDCARD_WIDTH / 2;
                }
                else if (hand.Count < 6)
                {
                    int totalWidth = Drawing.DrawConstants.COMBAT_HANDCARD_WIDTH * hand.Count + Drawing.DrawConstants.COMBAT_HANDCARDS_SPACE_BETWEEN_WHEN_LOW * (hand.Count - 1);
                    
                    clickableCard._x = Game1.VIRTUAL_WINDOW_WIDTH / 2 - totalWidth / 2 + (Drawing.DrawConstants.COMBAT_HANDCARD_WIDTH + Drawing.DrawConstants.COMBAT_HANDCARDS_SPACE_BETWEEN_WHEN_LOW) * i;
                }
                else
                {
                    clickableCard._x = Game1.VIRTUAL_WINDOW_WIDTH / 2 - Drawing.DrawConstants.COMBAT_HANDCARDS_X_FROMMID_LEFT
                        + i * ((Drawing.DrawConstants.COMBAT_HANDCARDS_AREAWIDTH - Drawing.DrawConstants.COMBAT_HANDCARD_WIDTH) / (hand.Count - 1));
                }
                clickableCard._y = Drawing.DrawConstants.COMBAT_HANDCARDS_Y;
                clickableCard._width = Drawing.DrawConstants.COMBAT_HANDCARD_WIDTH;
                clickableCard._height = Drawing.DrawConstants.COMBAT_HANDCARD_HEIGHT;

                //the first card is furthest to the right, and cards display from left to right as front to back, so add them to front each time
                ui.addClickableToFront(clickableCard);
            }
        }
    }
}
