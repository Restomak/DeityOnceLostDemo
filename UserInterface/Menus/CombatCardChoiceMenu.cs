using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Menus
{
    /// <summary>
    /// Version of NewCardChoiceMenu that's specialized for combat. Since NewCardChoiceMenu
    /// can handle multiple choices, all this menu needs to do is specify what occurs when
    /// the card is chosen, and what information is displayed to the player in order for
    /// them to understand what's going on. This is used for any in-combat card menu, such
    /// as choosing a card to Preserve or discard from the hand, etc.
    /// </summary>
    class CombatCardChoiceMenu : NewCardChoiceMenu
    {
        public enum whereFrom
        {
            drawPile,
            discardPile,
            newCard,
            discardToDraw,
            drawToDiscard,
            handToPreserve
        }

        whereFrom _whereFrom;

        public CombatCardChoiceMenu(List<DeckBuilder.Card> choices, whereFrom from, Action onConfirm, int amountOfChoices = 1, bool skippable = true) :
            base(choices, null, onConfirm, determineTitleBasedOnWhereFrom(from, amountOfChoices, skippable), amountOfChoices, skippable)
        {
            _whereFrom = from;

            if (from == whereFrom.drawPile)
            {
                _cards = DeckBuilder.Deck.shuffleAndSortByRarity(choices);
            }
        }

        private static String determineTitleBasedOnWhereFrom(whereFrom from, int amountOfChoices, bool skippable)
        {
            String startOfTitle = "Choose ";
            if (amountOfChoices == 1)
            {
                startOfTitle += "a card ";
            }
            else
            {
                if (skippable)
                {
                    startOfTitle += "up to ";
                }
                startOfTitle += amountOfChoices + " cards";
            }

            switch (from)
            {
                case whereFrom.drawPile:
                case whereFrom.discardPile:
                    return startOfTitle + " to put in your hand:";
                case whereFrom.newCard:
                    return startOfTitle + " to add to your hand:";
                case whereFrom.discardToDraw:
                    return startOfTitle + " to put in your draw pile:";
                case whereFrom.drawToDiscard:
                    return startOfTitle + " to discard from your draw pile:";
                case whereFrom.handToPreserve:
                    return startOfTitle + " to Preserve:";
                default:
                    return startOfTitle + ":";
            }
        }


        public override void chooseCard(Clickables.CardChoice chosenCard)
        {
            if (chosenCard != null)
            {
                if (_amountOfChoices == 1)
                {
                    dealWithCard(chosenCard);

                    Game1.closeMenu(this);

                    _onConfirm();
                }
                else
                {
                    //Check if it's the same one
                    int indexMatch = -1;
                    for (int i = 0; i < _clickedChoices.Count; i++)
                    {
                        if (chosenCard._x == _clickedChoices[i]._x && chosenCard._y == _clickedChoices[i]._y)
                        {
                            indexMatch = i;
                            break;
                        }
                    }

                    if (indexMatch > -1)
                    {
                        _clickedChoices.RemoveAt(indexMatch);
                    }
                    else
                    {
                        _clickedChoices.Add(chosenCard);

                        if (_clickedChoices.Count == _amountOfChoices)
                        {
                            for (int i = 0; i < _clickedChoices.Count; i++)
                            {
                                dealWithCard(_clickedChoices[i]);
                            }
                            Game1.closeMenu(this);

                            _onConfirm();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles what happens once the player makes a choice, based on the goal of this menu
        /// which is set when the menu was created.
        /// </summary>
        public override void dealWithCard(Clickables.CardChoice chosenCard)
        {
            switch (_whereFrom)
            {
                case whereFrom.drawPile:
                    Game1.getChamp().getDeck().pullCardFromDrawToHand(chosenCard.getCard());
                    break;
                case whereFrom.discardPile:
                    Game1.getChamp().getDeck().pullCardFromDiscardToHand(chosenCard.getCard());
                    break;
                case whereFrom.newCard:
                    Game1.getChamp().getDeck().addToHand(chosenCard.getCard().getNewCard()); //Get new card in case the choices are from somewhere it already exists
                    break;
                case whereFrom.discardToDraw:
                    Game1.getChamp().getDeck().pullCardFromDiscardToDraw(chosenCard.getCard());
                    break;
                case whereFrom.drawToDiscard:
                    Game1.getChamp().getDeck().discardFromDraw(chosenCard.getCard());
                    break;
                case whereFrom.handToPreserve:
                    Game1.getChamp().getDeck().preserveCardInHand(chosenCard.getCard());
                    break;
            }
        }
    }
}
