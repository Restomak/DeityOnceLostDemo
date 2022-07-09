using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Rooms
{
    /// <summary>
    /// A variant of room that contains a combat encounter.
    /// </summary>
    class CombatRoom : Room
    {
        public CombatRoom(Combat.Encounter roomEncounter)
        {
            _randomBattleChanceOnReturn = DEFAULT_RANDOM_BATTLE_CHANCE_ON_RETURN;
            _roomContents.Add(roomContents.combat);
            _roomEncounter = roomEncounter;
        }
    }
}
