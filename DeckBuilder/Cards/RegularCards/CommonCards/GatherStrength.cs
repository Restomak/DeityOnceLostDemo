using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.CommonCards
{
    /// <summary>
    /// Gather Strength | Common
    ///     (Costs 1 Divinity)
    /// Gain 2 defense.
    /// Gain 1 Strength.
    /// </summary>
    class GatherStrength : ReplayableSelfBuffAndDefenseCard
    {
        public const String NAME = "Gather Strength";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.COMMON;
        public const int PLAYCOST_DIV = 1;
        public const int STRENGTH_AMOUNT = 1;
        public const int DEFENSE_GAIN = 2;

        public GatherStrength(int defense = DEFENSE_GAIN) : base(NAME, CARDTYPE, RARITY, STRENGTH_AMOUNT, Combat.Unit.statType.strength, defense)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            int defense = champ.getDefenseAffectedByBuffs(iDefense);

            desc.Add("Gain " + defense + " defense.");
            desc.Add("Gain " + iStatBuffAmount + " " + iBuffStat.ToString() + ".");

            return desc;
        }

        public override List<String> getStylizedDescription(Characters.Champion champ, int descFontSize, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            int defense = champ.getDefenseAffectedByBuffs(iDefense);

            desc.Add(CardStylizing.basicDefenseString(DEFENSE_GAIN, defense, descFontSize));
            desc.Add(CardStylizing.basicStatBuffString(STRENGTH_AMOUNT, iStatBuffAmount, Combat.Unit.statType.strength, descFontSize));

            return desc;
        }

        public override Card getNewCard()
        {
            return new GatherStrength();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.CommonCards.GatherStrength_Empowered();
        }
    }
}
