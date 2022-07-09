using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.DefaultCards
{
    /// <summary>
    /// Kick | Default
    ///     (Costs 1 Divinity)
    /// Deal 8 damage.
    /// </summary>
    class Kick : BasicAttackCard
    {
        public const String NAME = "Kick";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.ATTACK;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.DEFAULT;
        public const int PLAYCOST_DIV = 1;
        public const int ATTACK_DAMAGE = 8;

        public Kick(int damage = ATTACK_DAMAGE) : base(NAME, CARDTYPE, RARITY, damage)
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
            return new Kick();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.DefaultCards.Kick_Empowered();
        }
    }
}
