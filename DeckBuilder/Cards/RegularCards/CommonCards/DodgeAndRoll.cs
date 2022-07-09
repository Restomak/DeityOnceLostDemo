using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.CommonCards
{
    /// <summary>
    /// Dodge and Roll | Common
    ///     (Costs 1 Divinity)
    /// Gain 6 defense.
    /// Draw 1 card.
    /// </summary>
    class DodgeAndRoll : DefenseAndDrawCard
    {
        public const String NAME = "Dodge and Roll";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.COMMON;
        public const int PLAYCOST_DIV = 1;
        public const int DEFENSE_GAIN = 6;
        public const int CARD_DRAW = 1;

        public DodgeAndRoll(int cardDraw = CARD_DRAW, int defense = DEFENSE_GAIN) : base(NAME, CARDTYPE, RARITY, defense, cardDraw)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<String>();
            int defense = champ.getDefenseAffectedByBuffs(iDefense);

            String drawCardString = "Draw " + iDrawAmount + " card";
            if (iDrawAmount > 1)
            {
                drawCardString += "s";
            }
            drawCardString += ".";

            desc.Add("Gain " + defense + " defense.");
            desc.Add(drawCardString);

            return desc;
        }

        public override List<String> getStylizedDescription(Characters.Champion champ, int descFontSize, bool activeCard = false)
        {
            List<String> desc = new List<String>();
            int defense = champ.getDefenseAffectedByBuffs(iDefense);

            desc.Add(CardStylizing.basicDefenseString(DEFENSE_GAIN, defense, descFontSize));
            desc.Add(CardStylizing.basicCardDrawString(CARD_DRAW, iDrawAmount, descFontSize));

            return desc;
        }

        public override Card getNewCard()
        {
            return new DodgeAndRoll();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.CommonCards.DodgeAndRoll_Empowered();
        }
    }
}
