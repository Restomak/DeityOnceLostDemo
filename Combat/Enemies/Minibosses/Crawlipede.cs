using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Enemies.Minibosses
{
    /// <summary>
    /// Miniboss used in the tutorial dungeon, meant to be a much larger and more dangerous
    /// version of the dungeon's Crawlers.
    /// </summary>
    class Crawlipede : Enemy
    {
        public const String NAME = "Crawlipede";
        public const int HITPOINTS = 150;
        public const int DEFAULT_STR = 0;
        public const int DEFAULT_DEX = 0;
        public const int DEFAULT_RES = 0;
        public const int DEFAULT_DAMAGE = 18;
        public const int DEFAULT_LIGHT_DAMAGE = 8;
        public const int DEFAULT_DEFENSE = 0; //it doesn't block

        public const int WIDTH = 240;
        public const int HEIGHT = 320;

        public Crawlipede(int drawX, int drawY) : base(NAME, HITPOINTS, new AIPatterns.Minibosses.CrawlipedeAI(),
            Game1.pic_enemy_crawler, WIDTH, HEIGHT, drawX, drawY)
        {
            resetStrength();
            resetDexterity();
            resetResilience();

            _aiPattern.setEnemy(this);
        }

        public override void resetStrength()
        {
            _strength = DEFAULT_STR;
        }
        public override void resetDexterity()
        {
            _dexterity = DEFAULT_DEX;
        }
        public override void resetResilience()
        {
            _resilience = DEFAULT_RES;
        }
        public override void resetBuffs()
        {
            _buffs.Clear();
        }

        public override int getBasicDamage_noStrength()
        {
            return getDamageAffectedByBuffs(DEFAULT_DAMAGE);
        }
        public override int getLightDamage()
        {
            return getDamageAffectedByBuffs(DEFAULT_LIGHT_DAMAGE);
        }
        public override int getBasicDefense()
        {
            return DEFAULT_DEFENSE + _dexterity;
        }
    }
}
