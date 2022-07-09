using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Combat
{
    /// <summary>
    /// Abstract base unit class for enemies, used by Encounters and stores the enemy's AIPattern.
    /// </summary>
    public abstract class Enemy : Unit
    {
        public const int DEFAULT_AOE_DAMAGE = 5;
        public const int DEFAULT_BUFF_STRENGTH = 2;
        public const int DEFAULT_DEBUFF_RESILIENCE = -1;

        protected AIPattern _aiPattern;
        public Texture2D _texture;
        public int _width, _height, _drawX, _drawY;

        /// <summary>
        /// Every Enemy has one AIPattern, while each AIPattern can be associated with more than one enemy.
        /// 
        /// What AIPatterns are responsible for:
        /// • determining intents each round
        /// • determining the types of attacks that occur when attacks do occur
        /// • determining the types of buffs or debuffs that occur
        /// 
        /// What Enemies are responsible for:
        /// • supplying the raw stats (texture, name, hp, etc)
        /// • supplying damage & defense amounts
        /// </summary>
        public Enemy(String name, int hitPoints, AIPattern aiPattern, Texture2D texture, int width, int height, int drawX, int drawY) : base(name, hitPoints, true)
        {
            _aiPattern = aiPattern;
            _texture = texture;
            _width = width;
            _height = height;
            _drawX = drawX;
            _drawY = drawY;
        }

        public AIPattern getAIPattern()
        {
            return _aiPattern;
        }

        //Abstracts
        public abstract int getBasicDamage_noStrength();
        public abstract int getBasicDefense();

        

        public int getDamageAffectedByBuffs(int damage)
        {
            double newDamage = damage + _strength;

            if (!_feeble && !Game1.getChamp().vulnerable())
            {
                return (int)(Math.Round(newDamage));
            }

            if (_feeble)
            {
                newDamage = newDamage * Buff.FEEBLE_MODIFIER;
            }

            if (Game1.getChamp().vulnerable())
            {
                newDamage = newDamage * Buff.VULNERABLE_MODIFIER;
            }

            return (int)(Math.Round(newDamage));
        }

    

        public virtual int getRegularDamage()
        {
            if (!Game1.getChamp().feeble())
            {
                return getBasicDamage_noStrength();
            }
            else
            {
                return getBasicDamage_noStrength();
            }
        }

        /// <summary>
        /// Defaults to an attack that deals half damage
        /// </summary>
        public virtual int getLightDamage()
        {
            return getBasicDamage_noStrength() / 2;
        }

        /// <summary>
        /// Defaults to an attack that deals 1.5x damage
        /// </summary>
        public virtual int getHeavyDamage()
        {
            return (int)((double)getBasicDamage_noStrength() * 3.0 / 2.0);
        }

        /// <summary>
        /// Defaults to basic damage
        /// </summary>
        public virtual int getMultiAttackDamage()
        {
            return getBasicDamage_noStrength();
        }

        /// <summary>
        /// Defaults to the Enemy-defined amount of damage
        /// </summary>
        public virtual int aoeDamage()
        {
            return DEFAULT_AOE_DAMAGE;
        }
    }
}
