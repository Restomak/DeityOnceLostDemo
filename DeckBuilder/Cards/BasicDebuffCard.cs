using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    /// <summary>
    /// Base class to make card design easier for cards that apply a debuff to an enemy.
    /// </summary>
    abstract class BasicDebuffCard : Card, IDebuffCard
    {
        public BasicDebuffCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, Combat.Buff.buffType buffType,
            int duration, int amount, bool hasDuration, bool hasAmount) :
            base(name, cardType, rarity, CardEnums.TargetingType.enemies)
        {
            iBuffType = buffType;
            iBuffDuration = duration;
            iBuffAmount = amount;
            iHasDuration = hasDuration;
            iHasAmount = hasAmount;
        }

        public Combat.Buff.buffType iBuffType { get; }
        public int iBuffDuration { get; }
        public int iBuffAmount { get; }
        public bool iHasDuration { get; }
        public bool iHasAmount { get; }

        public override void onPlay()
        {
            iApplyDebuff();
        }

        public virtual void iApplyDebuff()
        {
            if (_target != null)
            {
                _target.gainBuff(new Combat.Buff(iBuffType, iBuffDuration, iBuffAmount, iHasDuration, iHasAmount));
            }
            else
            {
                Game1.addToErrorLog("Attempted to use debuff card without a target!");
            }
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(Combat.Buff.getExtraInfo(iBuffType));

            return extraInfo;
        }
    }
}
