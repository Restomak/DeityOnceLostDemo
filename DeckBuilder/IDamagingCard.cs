using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    /// <summary>
    /// Interface for helping create cards that deal damage.
    /// </summary>
    interface IDamagingCard
    {
        int iDamage
        {
            get;
        }

        void iDealDamage();
    }
}
