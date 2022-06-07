using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat
{
    public abstract class Unit
    {
        protected String _name;
        protected int _currentHP, _maxHP;
        protected bool _isEnemy, _downed;
        protected int _defense;
        
        //stats
        protected int _strength, _dexterity, _resilience;

        public Unit(String name, int hitPoints, bool enemy = false)
        {
            _name = name;
            _currentHP = hitPoints;
            _maxHP = hitPoints;
            _isEnemy = enemy;
            _downed = false;

            resetStrength();
            resetDexterity();
            resetResilience();
        }

        //Getters
        public String getName()
        {
            return _name;
        }
        public int getCurrentHP()
        {
            return _currentHP;
        }
        public int getMaxHP()
        {
            return _maxHP;
        }
        public int getDefense()
        {
            return _defense;
        }
        public bool getDowned()
        {
            return _downed;
        }
        public bool isEnemy()
        {
            return _isEnemy;
        }
        
        //Handled differently depending on children
        public abstract void resetStrength();
        public abstract void resetDexterity();
        public abstract void resetResilience();
        public virtual void onDamageTaken() { }




        /// <summary>
        /// Unit gains defense equal to amount + dexterity unless useDexterity is made false
        /// </summary>
        public void gainDefense(int amount, bool useDexterity = true)
        {
            _defense += amount + _dexterity;
        }

        /// <summary>
        /// Resets defense to zero, used at the start of the unit's turn
        /// </summary>
        public virtual void resetDefense()
        {
            _defense = 0;
        }

        /// <summary>
        /// Damage taken is affected by resilience by default. Things that bypass resilience should set affectedByResilience to false
        /// </summary>
        public virtual void takeDamage(int damage, bool affectedByResilience = true)
        {
            if (affectedByResilience)
            {
                damage -= _resilience;
            }

            if (damage > 0)
            {
                if (_defense > 0)
                {
                    _defense -= damage;

                    if (_defense < 0)
                    {
                        damage = -_defense;
                    }
                    else
                    {
                        damage = 0;
                    }
                }
                
                _currentHP -= damage;
            }
            if (_currentHP <= 0)
            {
                _currentHP = 0;
                _downed = true;
            }
            else if (damage > 0)
            {
                onDamageTaken(); //only call if they actually took damage
            }
        }

        /// <summary>
        /// Meant for buffs and debuffs (accepts negative amounts as well). Strength increases or
        /// decreases the amount of damage dealt by attacks.
        /// </summary>
        public void affectStrength(int amount)
        {
            _strength += amount;
        }

        /// <summary>
        /// Meant for buffs and debuffs (accepts negative amounts as well). Dexterity increases or
        /// decreases the amount of defense gained when defending.
        /// </summary>
        public void affectDexterity(int amount)
        {
            _dexterity += amount;
        }

        /// <summary>
        /// Meant for buffs and debuffs (accepts negative amounts as well). Resilience affects the
        /// amount of damage taken from each hit; a Resilience of 1 takes 1 damage off each attack
        /// dealt against the unit while a Resilience of -1 adds 1 damage to each attack dealt
        /// against the unit.
        /// </summary>
        public void affectResilience(int amount)
        {
            _resilience += amount;
        }
    }
}
