using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Treasures
{
    public abstract class Curse : Relic
    {
        /* Possible curse ideas
         * 
         * NOTE: A rule that separates blessings from curses is that curses are not always
         * optional. They only appear in events, or as a result of combat somehow. While
         * taking a blessing can always be avoided, players will either feel the curse was
         * put upon them, or they took it as the lesser of evils.
         * 
         * Possible conditions:
         *    - At the start of combat / miniboss / boss
         *    - At the start of each turn / end of each turn / every # turns / after # turns
         *    - Upon getting loot
         *    - Each room / each room of x type
         *    - When spending gold
         *    - Upon losing HP
         *    - When playing a card
         *    - Upon adding/removing/transforming/upgrading a card
         *    - On death / enemy death / party member death
         *    - On rest
         *    - If you do / don't play a specific type of card / # in a turn
         *    - On damage taken
         *    - Above / below % HP
         *    - When applying buff / debuff
         *    - On deck shuffle
         *    - When a card dissipates
         *    - On blessing gain
         *    - On discard
         *    - When defense gets broken
         *    - When enemy defense gets broken
         *    
         *    - Until x condition
         *    - Depends on # cards in deck
         * 
         * Possible effects
         *    - (party-based curses)
         *    - (karma-or-soul-based curses)
         *    - (map-based curses)
         *    - (something from Happening - negatives)
         *    - Decrease max HP (persists & not)
         *    - Gain one or more debuffs
         *    - Enemies gain one or more buffs
         *    - Affects loot negatively
         *    - Your debuffs have a higher effect
         *    - Buffs on enemies have a higher effect
         *    - Extra karma / void gain
         *    - Cards costing more
         *    - Gain less gold
         *    - Can no longer heal
         *    - Can no longer gain specific loot
         *    - Limit cards played
         *    - Decreased card draw
         *    - Negate blessings
         *    - Makes certain cards Dissipate
         *    - Less options for card choices
         *    - Increased prices
         *    - Discarding
         *    - Lose more HP
         *    - Immune to one or more debuffs
         *    - Decreased healing
         *    - Inability to see intents
         *    - Cards don't dissipate
         *    
         *    - Can no longer upgrade (FIXIT figure out card upgrading system)
         */

        public Curse(String name, List<String> description, bool persists) : base(name, description, persists, treasureType.curse)
        {

        }
    }
}
