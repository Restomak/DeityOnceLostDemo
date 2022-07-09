using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.CommonCards
{
    /// <summary>
    /// Gather Strength | Common
    ///     (Costs 1 Divinity)
    ///     (Empowered)
    /// Gain 5 defense.
    /// Gain 1 Strength.
    /// </summary>
    class GatherStrength_Empowered : RegularCards.CommonCards.GatherStrength
    {
        public const int EMPOWERED_DEFENSE = 5;

        public GatherStrength_Empowered() : base(EMPOWERED_DEFENSE)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new GatherStrength_Empowered();
        }
    }
}
