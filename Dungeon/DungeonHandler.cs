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
        bool _currentRoomHandled, _isRandomEncounter;
        List<Room.roomContents> _currentContentHandled;
        int _gold;
        List<Treasury.Treasures.Relic> _relics;

        public DungeonHandler()
        {
            _mapUI = new UserInterface.MapUI(this);
            _currentContentHandled = new List<Room.roomContents>();
            _gold = 0;
            _isRandomEncounter = false;
            _relics = new List<Treasury.Treasures.Relic>();
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
        public int getGold()
        {
            return _gold;
        }
        public bool isRandomEncounter()
        {
            return _isRandomEncounter;
        }
        public List<Treasury.Treasures.Relic> getRelics()
        {
            return _relics;
        }

        //Setters
        public void movePlayer(Point newLoc)
        {
            _playerLocation = newLoc;

            _currentRoomHandled = false;
            _currentContentHandled.Clear();

            _currentFloor.scout(_playerLocation);

            _isRandomEncounter = false;
        }
        public void addGold(int amount)
        {
            _gold += amount;
        }
        public void randomEncounterComplete()
        {
            _isRandomEncounter = false;
        }
        public void setStartingRelics(List<Treasury.Treasures.Relic> startingRelics)
        {
            _relics = startingRelics;
        }
        public void addRelic(Treasury.Treasures.Relic relic)
        {
            _relics.Add(relic);
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

        private void proceedToNextFloor()
        {
            Game1.debugLog.Add("Proceeding to the next floor");

            _currentFloor = _dungeon.getNextFloor();

            if (_currentFloor == null)
            {
                Game1.errorLog.Add("Next floor or end of dungeon run not yet implemented!"); //FIXIT implement
            }
            
            _playerLocation = _currentFloor.getStart();
            _currentRoomHandled = false;
            _currentContentHandled.Clear();

            _currentFloor.scout(_playerLocation);
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
                    if (_currentFloor.getRooms()[_playerLocation.X][_playerLocation.Y].onVisit_hasRandomEncounter())
                    {
                        _isRandomEncounter = true;
                        Game1.enterNewCombat(_currentFloor.getRandomEncounter(), currentRoom);
                    }
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
                        case Room.roomContents.happening:
                            if (currentRoom.getRoomEvent() != null)
                            {
                                Game1.startEvent(currentRoom);
                            }
                            else
                            {
                                currentRoom.finishTopContent();
                                Game1.errorLog.Add("Skipping currentRoom.getRoomEvent because it's null");
                            }
                            break;
                        case Room.roomContents.exit:
                            proceedToNextFloor();
                            Game1.returnToDungeon();
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
