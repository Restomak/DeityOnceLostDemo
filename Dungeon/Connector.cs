using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon
{
    /// <summary>
    /// The base class for the various types of connectors used by rooms in a
    /// floor of a dungeon. Each room has four Connectors; one for each cardinal
    /// direction, though not all of them will be instantiated (a Connector being
    /// set to null means there is a wall in that direction). As Connectors are
    /// open and traversable by default, they are this base class by default.
    /// </summary>
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
            window,
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
