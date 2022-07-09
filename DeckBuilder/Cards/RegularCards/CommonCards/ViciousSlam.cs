using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.CommonCards
{
    /// <summary>
    /// Vicious Slam | Common
    ///     (Costs 2 Divinity)
    /// Deal 11 damage.
    /// Apply 2 Vulnerable.
    /// </summary>
    class ViciousSlam : AttackAndDebuffCard
    {
        public const String NAME = "Vicious Slam";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.ATTACK;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.COMMON;
        public const int PLAYCOST_DIV = 2;
        public const int ATTACK_DAMAGE = 11;
        public const int DEBUFF_DURATION = 2;

        public ViciousSlam(int damage = ATTACK_DAMAGE, int debuffDuration = DEBUFF_DURATION) : base(NAME, CARDTYPE, RARITY, damage, Combat.Buff.buffType.vulnerable, debuffDuration, 1, true, false)
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

            desc.Add("Deal " + damage + " damage.");
            desc.Add("Apply " + iBuffDuration + " " + Combat.Buff.buffString(iBuffType) + ".");

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

            desc.Add(CardStylizing.basicDamageString(ATTACK_DAMAGE, damage, descFontSize));
            desc.Add(CardStylizing.basicApplyDebuffString(DEBUFF_DURATION, iBuffDuration, iBuffType, descFontSize));

            return desc;
        }

        public override Card getNewCard()
        {
            return new ViciousSlam();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.CommonCards.ViciousSlam_Empowered();
        }
    }
}
