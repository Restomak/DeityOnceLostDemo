using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Treasures
{
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
        public PartyBuff(String name, List<String> description) : base(name, description, true, treasureType.partyBuff)
        {

        }

        public override void onTaken()
        {
            //Uneccessary since they won't show up as loot
        }
    }
}
