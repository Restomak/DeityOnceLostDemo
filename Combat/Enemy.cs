using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Combat
{
    abstract class Enemy : Unit
    {
        public const int DEFAULT_AOE_DAMAGE = 5;
        public const int DEFAULT_BUFF_STRENGTH = 2;
        public const int DEFAULT_DEBUFF_RESILIENCE = -1;

        protected AIPattern _aiPattern;
        public Texture2D _texture;

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
        public Enemy(String name, int hitPoints, AIPattern aiPattern, Texture2D texture) : base(name, hitPoints, true)
        {
            _aiPattern = aiPattern;
            _texture = texture;
        }

        public AIPattern getAIPattern()
        {
            return _aiPattern;
        }

        //Abstracts
        public abstract int getBasicDamage_noStrength();
        public abstract int getBasicDefense();



        /// <summary>
        /// Adds strength to getBasicDamage so that it's not done there
        /// </summary>
        public virtual int getRegularDamage()
        {
            return getBasicDamage_noStrength() + _strength;
        }

        /// <summary>
        /// Defaults to an attack that deals half damage
        /// </summary>
        public virtual int getLightDamage()
        {
            return getBasicDamage_noStrength() / 2 + _strength;
        }

        /// <summary>
        /// Defaults to an attack that deals 1.5x damage
        /// </summary>
        public virtual int getHeavyDamage()
        {
            return (int)((double)getBasicDamage_noStrength() * 3.0 / 2.0) + _strength;
        }

        /// <summary>
        /// Defaults to basic damage
        /// </summary>
        public virtual int getMultiAttackDamage()
        {
            return getBasicDamage_noStrength() + _strength;
        }

        /// <summary>
        /// Defaults to the Enemy-defined amount of damage
        /// </summary>
        public virtual int aoeDamage()
        {
            return DEFAULT_AOE_DAMAGE + _strength;
        }
    }
}
