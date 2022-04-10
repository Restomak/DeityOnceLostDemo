using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Encounters
{
    class SingleFanblade : Encounter
    {
        /// <summary>
        /// A very simple encounter of a single Fanblade Guard and no others
        /// </summary>
        public SingleFanblade()
        {

        }

        public override void initialize()
        {
            _enemies.Add(new Enemies.FanbladeGuard());
        }
    }
}
