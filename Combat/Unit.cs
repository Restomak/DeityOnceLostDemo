using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat
{
    /// <summary>
    /// Abstract base class for all units: the champion, party members, and all enemies.
    /// </summary>
    public abstract class Unit
    {
        public enum statType
        {
            strength,
            dexterity,
            resilience
        }

        protected String _name;
        protected int _currentHP, _maxHP;
        protected bool _isEnemy, _downed;
        protected int _defense;
        protected List<Buff> _buffs;
        
        //stats
        protected int _strength, _dexterity, _resilience;
        protected bool _feeble, _sluggish, _vulnerable;

        public Unit(String name, int hitPoints, bool enemy = false)
        {
            _buffs = new List<Buff>();

            _name = name;
            _currentHP = hitPoints;
            _maxHP = hitPoints;
            _isEnemy = enemy;
            _downed = false;
            _feeble = false;
            _sluggish = false;
            _vulnerable = false;

            resetStrength();
            resetDexterity();
            resetResilience();
            resetBuffs();
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
        public bool feeble()
        {
            return _feeble;
        }
        public bool sluggish()
        {
            return _sluggish;
        }
        public bool vulnerable()
        {
            return _vulnerable;
        }
        public List<Buff> getBuffs()
        {
            return _buffs;
        }

        //Handled differently depending on children
        public virtual void resetBuffs()
        {
            _buffs.Clear();
        }
        public virtual void resetStrength()
        {
            _strength = 0;
        }
        public virtual void resetDexterity()
        {
            _dexterity = 0;
        }
        public virtual void resetResilience()
        {
            _resilience = 0;
        }
        public virtual void onDamageTaken() { }


        
        /// <summary>
        /// Unit gains defense equal to amount + dexterity unless useDexterity is made false.
        /// Buffs are applied here.
        /// </summary>
        public void gainDefense(int amount, bool useDexterity = true)
        {
            if (!_sluggish)
            {
                _defense += amount;
                if (useDexterity)
                {
                    _defense += _dexterity;
                }
            }
            else
            {
                if (useDexterity)
                {
                    _defense += (int)(Math.Round((double)(amount + _dexterity) * Buff.SLUGGISH_MODIFIER));
                }
                else
                {
                    _defense += (int)(Math.Round((double)(amount) * Buff.SLUGGISH_MODIFIER));
                }
            }
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
            //Affected by defense first
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
            }

            //Affected by resilience if it passes defense
            if (affectedByResilience)
            {
                damage -= _resilience;
            }

            //Check if they actually took any damage
            if (damage > 0)
            {
                onDamageTaken();

                _currentHP -= damage;

                //Check if they die
                if (_currentHP <= 0)
                {
                    _currentHP = 0;
                    _downed = true;
                }
            }
        }

        /// <summary>
        /// Healing is currently unaffected by anything, but will eventually be affected by debuffs/blessings/curses/etc.
        /// </summary>
        public virtual void heal(int amount)
        {
            _currentHP += amount;
            if (_currentHP > _maxHP)
            {
                _currentHP = _maxHP;
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

        /// <summary>
        /// Called at the start of a new turn. Checks all buffs/debuffs and if they have a duration,
        /// lowers it by one. Removes any buffs/debuffs that have reached 0 duration.
        /// </summary>
        public virtual void newTurnLowerBuffs()
        {
            List<Buff> remove = new List<Buff>();

            for (int i = 0; i < _buffs.Count; i++)
            {
                if (_buffs[i].hasDuration())
                {
                    if (_buffs[i].waitATurnBeforeDecrease())
                    {
                        _buffs[i].waitedATurn();
                    }
                    else
                    {
                        _buffs[i].affectDuration(-1);
                    }
                }

                if (_buffs[i].hasDuration() && _buffs[i].getDuration() <= 0 || _buffs[i].hasAmount() && _buffs[i].getAmount() == 0)
                {
                    remove.Add(_buffs[i]);
                }
            }

            //Remove buffs
            for (int ri = 0; ri < remove.Count; ri++)
            {
                _buffs.Remove(remove[ri]);

                //Check to clear status effects
                if (remove[ri].getType() == Buff.buffType.feeble)
                {
                    _feeble = false;
                    for (int bi = 0; bi < _buffs.Count; bi++)
                    {
                        if (_buffs[bi].getType() == Buff.buffType.feeble)
                        {
                            _feeble = true; //there's another feeble in there
                        }
                    }
                }
                else if (remove[ri].getType() == Buff.buffType.sluggish)
                {
                    _sluggish = false;
                    for (int bi = 0; bi < _buffs.Count; bi++)
                    {
                        if (_buffs[bi].getType() == Buff.buffType.sluggish)
                        {
                            _sluggish = true; //there's another sluggish in there
                        }
                    }
                }
                else if (remove[ri].getType() == Buff.buffType.vulnerable)
                {
                    _vulnerable = false;
                    for (int bi = 0; bi < _buffs.Count; bi++)
                    {
                        if (_buffs[bi].getType() == Buff.buffType.vulnerable)
                        {
                            _vulnerable = true; //there's another vulnerable in there
                        }
                    }
                }
            }
        }
        
        public virtual void gainBuff(Buff buff)
        {
            if (buff.getType() == Buff.buffType.strength)
            {
                _strength += buff.getAmount();
            }
            else if (buff.getType() == Buff.buffType.dexterity)
            {
                _dexterity += buff.getAmount();
            }
            else if (buff.getType() == Buff.buffType.resilience)
            {
                _resilience += buff.getAmount();
            }
            else if (buff.getType() == Buff.buffType.feeble)
            {
                _feeble = true;
            }
            else if (buff.getType() == Buff.buffType.sluggish)
            {
                _sluggish = true;
            }
            else if (buff.getType() == Buff.buffType.vulnerable)
            {
                _vulnerable = true;
            }

            _buffs = Buff.addBuff(_buffs, buff);
        }
    }
}
