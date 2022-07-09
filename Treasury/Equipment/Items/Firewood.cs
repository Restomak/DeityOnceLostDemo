using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Equipment.Items
{
    /// <summary>
    /// Firewood is used out of combat in order to stop and rest. The champion and party members
    /// will be healed for 25% of their maximum hitpoints, and the player will be given three
    /// options to choose from for an added benefit:
    ///    - Healing another 25% of maximum hitpoints
    ///    - Gaining a rest-based Blessing(these are powerful, but have a short duration)
    ///    - Empowering (upgrading) a card in their deck
    /// </summary>
    public class Firewood : Item
    {
        /* Resting using Firewood is powerful, and so with its use comes a drawback: each time the player uses Firewood to rest,
         * there becomes an increased chance of it triggering one of the floor's rest encounters instead of gaining any of the
         * benefits. If the floor is out of rest encounters, it will instead trigger the floor's rest miniboss (if that has
         * already been defeated, resting will always succeed). The chances of combat are as follows:
         *    - First use: 0% chance
         *    - After first successful use: 20% chance
         *    - After second successful use: 36% chance (0.8 * 0.8 chance of not occuring)
         *    - After third successful use: 49% chance (0.8 * 0.8 * 0.8 chance of not occuring)
         *    - And so on (the upper limit is the maximum possible amount of Firewood you can carry + find in a dungeon)
         * 
         * These chances reset only after exiting a dungeon. Firewood is not consumed if combat is triggered instead of resting.
         */

        public const int WIDTH = 2;
        public const int HEIGHT = 1;

        public const double SUCCESS_CHANCE = 0.8;

        public Firewood() : base(Game1.pic_item_firewood, WIDTH, HEIGHT, true, false, "Firewood", new List<String>()
            {
                "Firewood:",
                "Use out of combat to rest.",
                "Resting heals you for 25% of",
                "max HP and lets you choose",
                "one of three extra benefits."
            })
        {

        }

        public static List<UserInterface.ExtraInfo> getExtraInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(new UserInterface.ExtraInfos.IconAndTextInfo(Game1.pic_item_firewood, Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE * WIDTH,
                Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE * HEIGHT, new List<String>()
            {
                "Firewood:",
                "Use out of combat to rest.",
                "Resting heals you for 25% of",
                "max HP and lets you choose",
                "one of three extra benefits."
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
            bool success = true;
            int numTimesRestedSuccessfully = Game1.getDungeonHandler().getNumTimesRestedSuccessfully();
            
            if (numTimesRestedSuccessfully > 0)
            {
                double successChance = Math.Pow(SUCCESS_CHANCE, numTimesRestedSuccessfully);
                success = Game1.randChance(successChance);
            }

            if (success)
            {
                Game1.getDungeonHandler().increaseNumTimesRestedSuccessfully();
                Game1.startEvent(new Events.FirewoodRest_Success(), false);
            }
            else
            {
                Combat.Encounter restEncounter = Game1.getDungeonHandler().getCurrentFloor().getRestEncounter();
                if (restEncounter != null)
                {
                    Game1.getDungeonHandler().enterRestEncounter(restEncounter);
                }
                else
                {
                    //No rest encounters left; no need to increment the rest success counter
                    Game1.startEvent(new Events.FirewoodRest_Success(), false);
                }
            }
        }
    }
}
