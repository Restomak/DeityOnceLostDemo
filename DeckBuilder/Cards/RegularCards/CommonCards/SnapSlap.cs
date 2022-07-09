using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.CommonCards
{
    /// <summary>
    /// Snap Slap | Common
    ///     (Costs 0 Divinity)
    /// Deal 3 damage.
    /// Draw 1 card.
    /// </summary>
    class SnapSlap : AttackAndDrawCard
    {
        public const String NAME = "Snap Slap";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.HYBRID;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.COMMON;
        public const int PLAYCOST_DIV = 0;
        public const int ATTACK_DAMAGE = 3;
        public const int CARD_DRAW = 1;

        public SnapSlap(int cardDraw = CARD_DRAW) : base(NAME, CARDTYPE, RARITY, ATTACK_DAMAGE, cardDraw)
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
            return new SnapSlap();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.CommonCards.SnapSlap_Empowered();
        }
    }
}
