using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Rooms
{
    public class StoryRoom : Room
    {
        //FIXIT add story stuff - for now it's being treated as an empty room

        public StoryRoom()
        {
            _randomBattleChanceOnReturn = NO_BATTLE_CHANCE_ON_RETURN;
            _roomContents.Add(roomContents.story);
        }
    }
}
