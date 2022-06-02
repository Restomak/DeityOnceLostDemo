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
        /// 
        /// </summary>
        public override void clickLogic()
        {
            //FIXIT implement
        }
    }
}
