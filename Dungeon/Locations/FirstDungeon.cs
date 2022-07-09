using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Locations
{
    /// <summary>
    /// The tutorial dungeon of the game, this dungeon's purpose is to introduce the
    /// player to the game's various mechanics one at a time, with each floor made
    /// slightly more intricate to ease the player in.
    /// 
    /// On the first floor, the player is given a champion and shown combat and that
    /// the game is a deckbuilder similar to Slay the Spire.
    /// 
    /// On the second floor, the player is introduced to events with choices, as well
    /// as the idea that the maps won't have a linear path. The player is also
    /// introduced to having party members, and requiring a key to proceed past some
    /// doors.
    /// 
    /// On the third floor, the player is introduced to items, relics, a miniboss
    /// encounter, and card empowerment, as well as the risk that their champion
    /// might die if they're not careful with how they play the game.
    /// 
    /// The demo ends after the third floor, although more is planned.
    /// </summary>
    public class FirstDungeon : Dungeon
    {
        public FirstDungeon()
        {
            _floors.Add(new Floors.FirstDungeon.Beginning());
            _floors.Add(new Floors.FirstDungeon.SecondFloor());
            _floors.Add(new Floors.FirstDungeon.ThirdFloor());
        }
    }
}
