using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Dungeon
{
    public class DungeonHandler
    {
        Dungeon _dungeon;
        Floor _currentFloor;
        UserInterface.MapUI _mapUI;
        Point _playerLocation;
        bool _currentRoomHandled;
        List<Room.roomContents> _currentContentHandled;

        public DungeonHandler()
        {
            _mapUI = new UserInterface.MapUI(this);
            _currentContentHandled = new List<Room.roomContents>();
        }

        //Getters
        public Point getPlayerLocation()
        {
            return _playerLocation;
        }
        public Floor getCurrentFloor()
        {
            return _currentFloor;
        }

        //Setters
        public void movePlayer(Point newLoc)
        {
            _playerLocation = newLoc;

            _currentRoomHandled = false;
            _currentContentHandled.Clear();

            _currentFloor.scout(_playerLocation);
        }



        /// <summary>
        /// Make sure setNewEncounter is called first so that the encounter's enemies are properly set up
        /// </summary>
        public void setupDungeon(List<UserInterface.UserInterface> activeUI, Dungeon dungeon)
        {
            Game1.debugLog.Add("Setting up dungeon");

            _mapUI.setAsActiveUI(activeUI);

            _dungeon = dungeon;
            _currentFloor = _dungeon.getFloor();
            _playerLocation = _currentFloor.getStart();
            _currentRoomHandled = false;
            _currentContentHandled.Clear();

            _currentFloor.scout(_playerLocation);

            _mapUI.updateMapUI(this);
        }

        public void returnToDungeon(List<UserInterface.UserInterface> activeUI)
        {
            Game1.debugLog.Add("Returning to dungeon");

            _mapUI.setAsActiveUI(activeUI);
            _mapUI.updateMapUI(this);
        }

        public void handleDungeonLogic()
        {
            Room currentRoom = _currentFloor.getRooms()[_playerLocation.X][_playerLocation.Y];

            if (!_currentRoomHandled)
            {
                //Pull room's contents to see if we missed anything
                if (_currentContentHandled.Count == 0)
                {
                    _currentContentHandled = currentRoom.getRoomContents();
                }

                //Check if there's anything left
                if (_currentContentHandled.Count == 0)
                {
                    _currentRoomHandled = true;
                }
                else
                {
                    //Deal with the top of the list
                    switch (_currentContentHandled[0])
                    {
                        case Room.roomContents.combat:
                            Game1.enterNewCombat(currentRoom.getRoomCombat(), currentRoom);
                            break;
                        case Room.roomContents.story:
                            currentRoom.finishTopContent(); //FIXIT actually do story stuff
                            break;
                        default:
                            Game1.errorLog.Add("handleDungeonLogic attempting to handle roomContents that haven't been implemented: " + _currentContentHandled[0].ToString());
                            currentRoom.finishTopContent();
                            break;
                    }
                }
            }
        }

        public void updateMapUI()
        {
            _mapUI.updateMapUI(this);
        }
    }
}
