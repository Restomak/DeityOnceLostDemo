using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.DefaultCards
{
    /// <summary>
    /// Kick | Default
    ///     (Costs 1 Divinity)
    ///     (Empowered)
    /// Deal 12 damage.
    /// </summary>
    class Kick_Empowered : RegularCards.DefaultCards.Kick
    {
        public const int EMPOWERED_DAMAGE = 12;

        public Kick_Empowered() : base(EMPOWERED_DAMAGE)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new Kick_Empowered();
        }
    }
}
