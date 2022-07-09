using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Clickables
{
    /// <summary>
    /// Used by not only the deck button on the top bar, but also the three card piles (draw,
    /// discard, and removed) in combat. When clicked, opens a CardCollectionMenu of that
    /// deck, after closing any others that are already open.
    /// </summary>
    public class DeckOfCards : Clickable
    {
        public enum typeOfDeck
        {
            DRAWPILE,
            DISCARDPILE,
            REMOVEDPILE,
            DECK,
            WHOLECOLLECTION
        }

        typeOfDeck _typeOfDeck;
        String _hoverDescription;

        public DeckOfCards(typeOfDeck type, Characters.Champion champ, String hoverDescription)
        {
            _typeOfDeck = type;
            _hoverDescription = hoverDescription;
            setupDeck(champ);
        }

        //Getters
        public typeOfDeck getDeckType()
        {
            return _typeOfDeck;
        }
        public String getHoverDescription()
        {
            return _hoverDescription;
        }

        /// <summary>
        /// Sets up the bounding boxes for each deck
        /// </summary>
        private void setupDeck(Characters.Champion champ)
        {
            switch (_typeOfDeck)
            {
                case typeOfDeck.DRAWPILE:
                    _x = Drawing.DrawConstants.COMBAT_DRAWPILE_X - Drawing.DrawConstants.COMBAT_DRAW_PILE_BUFFER;
                    _y = Drawing.DrawConstants.COMBAT_DRAWPILE_Y - Drawing.DrawConstants.COMBAT_DRAW_PILE_BUFFER;
                    _width = Drawing.DrawConstants.COMBAT_CARDDOWN_WIDTH + Drawing.DrawConstants.COMBAT_DRAW_PILE_BUFFER;
                    _height = Drawing.DrawConstants.COMBAT_CARDDOWN_HEIGHT + Drawing.DrawConstants.COMBAT_DRAW_PILE_BUFFER;

                    if (champ.getDeck() != null && champ.getDeck().getDrawPile() != null)
                    {
                        int size = champ.getDeck().getDrawPile().Count;

                        calculateWidthAndHeight(size);
                    }
                    break;
                case typeOfDeck.DISCARDPILE:
                    _y = Drawing.DrawConstants.COMBAT_DISCARDPILE_Y - Drawing.DrawConstants.COMBAT_DRAW_PILE_BUFFER;
                    _width = Drawing.DrawConstants.COMBAT_CARDDOWN_WIDTH;
                    _height = Drawing.DrawConstants.COMBAT_CARDDOWN_HEIGHT;

                    if (champ.getDeck() != null && champ.getDeck().getDiscardPile() != null)
                    {
                        int size = champ.getDeck().getDiscardPile().Count;

                        calculateWidthAndHeight(size);
                    }

                    _x = Game1.VIRTUAL_WINDOW_WIDTH - Drawing.DrawConstants.COMBAT_DISCARDPILE_X_FROMRIGHT - _width - Drawing.DrawConstants.COMBAT_DRAW_PILE_BUFFER;
                    _width += Drawing.DrawConstants.COMBAT_DRAW_PILE_BUFFER;
                    _height += Drawing.DrawConstants.COMBAT_DRAW_PILE_BUFFER;
                    break;
                case typeOfDeck.REMOVEDPILE:
                    _y = Drawing.DrawConstants.COMBAT_REMOVEDCARDS_Y - Drawing.DrawConstants.COMBAT_DRAW_PILE_BUFFER;
                    _width = Drawing.DrawConstants.COMBAT_CARDDOWN_WIDTH;
                    _height = Drawing.DrawConstants.COMBAT_CARDDOWN_HEIGHT;

                    if (champ.getDeck() != null && champ.getDeck().getDiscardPile() != null)
                    {
                        int size = champ.getDeck().getDiscardPile().Count;

                        calculateWidthAndHeight(size);
                    }

                    _x = Game1.VIRTUAL_WINDOW_WIDTH - Drawing.DrawConstants.COMBAT_REMOVEDCARDS_X_FROMRIGHT - _width - Drawing.DrawConstants.COMBAT_DRAW_PILE_BUFFER;
                    _width += Drawing.DrawConstants.COMBAT_DRAW_PILE_BUFFER;
                    _height += Drawing.DrawConstants.COMBAT_DRAW_PILE_BUFFER;
                    break;
                case typeOfDeck.DECK:
                    if (champ.getDeck() != null)
                    {
                        _x = Drawing.DrawConstants.TOPBAR_DECK_ICON_X;
                        _y = Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT + Drawing.DrawConstants.TOPBAR_DECK_ICON_Y_BUFFER;
                        _width = Drawing.DrawConstants.TOPBAR_DECK_ICON_WIDTH;
                        _height = Drawing.DrawConstants.TOPBAR_DECK_ICON_HEIGHT;
                    }
                    break;
                case typeOfDeck.WHOLECOLLECTION:
                    //FIXIT implement
                    break;
                default:
                    Game1.addToErrorLog("DeckOfCards typeOfDeck error: " + _typeOfDeck.ToString());
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
        /// Handles what happens when the user is no longer hovering over this object.
        /// </summary>
        public override void onHoverEnd()
        {
            _hovered = false;
            Game1.setHoveredClickable(null);
        }

        /// <summary>
        /// Handles what happens in logic when the user has clicked one of the various decks:
        /// the draw pile, discard pile, current deck, or entire collection (at base).
        /// Regarldess of which deck was clicked, the result will be a new CardCollectionMenu
        /// popping up displaying each card in the deck.
        /// </summary>
        public override void onClick()
        {
            //Close any deck menu that's already open
            Game1.closeCardCollectionMenus();

            //Deactivate current active card first
            HandCard activeCard = Game1.getCombatHandler().getCombatUI().getActiveCard();
            if (activeCard != null)
            {
                activeCard.deactivate();
            }

            switch (_typeOfDeck)
            {
                case typeOfDeck.DRAWPILE:
                    Game1.addToMenus(new Menus.CardCollectionMenu(Game1.getChamp().getDeck().getDrawPile(), true, "Draw Pile"));
                    break;
                case typeOfDeck.DISCARDPILE:
                    Game1.addToMenus(new Menus.CardCollectionMenu(Game1.getChamp().getDeck().getDiscardPile(), true, "Discard Pile"));
                    break;
                case typeOfDeck.REMOVEDPILE:
                    Game1.addToMenus(new Menus.CardCollectionMenu(Game1.getChamp().getDeck().getRemovedCards(), true, "Dissipated Cards"));
                    break;
                case typeOfDeck.DECK:
                    Game1.addToMenus(new Menus.CardCollectionMenu(Game1.getChamp().getDeck().getDeck(), false, "Deck"));
                    break;
                case typeOfDeck.WHOLECOLLECTION:
                    //FIXIT implement
                    break;
                default:
                    break;
            }
        }
    }
}
