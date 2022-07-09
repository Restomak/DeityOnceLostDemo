using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables.Hovers
{
    /// <summary>
    /// Version of HoverInfo that displays one of the grid spaces in the inventory.
    /// Doesn't store a description to display, but rather changes colour depending
    /// on the circumstances.
    /// </summary>
    class InventoryGrid : HoverInfo
    {
        public bool _hoveredCannotPlace;

        public InventoryGrid(Point xy) : base(xy, Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE,
            Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE, new List<String>())
        {
            _hoveredCannotPlace = false;
        }

        public Color getGridColor()
        {
            if (_hovered)
            {
                return Color.MidnightBlue;
            }
            else if (_hoveredCannotPlace)
            {
                return Color.DarkRed;
            }

            return Color.Black;
        }

        public void setHovered(bool hovered)
        {
            _hovered = hovered;
        }

        public void setHoveredCannotPlace (bool hoveredCannotPlace)
        {
            _hoveredCannotPlace = hoveredCannotPlace;
        }



        public static Color getGridOutlineColor()
        {
            return Color.DarkGray;
        }
    }
}
