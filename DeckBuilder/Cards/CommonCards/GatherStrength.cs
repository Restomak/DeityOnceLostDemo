using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.CommonCards
{
    class GatherStrength : ReplayableSelfBuffAndDefenseCard
    {
        public const String NAME = "Gather Strength";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.COMMON;
        public const int PLAYCOST_DIV = 1;
        public const int STRENGTH_AMOUNT = 1;
        public const int DEFENSE_GAIN = 4;

        public GatherStrength() : base(NAME, CARDTYPE, RARITY, STRENGTH_AMOUNT, Combat.Unit.statType.strength, DEFENSE_GAIN)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }
    }
}
