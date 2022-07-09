using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Encounters
{
    /// <summary>
    /// A simple encounter in the tutorial dungeon that uses a single Fanblade Guard.
    /// Relatively easy, this is the standard difficulty of encounter in the dungeon
    /// until after floor 3.
    /// </summary>
    class SingleFanblade : Encounter
    {
        public const int FANBLADE_LOC_X = 1060;
        public const int FANBLADE_LOC_Y = 350;

        public const double CHANCE_OF_LESSER_SOULSTONE = 0.05;

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

        public override double chanceForLesserSoulstone()
        {
            return CHANCE_OF_LESSER_SOULSTONE;
        }
    }
}
