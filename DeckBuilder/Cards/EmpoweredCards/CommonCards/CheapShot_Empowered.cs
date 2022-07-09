using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.CommonCards
{
    /// <summary>
    /// Cheap Shot | Common
    ///     (Costs 0 Divinity)
    ///     (Empowered)
    /// Deal 4 damage.
    /// Apply 2 Feeble.
    /// </summary>
    class CheapShot_Empowered : RegularCards.CommonCards.CheapShot
    {
        public const int EMPOWERED_DURATION = 2;

        public CheapShot_Empowered() : base(EMPOWERED_DURATION)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new CheapShot_Empowered();
        }
    }
}
