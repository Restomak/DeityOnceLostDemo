using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.DefaultCards
{
    class Dodge : BasicDefenseCard
    {
        public const String NAME = "Dodge";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.DEFAULT;
        public const int PLAYCOST_DIV = 1;
        public const int DEFENSE_GAIN = 6;

        public Dodge() : base(NAME, CARDTYPE, RARITY, DEFENSE_GAIN)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }
    }
}
