using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Dungeon
{
    /// <summary>
    /// The game's dungeon handler, which manages the dungeon's map and contents, as
    /// well as the game logic involved in moving between locations on the map. Also
    /// handles the player's relic collection and gold.
    /// </summary>
    public class DungeonHandler
    {
        Dungeon _dungeon;
        Floor _currentFloor;
        UserInterface.MapUI _mapUI;
        Point _playerLocation;
        bool _currentRoomHandled, _isRandomEncounter, _isRestEncounter;
        List<Room.roomContents> _currentContentHandled;
        int _gold;
        List<Treasury.Treasures.Relic> _relics, _relicsToRemove;
        List<Treasury.Equipment.Key> _floorKeys;
        int _numTimesRestedSuccessfully;

        public DungeonHandler()
        {
            _mapUI = new UserInterface.MapUI(this);
            _currentContentHandled = new List<Room.roomContents>();
            _gold = 0;
            _isRandomEncounter = false;
            _isRestEncounter = false;
            _relics = new List<Treasury.Treasures.Relic>();
            _relicsToRemove = new List<Treasury.Treasures.Relic>();
            _floorKeys = new List<Treasury.Equipment.Key>();
            _numTimesRestedSuccessfully = 0;
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
        public bool isRestEncounter()
        {
            return _isRestEncounter;
        }
        public List<Treasury.Treasures.Relic> getRelics()
        {
            return _relics;
        }
        public List<Treasury.Equipment.Key> getKeys()
        {
            return _floorKeys;
        }
        public int getNumTimesRestedSuccessfully()
        {
            return _numTimesRestedSuccessfully;
        }

        //Setters
        public void movePlayer(Point newLoc)
        {
            _playerLocation = newLoc;

            _currentRoomHandled = false;
            _currentContentHandled.Clear();

            _currentFloor.scout(_playerLocation);

            _isRandomEncounter = false;
            _isRestEncounter = false;
        }
        public void addGold(int amount)
        {
            _gold += amount;
        }
        public void randomEncounterComplete()
        {
            _isRandomEncounter = false;
        }
        public void restEncounterComplete()
        {
            _isRestEncounter = false;
        }
        public void setStartingRelics(List<Treasury.Treasures.Relic> startingRelics)
        {
            _relics = startingRelics;
        }
        public void addRelic(Treasury.Treasures.Relic relic)
        {
            _relics.Add(relic);
        }
        public void removeRelic(Treasury.Treasures.Relic relic)
        {
            _relicsToRemove.Add(relic);
        }
        public void clearRemovedRelics()
        {
            for (int i = 0; i < _relicsToRemove.Count; i++)
            {
                if (_relics.Contains(_relicsToRemove[i]))
                {
                    _relics.Remove(_relicsToRemove[i]);
                    return;
                }
                else
                {
                    Game1.addToErrorLog("Tried to remove a relic that wasn't there: " + _relicsToRemove[i].getName());
                }
            }
        }
        public void addKey(Treasury.Equipment.Key newKey)
        {
            _floorKeys.Add(newKey);
            _currentFloor.unlockAllDoorsOfColor(newKey.getKeyColor());
        }
        public void increaseNumTimesRestedSuccessfully()
        {
            _numTimesRestedSuccessfully++;
        }



        /// <summary>
        /// Sets up a new dungeon, but does not set up a new dungeon run entirely (relics and
        /// gold, etc, are untouched here). Makes a call to the MapUI to setup once it's done.
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
            _floorKeys.Clear();

            _currentFloor.scout(_playerLocation);

            _mapUI.updateMapUI(this);
        }

        /// <summary>
        /// Sets up the next floor of the dungeon and places the player at the start location
        /// of that floor. Also clears the player's keys list since they are per-floor only.
        /// </summary>
        private void proceedToNextFloor()
        {
            Game1.debugLog.Add("Proceeding to the next floor");

            if (_dungeon.getNextFloor() == null)
            {
                Game1.endDemo();
                Game1.addToErrorLog("Next floor or end of dungeon run not yet implemented!"); //FIXIT implement
                return;
            }

            _dungeon.incrementFloor();
            _currentFloor = _dungeon.getFloor();

            _playerLocation = _currentFloor.getStart();
            _currentRoomHandled = false;
            _currentContentHandled.Clear();
            _floorKeys.Clear();

            _currentFloor.scout(_playerLocation);
        }

        /// <summary>
        /// Called when the engine needs to switch its gameState back under the
        /// DungeonHandler's control. Tells the MapUI to update, as this usually
        /// happens after something has been completed (eg. a combat encounter).
        /// </summary>
        public void returnToDungeon(List<UserInterface.UserInterface> activeUI)
        {
            Game1.debugLog.Add("Returning to dungeon");

            _mapUI.setAsActiveUI(activeUI);
            _mapUI.updateMapUI(this);
        }

        /// <summary>
        /// The main game loop for handling dungeon logic. Checks if there's anything
        /// remaining to be done in the room the player is currently in (and double-
        /// checks if not). If there is, it tells the engine to pass control over to
        /// whatever needs it. Since movement and such is handled via MapGrids, the
        /// DungeonHandler needs not do anything else in this function except wait.
        /// </summary>
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
                        case Room.roomContents.miniboss:
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
                                Game1.addToErrorLog("Skipping currentRoom.getRoomEvent because it's null");
                            }
                            break;
                        case Room.roomContents.treasure:
                        case Room.roomContents.key:
                            Game1.addToMenus(new UserInterface.Menus.LootMenu(currentRoom.getRoomTreasure(), UserInterface.Menus.LootMenu.CHEST_LOOT));
                            currentRoom.finishTopContent();
                            break;
                        case Room.roomContents.exit:
                            proceedToNextFloor();
                            Game1.returnToDungeon();
                            break;
                        default:
                            Game1.addToErrorLog("handleDungeonLogic attempting to handle roomContents that haven't been implemented: " + _currentContentHandled[0].ToString());
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

        /// <summary>
        /// Called when the Firewood item is used but rest is unsuccessful. Tells the engine
        /// to switch over to combat, and sets up the CombatHandler for a combat that isn't
        /// from room contents.
        /// </summary>
        public void enterRestEncounter(Combat.Encounter restEncounter)
        {
            _isRestEncounter = true;
            Room currentRoom = _currentFloor.getRooms()[_playerLocation.X][_playerLocation.Y];
            Game1.enterNewCombat(restEncounter, currentRoom);
        }



        /// <summary>
        /// Checks in the passed direction to ensure the player is able to move in that
        /// direction, and if so, makes a call to move the player. This function is but
        /// one method for movement, and is called specifically upon directional
        /// keyboard input.
        /// </summary>
        public void attemptMovePlayer(Connector.direction direction)
        {
            switch (direction)
            {
                case Connector.direction.north:
                    if (_currentFloor.getRooms()[_playerLocation.X][_playerLocation.Y].getConnector(Connector.direction.north) != null &&
                        _currentFloor.getRooms()[_playerLocation.X][_playerLocation.Y].getConnector(Connector.direction.north).canTraverse())
                    {
                        movePlayer(new Point(_playerLocation.X, _playerLocation.Y + 1));
                    }
                    break;
                case Connector.direction.east:
                    if (_currentFloor.getRooms()[_playerLocation.X][_playerLocation.Y].getConnector(Connector.direction.east) != null &&
                        _currentFloor.getRooms()[_playerLocation.X][_playerLocation.Y].getConnector(Connector.direction.east).canTraverse())
                    {
                        movePlayer(new Point(_playerLocation.X + 1, _playerLocation.Y));
                    }
                    break;
                case Connector.direction.south:
                    if (_currentFloor.getRooms()[_playerLocation.X][_playerLocation.Y].getConnector(Connector.direction.south) != null &&
                        _currentFloor.getRooms()[_playerLocation.X][_playerLocation.Y].getConnector(Connector.direction.south).canTraverse())
                    {
                        movePlayer(new Point(_playerLocation.X, _playerLocation.Y - 1));
                    }
                    break;
                case Connector.direction.west:
                    if (_currentFloor.getRooms()[_playerLocation.X][_playerLocation.Y].getConnector(Connector.direction.west) != null &&
                        _currentFloor.getRooms()[_playerLocation.X][_playerLocation.Y].getConnector(Connector.direction.west).canTraverse())
                    {
                        movePlayer(new Point(_playerLocation.X - 1, _playerLocation.Y));
                    }
                    break;
            }
        }
    }
}
