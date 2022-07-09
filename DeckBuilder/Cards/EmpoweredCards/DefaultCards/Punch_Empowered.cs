using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.DefaultCards
{
    /// <summary>
    /// Punch | Default
    ///     (Costs 1 Divinity)
    ///     (Empowered)
    /// Deal 11 damage.
    /// </summary>
    class Punch_Empowered : RegularCards.DefaultCards.Punch
    {
        public const int EMPOWERED_DAMAGE = 11;

        public Punch_Empowered() : base(EMPOWERED_DAMAGE)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new Punch_Empowered();
        }
    }
}
