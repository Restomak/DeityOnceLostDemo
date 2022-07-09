using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    /// <summary>
    /// Interface for helping create cards that buff one of the three unit stats:
    /// Strength, Dexterity, or Resilience.
    /// </summary>
    interface IStatBuffCard
    {
        int iStatBuffAmount
        {
            get;
        }

        Combat.Unit.statType iBuffStat
        {
            get;
        }

        void iApplyBuff();
    }
}
