using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    /// <summary>
    /// Base class to make card design easier for cards that buff one  of the three unit
    /// stats: Strength, Dexterity, or Resilience.
    /// </summary>
    abstract class BasicReplayableSelfBuffCard : Card, IStatBuffCard
    {
        public BasicReplayableSelfBuffCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int buffAmount, Combat.Unit.statType buffType) : base(name, cardType, rarity, CardEnums.TargetingType.champion)
        {
            iStatBuffAmount = buffAmount;
            iBuffStat = buffType;
        }

        public int iStatBuffAmount { get; }

        public Combat.Unit.statType iBuffStat { get; }

        public override void onPlay()
        {
            iApplyBuff();
        }

        public void iApplyBuff()
        {
            switch (iBuffStat)
            {
                case Combat.Unit.statType.strength:
                    Game1.getChamp().gainBuff(new Combat.Buff(Combat.Buff.buffType.strength, 1, iStatBuffAmount, false, true));
                    break;
                case Combat.Unit.statType.dexterity:
                    Game1.getChamp().gainBuff(new Combat.Buff(Combat.Buff.buffType.dexterity, 1, iStatBuffAmount, false, true));
                    break;
                case Combat.Unit.statType.resilience:
                    Game1.getChamp().gainBuff(new Combat.Buff(Combat.Buff.buffType.resilience, 1, iStatBuffAmount, false, true));
                    break;
                default:
                    Game1.addToErrorLog("New Combat.Unit.statType was declared but not setup in BasicReplayableSelfBuffCard.applyBuff: " + iBuffStat.ToString());
                    break;
            }
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            switch (iBuffStat)
            {
                case Combat.Unit.statType.strength:
                    extraInfo.Add(Combat.Buff.getExtraInfo(Combat.Buff.buffType.strength));
                    break;
                case Combat.Unit.statType.dexterity:
                    extraInfo.Add(Combat.Buff.getExtraInfo(Combat.Buff.buffType.dexterity));
                    break;
                case Combat.Unit.statType.resilience:
                    extraInfo.Add(Combat.Buff.getExtraInfo(Combat.Buff.buffType.resilience));
                    break;
                default:
                    Game1.addToErrorLog("New Combat.Unit.statType was declared but not setup in BasicReplayableSelfBuffCard.getHoverInfo: " + iBuffStat.ToString());
                    break;
            }

            return extraInfo;
        }
    }
}
