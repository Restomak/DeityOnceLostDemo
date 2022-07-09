using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Equipment.Items
{
    /// <summary>
    /// Corpses are an item that represent the dead body of a Hero. If brought back to town,
    /// the player can resurrect the Hero from the Corpse and retain them in their roster.
    /// If this item is discarded, the Hero is permanently lost.
    /// </summary>
    public class Corpse : Item
    {
        public const int WIDTH = 3;
        public const int HEIGHT = 2;

        Characters.Hero _deadHero;

        public Corpse(String characterName, Characters.Hero deadHero) : base(Game1.pic_functionality_bar, WIDTH, HEIGHT, false, false,
            characterName + "'s Body", new List<String>()
            {
                characterName + "'s Body",
                "", //FIXIT write description
                ""
            }) //FIXIT get real texture
        {
            _deadHero = deadHero;
        }

        public static List<UserInterface.ExtraInfo> getExtraInfo(String characterName)
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(new UserInterface.ExtraInfos.IconAndTextInfo(Game1.pic_functionality_bar, Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE * WIDTH,
                Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE * HEIGHT, new List<String>()
            {
                characterName + "'s Body",
                "", //FIXIT write description
                ""
            }, true));

            return extraInfo;
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(new UserInterface.ExtraInfos.TextInfo(_description)); //FIXIT do we need more? figure that out when these are more implemented

            return extraInfo;
        }

        public override List<UserInterface.ExtraInfo> getHoverExtraInfo()
        {
            return getExtraInfo(_deadHero.getName());
        }

        public override void onUse()
        {
            //Cannot use
        }

        public Characters.Hero getHero()
        {
            return _deadHero;
        }
    }
}
