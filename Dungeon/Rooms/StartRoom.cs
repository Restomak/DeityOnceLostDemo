using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Rooms
{
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
