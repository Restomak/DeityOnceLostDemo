using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface
{
    class MapUI
    {
        List<UserInterface> _wholeUI;

        UserInterface _rooms;

        public MapUI(Dungeon.DungeonHandler dungeonHandler)
        {
            _wholeUI = new List<UserInterface>();

            _rooms = new UserInterface();

            //added in sorted fashion, top to bottom is front of the screen to back
            _wholeUI.Add(_rooms);
        }

        //Setters
        public void setAsActiveUI(List<UserInterface> activeUI)
        {
            activeUI.Clear();

            for (int i = 0; i < _wholeUI.Count; i++)
            {
                activeUI.Add(_wholeUI[i]);
            }
            
            Game1.addTopBar();
        }



        public void updateMapUI(Dungeon.DungeonHandler dungeonHandler)
        {
            _rooms.resetClickables();

            Clickables.MapGrid.setupRoomUI(_rooms, dungeonHandler.getCurrentFloor().getRooms());

            Game1.updateTopBar();
        }
    }
}
