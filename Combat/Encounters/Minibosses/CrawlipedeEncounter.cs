using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Encounters.Minibosses
{
    /// <summary>
    /// The Crawlipede miniboss of the tutorial dungeon.
    /// </summary>
    class CrawlipedeEncounter : Encounter
    {
        public const int CRAWLIPEDE_LOC_X = 1020;
        public const int CRAWLIPEDE_LOC_Y = 350;

        public CrawlipedeEncounter()
        {

        }

        public override void initialize()
        {
            _enemies.Add(new Enemies.Minibosses.Crawlipede(CRAWLIPEDE_LOC_X, CRAWLIPEDE_LOC_Y));
        }

        public override double chanceForLesserSoulstone()
        {
            return 0.0; //minibosses give greater soul stones
        }
    }
}
