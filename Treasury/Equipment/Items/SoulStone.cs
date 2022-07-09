using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Treasury.Equipment.Items
{
    /// <summary>
    /// Soul Stones are a currency item that drops from slain enemies, most notably minibosses,
    /// and higher quality ones from bosses. Uncommonly, smaller and lower quality Soul Stones
    /// can be found from regular combat encounters. Soul Stones can only be used as currency
    /// once the player returns to town, and so they are a tempting waste of inventory space
    /// during the dungeon run.
    /// </summary>
    class SoulStone : Item
    {
        public enum soulSize
        {
            lesser,
            greater,
            grand
        }

        public const int WIDTH = 1;
        public const int HEIGHT = 1;

        soulSize _soulSize;

        public SoulStone(soulSize size) : base(getTextureFromSize(size), WIDTH, HEIGHT, false, false, getNameFromSize(size), getDescriptionFromSize(size))
        {
            _soulSize = size;
        }

        private static Texture2D getTextureFromSize(soulSize size) //FIXIT get real textures
        {
            switch (size)
            {
                case soulSize.lesser:
                    return Game1.pic_item_soulStone;
                case soulSize.greater:
                    return Game1.pic_item_soulStone;
                case soulSize.grand:
                    return Game1.pic_item_soulStone;
                default:
                    Game1.addToErrorLog("Cannot retrieve texture for soul stone of size " + size.ToString());
                    return Game1.pic_item_soulStone;
            }
        }

        private static String getNameFromSize(soulSize size)
        {
            switch (size)
            {
                case soulSize.lesser:
                    return "Lesser Soul Stone";
                case soulSize.greater:
                    return "Greater Soul Stone";
                case soulSize.grand:
                    return "Grand Soul Stone";
                default:
                    Game1.addToErrorLog("Cannot retrieve name for soul stone of size " + size.ToString());
                    return "Soul Stone?";
            }
        }

        private static List<String> getDescriptionFromSize(soulSize size) //FIXIT make the descriptions more individual per size?
        {
            List<String> description = new List<String>();

            switch (size)
            {
                case soulSize.lesser:
                    description.Add("Lesser Soul Stone");
                    break;
                case soulSize.greater:
                    description.Add("Greater Soul Stone");
                    break;
                case soulSize.grand:
                    description.Add("Grand Soul Stone");
                    break;
                default:
                    description.Add("Soul Stone?");
                    Game1.addToErrorLog("Cannot description name for soul stone of size " + size.ToString());
                    break;
            }

            description.Add("Nearly invisible to your");
            description.Add("followers, but not to you.");
            description.Add("The fragment of a creature's");
            description.Add("soul, ready for harvesting.");
            description.Add("This may be of use to you");
            description.Add("once your champion and");
            description.Add("followers escape the dungeon.");

            return description;
        }

        public static List<UserInterface.ExtraInfo> getExtraInfo(soulSize size)
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(new UserInterface.ExtraInfos.IconAndTextInfo(getTextureFromSize(size), Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE * WIDTH,
                Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE * HEIGHT, getDescriptionFromSize(size), true));

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
            return getExtraInfo(_soulSize);
        }



        public override void onUse()
        {
            //Cannot use
        }

        public soulSize getSize()
        {
            return _soulSize;
        }
    }
}
