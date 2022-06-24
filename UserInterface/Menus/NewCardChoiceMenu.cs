using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Menus
{
    public class NewCardChoiceMenu : CardMenu
    {
        Treasury.Treasures.AddCardToDeck _addCardToDeck;
        protected int _amountOfChoices;
        protected List<Clickables.CardChoice> _clickedChoices;
        protected Action _onConfirm;

        public NewCardChoiceMenu(List<DeckBuilder.Card> choices, Treasury.Treasures.AddCardToDeck addCardToDeck, Action onConfirm, int amountOfChoices = 1) :
            base(choices, "Choose a Card")
        {
            _addCardToDeck = addCardToDeck;
            _amountOfChoices = amountOfChoices;
            _onConfirm = onConfirm;

            _clickedChoices = new List<Clickables.CardChoice>();

            if (amountOfChoices > choices.Count)
            {
                amountOfChoices = choices.Count;
            }
        }

        public bool multipleChoice()
        {
            return (_amountOfChoices > 1);
        }

        public List<Clickables.CardChoice> getClickedChoices()
        {
            return _clickedChoices;
        }



        public override void updateUI()
        {
            setupChoicesAsClickables();
        }

        public virtual void chooseCard(Clickables.CardChoice chosenCard)
        {
            if (chosenCard != null)
            {
                if (_amountOfChoices == 1)
                {
                    dealWithCard(chosenCard);

                    _addCardToDeck.setTaken();
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
                            _addCardToDeck.setTaken();
                            Game1.closeMenu(this);

                            _onConfirm();
                        }
                    }
                }
            }
        }

        public virtual void dealWithCard(Clickables.CardChoice chosenCard)
        {
            Game1.debugLog.Add("Adding " + chosenCard.getCard().getName() + " to deck.");

            Game1.getChamp().getDeck().permanentlyAddToDeck(chosenCard.getCard());
        }



        public void setupChoicesAsClickables()
        {
            _cardsAsClickables.resetClickables();
            setupClickables(_cards.Count);

            for (int i = 0; i < _cards.Count; i++)
            {
                Clickables.CardChoice cardChoice = new Clickables.CardChoice(_cards[i], this);
                cardChoice._x = setupClickableParamter(_cards.Count, i, setupParameter.x);
                cardChoice._y = setupClickableParamter(_cards.Count, i, setupParameter.y);
                cardChoice._width = setupClickableParamter(_cards.Count, i, setupParameter.width);
                cardChoice._height = setupClickableParamter(_cards.Count, i, setupParameter.height);
                _cardsAsClickables.addClickableToBack(cardChoice); //order doesn't matter
            }

            Clickables.Button skipButton = new Clickables.Button(Game1.pic_functionality_skipButton,
                new Point(Game1.VIRTUAL_WINDOW_WIDTH / 2 - Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_WIDTH / 2, _y + Drawing.DrawConstants.CARDSELECTIONMENU_Y_BUFFER),
                Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_WIDTH, Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_HEIGHT, () =>
                {
                    Game1.closeMenu(this);
                }, new List<String>());
            _cardsAsClickables.addClickableToBack(skipButton); //order doesn't matter

            if (_amountOfChoices > 1)
            {
                skipButton._x = Game1.VIRTUAL_WINDOW_WIDTH / 2 - Drawing.DrawConstants.CARDSELECTIONMENU_SPACE_BETWEEN_BUTTONS / 2 - Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_WIDTH;

                Clickables.Button confirmButton = new Clickables.Button(Game1.pic_functionality_confirmButton,
                    new Point(skipButton._x + Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_WIDTH + Drawing.DrawConstants.CARDSELECTIONMENU_SPACE_BETWEEN_BUTTONS,
                    _y + Drawing.DrawConstants.CARDSELECTIONMENU_Y_BUFFER),
                    Drawing.DrawConstants.CARDSELECTIONMENU_CONFIRM_BUTTON_WIDTH, Drawing.DrawConstants.CARDSELECTIONMENU_CONFIRM_BUTTON_HEIGHT, () =>
                    {
                        for (int i = 0; i < _clickedChoices.Count; i++)
                        {
                            dealWithCard(_clickedChoices[i]);
                        }
                        Game1.closeMenu(this);

                        _onConfirm();
                    }, new List<String>());
                _cardsAsClickables.addClickableToBack(confirmButton); //order doesn't matter
            }
        }
    }
}
