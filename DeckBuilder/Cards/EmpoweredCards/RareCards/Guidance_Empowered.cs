using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.RareCards
{
    /// <summary>
    /// Guidance | Rare
    ///     (Costs 0 Divinity)
    ///     (Empowered)
    /// Draw 1 card and set its Divinity cost to 0 for the turn.
    /// </summary>
    class Guidance_Empowered : RegularCards.RareCards.Guidance
    {
        public const int EMPOWERED_DIV_COST = 0;

        public Guidance_Empowered() : base(EMPOWERED_DIV_COST)
        {

        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new Guidance_Empowered();
        }
    }
}
