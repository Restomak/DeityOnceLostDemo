using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeityOnceLost.Input
{
    class InputController
    {
        MouseControl mouseControl;

        public InputController()
        {
            mouseControl = new MouseControl();
        }

        public void updateInput(WindowControl windowControl, UserInterface.Clickable currentHover, List<UserInterface.UserInterface> activeUIs)
        {
            //Update mouse & keyboard
            mouseControl.checkMouse(windowControl);

            //Perform logic
            if (currentHover != null && !mouseControl.isLeftHeld())
            {
                Rectangle boundingBox = new Rectangle(currentHover._x, currentHover._y, currentHover._width, currentHover._height);
                if (!boundingBox.Contains(mouseControl.getMousePosition()))
                {
                    currentHover = null;
                    Game1.setHoveredClickable(null);
                }
            }

            if (mouseControl.isLeftClicked())
            {

            }
        }
    }
}
