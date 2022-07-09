using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.CommonCards
{
    /// <summary>
    /// Vicious Slam | Common
    ///     (Costs 2 Divinity)
    ///     (Empowered)
    /// Deal 15 damage.
    /// Apply 3 Vulnerable.
    /// </summary>
    class ViciousSlam_Empowered : RegularCards.CommonCards.ViciousSlam
    {
        public const int EMPOWERED_DAMAGE = 15;
        public const int EMPOWERED_DEBUFF_DURATION = 3;

        public ViciousSlam_Empowered() : base(EMPOWERED_DAMAGE, EMPOWERED_DEBUFF_DURATION)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new ViciousSlam_Empowered();
        }
    }
}
