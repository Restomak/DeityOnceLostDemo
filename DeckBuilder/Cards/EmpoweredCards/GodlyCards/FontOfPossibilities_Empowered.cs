using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.GodlyCards
{
    /// <summary>
    /// Font of Possibilities | Godly
    ///     (Costs 0 Divinity)
    ///     (Empowered)
    /// Draw 4 cards.
    /// Empower 2 of the cards in your hand.
    /// </summary>
    class FontOfPossibilities_Empowered : RegularCards.GodlyCards.FontOfPossibilities
    {
        public const int EMPOWERED_DIV_COST = 0;

        public FontOfPossibilities_Empowered() : base(EMPOWERED_DIV_COST)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new FontOfPossibilities_Empowered();
        }
    }
}
