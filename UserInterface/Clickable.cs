using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeityOnceLost.UserInterface
{
    public abstract class Clickable
    {
        public int _x, _y, _width, _height;
        protected bool _hovered, _clicked, _held;

        public bool mouseInBoundaries(Point mousePos)
        {
            Rectangle boundaries = new Rectangle(_x, _y, _width, _height);
            
            return boundaries.Contains(mousePos);
        }

        public abstract void onHover();
        public abstract void onHoverEnd();
        public abstract void onClick();
        public virtual void whileHeld()
        {
            //not everything cares if it's held, so by default just run click
            onClick();
        }
    }
}
