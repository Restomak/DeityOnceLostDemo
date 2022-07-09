using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Clickables
{
    /// <summary>
    /// One of the more involved Clickables, this is used in the CombatUI to display the
    /// cards in the players' hand. HandCards actually grow in size when hovered or clicked,
    /// so that the player is able to better read the card, as well as know which one is
    /// being (or about to be) selected. HandCards are one of the few Clickables that also
    /// make use of the onHeld, whileHeld, and onHeldEnd functions, as HandCards can be
    /// dragged to their associated Targets in order to play a card instead of clicking.
    /// </summary>
    public class HandCard : Clickable
    {
        DeckBuilder.Card _card;
        int _positionInHand;
        Target _heldOverTarget;

        public HandCard(DeckBuilder.Card card, int positionInHand)
        {
            _card = card;
            _positionInHand = positionInHand;

            _extraInfo = _card.getHoverInfo();
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
            if (Game1.getCombatHandler().getCombatUI().getActiveCard() == null)
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

            if (Game1.getCombatHandler().getCombatUI().getActiveCard() != this)
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
            HandCard activeCard = Game1.getCombatHandler().getCombatUI().getActiveCard();
            if (activeCard != null)
            {
                if (activeCard != this)
                {
                    activeCard.deactivate();
                    onHover();
                }
                activeCard.deactivate();
            }

            if (_card.canPlay())
            {
                Game1.getCombatHandler().getCombatUI().setActiveCard(this);
            }
        }

        /// <summary>
        /// Used when a card that is currently active ceases being so. Sets the sizes back
        /// to normal, similarly to onHoverEnd if the card were not active.
        /// </summary>
        public void deactivate()
        {
            Game1.getCombatHandler().getCombatUI().setActiveCard(null);

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
        /// Handles what happens in logic when the InputController determines that the HandCard
        /// is held by the player. Sets the card up to be draggable by the player.
        /// </summary>
        public override void onHeld()
        {
            _held = true;
            Game1.setHeldClickable(this);
        }

        /// <summary>
        /// Handles what happens in logic when the InputController calls this function. Allows
        /// the player to drag this HandCard over to a target.
        /// </summary>
        public override void whileHeld()
        {
            Clickable hoveredClickable = UserInterface.getFrontClickableFromUIList(Game1.getHoveredClickable(), Game1.getActiveUI(), Game1.getInputController().getMousePos());
            
            if (hoveredClickable != null && hoveredClickable.GetType() == typeof(Target))
            {
                _heldOverTarget = (Target)hoveredClickable;

                _heldOverTarget.onHover();
            }
            else if (_heldOverTarget != null)
            {
                _heldOverTarget.onHoverEnd();
                _heldOverTarget = null;
            }
        }

        /// <summary>
        /// Handles what happens in logic when the user lets go of this HandCard while it was
        /// held. Will check if it was held over a target, and if so, will act as if the target
        /// was clicked.
        /// </summary>
        public override void onHeldEnd()
        {
            _held = false;
            Game1.setHeldClickable(null);

            if (_heldOverTarget != null)
            {
                _heldOverTarget.onClick();
            }
            else if (!mouseInBoundaries(Game1.getInputController().getMousePos()))
            {
                deactivate();
            }
        }

        public override void onRightClick()
        {
            deactivate();
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
