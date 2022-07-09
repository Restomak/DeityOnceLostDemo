using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Menus
{
    /// <summary>
    /// The menu used when the player right clicks an item. It consists of a several buttons that
    /// make up a drop-down menu, as well as a screen-wide button used to check if the player clicks
    /// anywhere else on the screen so that it closes automatically.
    /// </summary>
    public class ItemRightClickMenu : MenuUI
    {
        InventoryMenu _inventoryMenu;
        UserInterface _dropDownButtons;
        UserInterface _autoClose;
        UserInterface _heldItem;
        bool _itemFromInventory;

        public ItemRightClickMenu(InventoryMenu inventoryMenu, bool itemFromInventory) : //none of the base stuff matters since this menu doesn't have a background
            base(0, 0, 0, 0, Game1.pic_functionality_bar, Color.White, "", 0, 0, Game1.roboto_regular_10, 0, Color.White, Color.White)
        {
            if (inventoryMenu.getHeldItem() == null)
            {
                Game1.closeMenu(this);
            }

            _inventoryMenu = inventoryMenu;
            _itemFromInventory = itemFromInventory;

            _dropDownButtons = new UserInterface();
            _autoClose = new UserInterface();
            _heldItem = new UserInterface();
            _wholeUI.Add(_dropDownButtons);
            _wholeUI.Add(_autoClose);
            _wholeUI.Add(_heldItem); //added after so it's not clickable (it's just there for visual effect)

            //Set up a fake button that if clicked (so, anywhere other than the buttons of this menu), the menu closes
            _autoClose.addClickableToBack(new Clickables.SpecialButtons.CannotHover(null, new Point(0, 0), Game1.VIRTUAL_WINDOW_WIDTH, Game1.VIRTUAL_WINDOW_HEIGHT, () =>
            {
                cancelCloseMenu();
            }));

            //Set up held item visual "clickable"
            _heldItem.addClickableToBack(_inventoryMenu.getHeldItem());

            setupDropDownButtons();
        }

        //we don't want the top bar clickable here. the only thing that matters is whether we click a button, or if we click anywhere else the menu closes
        public override bool addTopBar() { return false; }

        public override void updateUI() { } //no need

        public override void onEscapePressed()
        {
            cancelCloseMenu();
        }

        /// <summary>
        /// Lets the inventory menu know that this menu has been closed. If the item held is one
        /// that was from the player's inventory in the first place, it will attempt to put that
        /// item back. If it cannot, the result will be the player still holding the item when
        /// this menu is closed.
        /// </summary>
        private void cancelCloseMenu()
        {
            if (!_itemFromInventory)
            {
                _inventoryMenu.rightClickMenuCanceled();
            }
            else
            {
                _inventoryMenu.putItemBack(false);
            }

            Game1.closeMenu(this);
        }



        private void setupDropDownButtons()
        {
            //Use
            Clickables.SpecialButtons.DropDownButton useButton = new Clickables.SpecialButtons.DropDownButton("Use", () =>
            {
                if (_inventoryMenu.getHeldItem().getItem().canUse())
                {
                    _inventoryMenu.useHeldItem();
                    Game1.closeMenu(this);
                }
            }, _inventoryMenu.getHeldItem().getItem().canUse());

            //Discard
            Clickables.SpecialButtons.DropDownButton discardButton = new Clickables.SpecialButtons.DropDownButton("Discard", () =>
            {
                _inventoryMenu.discardHeldItem();
                Game1.closeMenu(this);
            });

            //Cancel
            Clickables.SpecialButtons.DropDownButton cancelButton = new Clickables.SpecialButtons.DropDownButton("Cancel", () =>
            {
                cancelCloseMenu();
            });


            //Set up width
            int buttonWidth = (int)Clickables.SpecialButtons.DropDownButton.getDropDownButtonFont().MeasureString("Discard").X + Drawing.DrawConstants.ITEM_RIGHT_CLICK_MENU_HORIZONTAL_BUFFER * 2;
            useButton._width = buttonWidth;
            discardButton._width = buttonWidth;
            cancelButton._width = buttonWidth;

            //Set up their locations
            Point anchorPoint = Game1.getInputController().getMousePos(); //anchors where you clicked

            //X
            int buttonX = anchorPoint.X;
            if (buttonX + buttonWidth > Game1.VIRTUAL_WINDOW_WIDTH)
            {
                buttonX = anchorPoint.X - buttonWidth;
            }

            //Y
            int menuY = anchorPoint.Y - Clickables.SpecialButtons.DropDownButton.getDropDownButtonHeight() * 3;
            if (menuY < 0)
            {
                menuY = anchorPoint.Y;
            }

            //Put it together
            useButton._x = buttonX;
            discardButton._x = buttonX;
            cancelButton._x = buttonX;

            cancelButton._y = menuY;
            discardButton._y = menuY + Clickables.SpecialButtons.DropDownButton.getDropDownButtonHeight();
            useButton._y = menuY + Clickables.SpecialButtons.DropDownButton.getDropDownButtonHeight() * 2;

            _dropDownButtons.addClickableToBack(useButton);
            _dropDownButtons.addClickableToBack(discardButton);
            _dropDownButtons.addClickableToBack(cancelButton);
        }
    }
}
