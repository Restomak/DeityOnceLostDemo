using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.DefaultCards
{
    class Kick : BasicAttackCard
    {
        public const String NAME = "Kick";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.ATTACK;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.DEFAULT;
        public const int PLAYCOST_DIV = 1;
        public const int ATTACK_DAMAGE = 8;

        public Kick() : base(NAME, CARDTYPE, RARITY, ATTACK_DAMAGE)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }
    }
}
