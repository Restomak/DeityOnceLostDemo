using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Encounters
{
    /// <summary>
    /// NOTE: this is for testing purposes only! The slime is balanced to be a quick kill that
    /// can hit hard if needed.
    /// </summary>
    class SingleLabTestSlime : Encounter
    {
        public const int SLIME_LOC_X = 1060;
        public const int SLIME_LOC_Y = 350;

        public const double CHANCE_OF_LESSER_SOULSTONE = 0.75;

        public SingleLabTestSlime()
        {

        }

        public override void initialize()
        {
            _enemies.Add(new Enemies.LabTestSlime(SLIME_LOC_X, SLIME_LOC_Y));
        }

        public override double chanceForLesserSoulstone()
        {
            return CHANCE_OF_LESSER_SOULSTONE;
        }
    }
}
