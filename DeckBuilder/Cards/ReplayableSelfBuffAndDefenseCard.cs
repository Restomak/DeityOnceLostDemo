using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    /// <summary>
    /// Base class to make card design easier for cards that both gain defense and buff one
    /// of the three unit stats: Strength, Dexterity, or Resilience.
    /// </summary>
    abstract class ReplayableSelfBuffAndDefenseCard : BasicReplayableSelfBuffCard, IDefendingCard
    {
        public ReplayableSelfBuffAndDefenseCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int buffAmount, Combat.Unit.statType buffType, int defense) :
            base(name, cardType, rarity, buffAmount, buffType)
        {
            iDefense = defense;
        }

        public int iDefense { get; }

        public override void onPlay()
        {
            iGainDefense();
            iApplyBuff();
        }

        public void iGainDefense()
        {
            Game1.getChamp().gainDefense(iDefense);
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = base.getHoverInfo();

            extraInfo.Add(getDefenseExtraInfo());

            return extraInfo;
        }
    }
}
