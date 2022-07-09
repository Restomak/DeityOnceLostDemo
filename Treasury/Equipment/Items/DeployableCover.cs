using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Equipment.Items
{
    /// <summary>
    /// Deployable Cover is a disarming tool usable in certain Trap Event situations. An extra
    /// option will appear if the player has one of these in inventory, allowing them to avoid
    /// the nasty effects of the trap.
    /// 
    /// Alternatively, the player may use it in combat to gain some extra defense for one turn.
    /// </summary>
    class DeployableCover : Item
    {
        public const int WIDTH = 1;
        public const int HEIGHT = 2;
        public const int DEFENSE_GAIN = 18;

        public DeployableCover() : base(Game1.pic_item_deployableCover, WIDTH, HEIGHT, false, true, "Deployable Cover", new List<String>()
            { "Deployable Cover:",
              "Negates certain traps.",
              "Also usable in combat",
              "to gain " + DEFENSE_GAIN + " defense."
            })
        {

        }

        public static List<UserInterface.ExtraInfo> getExtraInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(new UserInterface.ExtraInfos.IconAndTextInfo(Game1.pic_item_deployableCover, Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE * WIDTH,
                Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE * HEIGHT, new List<String>()
            { "Deployable Cover:",
              "Negates certain traps.",
              "Also usable in combat",
              "to gain " + DEFENSE_GAIN + " defense."
            }, true));

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

        public override void onUse()
        {
            Game1.getChamp().gainDefense(DEFENSE_GAIN, false);
        }
    }
}
