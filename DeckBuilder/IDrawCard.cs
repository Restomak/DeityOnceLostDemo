using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    interface IDrawCard
    {
        int amount
        {
            get;
        }

        void cardDraw();
    }
}
