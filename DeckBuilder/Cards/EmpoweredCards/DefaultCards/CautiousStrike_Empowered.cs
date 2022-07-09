using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.DefaultCards
{
    /// <summary>
    /// Cautious Strike | Default
    ///     (Costs 0 Divinity)
    ///     (Empowered)
    /// Deal 3 damage.
    /// Gain 3 defense.
    /// </summary>
    class CautiousStrike_Empowered : RegularCards.DefaultCards.CautiousStrike
    {
        public const int EMPOWERED_COST = 0;

        public CautiousStrike_Empowered() : base(EMPOWERED_COST)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new CautiousStrike_Empowered();
        }
    }
}
