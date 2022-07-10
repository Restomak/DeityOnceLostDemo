using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Enemies
{
    /// <summary>
    /// This enemy is used for testing purposes only. Maybe it'll get repurposed later, but
    /// for now this enemy's purpose is getting through content quickly and painlessly so
    /// that I can test things locked behind combat.
    /// </summary>
    class LabTestSlime : Enemy
    {
        public const String NAME = "Lab Test Slime";
        public const int HITPOINTS_MIN = 6;
        public const int HITPOINTS_MAX = 6;
        public const int DEFAULT_STR = 0;
        public const int DEFAULT_DEX = 0;
        public const int DEFAULT_RES = 0;
        public const int DEFAULT_DAMAGE = 50;
        public const int DEFAULT_LIGHT_DAMAGE = 3;
        public const int DEFAULT_HEAVY_DAMAGE = 100;
        public const int DEFAULT_DEFENSE = 3;
        public const int BUFF_STRENGTH_GAIN = 1;
        public const int BUFF_INTERVAL_MIN = 4;
        public const int BUFF_INTERVAL_MAX = 5;

        public const int WIDTH = 120;
        public const int HEIGHT = 90;

        public LabTestSlime(int drawX, int drawY) : base(NAME, Game1.randint(HITPOINTS_MIN, HITPOINTS_MAX), new AIPatterns.SimpleSlowRoller(BUFF_STRENGTH_GAIN, BUFF_INTERVAL_MIN, BUFF_INTERVAL_MAX),
            Game1.pic_enemy_labTestSlime, WIDTH, HEIGHT, drawX, drawY)
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
