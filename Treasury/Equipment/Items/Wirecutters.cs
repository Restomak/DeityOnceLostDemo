using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Equipment.Items
{
    /// <summary>
    /// Wirecutters are a disarming tool usable in certain Trap Event situations. An extra option
    /// will appear if the player has one of these in inventory, allowing them to avoid the nasty
    /// effects of the trap.
    /// </summary>
    public class Wirecutters : Item
    {
        public const int WIDTH = 1;
        public const int HEIGHT = 1;

        public Wirecutters() : base(Game1.pic_item_wirecutters, WIDTH, HEIGHT, false, false, "Wirecutters", new List<String>()
            { "Wirecutters:",
              "Negates certain traps."
            })
        {

        }

        public static List<UserInterface.ExtraInfo> getExtraInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(new UserInterface.ExtraInfos.IconAndTextInfo(Game1.pic_item_wirecutters, Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE * WIDTH,
                Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE * HEIGHT, new List<String>()
            { "Wirecutters:",
              "Negates certain traps."
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
            //Cannot use
        }
    }
}
