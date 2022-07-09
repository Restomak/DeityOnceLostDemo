using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.RareCards
{
    /// <summary>
    /// Hunter's Mark | Rare
    ///     (Costs 0 Divinity)
    /// Apply 3 Vulnerable.
    /// Dissipates.
    /// </summary>
    class HuntersMark : BasicDebuffCard
    {
        public const String NAME = "Hunter's Mark";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.RARE;
        public const int PLAYCOST_DIV = 0;
        public const int DEBUFF_DURATION = 3;

        public HuntersMark() : base(NAME, CARDTYPE, RARITY, Combat.Buff.buffType.vulnerable, DEBUFF_DURATION, 1, true, false)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
            _dissipates = true;
        }

        public override void onPlay()
        {
            iApplyDebuff();
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            desc.Add("Apply " + iBuffDuration + " " + Combat.Buff.buffString(iBuffType) + ".");
            desc.Add("Dissipates.");

            return desc;
        }

        public override List<String> getStylizedDescription(Characters.Champion champ, int descFontSize, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            desc.Add(CardStylizing.basicApplyDebuffString(DEBUFF_DURATION, iBuffDuration, iBuffType, descFontSize));
            desc.Add(CardStylizing.dissipates(descFontSize));

            return desc;
        }

        public override Card getNewCard()
        {
            return new HuntersMark();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.RareCards.HuntersMark_Empowered();
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = base.getHoverInfo();

            extraInfo.Add(getDissipateExtraInfo());

            return extraInfo;
        }
    }
}
