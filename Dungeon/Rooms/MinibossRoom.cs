using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Rooms
{
    /// <summary>
    /// A variant of room that contains a miniboss combat encounter.
    /// </summary>
    class MinibossRoom : Room
    {
        public MinibossRoom(Combat.Encounter roomEncounter)
        {
            _roomContents.Add(roomContents.miniboss);
            _roomEncounter = roomEncounter;
        }
    }
}
