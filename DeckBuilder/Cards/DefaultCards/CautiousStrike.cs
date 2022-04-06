using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.DefaultCards
{
    class CautiousStrike : AttackAndDefenseCard
    {
        public const String NAME = "Cautious Strike";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.HYBRID;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.DEFAULT;
        public const int PLAYCOST_DIV = 1;
        public const int ATTACK_DAMAGE = 3;
        public const int DEFENSE_GAIN = 3;

        public CautiousStrike() : base(NAME, CARDTYPE, RARITY, ATTACK_DAMAGE, DEFENSE_GAIN)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }
    }
}
