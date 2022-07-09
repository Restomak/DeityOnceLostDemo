using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.CommonCards
{
    /// <summary>
    /// Rolling Kick | Common
    ///     (Costs 1 Divinity)
    ///     (Empowered)
    /// Deal 9 damage.
    /// Draw 2 cards.
    /// </summary>
    class RollingKick_Empowered : RegularCards.CommonCards.RollingKick
    {
        public const int EMPOWERED_CARD_DRAW = 2;
        public const int EMPOWERED_DAMAGE = 9;

        public RollingKick_Empowered() : base(EMPOWERED_CARD_DRAW, EMPOWERED_DAMAGE)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new RollingKick_Empowered();
        }
    }
}
