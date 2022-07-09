using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.RareCards
{
    /// <summary>
    /// Recurring Blows | Rare
    ///     (Costs 1 Divinity)
    ///     (Empowered)
    /// Deal 6 damage 3 times. Increase the number of attacks this
    ///     card does by 1 this combat.
    /// </summary>
    class RecurringBlows_Empowered : RegularCards.RareCards.RecurringBlows
    {
        public const int EMPOWERED_NUM_HITS = 3;

        public RecurringBlows_Empowered() : base(EMPOWERED_NUM_HITS)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new RecurringBlows_Empowered();
        }
    }
}
