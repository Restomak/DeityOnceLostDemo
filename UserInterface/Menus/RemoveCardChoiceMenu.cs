using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Menus
{
    /// <summary>
    /// Version of CardMenu that's used specifically for choosing a card to remove from the
    /// player's deck. Usually pops up as a result of an event choice.
    /// </summary>
    class RemoveCardChoiceMenu : CardMenu
    {
        Treasury.Treasures.RemoveCardFromDeck _removeCardFromDeck;
        bool _skippable;

        public RemoveCardChoiceMenu(Treasury.Treasures.RemoveCardFromDeck removeCardFromDeck, bool skippable = true) : base(Game1.getChamp().getDeck().getDeck(), "Remove a card from your deck:")
        {
            _removeCardFromDeck = removeCardFromDeck;
            _skippable = skippable;
        }

        public override void updateUI()
        {
            setupChoicesAsClickables();
        }

        public override void onEscapePressed()
        {
            if (_skippable)
            {
                Game1.closeMenu(this);
            }
        }

        /// <summary>
        /// When a card is chosen, it will double-check to make sure the card is actually in
        /// the player's deck. If so, it will be removed. The menu closes afterwards, having
        /// accomplished its purpose.
        /// </summary>
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

            if (_skippable)
            {
                _cardsAsClickables.addClickableToBack(skipButton); //order doesn't matter
            }
        }
    }
}
