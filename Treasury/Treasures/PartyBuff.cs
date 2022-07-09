using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Treasury.Treasures
{
    /// <summary>
    /// Abstract base class for the various relic-style buffs given by having party members
    /// along. Inherits from Relic, so that it can make use of the same wide variety of
    /// functions. Since PartyBuffs cannot be obtained through any usual means of obtaining
    /// Treasure, some redundant things (such as the onTaken function) are unnecessary.
    /// </summary>
    public abstract class PartyBuff : Relic
    {
        /* Ideas for future Party Buffs
         * 
         * At the start of combat, draw two extra cards. Pick a card from the starting hand (can choose none) - it dissipates
         * At the end of your turn, if you have one or more divinity left over, carries over one to the next turn
         * 
         * 
         * note to self: eventually unlock third party member slot through townbuilding
         */
        public PartyBuff(Texture2D texture, String name, List<String> description) : base(texture, name, description, true, treasureType.partyBuff)
        {

        }

        public override Color getBorderColor()
        {
            return Color.GreenYellow;
        }

        public override void onTaken()
        {
            //Uneccessary since they won't show up as loot
        }

        /// <summary>
        /// At least one card has the ability to trigger party member buffs outside of
        /// their usual time to be triggered, and so party member buffs require this
        /// function in order to do so.
        /// </summary>
        public abstract void onCardProc();

        public override List<UserInterface.ExtraInfo> getHoverExtraInfo()
        {
            return null; //Not needed
        }
    }
}
