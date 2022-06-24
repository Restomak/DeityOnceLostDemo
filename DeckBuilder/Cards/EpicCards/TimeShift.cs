using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EpicCards
{
    class TimeShift : Card //don't need IDissipateCard since we're directly using Card's constructor
    {
        public const String NAME = "Time Shift";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.EPIC;
        public const int PLAYCOST_DIV = 0;
        public const int DIVINITY_GAIN = 1;

        public TimeShift() : base(NAME, CARDTYPE, RARITY, CardEnums.TargetingType.aoeFriendlies, true)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }

        public int divGain
        {
            get => DIVINITY_GAIN;
        }

        public override void onPlay()
        {
            gainDivinity();
            if (Game1.getCombatHandler().getParty() != null)
            {
                for (int i = 0; i < Game1.getCombatHandler().getParty().Count; i++)
                {
                    Game1.getCombatHandler().getParty()[i].getPartyMemberBuff().onCardProc();
                }
            }
        }

        public void gainDivinity()
        {
            Game1.getChamp().spendDivinity(-DIVINITY_GAIN);
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            desc.Add("Gain " + DIVINITY_GAIN + " Divinity.");
            desc.Add("Trigger all party");
            desc.Add("member abilities.");
            desc.Add("Dissipates.");

            return desc;
        }
    }
}
