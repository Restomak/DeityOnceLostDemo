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
    /// Abstract base class for blessings, the variant of relics most similar to the ones
    /// found throughout Slay the Spire. They generally provide net-positive effects, and
    /// most will last throughout an entire dungeon run.
    /// 
    /// NOTE: A rule that separates blessings from curses is that blessings are optional.
    /// Appearing either in loot, an event, or a shop, there will always be the option to
    /// simply not take it.For events, that means an option to take another good effect
    /// or nothing at all.Players should never feel "stuck" taking a blessing.
    /// </summary>
    public abstract class Blessing : Relic
    {
        /* Possible blessing ideas
         * 
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
         *    - If you do / don't play a specific type of card / cost of card
         *    - On damage taken
         *    - Above / below % HP
         *    - When applying buff / debuff
         *    - On deck shuffle
         *    - When a card dissipates
         *    - On curse gain
         *    - On discard
         *    - When defense gets broken
         *    - When enemy defense gets broken
         *    
         *    - Until x condition
         *    - Depends on # cards in deck
         * 
         * Possible effects
         *    - (party-based blessings)
         *    - (karma-or-soul-based blessings)
         *    - (map-based blessings)
         *    - (something from Happening - positives)
         *    - Increase max HP (persists & not)
         *    - Gain one or more buffs
         *    - Enemies gain one or more debuffs
         *    - Affects loot positively
         *    - Enemies have less HP
         *    - Higher chance of rarer cards
         *    - Higher chance of specific loot
         *    - Your buffs have a higher effect
         *    - Debuffs on enemies have a higher effect
         *    - Immunity to damage
         *    - Deal damage / more damage / double damage
         *    - Revive
         *    - Extra divinity
         *    - Reduced karma / void gain
         *    - Reduced blood cost
         *    - Cards costing less
         *    - Cards played twice
         *    - Gain more gold
         *    - Draw specific card
         *    - Increased card draw
         *    - Gain defense
         *    - Negate curses
         *    - Makes certain cards Dissipate
         *    - More options for card choices
         *    - Reduced prices
         *    - Lower defense loss instead of all each turn
         *    - Lower divinity loss instead of all each turn
         *    - Discarding
         *    - Lose less HP
         *    - Increased healing
         *    - Transferring debuffs
         *    - Cards don't dissipate
         */

        public Blessing(Texture2D texture, String name, List<String> description, bool persists) : base(texture, name, description, persists, treasureType.blessing)
        {

        }

        public override Color getBorderColor()
        {
            return Color.PowderBlue;
        }
    }
}
