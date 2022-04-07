using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat
{
    abstract class Enemy : Unit
    {
        AIPattern _aiPattern;

        public Enemy(String name, int hitPoints, AIPattern aiPattern) : base(name, hitPoints, true)
        {
            _aiPattern = aiPattern;
        }
    }
}
