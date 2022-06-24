using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Menus
{
    class CombatCardChoiceMenu : NewCardChoiceMenu
    {
        public enum whereFrom
        {
            drawPile,
            discardPile,
            newCard,
            discardToDraw,
            drawToDiscard
        }

        whereFrom _whereFrom;

        public CombatCardChoiceMenu(List<DeckBuilder.Card> choices, whereFrom from, Action onConfirm, int amountOfChoices = 1) : base(choices, null, onConfirm, amountOfChoices)
        {
            _whereFrom = from;

            if (from == whereFrom.drawPile)
            {
                _cards = DeckBuilder.Deck.shuffleAndSortByRarity(choices);
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
                    Game1.getChamp().getDeck().getHand().Add(chosenCard.getCard());
                    break;
                case whereFrom.discardToDraw:
                    Game1.getChamp().getDeck().pullCardFromDiscardToDraw(chosenCard.getCard());
                    break;
                case whereFrom.drawToDiscard:
                    Game1.getChamp().getDeck().discardFromDraw(chosenCard.getCard());
                    break;
            }
        }
    }
}
