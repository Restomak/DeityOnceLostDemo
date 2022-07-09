using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Menus
{
    class CardCollectionMenu : CardMenu
    {
        /// <summary>
        /// Version of CardMenu that's used specifically for viewing cards in a deck or pile
        /// or collection. The cards themselves are not clickable, however right clicking them
        /// in order to inspect them and their upgrades is still an option.
        /// </summary>
        public CardCollectionMenu(List<DeckBuilder.Card> cards, bool autoSort, String title) : base(cards, title)
        {
            if (autoSort)
            {
                _cards = DeckBuilder.Deck.shuffleAndSortByRarity(cards);
            }
        }

        public override void updateUI()
        {
            setupCardsAsClickables();
        }

        public override void onEscapePressed()
        {
            Game1.closeMenu(this);
        }



        public void setupCardsAsClickables()
        {
            _cardsAsClickables.resetClickables();
            setupClickables(_cards.Count);

            for (int i = 0; i < _cards.Count; i++)
            {
                Clickables.MenuCard menuCard = new Clickables.MenuCard(_cards[i]);
                menuCard._x = setupClickableParamter(_cards.Count, i, setupParameter.x);
                menuCard._y = setupClickableParamter(_cards.Count, i, setupParameter.y);
                menuCard._width = setupClickableParamter(_cards.Count, i, setupParameter.width);
                menuCard._height = setupClickableParamter(_cards.Count, i, setupParameter.height);
                _cardsAsClickables.addClickableToBack(menuCard); //order doesn't matter
            }

            Clickables.Button exitButton = new Clickables.Button(Game1.pic_functionality_exitButton,
                new Point(Game1.VIRTUAL_WINDOW_WIDTH / 2 - Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_WIDTH / 2, _y + Drawing.DrawConstants.CARDSELECTIONMENU_Y_BUFFER),
                Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_WIDTH, Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_HEIGHT, () =>
                {
                    Game1.closeMenu(this);
                }, new List<String>());
            _cardsAsClickables.addClickableToBack(exitButton); //order doesn't matter
        }
    }
}
