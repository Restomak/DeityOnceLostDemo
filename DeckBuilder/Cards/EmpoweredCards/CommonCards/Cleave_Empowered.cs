using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.CommonCards
{
    /// <summary>
    /// Cleave | Common
    ///     (Costs 1 Divinity)
    ///     (Empowered)
    /// Deal 8 damage to all enemies.
    /// </summary>
    class Cleave_Empowered : RegularCards.CommonCards.Cleave
    {
        public const int EMPOWERED_DAMAGE = 8;

        public Cleave_Empowered() : base(EMPOWERED_DAMAGE)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new Cleave_Empowered();
        }
    }
}
