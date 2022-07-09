using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Rooms
{
    /// <summary>
    /// A variant of room that contains the entrance to the floor of the dungeon.
    /// Currently unused, a the 'start' roomContents will be used to show that the
    /// player can retreat from the dungeon at the cost of any treasure they obtained
    /// within. The tutorial dungeon intentionally does not have this feature.
    /// </summary>
    public class StartRoom : Room
    {
        public StartRoom()
        {
            _randomBattleChanceOnReturn = NO_BATTLE_CHANCE_ON_RETURN;
            _roomContents.Add(roomContents.start);
            reveal();
        }
    }
}
