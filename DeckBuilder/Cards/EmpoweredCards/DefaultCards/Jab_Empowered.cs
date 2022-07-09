using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.DefaultCards
{
    /// <summary>
    /// Jab | Default
    ///     (Costs 1 Divinity)
    ///     (Empowered)
    /// Deal 10 damage.
    /// </summary>
    class Jab_Empowered : RegularCards.DefaultCards.Jab
    {
        public const int EMPOWERED_DAMAGE = 10;

        public Jab_Empowered() : base(EMPOWERED_DAMAGE)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new Jab_Empowered();
        }
    }
}
