using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Equipment.Items
{
    /// <summary>
    /// The Trident is an item that is usable only during combat, which creates and
    /// adds the Trident card to the player's hand.
    /// </summary>
    public class Trident : Item
    {
        public const int WIDTH = 1;
        public const int HEIGHT = 2;

        public Trident() : base(Game1.pic_item_trident, WIDTH, HEIGHT, false, true,
            "Trident", new List<String>()
            { "Trident:",
              "Adds a Trident to your hand."
            })
        {

        }

        public static List<UserInterface.ExtraInfo> getExtraInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(new UserInterface.ExtraInfos.IconAndTextInfo(Game1.pic_item_trident, Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE * WIDTH,
                Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE * HEIGHT, new List<String>()
            { "Trident:",
              "Adds a Trident to your hand."
            }, true));

            extraInfo.Add(new UserInterface.ExtraInfos.CardInfo(new DeckBuilder.Cards.RegularCards.NonCollection.ItemCards.Trident()));

            return extraInfo;
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(new UserInterface.ExtraInfos.TextInfo(_description));
            extraInfo.Add(new UserInterface.ExtraInfos.CardInfo(new DeckBuilder.Cards.RegularCards.NonCollection.ItemCards.Trident()));

            return extraInfo;
        }

        public override List<UserInterface.ExtraInfo> getHoverExtraInfo()
        {
            return getExtraInfo();
        }

        public override void onUse()
        {
            Game1.getChamp().getDeck().addToHand(new DeckBuilder.Cards.RegularCards.NonCollection.ItemCards.Trident());
        }
    }
}
