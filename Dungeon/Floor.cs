using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Dungeon
{
    public abstract class Floor
    {
        protected List<List<Room>> _rooms;
        protected int _width, _height; //length is a better word but it's easier to visualize as height from a top-down view
        protected Point _start, _end;
        protected int _defaultGoldFromCombat_Min, _defaultGoldFromCombat_Max, _remainingRandomEncounters;

        public Floor(int width, int height, int defaultGoldFromCombat_Min, int defaultGoldFromCombat_Max)
        {
            _rooms = new List<List<Room>>();

            _width = width;
            _height = height;
            _defaultGoldFromCombat_Min = defaultGoldFromCombat_Min;
            _defaultGoldFromCombat_Max = defaultGoldFromCombat_Max;

            initializeAllRooms();
        }

        //Setters
        protected void setStart(Point start)
        {
            _start = start;
        }
        protected void setEnd(Point end)
        {
            _end = end;
        }

        //Getters
        public Point getStart()
        {
            return _start;
        }
        public Point getEnd()
        {
            return _end;
        }
        public List<List<Room>> getRooms()
        {
            return _rooms;
        }
        public int getDefaultGoldFromCombat_Min()
        {
            return _defaultGoldFromCombat_Min;
        }
        public int getDefaultGoldFromCombat_Max()
        {
            return _defaultGoldFromCombat_Max;
        }
        public abstract Combat.Encounter getRandomEncounter();
        protected abstract int getRemainingRandomEncounters();



        /// <summary>
        /// Defaults all rooms in the floor as empty rooms with open connectors
        /// </summary>
        public void initializeAllRooms()
        {
            for (int x = 0; x < _width; x++)
            {
                _rooms.Add(new List<Room>());
                for (int y = 0; y < _height; y++)
                {
                    _rooms[x].Add(new Room());
                }
            }

            bool offset = false;
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (!offset)
                    {
                        initializeRoom(x, y);
                    }
                    else if (x + 1 < _width)
                    {
                        initializeRoom(x + 1, y);
                    }
                }
                offset = !offset;
            }
        }

        /// <summary>
        /// Connects an individual room to its neighbors with open connectors
        /// </summary>
        private void initializeRoom(int x, int y)
        {
            //North
            if (y < _height - 1)
            {
                Room.connectRooms(_rooms[x][y], Connector.direction.north, _rooms[x][y + 1]);
            }

            //East
            if (x < _width - 1)
            {
                Room.connectRooms(_rooms[x][y], Connector.direction.east, _rooms[x + 1][y]);
            }

            //South
            if (y > 0)
            {
                Room.connectRooms(_rooms[x][y], Connector.direction.south, _rooms[x][y - 1]);
            }

            //West
            if (x > 0)
            {
                Room.connectRooms(_rooms[x][y], Connector.direction.west, _rooms[x - 1][y]);
            }
        }
        
        public void replaceRoom(Room newRoom, Point insertAt)
        {
            Room oldRoom = _rooms[insertAt.X][insertAt.Y];

            Room.connectRooms(newRoom, Connector.direction.north, oldRoom.getRoom(Connector.direction.north), oldRoom.getConnector(Connector.direction.north));
            Room.connectRooms(newRoom, Connector.direction.east, oldRoom.getRoom(Connector.direction.east), oldRoom.getConnector(Connector.direction.east));
            Room.connectRooms(newRoom, Connector.direction.south, oldRoom.getRoom(Connector.direction.south), oldRoom.getConnector(Connector.direction.south));
            Room.connectRooms(newRoom, Connector.direction.west, oldRoom.getRoom(Connector.direction.west), oldRoom.getConnector(Connector.direction.west));

            _rooms[insertAt.X][insertAt.Y] = newRoom;
        }



        /// <summary>
        /// Beginning at the scouting point, reveals all rooms in a straight line in each cardinal
        /// direction, until it hits a Connector that blocks scouting. Call every time the player
        /// moves, as well as when starting each floor.
        /// </summary>
        public void scout(Point scoutFrom)
        {
            Room start = _rooms[scoutFrom.X][scoutFrom.Y];
            start.reveal();

            Room current;
            Connector connection;
            int distance;

            //Scout north
            current = start;
            connection = current.getConnector(Connector.direction.north);
            distance = 1;
            while (connection != null && !connection.blocksScouting())
            {
                current = _rooms[scoutFrom.X][scoutFrom.Y + distance];
                current.reveal();
                connection = current.getConnector(Connector.direction.north);

                distance += 1;
            }

            //Scout east
            current = start;
            connection = current.getConnector(Connector.direction.east);
            distance = 1;
            while (connection != null && !connection.blocksScouting())
            {
                current = _rooms[scoutFrom.X + distance][scoutFrom.Y];
                current.reveal();
                connection = current.getConnector(Connector.direction.east);

                distance += 1;
            }

            //Scout south
            current = start;
            connection = current.getConnector(Connector.direction.south);
            distance = 1;
            while (connection != null && !connection.blocksScouting())
            {
                current = _rooms[scoutFrom.X][scoutFrom.Y - distance];
                current.reveal();
                connection = current.getConnector(Connector.direction.south);

                distance += 1;
            }

            //Scout west
            current = start;
            connection = current.getConnector(Connector.direction.west);
            distance = 1;
            while (connection != null && !connection.blocksScouting())
            {
                current = _rooms[scoutFrom.X - distance][scoutFrom.Y];
                current.reveal();
                connection = current.getConnector(Connector.direction.west);

                distance += 1;
            }
        }



        public void unlockAllDoorsOfColor(Treasury.Equipment.Key.keyColor color)
        {
            Connector currentConnector = null;

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    currentConnector = _rooms[x][y].getConnector(Connector.direction.north);
                    if (currentConnector != null && currentConnector.getConnectorType() == Connector.connectorType.locked &&
                        ((Connectors.LockedDoor)currentConnector).getLockColor() == color)
                    {
                        ((Connectors.LockedDoor)currentConnector).unlock();
                    }

                    currentConnector = _rooms[x][y].getConnector(Connector.direction.east);
                    if (currentConnector != null && currentConnector.getConnectorType() == Connector.connectorType.locked &&
                        ((Connectors.LockedDoor)currentConnector).getLockColor() == color)
                    {
                        ((Connectors.LockedDoor)currentConnector).unlock();
                    }

                    currentConnector = _rooms[x][y].getConnector(Connector.direction.south);
                    if (currentConnector != null && currentConnector.getConnectorType() == Connector.connectorType.locked &&
                        ((Connectors.LockedDoor)currentConnector).getLockColor() == color)
                    {
                        ((Connectors.LockedDoor)currentConnector).unlock();
                    }

                    currentConnector = _rooms[x][y].getConnector(Connector.direction.west);
                    if (currentConnector != null && currentConnector.getConnectorType() == Connector.connectorType.locked &&
                        ((Connectors.LockedDoor)currentConnector).getLockColor() == color)
                    {
                        ((Connectors.LockedDoor)currentConnector).unlock();
                    }
                }
            }
        }
    }
}
