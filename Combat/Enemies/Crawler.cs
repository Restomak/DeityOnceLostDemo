using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Enemies
{
    class Crawler : Enemy
    {
        public const String NAME = "Crawler";
        public const int HITPOINTS_MIN = 18;
        public const int HITPOINTS_MAX = 22;
        public const int DEFAULT_STR = 0;
        public const int DEFAULT_DEX = 0;
        public const int DEFAULT_RES = 0;
        public const int DEFAULT_DAMAGE = 6;
        public const int DEFAULT_HEAVY_DAMAGE = 9;
        public const int DEFAULT_DEFENSE = 5;

        public const int WIDTH = 120;
        public const int HEIGHT = 160;
        
        public const int FEEBLE_AMOUNT = 1;

        public Crawler(int drawX, int drawY) : base(NAME, Game1.randint(HITPOINTS_MIN, HITPOINTS_MAX), new AIPatterns.AttackAndEnfeeble(FEEBLE_AMOUNT, false),
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
            _strength = DEFAULT_DEX;
        }
        public override void resetResilience()
        {
            _strength = DEFAULT_RES;
        }

        public override int getBasicDamage_noStrength()
        {
            return DEFAULT_DAMAGE;
        }
        public override int getHeavyDamage()
        {
            return DEFAULT_HEAVY_DAMAGE + _strength;
        }
        public override int getBasicDefense()
        {
            return DEFAULT_DEFENSE + _dexterity;
        }
    }
}
