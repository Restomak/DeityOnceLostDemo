using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.CommonCards
{
    /// <summary>
    /// Snap Slap | Common
    ///     (Costs 0 Divinity)
    ///     (Empowered)
    /// Deal 3 damage.
    /// Draw 2 cards.
    /// </summary>
    class SnapSlap_Empowered : RegularCards.CommonCards.SnapSlap
    {
        public const int EMPOWERED_CARD_DRAW = 2;

        public SnapSlap_Empowered() : base(EMPOWERED_CARD_DRAW)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new SnapSlap_Empowered();
        }
    }
}
