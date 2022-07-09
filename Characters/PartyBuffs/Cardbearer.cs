using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Characters.PartyBuffs
{
    /// <summary>
    /// At the start of each combat, you will draw 2 extra cards.
    /// </summary>
    class Cardbearer : Treasury.Treasures.PartyBuff
    {
        public const int CARD_DRAW_AMOUNT = 2;

        public Cardbearer() : base(Game1.pic_functionality_bar, "Cardbearer", new List<String>()
            { "This hero is a cardbearer, and their job",
              "is to support your champion in battle.",
              "At the start of each combat, you will",
              "draw " + CARD_DRAW_AMOUNT + " extra cards."
            })
        {

        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            return null;
        }

        public override void onCombatStart()
        {
            Game1.getChamp().getDeck().drawNumCards(CARD_DRAW_AMOUNT);
        }

        public override void onCardProc()
        {
            onCombatStart();
        }
    }
}
