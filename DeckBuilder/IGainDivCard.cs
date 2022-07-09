using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    /// <summary>
    /// Interface for helping create cards that have Divinity gain.
    /// </summary>
    interface IGainDivCard
    {
        int iDivGain
        {
            get;
        }

        void iGainDivinity();
    }
}
