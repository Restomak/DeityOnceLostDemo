using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.GodlyCards
{
    /// <summary>
    /// Fist of the Stars | Godly
    ///     (Costs 2 Divinity)
    ///     (Empowered)
    /// Deal 4 damage 9 times.
    /// </summary>
    class FistOfTheStars_Empowered : RegularCards.GodlyCards.FistOfTheStars
    {
        public const int EMPOWERED_NUM_HITS = 9;

        public FistOfTheStars_Empowered() : base(EMPOWERED_NUM_HITS)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new FistOfTheStars_Empowered();
        }
    }
}
