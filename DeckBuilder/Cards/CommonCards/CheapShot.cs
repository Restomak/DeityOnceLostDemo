using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.CommonCards
{
    class CheapShot : AttackAndDebuffCard
    {
        public const String NAME = "Cheap Shot";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.ATTACK;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.COMMON;
        public const int PLAYCOST_DIV = 0;
        public const int ATTACK_DAMAGE = 4;
        public const int DEBUFF_DURATION = 1;

        public CheapShot() : base(NAME, CARDTYPE, RARITY, ATTACK_DAMAGE, Combat.Buff.buffType.feeble, DEBUFF_DURATION, 1, true, false)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }
    }
}
