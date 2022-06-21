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

        public void updateInput(WindowControl windowControl, List<UserInterface.UserInterface> activeUIs, Game1.gameState gameState)
        {
            /*____________________.Setup._____________________*/

            gameChanged = false;

            //Update mouse //FIXIT and keyboard
            mouseControl.checkMouse(windowControl);



            /*____________________.Perform Logic: Mouse._____________________*/
            

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
                    UserInterface.Clickables.HandCard activeCard = Game1.getCombatHandler().getCombatUI().getActiveCard();
                    if (gameState != Game1.gameState.combat ||
                        gameState == Game1.gameState.combat && (activeCard == null || clicked.GetType() != typeof(UserInterface.Clickables.HandCard) || clicked != activeCard))
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
                UserInterface.Clickables.HandCard activeCard = Game1.getCombatHandler().getCombatUI().getActiveCard();
                if (activeCard != null && !activeCard.mouseInBoundaries(mouseControl.getMousePosition()))
                {
                    Game1.getCombatHandler().getCombatUI().setActiveCard(null);
                    gameChanged = true;
                }

                //FIXIT implement more
            }


            //Mouse scroll wheel logic
            if (Game1.menuActive() && Game1.getTopMenu().isScrollable())
            {
                if (mouseControl.didScrollUp())
                {
                    Game1.getTopMenu()._scrollY += Drawing.DrawConstants.SCROLL_Y_WHEEL_TICK_AMOUNT;
                    if (Game1.getHoveredClickable() != null)
                    {
                        Game1.getHoveredClickable().onHoverEnd();
                    }
                    gameChanged = true;
                }
                else if (mouseControl.didScrollDown())
                {
                    Game1.getTopMenu()._scrollY -= Drawing.DrawConstants.SCROLL_Y_WHEEL_TICK_AMOUNT;
                    if (Game1.getHoveredClickable() != null)
                    {
                        Game1.getHoveredClickable().onHoverEnd();
                    }
                    gameChanged = true;
                }
            }



            /*____________________.Perform Logic: Keyboard._____________________*/
            
            //FIXIT implement hitting escape closing CardCollectionMenus



            /*____________________.Cleanup._____________________*/

            if (gameChanged)
            {
                switch (gameState)
                {
                    case Game1.gameState.combat:
                        Game1.getCombatHandler().updateCombatUI();
                        break;
                    case Game1.gameState.dungeon:
                        Game1.getDungeonHandler().updateMapUI();
                        break;
                }

                Game1.updateMenus();
                Game1.updateTopBar();
            }
        }

        public Point getMousePos()
        {
            return mouseControl.getMousePosition();
        }
    }
}
