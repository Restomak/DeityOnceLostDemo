using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.EpicCards
{
    /// <summary>
    /// Time Shift | Epic
    ///     (Costs 0 Divinity)
    ///     (Empowered)
    /// Gain 2 Divinity.
    /// Trigger all party member abilities.
    /// Dissipates.
    /// </summary>
    class TimeShift_Empowered : RegularCards.EpicCards.TimeShift
    {
        public const int EMPOWERED_DIV_GAIN = 2;

        public TimeShift_Empowered() : base(EMPOWERED_DIV_GAIN)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new TimeShift_Empowered();
        }
    }
}
