using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeityOnceLost.Combat;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.RareCards
{
    /// <summary>
    /// Hunter's Mark | Rare
    ///     (Costs 0 Divinity)
    ///     (Empowered)
    /// Apply 3 Vulnerable.
    /// Gain 1 Strength.
    /// Dissipates.
    /// </summary>
    class HuntersMark_Empowered : RegularCards.RareCards.HuntersMark, IStatBuffCard
    {
        public const int EMPOWERED_STRENGTH_AMOUNT = 1;

        public HuntersMark_Empowered() : base()
        {
            _empowered = true;

            iBuffStat = Unit.statType.strength;
            iStatBuffAmount = EMPOWERED_STRENGTH_AMOUNT;
        }

        public Unit.statType iBuffStat { get; }

        public int iStatBuffAmount { get; }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override void onPlay()
        {
            base.onPlay();
            iApplyBuff();
        }

        public void iApplyBuff()
        {
            Game1.getChamp().gainBuff(new Buff(Buff.buffType.strength, 1, iStatBuffAmount, false, true));
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = base.getDescription(champ, activeCard);

            desc.Insert(desc.Count - 1, "Gain " + iStatBuffAmount + " " + iBuffStat.ToString() + ".");

            return desc;
        }

        public override List<String> getStylizedDescription(Characters.Champion champ, int descFontSize, bool activeCard = false)
        {
            List<String> desc = base.getStylizedDescription(champ, descFontSize, activeCard);

            desc.Insert(desc.Count - 1, "[f: " + descFontSize + " m]" + "[s: Black][c: Lawn Green]Gain " + iStatBuffAmount + " " + iBuffStat.ToString() + ".");

            return desc;
        }

        public override Card getNewCard()
        {
            return new HuntersMark_Empowered();
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = base.getHoverInfo();

            extraInfo.Add(Combat.Buff.getExtraInfo(Combat.Buff.buffType.strength));

            return extraInfo;
        }
    }
}
