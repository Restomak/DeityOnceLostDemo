using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.DefaultCards
{
    /// <summary>
    /// Cautious Strike | Default
    ///     (Costs 1 Divinity)
    /// Deal 3 damage.
    /// Gain 3 defense.
    /// </summary>
    class CautiousStrike : AttackAndDefenseCard
    {
        public const String NAME = "Cautious Strike";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.HYBRID;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.DEFAULT;
        public const int PLAYCOST_DIV = 1;
        public const int ATTACK_DAMAGE = 3;
        public const int DEFENSE_GAIN = 3;

        public CautiousStrike(int playCostDiv = PLAYCOST_DIV) : base(NAME, CARDTYPE, RARITY, ATTACK_DAMAGE, DEFENSE_GAIN)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, playCostDiv);
            addBaseCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<string>();
            Combat.Unit descTarget = null;
            if (activeCard)
            {
                descTarget = Card.getTargetForDescription(_targetType);
            }
            int damage = champ.getDamageAffectedByBuffs(iDamage, descTarget);

            int defense = champ.getDefenseAffectedByBuffs(iDefense);

            desc.Add("Deal " + damage + " damage.");
            desc.Add("Gain " + defense + " defense.");

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

            int defense = champ.getDefenseAffectedByBuffs(iDefense);

            desc.Add(CardStylizing.basicDamageString(ATTACK_DAMAGE, damage, descFontSize));
            desc.Add(CardStylizing.basicDefenseString(DEFENSE_GAIN, defense, descFontSize));

            return desc;
        }

        public override Card getNewCard()
        {
            return new CautiousStrike();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.DefaultCards.CautiousStrike_Empowered();
        }
    }
}
