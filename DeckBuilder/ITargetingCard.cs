using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    interface ITargetingCard
    {
        Combat.Unit target
        {
            get;
            set;
        }

        void selectTarget();
    }
}
