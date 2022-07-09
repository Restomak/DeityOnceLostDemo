using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    /// <summary>
    /// Interface for helping create cards that increase the player's defense.
    /// </summary>
    interface IDefendingCard
    {
        int iDefense
        {
            get;
        }

        void iGainDefense();
    }
}
