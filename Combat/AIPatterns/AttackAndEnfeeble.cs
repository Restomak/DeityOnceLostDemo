using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeityOnceLost.Characters;

namespace DeityOnceLost.Combat.AIPatterns
{
    /// <summary>
    /// AttackAndEnfeeble is a type of AIPattern that will usually attack, but will
    /// occaistionally instead attempt to debuff the champion with Feeble. Rarely,
    /// if enabled, it will perform a heavy attack.
    /// </summary>
    class AttackAndEnfeeble : AIPattern
    {
        public const int WEIGHT_ATTACK = 5;
        public const int WEIGHT_ENFEEBLE = 2;
        public const int WEIGHT_HEAVY_ATTACK = 1;

        int _feebleAmount;
        bool _canHeavyAttack;
        specificIntent _specificIntent;

        enum specificIntent
        {
            ATTACK,
            ENFEEBLE,
            HEAVY_ATTACK
        }

        public AttackAndEnfeeble(int feebleAmount, bool canHeavyAttack = true) : base()
        {
            _feebleAmount = feebleAmount;
            _canHeavyAttack = canHeavyAttack;

            _intentsForThisTurn = new List<intent>();
        }

        public override void setEnemy(Enemy enemy)
        {
            _enemy = enemy;
        }

        public override List<intent> determineIntents(Champion champ, List<PartyMember> party)
        {
            _intentsForThisTurn.Clear();
            _intendedDamage = 0;

            int rand = 0;
            if (_canHeavyAttack)
            {
                rand = Game1.randint(1, WEIGHT_ATTACK + WEIGHT_ENFEEBLE + WEIGHT_HEAVY_ATTACK);
            }
            else
            {
                rand = Game1.randint(1, WEIGHT_ATTACK + WEIGHT_ENFEEBLE);
            }

            if (rand <= WEIGHT_ATTACK)
            {
                _intentsForThisTurn.Add(intent.ATTACK);
                _specificIntent = specificIntent.ATTACK;
                _numHits = 1;
                _intendedDamage = _enemy.getRegularDamage();
            }
            else if (rand <= WEIGHT_ATTACK + WEIGHT_ENFEEBLE)
            {
                _intentsForThisTurn.Add(intent.DEBUFF);
                _specificIntent = specificIntent.ENFEEBLE;
            }
            else //heavy attack
            {
                _intentsForThisTurn.Add(intent.ATTACK);
                _specificIntent = specificIntent.HEAVY_ATTACK;
                _numHits = 1;
                _intendedDamage = _enemy.getHeavyDamage();
            }

            return _intentsForThisTurn;
        }

        public override List<intent> updateIntents()
        {
            switch (_specificIntent)
            {
                case specificIntent.ATTACK:
                    _intendedDamage = _enemy.getRegularDamage();
                    break;
                case specificIntent.ENFEEBLE:
                    //Nothing changes
                    break;
                case specificIntent.HEAVY_ATTACK:
                    _intendedDamage = _enemy.getHeavyDamage();
                    break;
            }

            return _intentsForThisTurn; //no change
        }

        public override void doTurnAction(Champion champ, List<PartyMember> party)
        {
            switch (_specificIntent)
            {
                case specificIntent.ATTACK:
                    champ.takeDamage(_enemy.getDamageAffectedByBuffs(_enemy.getRegularDamage()));
                    break;
                case specificIntent.ENFEEBLE:
                    Game1.getChamp().gainBuff(new Buff(Buff.buffType.feeble, _feebleAmount, 1, true, false, true));
                    break;
                case specificIntent.HEAVY_ATTACK:
                    champ.takeDamage(_enemy.getDamageAffectedByBuffs(_enemy.getHeavyDamage()));
                    break;
                default:
                    break;
            }

            _intentsForThisTurn.Clear();
        }

        public override int getDamage()
        {
            switch (_specificIntent)
            {
                case specificIntent.ATTACK:
                    return _enemy.getRegularDamage();
                case specificIntent.ENFEEBLE:
                    return 0;
                case specificIntent.HEAVY_ATTACK:
                    return _enemy.getHeavyDamage();
                default:
                    return _enemy.getRegularDamage();
            }
        }
    }
}
