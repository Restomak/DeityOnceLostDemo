using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Connectors
{
    /// <summary>
    /// A variant of Connector that is window-style; the player can see (scout)
    /// through it, but passage through it is impossible.
    /// </summary>
    public class SeeNotTraverse : Connector
    {
        public SeeNotTraverse()
        {
            _passageGained = false;
            _blocksScouting = false;
            _connectorType = connectorType.window;
        }
    }
}
