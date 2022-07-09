using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.RareCards
{
    /// <summary>
    /// Sacrifice | Rare
    ///     (Costs 3 Blood)
    ///     (Empowered)
    /// Gain 3 Divinity.
    /// Draw 3 cards.
    /// </summary>
    class Sacrifice_Empowered : RegularCards.RareCards.Sacrifice
    {
        public const int EMPOWERED_CARD_DRAW = 3;

        public Sacrifice_Empowered() : base(EMPOWERED_CARD_DRAW)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new Sacrifice_Empowered();
        }
    }
}
