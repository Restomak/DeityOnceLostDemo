using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.CommonCards
{
    /// <summary>
    /// First Strike | Common
    ///     (Costs 1 Divinity)
    ///     (Empowered)
    /// Deal 11 damage.
    /// Damage is doubled if target is full HP.
    /// </summary>
    class FirstStrike_Empowered : RegularCards.CommonCards.FirstStrike
    {
        public const int EMPOWERED_DAMAGE = 11;

        public FirstStrike_Empowered() : base(EMPOWERED_DAMAGE)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new FirstStrike_Empowered();
        }
    }
}
