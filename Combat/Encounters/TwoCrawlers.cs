using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Encounters
{
    /// <summary>
    /// A simple encounter in the tutorial dungeon that uses two Crawlers. This is the
    /// easiest encounter of the tutorial dungeon, and should not be used after the
    /// third floor.
    /// </summary>
    class TwoCrawlers : Encounter
    {
        public const int CRAWLER_1_LOC_X = 960;
        public const int CRAWLER_1_LOC_Y = 350;
        public const int CRAWLER_2_LOC_X = 1160;
        public const int CRAWLER_2_LOC_Y = 350;

        public const double CHANCE_OF_LESSER_SOULSTONE = 0.01;

        public TwoCrawlers()
        {

        }

        public override void initialize()
        {
            _enemies.Add(new Enemies.Crawler(CRAWLER_1_LOC_X, CRAWLER_1_LOC_Y));
            _enemies.Add(new Enemies.Crawler(CRAWLER_2_LOC_X, CRAWLER_2_LOC_Y));
        }

        public override double chanceForLesserSoulstone()
        {
            return CHANCE_OF_LESSER_SOULSTONE;
        }
    }
}
