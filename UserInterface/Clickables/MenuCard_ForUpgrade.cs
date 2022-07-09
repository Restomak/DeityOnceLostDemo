using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Clickables
{
    /// <summary>
    /// Version of MenuCard used by the EmpowerCardChoiceMenu to display the cards while
    /// giving them the ability to be clicked on to create a menu for upgrading them (the
    /// CardInspectionMenu_Empower menu).
    /// </summary>
    class MenuCard_ForUpgrade : MenuCard
    {
        MenuUI _upgradeMenu;
        Action _onConfirm;

        public MenuCard_ForUpgrade(DeckBuilder.Card card, MenuUI upgradeMenu, Action onConfirm) : base(card)
        {
            _upgradeMenu = upgradeMenu;
            _onConfirm = onConfirm;
        }
        
        /// <summary>
        /// Handles what happens in logic when the user clicks on the card. Determines which
        /// type of MenuUI is using this object and calls the appropriate upgrade menu
        /// </summary>
        public override void onClick()
        {
            onHoverEnd();

            if (_upgradeMenu.GetType() == typeof(Menus.EmpowerCardChoiceMenu))
            {
                Game1.addToMenus(new Menus.CardInspectionMenu_Empower(_card, _onConfirm));
            }
            else
            {
                Game1.addToErrorLog("MenuCard_ForUpgrade not yet set up with the new upgrade menu: " + _upgradeMenu.ToString());
            }
        }
    }
}
