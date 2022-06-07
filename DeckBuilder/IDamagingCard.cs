using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    interface IDamagingCard
    {
        int damage
        {
            get;
        }

        void dealDamage();
    }
}
