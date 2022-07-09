using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeityOnceLost.Characters;

namespace DeityOnceLost.Combat.AIPatterns
{
    /// <summary>
    /// SimpleSlowRoller is a type of AIPattern that will buff its strength every few
    /// rounds (+ defend) and, in between those, will randomly choose between the
    /// following actions:
    ///    - basic attack
    ///    - light attack + defend
    ///    - (rarely, if enabled) heavy attack
    /// </summary>
    class SimpleSlowRoller : AIPattern
    {
        public const int WEIGHT_BASIC_ATTACK = 6;
        public const int WEIGHT_ATTACK_AND_DEFEND = 8;
        public const int WEIGHT_HEAVY_ATTACK = 2;
        public const int DEFAULT_BUFF_INTERVAL_MIN = 3;
        public const int DEFAULT_BUFF_INTERVAL_MAX = 4;

        int _strengthGain, _buffIntervalMin, _buffIntervalMax, _turnsLeftUntilBuff;
        bool _canHeavyAttack;
        specificIntent _specificIntent;

        enum specificIntent
        {
            BUFF_AND_DEFEND,
            BASIC_ATTACK,
            ATTACK_AND_DEFEND,
            HEAVY_ATTACK
        }

        public SimpleSlowRoller(int strengthGain, int buffIntervalMin = DEFAULT_BUFF_INTERVAL_MIN, int buffIntervalMax = DEFAULT_BUFF_INTERVAL_MAX, bool canHeavyAttack = true) : base()
        {
            _strengthGain = strengthGain;
            _canHeavyAttack = canHeavyAttack;
            _buffIntervalMin = buffIntervalMin;
            _buffIntervalMax = buffIntervalMax;

            _intentsForThisTurn = new List<intent>();

            _turnsLeftUntilBuff = Game1.randint(_buffIntervalMin, _buffIntervalMax);
        }

        public override void setEnemy(Enemy enemy)
        {
            _enemy = enemy;
        }

        public override List<intent> determineIntents(Champion champ, List<PartyMember> party)
        {
            _intentsForThisTurn.Clear();
            _turnsLeftUntilBuff -= 1;
            _intendedDamage = 0;

            if (_turnsLeftUntilBuff <= 0) //buff turn
            {
                _intentsForThisTurn.Add(intent.BUFF);
                _intentsForThisTurn.Add(intent.DEFEND);
                _specificIntent = specificIntent.BUFF_AND_DEFEND;
                _turnsLeftUntilBuff = Game1.randint(_buffIntervalMin, _buffIntervalMax); //reset timer until next buff turn
                _numHits = 0;
            }
            else
            {
                int rand = 0;
                if (_canHeavyAttack)
                {
                    rand = Game1.randint(1, WEIGHT_BASIC_ATTACK + WEIGHT_ATTACK_AND_DEFEND + WEIGHT_HEAVY_ATTACK);
                }
                else
                {
                    rand = Game1.randint(1, WEIGHT_BASIC_ATTACK + WEIGHT_ATTACK_AND_DEFEND);
                }

                if (rand <= WEIGHT_BASIC_ATTACK)
                {
                    _intentsForThisTurn.Add(intent.ATTACK);
                    _specificIntent = specificIntent.BASIC_ATTACK;
                    _numHits = 1;
                    _intendedDamage = _enemy.getRegularDamage();
                }
                else if (rand <= WEIGHT_BASIC_ATTACK + WEIGHT_ATTACK_AND_DEFEND)
                {
                    _intentsForThisTurn.Add(intent.ATTACK);
                    _intentsForThisTurn.Add(intent.DEFEND);
                    _specificIntent = specificIntent.ATTACK_AND_DEFEND;
                    _numHits = 1;
                    _intendedDamage = _enemy.getLightDamage();
                }
                else //heavy attack
                {
                    _intentsForThisTurn.Add(intent.ATTACK);
                    _specificIntent = specificIntent.HEAVY_ATTACK;
                    _numHits = 1;
                    _intendedDamage = _enemy.getHeavyDamage();
                }
            }

            return _intentsForThisTurn;
        }

        public override List<intent> updateIntents()
        {
            switch (_specificIntent)
            {
                case specificIntent.BUFF_AND_DEFEND:
                    //Nothing changes
                    break;
                case specificIntent.BASIC_ATTACK:
                    _intendedDamage = _enemy.getRegularDamage();
                    break;
                case specificIntent.ATTACK_AND_DEFEND:
                    _intendedDamage = _enemy.getLightDamage();
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
                case specificIntent.BUFF_AND_DEFEND:
                    _enemy.gainDefense(_enemy.getBasicDefense());
                    _enemy.gainBuff(new Buff(Buff.buffType.strength, -1, _strengthGain, false, true, false));
                    break;
                case specificIntent.BASIC_ATTACK:
                    champ.takeDamage(_enemy.getDamageAffectedByBuffs(_enemy.getRegularDamage()));
                    break;
                case specificIntent.ATTACK_AND_DEFEND:
                    _enemy.gainDefense(_enemy.getBasicDefense());
                    champ.takeDamage(_enemy.getDamageAffectedByBuffs(_enemy.getLightDamage()));
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
                case specificIntent.BUFF_AND_DEFEND:
                    return 0;
                case specificIntent.BASIC_ATTACK:
                    return _enemy.getRegularDamage();
                case specificIntent.ATTACK_AND_DEFEND:
                    return _enemy.getLightDamage();
                case specificIntent.HEAVY_ATTACK:
                    return _enemy.getHeavyDamage();
                default:
                    return _enemy.getRegularDamage();
            }
        }
    }
}
