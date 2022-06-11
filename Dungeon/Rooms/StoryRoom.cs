using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Rooms
{
    public class StoryRoom : Room
    {
        Events.Happening _roomEvent;

        public StoryRoom()
        {
            _randomBattleChanceOnReturn = NO_BATTLE_CHANCE_ON_RETURN;
            _roomContents.Add(roomContents.story);
        }

        public void setRoomEvent(Events.Happening roomEvent)
        {
            _roomEvent = roomEvent;
        }

        public override Events.Happening getRoomEvent()
        {
            return _roomEvent;
        }
    }
}
