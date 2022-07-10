using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Enemies
{
    /// <summary>
    /// Basic medium difficulty enemy used in the tutorial dungeon, representing the
    /// dungeon's guards. Carries a polearm whose blades look like a fan.
    /// </summary>
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

        public FanbladeGuard(int drawX, int drawY) : base(NAME, Game1.randint(HITPOINTS_MIN, HITPOINTS_MAX), new AIPatterns.SimpleSlowRoller(BUFF_STRENGTH_GAIN, BUFF_INTERVAL_MIN, BUFF_INTERVAL_MAX),
            Game1.pic_enemy_fanbladeGuard, WIDTH, HEIGHT, drawX, drawY)
        {
            _aiPattern.setEnemy(this);
        }

        public override int getBasicDamage_noStrength()
        {
            return getDamageAffectedByBuffs(DEFAULT_DAMAGE);
        }
        public override int getBasicDefense()
        {
            return DEFAULT_DEFENSE + _dexterity;
        }
        public override int getLightDamage()
        {
            return getDamageAffectedByBuffs(DEFAULT_LIGHT_DAMAGE);
        }
        public override int getHeavyDamage()
        {
            return getDamageAffectedByBuffs(DEFAULT_HEAVY_DAMAGE);
        }
    }
}
