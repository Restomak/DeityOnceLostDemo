using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.CommonCards
{
    /// <summary>
    /// Heroic Blow | Common
    ///     (Costs 2 Divinity)
    ///     (Empowered)
    /// Deal 28 damage.
    /// </summary>
    class HeroicBlow_Empowered : RegularCards.CommonCards.HeroicBlow
    {
        public const int EMPOWERED_DAMAGE = 28;

        public HeroicBlow_Empowered() : base(EMPOWERED_DAMAGE)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new HeroicBlow_Empowered();
        }
    }
}
