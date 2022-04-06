using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.DefaultCards
{
    class Parry : BasicDefenseCard
    {
        public const String NAME = "Parry";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.DEFAULT;
        public const int PLAYCOST_DIV = 1;
        public const int DEFENSE_GAIN = 4;

        public Parry() : base(NAME, CARDTYPE, RARITY, DEFENSE_GAIN)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }
    }
}
