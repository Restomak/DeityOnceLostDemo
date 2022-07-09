using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Rooms
{
    /// <summary>
    /// A variant of room that contains the floor's exit.
    /// </summary>
    public class ExitRoom : Room
    {
        public ExitRoom()
        {
            _randomBattleChanceOnReturn = NO_BATTLE_CHANCE_ON_RETURN;
            _roomContents.Add(roomContents.exit);
        }
    }
}
