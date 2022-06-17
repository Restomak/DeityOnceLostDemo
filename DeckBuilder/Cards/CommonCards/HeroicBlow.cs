using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.CommonCards
{
    class HeroicBlow : BasicAttackCard
    {
        public const String NAME = "Heroic Blow";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.ATTACK;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.COMMON;
        public const int PLAYCOST_DIV = 2;
        public const int ATTACK_DAMAGE = 20;

        public HeroicBlow() : base(NAME, CARDTYPE, RARITY, ATTACK_DAMAGE)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }
    }
}
