using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Characters.PartyBuffs
{
    class Healer : Treasury.Treasures.PartyBuff
    {
        public const int HEAL_AMOUNT = 3;
        public const int HEAL_AMOUNT_PARTY = 1;

        public Healer() : base("Healer", new List<String>()
            { "This hero is a healer, and their job is",
              "to look out for the others of their team.",
              "At the start of each combat, will heal",
              "your champion for " + HEAL_AMOUNT + ". If your champion is",
              "full on hitpoints, will heal the party."
            })
        {

        }

        public override void onCombatStart()
        {
            if (Game1.getChamp().getCurrentHP() < Game1.getChamp().getMaxHP())
            {
                Game1.getChamp().heal(HEAL_AMOUNT);
            }
            else
            {
                for (int i = 0; i < Game1.getCombatHandler().getParty().Count; i++)
                {
                    Game1.getCombatHandler().getParty()[i].heal(HEAL_AMOUNT_PARTY);
                }
            }
        }

        public override void onCardProc()
        {
            onCombatStart();
        }
    }
}
