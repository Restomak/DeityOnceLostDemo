using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.EpicCards
{
    /// <summary>
    /// Time Shift | Epic
    ///     (Costs 0 Divinity)
    /// Gain 1 Divinity.
    /// Trigger all party member abilities.
    /// Dissipates.
    /// </summary>
    class TimeShift : Card, IGainDivCard //don't need IDissipateCard since we're directly using Card's constructor
    {
        public const String NAME = "Time Shift";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.EPIC;
        public const int PLAYCOST_DIV = 0;
        public const int DIVINITY_GAIN = 1;

        public TimeShift(int divGain = DIVINITY_GAIN) : base(NAME, CARDTYPE, RARITY, CardEnums.TargetingType.aoeFriendlies, true)
        {
            iDivGain = divGain;

            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
            _dissipates = true;
        }

        public int iDivGain { get; }

        public override void onPlay()
        {
            iGainDivinity();
            if (Game1.getCombatHandler().getParty() != null)
            {
                for (int i = 0; i < Game1.getCombatHandler().getParty().Count; i++)
                {
                    Game1.getCombatHandler().getParty()[i].getPartyMemberBuff().onCardProc();
                }
            }
        }

        public void iGainDivinity()
        {
            Game1.getChamp().spendDivinity(-iDivGain);
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            desc.Add("Gain " + iDivGain + " Divinity.");
            desc.Add("Trigger all party");
            desc.Add("member abilities.");
            desc.Add("Dissipates.");

            return desc;
        }

        public override List<String> getStylizedDescription(Characters.Champion champ, int descFontSize, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            desc.Add(CardStylizing.basicDivGainString(DIVINITY_GAIN, iDivGain, descFontSize));
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]Trigger all party");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]member abilities.");
            desc.Add(CardStylizing.dissipates(descFontSize));

            return desc;
        }

        public override Card getNewCard()
        {
            return new TimeShift();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.EpicCards.TimeShift_Empowered();
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(getDivinityExtraInfo());
            extraInfo.Add(getDissipateExtraInfo());

            return extraInfo;
        }
    }
}
