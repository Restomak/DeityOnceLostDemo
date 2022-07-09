using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Encounters
{
    /// <summary>
    /// A simple encounter in the tutorial dungeon that uses three Crawlers. Relatively
    /// easy, this is the standard difficulty of encounter in the dungeon until after
    /// floor 3.
    /// </summary>
    class ThreeCrawlers : Encounter
    {
        public const int CRAWLER_1_LOC_X = 860;
        public const int CRAWLER_1_LOC_Y = 350;
        public const int CRAWLER_2_LOC_X = 1060;
        public const int CRAWLER_2_LOC_Y = 350;
        public const int CRAWLER_3_LOC_X = 1260;
        public const int CRAWLER_3_LOC_Y = 350;

        public const double CHANCE_OF_LESSER_SOULSTONE = 0.015;

        /// <summary>
        /// A simple encounter of three Crawlers
        /// </summary>
        public ThreeCrawlers()
        {

        }

        public override void initialize()
        {
            _enemies.Add(new Enemies.Crawler(CRAWLER_1_LOC_X, CRAWLER_1_LOC_Y));
            _enemies.Add(new Enemies.Crawler(CRAWLER_2_LOC_X, CRAWLER_2_LOC_Y));
            _enemies.Add(new Enemies.Crawler(CRAWLER_3_LOC_X, CRAWLER_3_LOC_Y));
        }

        public override double chanceForLesserSoulstone()
        {
            return CHANCE_OF_LESSER_SOULSTONE;
        }
    }
}
