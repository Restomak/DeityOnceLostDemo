using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.Enemies
{
    /// <summary>
    /// Basic easy difficulty enemy used in the tutorial dungeon. A small green and
    /// creepy many-legged monster that spits debilitating goop and has small but
    /// sharp teeth.
    /// </summary>
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

        public const int WIDTH = 120;
        public const int HEIGHT = 160;
        
        public const int FEEBLE_AMOUNT = 1;

        public Crawler(int drawX, int drawY) : base(NAME, Game1.randint(HITPOINTS_MIN, HITPOINTS_MAX), new AIPatterns.AttackAndEnfeeble(FEEBLE_AMOUNT, false),
            Game1.pic_enemy_crawler, WIDTH, HEIGHT, drawX, drawY)
        {
            _aiPattern.setEnemy(this);
        }
        
        public override int getBasicDamage_noStrength()
        {
            return getDamageAffectedByBuffs(DEFAULT_DAMAGE);
        }
        public override int getHeavyDamage()
        {
            return getDamageAffectedByBuffs(DEFAULT_HEAVY_DAMAGE);
        }
    }
}
