using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Connectors
{
    /// <summary>
    /// A variant of Connector that blocks scouting but does not block passage.
    /// </summary>
    class Door : Connector
    {
        public Door()
        {
            _passageGained = true;
            _blocksScouting = true;
            _connectorType = connectorType.closed;
        }
    }
}
