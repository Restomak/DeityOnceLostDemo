using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeityOnceLost.Input
{
    public class InputController
    {
        MouseControl mouseControl;
        bool gameChanged;

        public InputController()
        {
            mouseControl = new MouseControl();
        }

        public void updateInput(WindowControl windowControl, List<UserInterface.UserInterface> activeUIs)
        {
            /*____________________.Setup._____________________*/

            gameChanged = false;

            //Update mouse //FIXIT and keyboard
            mouseControl.checkMouse(windowControl);



            /*____________________.Perform Logic._____________________*/
            

            //Hover logic - only do if the left mouse button isn't currently held down
            if (!mouseControl.isLeftHeld())
            {
                UserInterface.Clickable currentHover = Game1.getHoveredClickable();
                UserInterface.Clickable newHover = UserInterface.UserInterface.getFrontClickableFromUIList(Game1.getHoveredClickable(), activeUIs, mouseControl.getMousePosition());

                //If there was already something being hovered and that's no longer the case, tell the game to end its hover
                if (currentHover != null && newHover != currentHover)
                {
                    Game1.debugLog.Add("No longer hovering over " + currentHover.ToString());

                    currentHover.onHoverEnd();
                }
                
                //Check if there's a new hover
                if (newHover != null && newHover != currentHover)
                {
                    Game1.debugLog.Add("Now hovering over " + newHover.ToString());

                    newHover.onHover();
                }
            }
            

            //Mouse left click logic
            if (mouseControl.isLeftClicked())
            {
                UserInterface.Clickable clicked = UserInterface.UserInterface.getFrontClickableFromUIList(Game1.getHoveredClickable(), activeUIs, mouseControl.getMousePosition());
                
                if (clicked != null)
                {
                    //Check if we already have a HandCard active, and if so, make sure it's not the same one
                    if (Game1.getActiveCard() == null || clicked.GetType() != typeof(UserInterface.Clickables.HandCard) || clicked != Game1.getActiveCard())
                    {
                        clicked.onClick();
                        gameChanged = true;
                    }

                    //FIXIT implement more
                }
            }

            if (mouseControl.isRightClicked())
            {
                UserInterface.Clickable clicked = UserInterface.UserInterface.getFrontClickableFromUIList(Game1.getHoveredClickable(), activeUIs, mouseControl.getMousePosition());

                //Unclick active card if you right click somewhere else
                if (Game1.getActiveCard() != null && !Game1.getActiveCard().mouseInBoundaries(mouseControl.getMousePosition()))
                {
                    Game1.setActiveCard(null);
                    gameChanged = true;
                }

                //FIXIT implement more
            }



            /*____________________.Cleanup._____________________*/

            if (gameChanged)
            {
                Game1.updateBattleUI(); //Be sure to update the UI
            }
        }

        public Point getMousePos()
        {
            return mouseControl.getMousePosition();
        }
    }
}
