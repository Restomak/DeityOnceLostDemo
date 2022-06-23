using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.CommonCards
{
    class ViciousSlam : AttackAndDebuffCard
    {
        public const String NAME = "Vicious Slam";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.ATTACK;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.COMMON;
        public const int PLAYCOST_DIV = 2;
        public const int ATTACK_DAMAGE = 11;
        public const int DEBUFF_DURATION = 2;

        public ViciousSlam() : base(NAME, CARDTYPE, RARITY, ATTACK_DAMAGE, Combat.Buff.buffType.vulnerable, DEBUFF_DURATION, 1, true, false)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }
    }
}
