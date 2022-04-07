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
        int _damageAmount, _defendAmount;

        public Enemy(String name, int hitPoints, AIPattern aiPattern) : base(name, hitPoints, true)
        {
            _aiPattern = aiPattern;
        }

        public AIPattern getAIPattern()
        {
            return _aiPattern;
        }

        public int getDamageAmount()
        {
            return _damageAmount + _strength;
        }
        public int getDefendAmount()
        {
            return _defendAmount + _dexterity;
        }
    }
}
