using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Enemies
{
    class FanbladeGuard : Enemy
    {
        public const String NAME = "Fanblade Guard";
        public const int HITPOINTS_MIN = 40;
        public const int HITPOINTS_MAX = 50;
        public const int DEFAULT_STR = 0;
        public const int DEFAULT_DEX = 0;
        public const int DEFAULT_RES = 0;
        public const int DEFAULT_DAMAGE = 12;
        public const int DEFAULT_LIGHT_DAMAGE = 8;
        public const int DEFAULT_HEAVY_DAMAGE = 14;
        public const int DEFAULT_DEFENSE = 9;
        public const int BUFF_STRENGTH_GAIN = 3;
        public const int BUFF_INTERVAL_MIN = 2;
        public const int BUFF_INTERVAL_MAX = 3;

        public const int WIDTH = 180;
        public const int HEIGHT = 240;

        public FanbladeGuard() : base(NAME, Game1.randint(HITPOINTS_MIN, HITPOINTS_MAX), new AIPatterns.SimpleSlowRoller(BUFF_STRENGTH_GAIN, BUFF_INTERVAL_MIN, BUFF_INTERVAL_MAX), Game1.pic_enemy_fanbladeGuard, WIDTH, HEIGHT)
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
        public override int getBasicDefense()
        {
            return DEFAULT_DEFENSE + _dexterity;
        }
        public override int getLightDamage()
        {
            return DEFAULT_LIGHT_DAMAGE + _strength;
        }
        public override int getHeavyDamage()
        {
            return DEFAULT_HEAVY_DAMAGE + _strength;
        }
    }
}
