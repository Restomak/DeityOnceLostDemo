using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    /// <summary>
    /// Interface for helping create cards that apply a buff or debuff.
    /// </summary>
    interface IDebuffCard
    {
        Combat.Buff.buffType iBuffType
        {
            get;
        }
        int iBuffDuration
        {
            get;
        }
        int iBuffAmount
        {
            get;
        }
        bool iHasDuration
        {
            get;
        }
        bool iHasAmount
        {
            get;
        }

        void iApplyDebuff();
    }
}
