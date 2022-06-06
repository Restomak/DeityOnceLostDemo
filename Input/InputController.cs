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
        bool gameChanged;

        public InputController()
        {
            mouseControl = new MouseControl();
        }

        public void updateInput(WindowControl windowControl, List<UserInterface.UserInterface> activeUIs)
        {
            gameChanged = false;

            //Update mouse & keyboard
            mouseControl.checkMouse(windowControl);

            //Perform logic
            if (!mouseControl.isLeftHeld())
            {
                UserInterface.Clickable currentHover = Game1.getHoveredClickable();
                if (currentHover != null)
                {
                    Rectangle boundingBox = new Rectangle(currentHover._x, currentHover._y, currentHover._width, currentHover._height);
                    if (!boundingBox.Contains(mouseControl.getMousePosition()))
                    {
                        Game1.debugLog.Add("No longer hovering over " + currentHover.ToString());
                        
                        Game1.setHoveredClickable(null);
                        currentHover = null;
                    }
                }
                
                if (currentHover == null)
                {
                    currentHover = UserInterface.UserInterface.getFrontClickableFromUIList(activeUIs, mouseControl.getMousePosition());

                    if (currentHover != null)
                    {
                        Game1.debugLog.Add("Now hovering over " + currentHover.ToString());

                        currentHover.onHover();
                    }
                }
            }



            if (mouseControl.isLeftClicked())
            {
                UserInterface.Clickable clicked = UserInterface.UserInterface.getFrontClickableFromUIList(activeUIs, mouseControl.getMousePosition());

                //FIXIT implement an actively clicked Clickable, like I have with hover?
                if (clicked != null)
                {
                    clicked.onClick();
                    gameChanged = true;
                }
                else
                {

                }
            }

            if (gameChanged)
            {
                Game1.updateBattleUI(); //Be sure to update the UI
            }
        }
    }
}
