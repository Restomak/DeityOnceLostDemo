using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Connectors
{
    class LockedDoor : Door
    {
        Treasury.Equipment.Key.keyColor _lockColor;

        public LockedDoor(Treasury.Equipment.Key.keyColor color)
        {
            _passageGained = false;
            _connectorType = connectorType.locked;
            _lockColor = color;
        }

        public Treasury.Equipment.Key.keyColor getLockColor()
        {
            return _lockColor;
        }

        public void unlock()
        {
            _passageGained = true;
        }
    }
}
