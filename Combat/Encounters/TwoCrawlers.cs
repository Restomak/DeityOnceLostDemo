using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Encounters
{
    class TwoCrawlers : Encounter
    {
        public const int CRAWLER_1_LOC_X = 960;
        public const int CRAWLER_1_LOC_Y = 350;
        public const int CRAWLER_2_LOC_X = 1160;
        public const int CRAWLER_2_LOC_Y = 350;

        /// <summary>
        /// A simple encounter of two Crawlers
        /// </summary>
        public TwoCrawlers()
        {

        }

        public override void initialize()
        {
            _enemies.Add(new Enemies.Crawler(CRAWLER_1_LOC_X, CRAWLER_1_LOC_Y));
            _enemies.Add(new Enemies.Crawler(CRAWLER_2_LOC_X, CRAWLER_2_LOC_Y));
        }
    }
}
