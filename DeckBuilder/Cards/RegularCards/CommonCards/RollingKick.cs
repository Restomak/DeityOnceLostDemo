using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.CommonCards
{
    /// <summary>
    /// Rolling Kick | Common
    ///     (Costs 1 Divinity)
    /// Deal 8 damage.
    /// Draw 1 card.
    /// </summary>
    class RollingKick : AttackAndDrawCard
    {
        public const String NAME = "Rolling Kick";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.HYBRID;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.COMMON;
        public const int PLAYCOST_DIV = 1;
        public const int ATTACK_DAMAGE = 8;
        public const int CARD_DRAW = 1;

        public RollingKick(int cardDraw = CARD_DRAW, int damage = ATTACK_DAMAGE) : base(NAME, CARDTYPE, RARITY, damage, cardDraw)
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

            String drawCardString = "Draw " + iDrawAmount + " card";
            if (iDrawAmount > 1)
            {
                drawCardString += "s";
            }
            drawCardString += ".";

            desc.Add("Deal " + damage + " damage.");
            desc.Add(drawCardString);

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
            desc.Add(CardStylizing.basicCardDrawString(CARD_DRAW, iDrawAmount, descFontSize));

            return desc;
        }

        public override Card getNewCard()
        {
            return new RollingKick();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.CommonCards.RollingKick_Empowered();
        }
    }
}
