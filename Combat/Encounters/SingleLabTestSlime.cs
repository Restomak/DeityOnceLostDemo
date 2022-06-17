using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Encounters
{
    class SingleLabTestSlime : Encounter
    {
        /// <summary>
        /// NOTE: this is for testing purposes only! The slime is balanced to be a quick kill that can hit hard if needed.
        /// </summary>
        public SingleLabTestSlime()
        {

        }

        public override void initialize()
        {
            _enemies.Add(new Enemies.LabTestSlime());
        }
    }
}
