using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon
{
    /// <summary>
    /// The abstract base class of an entire dungeon. Stores a list of floors as well
    /// as an index representing the floor the player is currently on.
    /// </summary>
    public abstract class Dungeon
    {
        protected List<Floor> _floors;
        int _currentFloor;

        public Dungeon()
        {
            _floors = new List<Floor>();

            _currentFloor = 0;
        }

        public Floor getFloor()
        {
            if (_currentFloor >= _floors.Count)
            {
                return null;
            }

            return _floors[_currentFloor];
        }

        public Floor getNextFloor()
        {
            if (_currentFloor + 1 >= _floors.Count)
            {
                return null;
            }

            return _floors[_currentFloor + 1];
        }

        public void incrementFloor()
        {
            _currentFloor++;
        }
    }
}
