using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.CommonCards
{
    /// <summary>
    /// Dodge and Roll | Common
    ///     (Costs 1 Divinity)
    ///     (Empowered)
    /// Gain 7 defense.
    /// Draw 2 cards.
    /// </summary>
    class DodgeAndRoll_Empowered : RegularCards.CommonCards.DodgeAndRoll
    {
        public const int EMPOWERED_CARD_DRAW = 2;
        public const int EMPOWERED_DEFENSE = 7;

        public DodgeAndRoll_Empowered() : base(EMPOWERED_CARD_DRAW, EMPOWERED_DEFENSE)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new DodgeAndRoll_Empowered();
        }
    }
}
