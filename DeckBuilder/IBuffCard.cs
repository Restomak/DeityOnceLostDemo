﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    interface IBuffCard
    {
        int amount
        {
            get;
        }

        Combat.Unit.statType stat
        {
            get;
        }

        void applyBuff();
    }
}