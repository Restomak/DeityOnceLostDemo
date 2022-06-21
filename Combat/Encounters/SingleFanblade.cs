using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Encounters
{
    class SingleFanblade : Encounter
    {
        public const int FANBLADE_LOC_X = 1060;
        public const int FANBLADE_LOC_Y = 350;

        /// <summary>
        /// A very simple encounter of a single Fanblade Guard and no others
        /// </summary>
        public SingleFanblade()
        {

        }

        public override void initialize()
        {
            _enemies.Add(new Enemies.FanbladeGuard(FANBLADE_LOC_X, FANBLADE_LOC_Y));
        }
    }
}
