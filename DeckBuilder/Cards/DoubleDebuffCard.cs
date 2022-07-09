using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    /// <summary>
    /// Base class to make card design easier for cards that apply two debuffs to an enemy.
    /// </summary>
    abstract class DoubleDebuffCard : BasicDebuffCard
    {
        public DoubleDebuffCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity,
            Combat.Buff.buffType buffType, Combat.Buff.buffType buffType2,
            int duration, int duration2, int amount, int amount2,
            bool hasDuration, bool hasDuration2, bool hasAmount, bool hasAmount2) :
            base(name, cardType, rarity, buffType, duration, amount, hasDuration, hasAmount)
        {
            iBuffType2 = buffType2;
            iBuffDuration2 = duration2;
            iBuffAmount2 = amount2;
            iHasDuration2 = hasDuration2;
            iHasAmount2 = hasAmount2;
        }

        public Combat.Buff.buffType iBuffType2 { get; }
        public int iBuffDuration2 { get; }
        public int iBuffAmount2 { get; }
        public bool iHasDuration2 { get; }
        public bool iHasAmount2 { get; }

        public override void onPlay()
        {
            iApplyDebuff();
            applyDebuff2();
        }

        public virtual void applyDebuff2()
        {
            if (_target != null)
            {
                _target.gainBuff(new Combat.Buff(iBuffType2, iBuffDuration2, iBuffAmount2, iHasDuration2, iHasAmount2));
            }
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = base.getHoverInfo();

            extraInfo.Add(Combat.Buff.getExtraInfo(iBuffType2));

            return extraInfo;
        }
    }
}
