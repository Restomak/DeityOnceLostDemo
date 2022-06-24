using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RareCards
{
    class HuntersMark : BasicDebuffCard, IDissipateCard
    {
        public const String NAME = "Hunter's Mark";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.RARE;
        public const int PLAYCOST_DIV = 0;
        public const int DEBUFF_DURATION = 3;

        public HuntersMark() : base(NAME, CARDTYPE, RARITY, Combat.Buff.buffType.vulnerable, DEBUFF_DURATION, 1, true, false)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
            dissipate();
        }

        public override void onPlay()
        {
            applyDebuff();
        }

        public void dissipate()
        {
            _dissipates = true;
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = base.getDescription(champ, activeCard);

            desc.Add("Dissipates.");

            return desc;
        }
    }
}
