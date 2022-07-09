using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Connectors
{
    /// <summary>
    /// A variant of Connector that blocks scouting, and is also impassable to the
    /// player until the Key with the associated color is found.
    /// </summary>
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
