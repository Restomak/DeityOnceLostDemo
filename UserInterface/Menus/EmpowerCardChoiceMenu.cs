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
    /// Version of CardMenu that's used specifically for choosing a card to empower.
    /// Usually pops up as a result of an event choice, such as resting using Firewood.
    /// Does not actually handle the empowerment itself, but rather passes the _onConfirm
    /// Action to the Clickable element that handles the upgrade (a MenuCard_ForUpgrade).
    /// Once the MenuCard_ForUpgrade has handled the upgrade, this menu closes automatically.
    /// </summary>
    class EmpowerCardChoiceMenu : CardMenu
    {
        Action _onConfirm;
        bool _canClose;

        public EmpowerCardChoiceMenu(Action onConfirm, bool canClose = true) : base(Game1.getChamp().getDeck().getDeck(), "Choose a card to Empower:")
        {
            _onConfirm = onConfirm;
            _canClose = canClose;
        }

        public override void updateUI()
        {
            setupCardsAsClickables();
        }

        public override void onEscapePressed()
        {
            if (_canClose)
            {
                Game1.closeMenu(this);
            }
        }



        public void setupCardsAsClickables()
        {
            _cardsAsClickables.resetClickables();
            setupClickables(_cards.Count);

            for (int i = 0; i < _cards.Count; i++)
            {
                Clickables.MenuCard_ForUpgrade menuCard = new Clickables.MenuCard_ForUpgrade(_cards[i], this, () =>
                {
                    _onConfirm();
                    Game1.closeMenu(this);
                });
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
                    if (_canClose)
                    {
                        Game1.closeMenu(this);
                    }
                }, new List<String>());
            _cardsAsClickables.addClickableToBack(exitButton); //order doesn't matter
        }
    }
}
