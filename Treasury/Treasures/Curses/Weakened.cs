using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Treasures.Curses
{
    /// <summary>
    /// Start the next combat with 3 Feeble and Sluggish.
    /// </summary>
    public class Weakened : Curse
    {
        public const int DEBUFFS_DURATION = 3;

        public Weakened() : base(Game1.pic_relic_weakened, "Weakened", new List<String>()
            { "Weakened",
              "",
              "Start the next combat with",
              "3 Feeble and Sluggish."
            }, false)
        {

        }

        public static List<UserInterface.ExtraInfo> getExtraInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(new UserInterface.ExtraInfos.IconAndTextInfo(Game1.pic_relic_weakened, Drawing.DrawConstants.TOPBAR_RELICS_SIZE,
                Drawing.DrawConstants.TOPBAR_RELICS_SIZE, new List<String>()
            { "Weakened",
              "",
              "Start the next combat with",
              "3 Feeble and Sluggish."
            }));

            extraInfo.AddRange(getAttachedExtraInfo());

            return extraInfo;
        }

        private static List<UserInterface.ExtraInfo> getAttachedExtraInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(Combat.Buff.getExtraInfo(Combat.Buff.buffType.feeble));
            extraInfo.Add(Combat.Buff.getExtraInfo(Combat.Buff.buffType.sluggish));

            return extraInfo;
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(new UserInterface.ExtraInfos.TextInfo(_description));
            extraInfo.AddRange(getAttachedExtraInfo());

            return extraInfo;
        }

        public override List<UserInterface.ExtraInfo> getHoverExtraInfo()
        {
            return getExtraInfo();
        }

        public override void onTaken()
        {
            Game1.getDungeonHandler().addRelic(this);
            _taken = true;
            Game1.updateTopBar();
        }

        public override void onCombatStart()
        {
            Game1.getChamp().gainBuff(new Combat.Buff(Combat.Buff.buffType.feeble, DEBUFFS_DURATION, 1, true, false));
            Game1.getChamp().gainBuff(new Combat.Buff(Combat.Buff.buffType.sluggish, DEBUFFS_DURATION, 1, true, false));

            Game1.getDungeonHandler().removeRelic(this);
        }
    }
}
