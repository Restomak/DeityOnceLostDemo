using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeityOnceLost.UserInterface
{
    /// <summary>
    /// Base class for every user interface element that the player can interact with.
    /// Each Clickable stores its own list of ExtraInfo, in case hovering over the
    /// Clickable should result in extra displayed information (eg. hovering over the
    /// Trident item should display not only what the item can do, but also the card
    /// that is added to the player's hand on use).
    /// </summary>
    public abstract class Clickable
    {
        public int _x, _y, _width, _height;
        protected bool _hovered, _held;
        protected List<ExtraInfo> _extraInfo;
        protected bool _extraInfoAtMouse;

        public Clickable()
        {
            _extraInfo = new List<ExtraInfo>();
            _extraInfoAtMouse = false;
        }

        public bool mouseInBoundaries(Point mousePos)
        {
            Rectangle boundaries = new Rectangle(_x, _y, _width, _height);
            
            return boundaries.Contains(mousePos);
        }

        public abstract void onHover();
        public abstract void onHoverEnd();
        public abstract void onClick();
        public virtual void onRightClick() { } //by default does nothing
        public virtual void onHeld() { } //by default does nothing
        public virtual void whileHeld() { } //by default does nothing
        public virtual void onHeldEnd() { } //by default does nothing

        public List<ExtraInfo> getExtraInfo()
        {
            return _extraInfo;
        }
        public bool extraInfoAtMouse()
        {
            return _extraInfoAtMouse;
        }

        public void setExtraInfo(List<ExtraInfo> extraInfo)
        {
            _extraInfo = extraInfo;
        }
    }
}
