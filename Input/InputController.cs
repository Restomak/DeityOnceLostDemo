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
        KeyboardControl keyboardControl;
        bool gameChanged;

        public InputController()
        {
            mouseControl = new MouseControl();
            keyboardControl = new KeyboardControl();
        }

        public void updateInput(WindowControl windowControl, List<UserInterface.UserInterface> activeUIs, Game1.gameState gameState)
        {
            /*____________________.Setup._____________________*/

            gameChanged = false;

            //Update mouse and keyboard
            mouseControl.checkMouse(windowControl);
            keyboardControl.checkKeyboard();

            UserInterface.Menus.InventoryMenu inventoryMenu = Game1.getInventoryMenuIfOnTop();
            if (inventoryMenu != null)
            {
                inventoryMenu.updateHeldItem();
            }



            /*____________________.Perform Logic: Mouse._____________________*/
            

            //Hover logic - only do if the left mouse button isn't currently held down
            if (!mouseControl.isLeftHeld())
            {
                if (inventoryMenu != null && inventoryMenu.getHeldItem() != null)
                {
                    //Not sure if I want to do any of the below stuff while an item is held. For now, I won't
                }
                else if (Game1.getHeldClickable() != null)
                {
                    Game1.getHeldClickable().onHeldEnd();
                }
                else
                {
                    UserInterface.Clickable currentHover = Game1.getHoveredClickable();
                    UserInterface.Clickable newHover = UserInterface.UserInterface.getFrontClickableFromUIList(Game1.getHoveredClickable(), activeUIs, mouseControl.getMousePosition());
                    UserInterface.Clickables.HandCard activeCard = Game1.getCombatHandler().getCombatUI().getActiveCard();

                    if (activeCard != null && activeCard.mouseInBoundaries(mouseControl.getMousePosition()))
                    {

                    }
                    else
                    {
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
                }
            }
            else //Logic for when mouse left click button is held down
            {
                if (Game1.getHeldClickable() != null)
                {
                    Game1.getHeldClickable().whileHeld();
                }
            }


            //Mouse left click logic
            if (mouseControl.isLeftClicked())
            {
                //First we check if we're dealing with a held item in the inventory menu
                if (inventoryMenu != null && inventoryMenu.getHeldItem() != null)
                {
                    inventoryMenu.leftClickedWithHeldItem();
                    gameChanged = true;
                }
                else
                {
                    UserInterface.Clickable clicked = UserInterface.UserInterface.getFrontClickableFromUIList(Game1.getHoveredClickable(), activeUIs, mouseControl.getMousePosition());
                    UserInterface.Clickables.HandCard activeCard = Game1.getCombatHandler().getCombatUI().getActiveCard();

                    if (activeCard != null && activeCard.mouseInBoundaries(mouseControl.getMousePosition()))
                    {
                        activeCard.onHeld(); //set up HandCard for dragging
                    }
                    else if (clicked != null)
                    {
                        //Check if we already have a HandCard active, and if so, make sure it's not the same one
                        if (gameState != Game1.gameState.combat ||
                            gameState == Game1.gameState.combat && (activeCard == null || clicked.GetType() != typeof(UserInterface.Clickables.HandCard) || clicked != activeCard))
                        {
                            clicked.onClick();
                            clicked.onHeld(); //set up the object for being held in case it uses those functions
                            gameChanged = true;
                        }
                    }
                }
            }


            //Mouse right click logic (cancels held)
            if (mouseControl.isRightClicked())
            {
                //First we check if we're dealing with a held item in the inventory menu
                if (inventoryMenu != null && inventoryMenu.getHeldItem() != null)
                {
                    inventoryMenu.rightClickedWithHeldItem();
                    gameChanged = true;
                }
                else if (Game1.getHeldClickable() != null)
                {
                    //If a Clickable is held, we focus on that instead of anything else
                    Game1.getHeldClickable().onRightClick();
                    gameChanged = true;
                }
                else
                {
                    UserInterface.Clickable clicked = UserInterface.UserInterface.getFrontClickableFromUIList(Game1.getHoveredClickable(), activeUIs, mouseControl.getMousePosition());

                    //Unclick active card if you right click somewhere else
                    UserInterface.Clickables.HandCard activeCard = Game1.getCombatHandler().getCombatUI().getActiveCard();
                    if (activeCard != null && !activeCard.mouseInBoundaries(mouseControl.getMousePosition()))
                    {
                        Game1.getCombatHandler().getCombatUI().setActiveCard(null);
                        gameChanged = true;
                    }

                    if (clicked != null)
                    {
                        clicked.onRightClick();
                    }
                }
            }


            //Mouse scroll wheel logic
            if (Game1.menuActive() && Game1.getTopMenu().isScrollable())
            {
                int scrollY = Game1.getTopMenu().getScrollY();
                if (mouseControl.didScrollUp())
                {
                    Game1.getTopMenu().setScrollY(scrollY + Drawing.DrawConstants.SCROLL_Y_WHEEL_TICK_AMOUNT);
                    if (Game1.getHoveredClickable() != null)
                    {
                        Game1.getHoveredClickable().onHoverEnd();
                    }
                    gameChanged = true;
                }
                else if (mouseControl.didScrollDown())
                {
                    Game1.getTopMenu().setScrollY(scrollY - Drawing.DrawConstants.SCROLL_Y_WHEEL_TICK_AMOUNT);
                    if (Game1.getHoveredClickable() != null)
                    {
                        Game1.getHoveredClickable().onHoverEnd();
                    }
                    gameChanged = true;
                }
            }



            /*____________________.Perform Logic: Keyboard._____________________*/
            
            if (keyboardControl.isEscapePressed())
            {
                //Exits menus that allow canceling out
                UserInterface.MenuUI topUI = Game1.getTopMenu();
                if (topUI != null)
                {
                    topUI.onEscapePressed();
                }
            }

            if (keyboardControl.isContinuePressed())
            {
                //Continues events as if the only available choice was clicked
                if (Game1.getGameState() == Game1.gameState.happening)
                {
                    Game1.getEventHandler().continuePressed();
                    gameChanged = true;
                }
            }

            if (keyboardControl.isUpPressed())
            {
                //Moves north on the map, if up is a viable direction
                if (Game1.getGameState() == Game1.gameState.dungeon)
                {
                    Game1.getDungeonHandler().attemptMovePlayer(Dungeon.Connector.direction.north);
                    gameChanged = true;
                }
            }

            if (keyboardControl.isRightPressed())
            {
                //Moves east on the map, if up is a viable direction
                if (Game1.getGameState() == Game1.gameState.dungeon)
                {
                    Game1.getDungeonHandler().attemptMovePlayer(Dungeon.Connector.direction.east);
                    gameChanged = true;
                }
            }

            if (keyboardControl.isDownPressed())
            {
                //Moves south on the map, if up is a viable direction
                if (Game1.getGameState() == Game1.gameState.dungeon)
                {
                    Game1.getDungeonHandler().attemptMovePlayer(Dungeon.Connector.direction.south);
                    gameChanged = true;
                }
            }

            if (keyboardControl.isLeftPressed())
            {
                //Moves west on the map, if up is a viable direction
                if (Game1.getGameState() == Game1.gameState.dungeon)
                {
                    Game1.getDungeonHandler().attemptMovePlayer(Dungeon.Connector.direction.west);
                    gameChanged = true;
                }
            }



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
