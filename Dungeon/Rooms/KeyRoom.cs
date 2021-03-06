using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Rooms
{
    /// <summary>
    /// A variant of TreasureRoom that contains a key in its Loot object, possibly
    /// along with other Treasures.
    /// </summary>
    class KeyRoom : TreasureRoom
    {
        protected Treasury.Equipment.Key _roomKey;

        public KeyRoom(Treasury.Equipment.Key roomKey) : base(roomKey)
        {
            _roomKey = roomKey;
        }

        public override Treasury.Equipment.Key getRoomKey()
        {
            return _roomKey;
        }
    }
}
