using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.CommonCards
{
    /// <summary>
    /// Cheap Shot | Common
    ///     (Costs 0 Divinity)
    /// Deal 4 damage.
    /// Apply 1 Feeble.
    /// </summary>
    class CheapShot : AttackAndDebuffCard
    {
        public const String NAME = "Cheap Shot";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.ATTACK;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.COMMON;
        public const int PLAYCOST_DIV = 0;
        public const int ATTACK_DAMAGE = 4;
        public const int DEBUFF_DURATION = 1;

        public CheapShot(int debuffDuration = DEBUFF_DURATION) : base(NAME, CARDTYPE, RARITY, ATTACK_DAMAGE, Combat.Buff.buffType.feeble, debuffDuration, 1, true, false)
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
            return new CheapShot();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.CommonCards.CheapShot_Empowered();
        }
    }
}
