using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Treasures.Blessings
{
    /// <summary>
    /// Your first attack each combat deals double damage.
    /// </summary>
    class ScarletCloth : Blessing
    {
        public ScarletCloth() : base(Game1.pic_relic_scarletCloth, "Scarlet Cloth", new List<String>()
            { "Scarlet Cloth",
              "",
              "Your first attack each",
              "combat deals double damage."
            }, true)
        {

        }

        public static List<UserInterface.ExtraInfo> getExtraInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(new UserInterface.ExtraInfos.IconAndTextInfo(Game1.pic_relic_scarletCloth, Drawing.DrawConstants.TOPBAR_RELICS_SIZE,
                Drawing.DrawConstants.TOPBAR_RELICS_SIZE, new List<String>()
            { "Scarlet Cloth",
              "",
              "Your first attack each",
              "combat deals double damage."
            }));

            return extraInfo;
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(new UserInterface.ExtraInfos.TextInfo(_description));

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
            Game1.getChamp().setDoubleDamage(true);
        }

        public override void onChampionAttacked()
        {
            Game1.getChamp().setDoubleDamage(false);
            _active = false;
        }

        public override void onCombatEnd()
        {
            _active = true;
        }
    }
}
