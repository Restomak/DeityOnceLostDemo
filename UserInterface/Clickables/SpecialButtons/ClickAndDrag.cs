using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables.SpecialButtons
{
    /// <summary>
    /// Version of Button that is meant to be able to be clicked and dragged with the
    /// mouse. Used for scroll bars.
    /// </summary>
    class ClickAndDrag : Button
    {
        int _heldStartX, _heldStartY;

        public ClickAndDrag(Texture2D texture, Point xy, int width, int height, Action function) :
            base(texture, xy, width, height, function, new List<String>())
        {
        }

        public override void onHover() { } //Does nothing
        public override void onHoverEnd() { } //Does nothing
        public override void onClick() { } //Does nothing

        public override void onHeld()
        {
            _heldStartX = _x;
            _heldStartY = _y;
            _held = true;
            Game1.setHeldClickable(this);
        }

        public override void whileHeld()
        {
            _buttonFunction();
        }

        public override void onHeldEnd()
        {
            _held = false;
            Game1.setHeldClickable(null);
        }

        public override void onRightClick()
        {
            onHeldEnd();

            //reset back to original coords
            _x = _heldStartX;
            _y = _heldStartY;
        }
    }
}
