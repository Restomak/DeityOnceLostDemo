using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Encounters
{
    /// <summary>
    /// An encounter in the tutorial dungeon that uses two Fanblade Guards. Moderately
    /// difficult; the champion is guaranteed to take some damage, especially if fought
    /// before the party members are acquired.
    /// </summary>
    class TwoFanblades : Encounter
    {
        public const int FANBLADE_1_LOC_X = 910;
        public const int FANBLADE_1_LOC_Y = 350;
        public const int FANBLADE_2_LOC_X = 1210;
        public const int FANBLADE_2_LOC_Y = 350;

        public const double CHANCE_OF_LESSER_SOULSTONE = 0.1;

        public TwoFanblades()
        {

        }

        public override void initialize()
        {
            _enemies.Add(new Enemies.FanbladeGuard(FANBLADE_1_LOC_X, FANBLADE_1_LOC_Y));
            _enemies.Add(new Enemies.FanbladeGuard(FANBLADE_2_LOC_X, FANBLADE_2_LOC_Y));
        }

        public override double chanceForLesserSoulstone()
        {
            return CHANCE_OF_LESSER_SOULSTONE;
        }
    }
}
