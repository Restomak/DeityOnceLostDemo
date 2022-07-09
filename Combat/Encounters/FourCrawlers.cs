using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Encounters
{
    /// <summary>
    /// An encounter in the tutorial dungeon that uses four Crawlers. Meant to be used
    /// as a moderately difficult encounter until after floor 3.
    /// </summary>
    class FourCrawlers : Encounter
    {
        public const int CRAWLER_1_LOC_X = 810;
        public const int CRAWLER_1_LOC_Y = 350;
        public const int CRAWLER_2_LOC_X = 990;
        public const int CRAWLER_2_LOC_Y = 350;
        public const int CRAWLER_3_LOC_X = 1170;
        public const int CRAWLER_3_LOC_Y = 350;
        public const int CRAWLER_4_LOC_X = 1350;
        public const int CRAWLER_4_LOC_Y = 350;

        public const double CHANCE_OF_LESSER_SOULSTONE = 0.02;

        /// <summary>
        /// A simple encounter of three Crawlers
        /// </summary>
        public FourCrawlers()
        {

        }

        public override void initialize()
        {
            _enemies.Add(new Enemies.Crawler(CRAWLER_1_LOC_X, CRAWLER_1_LOC_Y));
            _enemies.Add(new Enemies.Crawler(CRAWLER_2_LOC_X, CRAWLER_2_LOC_Y));
            _enemies.Add(new Enemies.Crawler(CRAWLER_3_LOC_X, CRAWLER_3_LOC_Y));
            _enemies.Add(new Enemies.Crawler(CRAWLER_4_LOC_X, CRAWLER_4_LOC_Y));
        }

        public override double chanceForLesserSoulstone()
        {
            return CHANCE_OF_LESSER_SOULSTONE;
        }
    }
}
