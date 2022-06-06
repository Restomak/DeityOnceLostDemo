using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat
{
    public class PartyMember : Unit
    {
        Characters.Hero _hero;

        public PartyMember(Characters.Hero hero) : base(hero.getName(), hero.getMaxHP())
        {
            _hero = hero;
        }

        //Getters
        public Characters.Hero getHero()
        {
            return _hero;
        }
        
        //Unit Overrides
        public override void resetStrength()
        {
            _strength = _hero.getDefaultStrength();
        }
        public override void resetDexterity()
        {
            _dexterity = _hero.getDefaultDexterity();
        }
        public override void resetResilience()
        {
            _resilience = _hero.getDefaultResilience();
        }
    }
}
