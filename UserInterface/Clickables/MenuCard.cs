using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Clickables
{
    /// <summary>
    /// Used by CardMenus to display all of the cards in a collection on screen. Does
    /// nothing when clicked, though the child classes of this override that.
    /// </summary>
    public class MenuCard : Clickable
    {
        protected DeckBuilder.Card _card;

        public MenuCard(DeckBuilder.Card card)
        {
            _card = card;

            _extraInfo = _card.getHoverInfo();
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
        /// Handles what happens in logic when the user clicks on the card. For the base
        /// MenuCard, nothing will happen.
        /// </summary>
        public override void onClick()
        {
            onHoverEnd();
        }

        /// <summary>
        /// Handles what happens in logic when the user clicks on the card. Opens up the
        /// CardInspectionMenu for the card
        /// </summary>
        public override void onRightClick()
        {
            onHoverEnd();
            Game1.addToMenus(new Menus.CardInspectionMenu(_card));
        }
    }
}
