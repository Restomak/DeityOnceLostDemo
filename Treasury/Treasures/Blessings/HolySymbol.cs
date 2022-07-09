using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Treasures.Blessings
{
    /// <summary>
    /// At the start of each combat, empower a random card in your hand.
    /// </summary>
    class HolySymbol : Blessing
    {
        public HolySymbol() : base(Game1.pic_relic_holySymbol, "Holy Symbol", new List<String>()
            { "Holy Symbol",
              "",
              "At the start of each combat,",
              "empower a random card in",
              "your hand."
            }, true)
        {

        }

        public static List<UserInterface.ExtraInfo> getExtraInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(new UserInterface.ExtraInfos.IconAndTextInfo(Game1.pic_relic_holySymbol, Drawing.DrawConstants.TOPBAR_RELICS_SIZE,
                Drawing.DrawConstants.TOPBAR_RELICS_SIZE, new List<String>()
            { "Holy Symbol",
              "",
              "At the start of each combat,",
              "empower a random card in",
              "your hand."
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

        public override void onCombatStartAfterFirstDraw()
        {
            List<DeckBuilder.Card> empowerPossibilities = new List<DeckBuilder.Card>();
            List<DeckBuilder.Card> cardsInHand = Game1.getChamp().getDeck().getHand();
            for (int i = 0; i < cardsInHand.Count; i++)
            {
                if (!cardsInHand[i].isEmpowered())
                {
                    empowerPossibilities.Add(cardsInHand[i]);
                }
            }

            if (empowerPossibilities.Count > 0)
            {
                int randomIndex = Game1.randint(0, empowerPossibilities.Count - 1);
                int cardReplaceIndex = Game1.getChamp().getDeck().getHand().IndexOf(empowerPossibilities[randomIndex]);
                
                Game1.getChamp().getDeck().getHand().RemoveAt(cardReplaceIndex);
                Game1.getChamp().getDeck().getHand().Insert(cardReplaceIndex, empowerPossibilities[randomIndex].getEmpoweredCard());
            }
        }
    }
}
