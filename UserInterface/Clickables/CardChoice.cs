using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Clickables
{
    public class CardChoice : MenuCard
    {
        Menus.NewCardChoiceMenu _cardChoiceMenu;

        public CardChoice(DeckBuilder.Card card, Menus.NewCardChoiceMenu cardChoiceMenu) : base (card)
        {
            _cardChoiceMenu = cardChoiceMenu;
        }
        


        /// <summary>
        /// Handles what happens in logic when the user clicks on the card. It will tell the
        /// NewCardChoiceMenu that this was the chosen card, so that it will be added to the
        /// deck.
        /// </summary>
        public override void onClick()
        {
            _cardChoiceMenu.chooseCard(this);
            onHoverEnd();
        }
    }
}
