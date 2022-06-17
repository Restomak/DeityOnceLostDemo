using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.CommonCards
{
    class RollingKick : AttackAndDrawCard
    {
        public const String NAME = "Rolling Kick";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.ATTACK;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.COMMON;
        public const int PLAYCOST_DIV = 1;
        public const int ATTACK_DAMAGE = 8;
        public const int CARD_DRAW = 1;

        public RollingKick() : base(NAME, CARDTYPE, RARITY, ATTACK_DAMAGE, CARD_DRAW)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }
    }
}
