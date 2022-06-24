using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables.Hovers
{
    class InventoryGrid : HoverInfo
    {
        public InventoryGrid(Point xy) : base(xy, Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE,
            Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE, new List<String>())
        {

        }

        public Color getGridColor()
        {
            if (_hovered)
            {
                return Color.MidnightBlue;
            }

            return Color.Black;
        }



        public static Color getGridOutlineColor()
        {
            return Color.DarkGray;
        }
    }
}
