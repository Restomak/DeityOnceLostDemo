using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon
{
    public class Connector
    {
        public enum direction
        {
            north,
            east,
            south,
            west
        }
        public static direction getOppositeDir(direction dir)
        {
            switch (dir)
            {
                case direction.north:
                    return direction.south;
                case direction.east:
                    return direction.west;
                case direction.south:
                    return direction.north;
                default: //west
                    return direction.east;
            }
        }

        public enum connectorType
        {
            open,
            closed,
            locked,
            none
        }

        protected bool _passageGained, _blocksScouting;
        protected connectorType _connectorType;

        public Connector()
        {
            _passageGained = true;
            _blocksScouting = false;
            _connectorType = connectorType.open;
        }

        //Setters
        public void closePassage()
        {
            _passageGained = false;
        }
        public void openPassage()
        {
            _passageGained = true;
        }

        //Getters
        public connectorType getConnectorType()
        {
            return _connectorType;
        }
        public bool blocksScouting()
        {
            return _blocksScouting;
        }
        public bool canTraverse()
        {
            return _passageGained;
        }
    }
}
