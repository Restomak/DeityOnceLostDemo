using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Clickables
{
    public class DeckOfCards : Clickable
    {
        public enum typeOfDeck
        {
            DRAWPILE,
            DISCARDPILE,
            DECK,
            WHOLECOLLECTION
        }

        public const int DRAWDISCARD_BUFFER = 2;

        typeOfDeck _typeOfDeck;

        public DeckOfCards(typeOfDeck type, Characters.Champion champ)
        {
            _typeOfDeck = type;
            setupDeck(champ);
        }

        /// <summary>
        /// Sets up the bounding boxes for each deck
        /// </summary>
        private void setupDeck(Characters.Champion champ)
        {
            switch (_typeOfDeck)
            {
                case typeOfDeck.DRAWPILE:
                    _x = Draw.DrawHandler.COMBAT_DRAWPILE_X - DRAWDISCARD_BUFFER;
                    _y = Draw.DrawHandler.COMBAT_DRAWPILE_Y - DRAWDISCARD_BUFFER;
                    _width = Draw.DrawHandler.COMBAT_CARDDOWN_WIDTH + DRAWDISCARD_BUFFER;
                    _height = Draw.DrawHandler.COMBAT_CARDDOWN_HEIGHT + DRAWDISCARD_BUFFER;

                    if (champ.getDeck() != null && champ.getDeck().getDrawPile() != null)
                    {
                        int size = champ.getDeck().getDrawPile().Count;

                        calculateWidthAndHeight(size);
                    }
                    break;
                case typeOfDeck.DISCARDPILE:
                    _width = Draw.DrawHandler.COMBAT_CARDDOWN_WIDTH;
                    _height = Draw.DrawHandler.COMBAT_CARDDOWN_HEIGHT;

                    if (champ.getDeck() != null && champ.getDeck().getDiscardPile() != null)
                    {
                        int size = champ.getDeck().getDiscardPile().Count;

                        calculateWidthAndHeight(size);
                    }

                    _x = Game1.VIRTUAL_WINDOW_WIDTH - Draw.DrawHandler.COMBAT_DRAWPILE_X - _width - DRAWDISCARD_BUFFER;
                    _y = Game1.VIRTUAL_WINDOW_HEIGHT - Draw.DrawHandler.COMBAT_DRAWPILE_Y - _height - DRAWDISCARD_BUFFER;
                    _width += DRAWDISCARD_BUFFER;
                    _height += DRAWDISCARD_BUFFER;
                    break;
                case typeOfDeck.DECK:
                    //FIXIT implement
                    break;
                case typeOfDeck.WHOLECOLLECTION:
                    //FIXIT implement
                    break;
                default:
                    Game1.errorLog.Add("DeckOfCards typeOfDeck error: " + _typeOfDeck.ToString());
                    _x = -1;
                    _y = -1;
                    _width = 0;
                    _height = 0;
                    break;
            }
        }
        
        private void calculateWidthAndHeight(int size)
        {
            if (size < DeckBuilder.Deck.MAX_HAND_CAPACITY * 2)
            {
                _width += (int)((double)size * 0.25);
                _height += size * 2;
            }
            else
            {
                _width += DeckBuilder.Deck.MAX_HAND_CAPACITY / 2;
                _height += DeckBuilder.Deck.MAX_HAND_CAPACITY * 4;
            }
        }

        /// <summary>
        /// Handles what happens in logic when the user hovers over one of the various decks:
        /// the draw pile, discard pile, current deck, or entire collection (at base). For
        /// each, it will simply enable the hover flag so that it can be displayed as glowing
        /// so the player knows it's clickable.
        /// </summary>
        public override void onHover()
        {
            _hovered = true;
            Game1.setHoveredClickable(this);
        }

        /// <summary>
        /// Handles what happens in logic when the user has clicked one of the various decks:
        /// the draw pile, discard pile, current deck, or entire collection (at base).
        /// Regarldess of which deck was clicked, the result will be a new UserInterface
        /// popping up displaying each card in the deck.
        /// </summary>
        public override void onClick()
        {
            //FIXIT implement, regardless of the deck, it pops up a new UserInterface that is a list of the cards in the deck
        }
    }
}
