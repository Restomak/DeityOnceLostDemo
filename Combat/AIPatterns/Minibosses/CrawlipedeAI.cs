using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat.AIPatterns.Minibosses
{
    /// <summary>
    /// CrawlipedeAI is a type of AIPattern used specifically for the Crawlipede miniboss.
    /// It has a cycle it repeats as a pattern:
    ///    1) Debuff with feeble
    ///    2) Regular attack
    ///    3) Light attack and debuff with vulnerable
    ///    4) Regular attack
    /// </summary>
    class CrawlipedeAI : AIPattern
    {
        public const int FEEBLE_AMOUNT = 3;
        public const int VULNERABLE_AMOUNT = 1;

        int _cycle;

        public CrawlipedeAI() : base()
        {
            _cycle = 1;

            _intentsForThisTurn = new List<intent>();
        }

        public override void setEnemy(Enemy enemy)
        {
            _enemy = enemy;
        }

        public override List<intent> determineIntents(Characters.Champion champ, List<PartyMember> party)
        {
            _intentsForThisTurn.Clear();
            _intendedDamage = 0;
            _numHits = 0;

            switch (_cycle)
            {
                case 1:
                    _intentsForThisTurn.Add(intent.DEBUFF);
                    break;
                case 2:
                case 4:
                    _intentsForThisTurn.Add(intent.ATTACK);
                    _numHits = 1;
                    _intendedDamage = _enemy.getRegularDamage();
                    break;
                case 3:
                    _intentsForThisTurn.Add(intent.DEBUFF);
                    _intentsForThisTurn.Add(intent.ATTACK);
                    _numHits = 1;
                    _intendedDamage = _enemy.getLightDamage();
                    break;
            }

            return _intentsForThisTurn;
        }

        public override List<intent> updateIntents()
        {
            switch (_cycle)
            {
                case 2:
                case 4:
                    _intendedDamage = _enemy.getRegularDamage();
                    break;
                case 3:
                    _intendedDamage = _enemy.getLightDamage();
                    break;
            }

            return _intentsForThisTurn; //no change
        }

        public override void doTurnAction(Characters.Champion champ, List<PartyMember> party)
        {

            switch (_cycle)
            {
                case 1:
                    Game1.getChamp().gainBuff(new Buff(Buff.buffType.feeble, FEEBLE_AMOUNT, 1, true, false, true));
                    break;
                case 2:
                case 4:
                    champ.takeDamage(_enemy.getDamageAffectedByBuffs(_enemy.getRegularDamage()));
                    break;
                case 3:
                    champ.takeDamage(_enemy.getDamageAffectedByBuffs(_enemy.getLightDamage()));
                    Game1.getChamp().gainBuff(new Buff(Buff.buffType.vulnerable, VULNERABLE_AMOUNT, 1, true, false, true));
                    break;
            }

            _cycle += 1;
            if (_cycle > 4)
            {
                _cycle = 1;
            }

            _intentsForThisTurn.Clear();
        }

        public override int getDamage()
        {
            switch (_cycle)
            {
                case 1:
                    return 0;
                case 3:
                    return _enemy.getLightDamage();
                default:
                    return _enemy.getRegularDamage();
            }
        }
    }
}
