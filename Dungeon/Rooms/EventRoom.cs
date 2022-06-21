using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Rooms
{
    class EventRoom : Room
    {
        protected Events.Happening _roomEvent;

        public EventRoom()
        {
            _randomBattleChanceOnReturn = DEFAULT_RANDOM_BATTLE_CHANCE_ON_RETURN;
            _roomContents.Add(roomContents.happening);
        }
        public EventRoom(Events.Happening specificRoomEvent)
        {
            _randomBattleChanceOnReturn = DEFAULT_RANDOM_BATTLE_CHANCE_ON_RETURN;
            _roomContents.Add(roomContents.happening);

            _roomEvent = specificRoomEvent;
        }

        public virtual void setRoomEvent(Events.Happening roomEvent)
        {
            _roomEvent = roomEvent;
        }

        public override Events.Happening getRoomEvent()
        {
            return _roomEvent;
        }
    }
}
