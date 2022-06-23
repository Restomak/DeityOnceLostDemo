using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Characters.PartyBuffs
{
    public class Fighter : Treasury.Treasures.PartyBuff
    {
        public const int FIGHTER_DAMAGE = 6;

        public Combat.Enemy _target;

        public Fighter() : base("Fighter", new List<String>()
            { "This hero is a fighter, and they make",
              "themselves useful by attacking. At the",
              "end of your turn, will attack the last",
              "enemy you attacked for " + FIGHTER_DAMAGE + ". Without",
              "a target, will attack a random enemy."
            })
        {

        }

        public override void onCombatStart()
        {
            _target = null;
        }

        public override void onChampionAttack()
        {
            _target = Game1.getCombatHandler().getLastAttackedEnemy();
        }

        public override void onTurnEnd()
        {
            if (_target != null && _target.getDowned())
            {
                _target = null;
            }

            if (_target == null)
            {
                _target = Game1.getCombatHandler().getRandomEnemy();
            }

            if (_target != null)
            {
                _target.takeDamage(_target.getDamageAffectedByBuffs(FIGHTER_DAMAGE));
            }
        }
    }
}
