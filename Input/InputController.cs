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

                        currentHover.onHoverEnd();
                        currentHover = null;
                    }
                }
                
                if (currentHover == null)
                {
                    bool activeCardInFront = false;
                    if (Game1.getActiveCard() != null)
                    {
                        UserInterface.Clickables.HandCard activeCard = Game1.getActiveCard();
                        Rectangle boundingBox = new Rectangle(activeCard._x, activeCard._y, activeCard._width, activeCard._height);

                        if (boundingBox.Contains(mouseControl.getMousePosition()))
                        {
                            activeCardInFront = true;
                        }
                    }

                    if (!activeCardInFront)
                    {
                        currentHover = UserInterface.UserInterface.getFrontClickableFromUIList(activeUIs, mouseControl.getMousePosition());

                        if (currentHover != null)
                        {
                            Game1.debugLog.Add("Now hovering over " + currentHover.ToString());

                            currentHover.onHover();
                        }
                    }
                }
            }



            if (mouseControl.isLeftClicked())
            {
                UserInterface.Clickable clicked = UserInterface.UserInterface.getFrontClickableFromUIList(activeUIs, mouseControl.getMousePosition());
                
                //FIXIT implement more
                if (clicked != null)
                {
                    clicked.onClick();
                    gameChanged = true;
                }
                else
                {

                }
            }

            if (mouseControl.isRightClicked())
            {
                UserInterface.Clickable clicked = UserInterface.UserInterface.getFrontClickableFromUIList(activeUIs, mouseControl.getMousePosition());

                //Unclick active card if you right click somewhere else
                if (Game1.getActiveCard() != null)
                {
                    UserInterface.Clickables.HandCard activeCard = Game1.getActiveCard();
                    Rectangle boundingBox = new Rectangle(activeCard._x, activeCard._y, activeCard._width, activeCard._height);
                    
                    if (!boundingBox.Contains(mouseControl.getMousePosition()))
                    {
                        Game1.setActiveCard(null);
                        gameChanged = true;
                    }
                }

                //FIXIT implement more
            }

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
