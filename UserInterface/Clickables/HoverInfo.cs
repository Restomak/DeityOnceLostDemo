using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables
{
    public abstract class HoverInfo : Clickable
    {
        protected List<String> _description;

        public HoverInfo(Point xy, int width, int height, List<String> description)
        {
            _x = xy.X;
            _y = xy.Y;
            _width = width;
            _height = height;
            _description = description;
        }

        public virtual List<String> getDescription()
        {
            return _description;
        }
        
        /// <summary>
        /// Handles what happens in logic when the user hovers over the object. Will
        /// pop up a description box giving more details about what it is.
        /// </summary>
        public override void onHover()
        {
            _hovered = true;
            Game1.setHoveredClickable(this);
            //FIXIT implement in DrawHandler
        }

        /// <summary>
        /// Handles what happens when the user is no longer hovering over this object.
        /// </summary>
        public override void onHoverEnd()
        {
            _hovered = false;
            Game1.setHoveredClickable(null);
        }

        /// <summary>
        /// Has no function.
        /// </summary>
        public override void onClick() { }
    }
}
