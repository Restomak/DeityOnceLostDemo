using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Enemies
{
    /// <summary>
    /// This enemy is used for testing purposes only. Maybe it'll get repurposed later, but for now this enemy's purpose is getting through content quickly and painlessly so that I can test things locked behind combat
    /// </summary>
    class LabTestSlime : Enemy
    {
        public const String NAME = "Lab Test Slime";
        public const int HITPOINTS_MIN = 6;
        public const int HITPOINTS_MAX = 9;
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

        public LabTestSlime() : base(NAME, Game1.randint(HITPOINTS_MIN, HITPOINTS_MAX), new AIPatterns.SimpleSlowRoller(BUFF_STRENGTH_GAIN, BUFF_INTERVAL_MIN, BUFF_INTERVAL_MAX), Game1.pic_enemy_labTestSlime, WIDTH, HEIGHT)
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
