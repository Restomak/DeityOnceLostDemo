using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    interface IDebuffCard
    {
        Combat.Buff.buffType buffType
        {
            get;
        }
        int duration
        {
            get;
        }
        int amount
        {
            get;
        }
        bool hasDuration
        {
            get;
        }
        bool hasAmount
        {
            get;
        }

        void applyDebuff();
    }
}
