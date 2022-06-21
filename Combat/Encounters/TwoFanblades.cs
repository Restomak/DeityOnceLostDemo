using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Encounters
{
    class TwoFanblades : Encounter
    {
        public const int FANBLADE_1_LOC_X = 910;
        public const int FANBLADE_1_LOC_Y = 350;
        public const int FANBLADE_2_LOC_X = 1210;
        public const int FANBLADE_2_LOC_Y = 350;

        /// <summary>
        /// An encounter including two Fanblade Guards. Moderately difficult
        /// </summary>
        public TwoFanblades()
        {

        }

        public override void initialize()
        {
            _enemies.Add(new Enemies.FanbladeGuard(FANBLADE_1_LOC_X, FANBLADE_1_LOC_Y));
            _enemies.Add(new Enemies.FanbladeGuard(FANBLADE_2_LOC_X, FANBLADE_2_LOC_Y));
        }
    }
}
