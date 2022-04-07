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
        /// Damage taken is affected by resilience by default. Things that bypass resilience should set affectedByResilience to false
        /// </summary>
        public virtual void takeDamage(int damage, bool affectedByResilience = true)
        {
            if (affectedByResilience)
            {
                damage -= _resilience;
            }

            _currentHP -= damage;
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
    }
}
