using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    /// <summary>
    /// Interface for helping create cards that empower other cards in the player's
    /// hand.
    /// </summary>
    interface IHandEmpower
    {
        int iNumCards
        {
            get;
        }

        void iHandEmpower();
    }
}
