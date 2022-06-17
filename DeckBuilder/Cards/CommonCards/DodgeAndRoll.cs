using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.CommonCards
{
    class DodgeAndRoll : DefenseAndDrawCard
    {
        public const String NAME = "Dodge and Roll";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.COMMON;
        public const int PLAYCOST_DIV = 1;
        public const int DEFENSE_GAIN = 6;
        public const int CARD_DRAW = 1;

        public DodgeAndRoll() : base(NAME, CARDTYPE, RARITY, DEFENSE_GAIN, CARD_DRAW)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }
    }
}
