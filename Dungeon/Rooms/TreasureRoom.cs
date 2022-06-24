using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Rooms
{
    class TreasureRoom : Room
    {
        protected Treasury.Loot _roomTreasure;

        public TreasureRoom()
        {
            _roomTreasure = new Treasury.Loot(UserInterface.Menus.LootMenu.CHEST_LOOT);
            _roomContents.Add(roomContents.treasure);
        }
        public TreasureRoom(Treasury.Equipment.Key roomKey)
        {
            _roomTreasure = new Treasury.Loot(UserInterface.Menus.LootMenu.CHEST_LOOT);
            _roomTreasure.addTreasure(roomKey);
            _roomContents.Add(roomContents.key);
        }

        public override Treasury.Loot getRoomTreasure()
        {
            return _roomTreasure;
        }

        public void setRoomTreasure(Treasury.Loot roomTreasure)
        {
            _roomTreasure = roomTreasure;
        }

        public void addRoomTreasure(Treasury.Treasure treasure)
        {
            _roomTreasure.addTreasure(treasure);
        }
    }
}
