using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon
{
    public class Room
    {
        public enum roomContents
        {
            start,
            story,
            happening,
            combat,
            miniboss,
            boss,
            treasure,
            exit
        }

        public const int NO_BATTLE_CHANCE_ON_RETURN = 0;
        public const int DEFAULT_RANDOM_BATTLE_CHANCE_ON_RETURN = 20;

        protected int _randomBattleChanceOnReturn;
        protected Room _northRoom, _eastRoom, _southRoom, _westRoom;
        protected Connector _northConnector, _eastConnector, _southConnector, _westConnector;
        protected List<roomContents> _roomContents;
        protected bool _revealed, _partialRevealed, _visited;
        protected Combat.Encounter _roomEncounter;

        public Room()
        {
            _randomBattleChanceOnReturn = NO_BATTLE_CHANCE_ON_RETURN;
            _roomContents = new List<roomContents>();
            _revealed = false;
            _partialRevealed = false;
            _visited = false;
        }

        //Setters
        public void reveal()
        {
            _revealed = true;

            //Partially reveal adjacents if connectors exist that are traversable
            if (_northConnector != null && _northRoom != null && _northConnector.canTraverse())
            {
                _northRoom.partialReveal();
            }
            if (_eastConnector != null && _eastRoom != null && _eastConnector.canTraverse())
            {
                _eastRoom.partialReveal();
            }
            if (_southConnector != null && _southRoom != null && _southConnector.canTraverse())
            {
                _southRoom.partialReveal();
            }
            if (_westConnector != null && _westRoom != null && _westConnector.canTraverse())
            {
                _westRoom.partialReveal();
            }
        }
        public void partialReveal()
        {
            _partialRevealed = true;
        }

        public void setConnectedRoom(Connector.direction dir, Room connectedRoom, Connector connector)
        {
            switch (dir)
            {
                case Connector.direction.north:
                    _northConnector = connector;
                    _northRoom = connectedRoom;
                    break;
                case Connector.direction.east:
                    _eastConnector = connector;
                    _eastRoom = connectedRoom;
                    break;
                case Connector.direction.south:
                    _southConnector = connector;
                    _southRoom = connectedRoom;
                    break;
                case Connector.direction.west:
                    _westConnector = connector;
                    _westRoom = connectedRoom;
                    break;
            }
        }

        public void initializeWithData(Connector northConnector, Room northRoom, Connector eastConnector, Room eastRoom,
            Connector southConnector, Room southRoom, Connector westConnector, Room westRoom)
        {
            _northConnector = northConnector;
            _northRoom = northRoom;
            _eastConnector = eastConnector;
            _eastRoom = eastRoom;
            _southConnector = southConnector;
            _southRoom = southRoom;
            _westConnector = westConnector;
            _westRoom = westRoom;
        }

        

        //Getters
        public bool isRevealed()
        {
            return _revealed;
        }
        public bool isPartialRevealed()
        {
            return _partialRevealed;
        }

        public List<roomContents> getRoomContents()
        {
            return _roomContents;
        }

        public virtual Combat.Encounter getRoomCombat()
        {
            return _roomEncounter;
        }

        public virtual Events.Happening getRoomEvent()
        {
            return null;
        }

        //FIXIT implement
        /*public virtual Treasures.Loot getRoomTreasure()
        {
            return null;
        }*/

        public Connector getConnector(Connector.direction dir)
        {
            switch (dir)
            {
                case Connector.direction.north:
                    return _northConnector;
                case Connector.direction.east:
                    return _eastConnector;
                case Connector.direction.south:
                    return _southConnector;
                default: //west
                    return _westConnector;
            }
        }

        public Room getRoom(Connector.direction dir)
        {
            switch (dir)
            {
                case Connector.direction.north:
                    return _northRoom;
                case Connector.direction.east:
                    return _eastRoom;
                case Connector.direction.south:
                    return _southRoom;
                default: //west
                    return _westRoom;
            }
        }



        public void addRoomContents(roomContents contents)
        {
            _roomContents.Add(contents);
        }
        public void emptyRoomContents()
        {
            _roomContents.Clear();
        }
        public virtual void finishTopContent()
        {
            if (_roomContents.Count > 0)
            {
                _roomContents.RemoveAt(0);
            }
            else
            {
                Game1.errorLog.Add("Attempted to remove the top of _roomContents but there was nothing in the list");
            }
        }
        public bool onVisit_hasRandomEncounter()
        {
            if (!_visited)
            {
                _visited = true;
                return false;
            }

            if (Game1.randChance(_randomBattleChanceOnReturn))
            {
                return true;
            }

            return false;
        }



        public static void connectRooms(Room room, Connector.direction dir, Room connectedRoom)
        {
            connectRooms(room, dir, connectedRoom, new Connector());
        }
        public static void connectRooms(Room room, Connector.direction dir, Room connectedRoom, Connector connector)
        {
            if (room != null && connectedRoom != null && connector != null)
            {
                room.setConnectedRoom(dir, connectedRoom, connector);
                connectedRoom.setConnectedRoom(Connector.getOppositeDir(dir), room, connector);
            }
        }

        public static void removeConnector(Room room, Connector.direction dir, Room connectedRoom)
        {
            switch (dir)
            {
                case Connector.direction.north:
                    room._northConnector = null;
                    room._northRoom = null;
                    connectedRoom._southConnector = null;
                    connectedRoom._southRoom = null;
                    break;
                case Connector.direction.east:
                    room._eastConnector = null;
                    room._eastRoom = null;
                    connectedRoom._westConnector = null;
                    connectedRoom._westRoom = null;
                    break;
                case Connector.direction.south:
                    room._southConnector = null;
                    room._southRoom = null;
                    connectedRoom._northConnector = null;
                    connectedRoom._northRoom = null;
                    break;
                case Connector.direction.west:
                    room._westConnector = null;
                    room._westRoom = null;
                    connectedRoom._eastConnector = null;
                    connectedRoom._eastRoom = null;
                    break;
            }
        }
    }
}
