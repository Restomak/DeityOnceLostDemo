using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon
{
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
            _currentFloor++;
            return getFloor();
        }
    }
}
