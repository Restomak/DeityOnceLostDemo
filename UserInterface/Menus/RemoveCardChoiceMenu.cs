using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Menus
{
    class RemoveCardChoiceMenu : CardMenu
    {
        Treasury.Treasures.RemoveCardFromDeck _removeCardFromDeck;

        public RemoveCardChoiceMenu(Treasury.Treasures.RemoveCardFromDeck removeCardFromDeck) : base(Game1.getChamp().getDeck().getDeck(), "Remove a card from your deck:")
        {
            _removeCardFromDeck = removeCardFromDeck;
        }

        public override void updateUI()
        {
            setupChoicesAsClickables();
        }

        public void chooseCard(Clickables.CardChoice chosenCard)
        {
            if (chosenCard != null)
            {
                Game1.debugLog.Add("Removing " + chosenCard.getCard().getName() + " from deck.");

                Game1.getChamp().getDeck().permanentlyRemoveFromDeck(chosenCard.getCard());
                _removeCardFromDeck.setTaken();
            }

            Game1.closeMenu(this);
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
                    chooseCard(null);
                }, new List<String>());
            _cardsAsClickables.addClickableToBack(skipButton); //order doesn't matter
        }
    }
}
