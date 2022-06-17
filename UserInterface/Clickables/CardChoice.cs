using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Clickables
{
    public class CardChoice : Clickable
    {
        DeckBuilder.Card _card;
        Menus.NewCardChoiceMenu _cardChoiceMenu;

        public CardChoice(DeckBuilder.Card card, Menus.NewCardChoiceMenu cardChoiceMenu)
        {
            _card = card;
            _cardChoiceMenu = cardChoiceMenu;
        }

        public DeckBuilder.Card getCard()
        {
            return _card;
        }



        /// <summary>
        /// Handles what happens in logic when the user hovers over the card. It will glow,
        /// similarly to a HandCard-- but it won't grow in size or anything.
        /// </summary>
        public override void onHover()
        {
            _hovered = true;
            Game1.setHoveredClickable(this);
        }

        public override void onHoverEnd()
        {
            _hovered = false;
            Game1.setHoveredClickable(null);
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
