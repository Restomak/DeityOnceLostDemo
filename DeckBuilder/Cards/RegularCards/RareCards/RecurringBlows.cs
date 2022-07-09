using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.RareCards
{
    /// <summary>
    /// Recurring Blows | Rare
    ///     (Costs 1 Divinity)
    /// Deal 6 damage 2 times. Increase the number of attacks this
    ///     card does by 1 this combat.
    /// </summary>
    class RecurringBlows : BasicAttackCard
    {
        public const String NAME = "Recurring Blows";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.ATTACK;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.RARE;
        public const int PLAYCOST_DIV = 1;
        public const int ATTACK_DAMAGE = 6;
        public const int NUM_STARTING_HITS = 2;

        int _numHits;

        public RecurringBlows(int numHits = NUM_STARTING_HITS) : base(NAME, CARDTYPE, RARITY, ATTACK_DAMAGE)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
            _numHits = numHits;
        }

        public override void onPlay()
        {
            iDealDamage();
        }

        public override void iDealDamage()
        {
            if (_target != null)
            {
                for (int i = 0; i < _numHits; i++)
                {
                    _target.takeDamage(Game1.getChamp().getDamageAffectedByBuffs(iDamage, _target));
                }
                _numHits++;
            }
            else
            {
                Game1.addToErrorLog("Attempted to use damage card without a target!");
            }
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<String>();
            Combat.Unit descTarget = null;
            if (activeCard)
            {
                descTarget = Card.getTargetForDescription(_targetType);
            }
            int damage = champ.getDamageAffectedByBuffs(iDamage, descTarget);
            
            desc.Add("Deal " + damage + " damage " + _numHits);
            desc.Add("times. Increase the");
            desc.Add("number of attacks this");
            desc.Add("card does by 1 this");
            desc.Add("combat.");

            return desc;
        }

        public override List<String> getStylizedDescription(Characters.Champion champ, int descFontSize, bool activeCard = false)
        {
            List<String> desc = new List<String>();
            Combat.Unit descTarget = null;
            if (activeCard)
            {
                descTarget = Card.getTargetForDescription(_targetType);
            }
            int damage = champ.getDamageAffectedByBuffs(iDamage, descTarget);
            String damageString = "";
            if (damage == iDamage)
            {
                damageString += damage + "|";
            }
            else if (damage < iDamage)
            {
                damageString += "|[f: " + descFontSize + " m][s: Black][c: Red]" + damage + "|";
            }
            else //greater
            {
                damageString += "|[f: " + descFontSize + " m][s: Black][c: Lawn Green]" + damage + "|";
            }

            String numHitsString = "";
            if (_numHits == NUM_STARTING_HITS)
            {
                numHitsString += _numHits;
            }
            else
            {
                numHitsString += "|[f: " + descFontSize + " m][s: Black][c: Lawn Green]" + _numHits;
            }

            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]Deal " + damageString + "[f: " + descFontSize + " m][c: Black]" + " damage " + numHitsString);
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]times. Increase the");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]number of attacks this");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]card does by 1 this");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]combat.");

            return desc;
        }

        public override Card getNewCard()
        {
            return new RecurringBlows();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.RareCards.RecurringBlows_Empowered();
        }
    }
}
