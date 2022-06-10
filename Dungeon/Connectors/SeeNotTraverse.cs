using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Connectors
{
    public class SeeNotTraverse : Connector
    {
        public SeeNotTraverse()
        {
            _passageGained = false;
            _blocksScouting = false;
            _connectorType = connectorType.none;
        }
    }
}
