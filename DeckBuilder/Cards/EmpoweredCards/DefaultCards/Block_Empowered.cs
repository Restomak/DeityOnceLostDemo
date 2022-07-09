using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.DefaultCards
{
    /// <summary>
    /// Parry | Default
    ///     (Costs 1 Divinity)
    ///     (Empowered)
    /// Gain 8 defense.
    /// </summary>
    class Block_Empowered : RegularCards.DefaultCards.Block
    {
        public const int EMPOWERED_DEFENSE = 8;

        public Block_Empowered() : base(EMPOWERED_DEFENSE)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new Block_Empowered();
        }
    }
}
