using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface
{
    /// <summary>
    /// One of the major three gameState UIs along with CombatUI and EventUI. MapUI is used
    /// most by the DungeonController, and it stores the individual rooms as MapGrids. It
    /// also stores the player's relics (blessings and curses) for display.
    /// </summary>
    class MapUI
    {
        List<UserInterface> _wholeUI;
        UserInterface _rooms;
        UserInterface _relics;

        public MapUI(Dungeon.DungeonHandler dungeonHandler)
        {
            _wholeUI = new List<UserInterface>();

            _rooms = new UserInterface();
            _relics = new UserInterface();

            //added in sorted fashion, top to bottom is front of the screen to back
            _wholeUI.Add(_rooms);
            _wholeUI.Add(_relics);
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



        /// <summary>
        /// Makes the calls to set up the rooms as MapGrids and player's relics as RelicDisplays.
        /// Also makes sure the top bar updates.
        /// </summary>
        public void updateMapUI(Dungeon.DungeonHandler dungeonHandler)
        {
            _rooms.resetClickables();
            _relics.resetClickables();

            Clickables.MapGrid.setupRoomUI(_rooms, dungeonHandler.getCurrentFloor().getRooms());
            Clickables.Hovers.RelicDisplay.setupRelicsUI(_relics);

            Game1.updateTopBar();
        }
    }
}
