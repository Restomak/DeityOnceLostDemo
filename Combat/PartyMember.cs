using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat
{
    /// <summary>
    /// The unit class for party members, which stores and uses their Hero data.
    /// </summary>
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
        public Treasury.Treasures.PartyBuff getPartyMemberBuff()
        {
            return _hero.getPartyMemberBuff();
        }
        
        //Unit Overrides
        public override void resetStrength()
        {
            _strength = 0;
        }
        public override void resetDexterity()
        {
            _dexterity = 0;
        }
        public override void resetResilience()
        {
            _resilience = 0;
        }
        public override void resetBuffs()
        {
            _buffs.Clear();
        }
        public override void gainBuff(Buff buff)
        {
            //Not sure party members will ever gain buffs. we'll see

            //base.gainBuff(buff);
        }



        /// <summary>
        /// Determines the final damage after checking the target's debuffs.
        /// </summary>
        public static int getDamageAffectedByBuffs(int damage, Unit target)
        {
            if (target == null || !target.vulnerable())
            {
                return damage;
            }

            double newDamage = damage;

            if (target != null && target.vulnerable())
            {
                newDamage = newDamage * Buff.VULNERABLE_MODIFIER;
            }

            return (int)(Math.Round(newDamage)); ;
        }
    }
}
