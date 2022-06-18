using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.GodlyCards
{
    class FistOfTheStars : BasicMultiAttackCard
    {
        public const String NAME = "Fist of the Stars";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.ATTACK;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.GODLY;
        public const int PLAYCOST_DIV = 2;
        public const int ATTACK_DAMAGE = 5;
        public const int NUM_HITS = 7;

        public FistOfTheStars() : base(NAME, CARDTYPE, RARITY, ATTACK_DAMAGE, NUM_HITS)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }
    }
}
