using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Connectors
{
    class LockedDoor : Door
    {
        public LockedDoor()
        {
            _passageGained = false;
            _connectorType = connectorType.locked;
        }

        public void unlock()
        {
            _passageGained = true;
        }
    }
}
