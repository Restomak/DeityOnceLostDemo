using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Clickables
{
    /// <summary>
    /// Version of MenuCard used by the NewCardChoiceMenus and RemoveCardChoiceMenu that has
    /// an onClick call that depends on the menu that uses it.
    /// </summary>
    public class CardChoice : MenuCard
    {
        Menus.CardMenu _cardChoiceMenu;

        public CardChoice(DeckBuilder.Card card, Menus.CardMenu cardChoiceMenu) : base (card)
        {
            _cardChoiceMenu = cardChoiceMenu;
        }
        


        /// <summary>
        /// Handles what happens in logic when the user clicks on the card. It will tell the
        /// NewCardChoiceMenu that this was the chosen card, so that it will be added to the
        /// deck.
        /// 
        /// Updated to include RemoveCardChoiceMenu, which has the same result except that
        /// the card will instead be removed from the deck.
        /// </summary>
        public override void onClick()
        {
            if (_cardChoiceMenu.GetType() == typeof(Menus.NewCardChoiceMenu))
            {
                ((Menus.NewCardChoiceMenu)_cardChoiceMenu).chooseCard(this);
            }
            else if (_cardChoiceMenu.GetType() == typeof(Menus.RemoveCardChoiceMenu))
            {
                ((Menus.RemoveCardChoiceMenu)_cardChoiceMenu).chooseCard(this);
            }
            else if (_cardChoiceMenu.GetType() == typeof(Menus.CombatCardChoiceMenu))
            {
                ((Menus.CombatCardChoiceMenu)_cardChoiceMenu).chooseCard(this);
            }

            onHoverEnd();
        }
    }
}
