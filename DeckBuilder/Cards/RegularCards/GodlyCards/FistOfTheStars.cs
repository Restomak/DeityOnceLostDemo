using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.GodlyCards
{
    /// <summary>
    /// Fist of the Stars | Godly
    ///     (Costs 2 Divinity)
    /// Deal 4 damage 7 times.
    /// </summary>
    class FistOfTheStars : BasicMultiAttackCard
    {
        public const String NAME = "Fist of the Stars";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.ATTACK;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.GODLY;
        public const int PLAYCOST_DIV = 2;
        public const int ATTACK_DAMAGE = 4;
        public const int NUM_HITS = 7;

        public FistOfTheStars(int numHits = NUM_HITS) : base(NAME, CARDTYPE, RARITY, ATTACK_DAMAGE, numHits)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
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

            desc.Add("Deal " + damage + " damage " + iNumHits);
            desc.Add("times.");

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
            if (damage == ATTACK_DAMAGE)
            {
                damageString += damage + "|";
            }
            else if (damage < ATTACK_DAMAGE)
            {
                damageString += "|[f: " + descFontSize + " m][s: Black][c: Red]" + damage + "|";
            }
            else //greater
            {
                damageString += "|[f: " + descFontSize + " m][s: Black][c: Lawn Green]" + damage + "|";
            }

            String numHitsString = "";
            if (iNumHits == NUM_HITS)
            {
                numHitsString += iNumHits;
            }
            else if (iNumHits < NUM_HITS)
            {
                numHitsString += "|[f: " + descFontSize + " m][s: Black][c: Red]" + iNumHits;
            }
            else //greater
            {
                numHitsString += "|[f: " + descFontSize + " m][s: Black][c: Lawn Green]" + iNumHits;
            }

            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]Deal " + damageString + "[f: " + descFontSize + " m][c: Black]" + " damage " + numHitsString);
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]times.");

            return desc;
        }

        public override Card getNewCard()
        {
            return new FistOfTheStars();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.GodlyCards.FistOfTheStars_Empowered();
        }
    }
}
