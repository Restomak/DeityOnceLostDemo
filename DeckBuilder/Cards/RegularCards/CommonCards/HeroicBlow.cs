using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.CommonCards
{
    /// <summary>
    /// Heroic Blow | Common
    ///     (Costs 2 Divinity)
    /// Deal 20 damage.
    /// </summary>
    class HeroicBlow : BasicAttackCard
    {
        public const String NAME = "Heroic Blow";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.ATTACK;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.COMMON;
        public const int PLAYCOST_DIV = 2;
        public const int ATTACK_DAMAGE = 20;

        public HeroicBlow(int damage = ATTACK_DAMAGE) : base(NAME, CARDTYPE, RARITY, damage)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
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

            desc.Add("Deal " + damage + " damage.");

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

            return desc;
        }

        public override Card getNewCard()
        {
            return new HeroicBlow();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.CommonCards.HeroicBlow_Empowered();
        }
    }
}
