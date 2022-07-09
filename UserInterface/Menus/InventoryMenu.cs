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
    /// The player's inventory in menu form. Accessible either via the top bar button,
    /// or when picking up an item from a loot or event reward. When opened in a manner
    /// other than via top bar, the player will be "holding" an item at their mouse
    /// position, indicating that they need to do something with this item (place it,
    /// use it, or discard it).
    /// </summary>
    public class InventoryMenu : MenuUI
    {
        Treasury.Equipment.Inventory _inventory;
        UserInterface _gridSpaces;
        UserInterface _moreGridSpaces;
        UserInterface _items;
        UserInterface _keys;
        UserInterface _buttons;
        Clickables.InventoryItem _heldItem;
        bool _fromLoot;
        int _heldItemBLCornerGridSpaceX, _heldItemBLCornerGridSpaceY;
        int _prevHeldItemBLCornerGridSpaceX, _prevHeldItemBLCornerGridSpaceY;

        public InventoryMenu(Treasury.Equipment.Inventory inventory) : base(0, 0, 0, 0, //all 0's because it needs to get calculated in the constructor
            Game1.pic_functionality_bar, Color.DarkSlateGray * Drawing.DrawConstants.INVENTORYMENU_BACKGROUND_FADE, "Inventory", 0, 0, Game1.roboto_black_24,
            Drawing.DrawConstants.TEXT_24_HEIGHT, Color.GreenYellow, Color.Black)
        {
            _inventory = inventory;
            _heldItem = null;
            _fromLoot = false;
            _prevHeldItemBLCornerGridSpaceX = -999; //Just set them to a random value off-screen since they're for telling the screen when to update anyway
            _prevHeldItemBLCornerGridSpaceY = -999;

            _width = Drawing.DrawConstants.INVENTORYMENU_HORIZONTAL_BUFFER * 2 +
                (inventory.getSpaceRectangle().Width * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2));
            _height = Drawing.DrawConstants.INVENTORYMENU_BOTTOM_BUFFER + Drawing.DrawConstants.INVENTORYMENU_BUTTONS_HEIGHT + Drawing.DrawConstants.INVENTORYMENU_BETWEEN_BUFFER * 2 +
                Drawing.DrawConstants.INVENTORYMENU_TOP_BUFFER + Drawing.DrawConstants.INVENTORYMENU_TITLE_FROM_TOP + _titleFontHeight +
                ((inventory.getSpaceRectangle().Height + 1) * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2)) +
                Drawing.DrawConstants.TEXT_12_HEIGHT * 2 + Drawing.DrawConstants.INVENTORYMENU_TEXT_BUFFER * 2;
            
            _x = Game1.VIRTUAL_WINDOW_WIDTH / 2 - _width / 2;
            _y = (Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT) / 2 - _height / 2;

            _titleX = _x + _width / 2 - (int)(Game1.roboto_black_24.MeasureString("Inventory").X / 2.0f);
            _titleY = _y + _height - Drawing.DrawConstants.INVENTORYMENU_TITLE_FROM_TOP - _titleFontHeight;


            _gridSpaces = new UserInterface();
            _moreGridSpaces = new UserInterface();
            _items = new UserInterface();
            _keys = new UserInterface();
            _buttons = new UserInterface();
            _wholeUI.Add(_items);
            _wholeUI.Add(_keys);
            _wholeUI.Add(_gridSpaces);
            _wholeUI.Add(_moreGridSpaces);
            _wholeUI.Add(_buttons);
        }

        public override bool addTopBar() { return true; }

        public override void updateUI()
        {
            if (Game1.getTopMenu().GetType() != typeof(ItemRightClickMenu))
            {
                setupInventoryClickables();
            }
        }

        /// <summary>
        /// Will not allow the player to press escape to cancel out of the menu if they are
        /// holdinng an item that cannot be automatically dealt with (either by putting it
        /// back where it belongs in the inventory, or returning to the loot menu from which
        /// it belonged). If the player isn't holding a menu, it'll close normally.
        /// </summary>
        public override void onEscapePressed()
        {
            if (_heldItem != null)
            {
                if (_fromLoot)
                {
                    //Go back to the LootMenu
                    _heldItem = null;
                    Game1.closeMenu(this);
                }
                else
                {
                    putItemBack();
                }
            }
            else
            {
                Game1.closeMenu(this);
            }
        }



        /// <summary>
        /// Used when picking up a new item from loot. Adds the item to your hand and sets
        /// the _fromLoot flag to true.
        /// </summary>
        public void pickUpItem(Treasury.Equipment.Item item)
        {
            _heldItem = new Clickables.InventoryItem(this, item);
            _fromLoot = true;
            _prevHeldItemBLCornerGridSpaceX = -999; //Just set them to a random value off-screen since they're for telling the screen when to update anyway
            _prevHeldItemBLCornerGridSpaceY = -999;
            updateHeldItem();
        }
        /// <summary>
        /// Used when picking up an item from the inventory, or when swapping an item (which
        /// is technically also picking it up from the inventory). In the case of right clicking
        /// an item for use, it calls this function as well, but sets the setToMouseCoords flag
        /// to false in order to create the illusion of the item not being picked up (instead it
        /// will appear as if the item is used straight from the inventory, but for engine purposes
        /// we consider it picked up first).
        /// </summary>
        public void pickUpItem(Clickables.InventoryItem item, bool setToMouseCoords = true)
        {
            _heldItem = item;
            _fromLoot = false;
            _prevHeldItemBLCornerGridSpaceX = -999; //Just set them to a random value off-screen since they're for telling the screen when to update anyway
            _prevHeldItemBLCornerGridSpaceY = -999;
            if (setToMouseCoords)
            {
                updateHeldItem();
            }
        }

        public Clickables.InventoryItem getHeldItem()
        {
            return _heldItem;
        }

        public void updateHeldItem()
        {
            if (_heldItem != null)
            {
                //Calculate bottom left corner of where the item is held (for drawing)
                _heldItem._x = Game1.getInputController().getMousePos().X - _heldItem._width / 2;
                _heldItem._y = Game1.getInputController().getMousePos().Y - _heldItem._height / 2; //y from bottom

                //Calculate bottom left corner of where the item is held (in units of grid spaces, for placing)
                int pointZeroX = _x + Drawing.DrawConstants.INVENTORYMENU_HORIZONTAL_BUFFER + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER;
                int pointZeroY = _y + Drawing.DrawConstants.INVENTORYMENU_BOTTOM_BUFFER + Drawing.DrawConstants.INVENTORYMENU_BUTTONS_HEIGHT + Drawing.DrawConstants.INVENTORYMENU_BETWEEN_BUFFER;

                _heldItemBLCornerGridSpaceX = ((_heldItem._x + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE / 2) - pointZeroX) / 
                    (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2);
                _heldItemBLCornerGridSpaceY = ((_heldItem._y + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE / 2) - pointZeroY) /
                    (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2);

                //Check if it's moved enough to bother updating the UI
                if (_heldItemBLCornerGridSpaceX != _prevHeldItemBLCornerGridSpaceX || _heldItemBLCornerGridSpaceY != _prevHeldItemBLCornerGridSpaceY)
                {
                    setupGridSpaces();
                }
            }
        }

        /// <summary>
        /// Returns whether or not the currently-held item is at a mouse position that is
        /// considered within the bounds of the grid of the player's inventory. Used when
        /// highlighting grid spaces, to display them as red if the item cannot be placed
        /// partially out of the grid.
        /// </summary>
        public bool heldItemInGridBounds()
        {
            Rectangle inventoryRect = _inventory.getSpaceRectangle();

            if (_heldItemBLCornerGridSpaceX < inventoryRect.X || _heldItemBLCornerGridSpaceX + (_heldItem.getItem().getWidth() - 1) > inventoryRect.X + (inventoryRect.Width - 1) ||
                _heldItemBLCornerGridSpaceY < inventoryRect.Y || _heldItemBLCornerGridSpaceY + (_heldItem.getItem().getHeight() - 1) > inventoryRect.Y + (inventoryRect.Height - 1))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns whether or not the passed grid space is "under" the currently-held item.
        /// Used when highlighting grid spaces, to determine whether they need to be displayed
        /// as a colour other than black (blue to signify the item can be placed here, red to
        /// signify the opposite).
        /// </summary>
        public bool gridSpaceWithinHeldItem(Point grid)
        {
            if (grid.X >= _heldItemBLCornerGridSpaceX && grid.X <= _heldItemBLCornerGridSpaceX + (_heldItem.getItem().getWidth() - 1) &&
                grid.Y >= _heldItemBLCornerGridSpaceY && grid.Y <= _heldItemBLCornerGridSpaceY + (_heldItem.getItem().getHeight() - 1))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Attempts to place the currently-held item at the grid location it's held over. If 
        /// successful (and if the item is from a loot menu), it confirms that with the loot
        /// menu so that it knows the item can be removed from its loot list. If unsuccessful,
        /// it determines whether the item can be placed and swapped with another instead (in
        /// which case it will swap the two held items), otherwise nothing will happen.
        /// </summary>
        public void leftClickedWithHeldItem()
        {
            bool itemPlaced = _inventory.addItemToInventory(_heldItemBLCornerGridSpaceX, _heldItemBLCornerGridSpaceY, _heldItem.getItem());

            if (itemPlaced)
            {                
                if (_fromLoot)
                {
                    _heldItem.getItem().confirmTaken();
                    Game1.closeMenu(this);
                }

                _heldItem = null;
            }
            else
            {
                Treasury.Equipment.Item itemSwapped = _inventory.swapItemToInventory(_heldItemBLCornerGridSpaceX, _heldItemBLCornerGridSpaceY, _heldItem.getItem());

                if (itemSwapped != null)
                {
                    if (_fromLoot)
                    {
                        _heldItem.getItem().confirmTaken();
                    }

                    pickUpItem(new Clickables.InventoryItem(this, itemSwapped));
                }
            }
        }

        public void rightClickedWithHeldItem()
        {
            Game1.addToMenus(new ItemRightClickMenu(this, false));
        }

        /// <summary>
        /// Uses the held item (an item used straight from the inventory is converted to the
        /// held item first for this reason), and if the item is Firewood, it will refund it
        /// to the player if combat happened instead of a successful rest. After using an
        /// item, the InventoryMenu closes.
        /// </summary>
        public void useHeldItem()
        {
            _heldItem.getItem().onUse();

            if (_heldItem.getItem().GetType() == typeof(Treasury.Equipment.Items.Firewood)) //Firewood is a special case where the item might not get used up on use
            {
                if (Game1.getGameState() == Game1.gameState.happening)
                {
                    _heldItem = null; //Rest was successful, consume the item
                    Game1.closeMenu(this);
                }
                else
                {
                    putItemBack(true); //try to put it back, otherwise they'll have to deal with closing the menu themselves before combat
                }
            }
            else
            {
                _heldItem = null;
                Game1.closeMenu(this);
            }
        }

        /// <summary>
        /// Closes the menu upon item discard if the item discarded was from a loot menu. In
        /// that case, technically the item is considered never having been picked up (and thus
        /// the player can change their mind and pick it up again).
        /// </summary>
        public void discardHeldItem()
        {
            _heldItem = null;
            
            if (_fromLoot)
            {
                Game1.closeMenu(this); //Go back to LootMenu
            }
        }

        /// <summary>
        /// Attempts to put the item back where it came from in the inventory, if that's
        /// where it came from in the first place (does nothing if the item was from a
        /// loot menu).
        /// </summary>
        public void rightClickMenuCanceled()
        {
            if (!_fromLoot)
            {
                putItemBack();
            }
        }

        /// <summary>
        /// Will attempt to put the item back where it was picked up from in the inventory. If
        /// it fails, the menu won't close. Returns whether or not it was successful.
        /// </summary>
        public void putItemBack(bool closeMenu = true)
        {
            if (_inventory.addItemToInventory(_heldItem.getItem().getInventoryX(), _heldItem.getItem().getInventoryY(), _heldItem.getItem()))
            {
                _heldItem = null;
                
                if (closeMenu)
                {
                    Game1.closeMenu(this);
                }
            }
        }



        public void setupInventoryClickables()
        {
            setupGridSpaces();
            setupMoreGridSpaces();
            setupItems();
            setupKeys();
            setupButtons();
        }

        private void setupGridSpaces()
        {
            _gridSpaces.resetClickables();

            //Inventory
            Rectangle inventoryRect = _inventory.getSpaceRectangle();

            for (int x = 0; x < inventoryRect.Width; x++)
            {
                for (int y = 0; y < inventoryRect.Height; y++)
                {
                    Point xy = new Point(_x + Drawing.DrawConstants.INVENTORYMENU_HORIZONTAL_BUFFER + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER +
                        x * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2),
                        _y + Drawing.DrawConstants.INVENTORYMENU_BOTTOM_BUFFER + Drawing.DrawConstants.INVENTORYMENU_BUTTONS_HEIGHT + Drawing.DrawConstants.INVENTORYMENU_BETWEEN_BUFFER +
                        y * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2) + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER);

                    Clickables.Hovers.InventoryGrid gridSpace = new Clickables.Hovers.InventoryGrid(xy);

                    if (_heldItem != null && (!heldItemInGridBounds() && gridSpaceWithinHeldItem(new Point(x, y)) || _inventory.doesSpaceHaveItem(new Point(x, y))))
                    {
                        gridSpace.setHoveredCannotPlace(true);
                    }
                    else if (_heldItem != null && gridSpaceWithinHeldItem(new Point(x, y)))
                    {
                        gridSpace.setHovered(true);
                    }
                    else
                    {
                        gridSpace.setHovered(false);
                    }

                    _gridSpaces.addClickableToBack(gridSpace);
                }
            }
        }

        private void setupMoreGridSpaces()
        {
            _moreGridSpaces.resetClickables();
            
            Rectangle inventoryRect = _inventory.getSpaceRectangle();

            //Keys
            int keysWidth = Treasury.Equipment.Key.MAX_NUM_KEYS * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2);

            for (int x = 0; x < Treasury.Equipment.Key.MAX_NUM_KEYS; x++)
            {
                Point xy = new Point(_x + _width / 2 - keysWidth / 2 + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER +
                    x * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2),
                    _y + _height - Drawing.DrawConstants.INVENTORYMENU_TITLE_FROM_TOP - _titleFontHeight - Drawing.DrawConstants.INVENTORYMENU_BETWEEN_BUFFER -
                    Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE - Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2 + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER -
                    Drawing.DrawConstants.TEXT_12_HEIGHT - Drawing.DrawConstants.INVENTORYMENU_TEXT_BUFFER);

                Clickables.Hovers.InventoryGrid gridSpace = new Clickables.Hovers.InventoryGrid(xy);
                _moreGridSpaces.addClickableToBack(gridSpace);
            }

            //Text
            String itemsText = "Items:";
            Clickables.Hovers.DynamicText itemTextHover = new Clickables.Hovers.DynamicText(new Point(_x + Drawing.DrawConstants.INVENTORYMENU_HORIZONTAL_BUFFER,
                _y + Drawing.DrawConstants.INVENTORYMENU_BOTTOM_BUFFER + Drawing.DrawConstants.INVENTORYMENU_BUTTONS_HEIGHT +
                Drawing.DrawConstants.INVENTORYMENU_BETWEEN_BUFFER + Drawing.DrawConstants.INVENTORYMENU_TEXT_BUFFER +
                inventoryRect.Height * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2)),
                Game1.roboto_black_12, Drawing.DrawConstants.TEXT_12_HEIGHT, itemsText, new List<String>(), Color.GreenYellow);
            _moreGridSpaces.addClickableToBack(itemTextHover);

            String keyText = "Keys:";
            Clickables.Hovers.DynamicText keyTextHover = new Clickables.Hovers.DynamicText(new Point(_x + _width / 2 - keysWidth / 2,
                _y + _height - Drawing.DrawConstants.INVENTORYMENU_TITLE_FROM_TOP - _titleFontHeight - Drawing.DrawConstants.INVENTORYMENU_BETWEEN_BUFFER - Drawing.DrawConstants.TEXT_12_HEIGHT),
                Game1.roboto_black_12, Drawing.DrawConstants.TEXT_12_HEIGHT, keyText, new List<String>(), Color.GreenYellow);
            _moreGridSpaces.addClickableToBack(keyTextHover);
        }

        private void setupItems()
        {
            _items.resetClickables();

            List<Treasury.Equipment.Item> inventoryItems = _inventory.getItems();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                Clickables.InventoryItem item = new Clickables.InventoryItem(this, inventoryItems[i]);

                item._x = _x + Drawing.DrawConstants.INVENTORYMENU_HORIZONTAL_BUFFER + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER +
                    inventoryItems[i].getInventoryX() * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2);
                item._y = _y + Drawing.DrawConstants.INVENTORYMENU_BOTTOM_BUFFER + Drawing.DrawConstants.INVENTORYMENU_BUTTONS_HEIGHT + Drawing.DrawConstants.INVENTORYMENU_BETWEEN_BUFFER +
                    inventoryItems[i].getInventoryY() * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2) +
                    Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER;

                item._width = inventoryItems[i].getWidth() * Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE +
                    (inventoryItems[i].getWidth() - 1) * Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2;
                item._height = inventoryItems[i].getHeight() * Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE +
                    (inventoryItems[i].getHeight() - 1) * Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2;

                _items.addClickableToBack(item); //order doesn't matter
            }
        }

        private void setupKeys()
        {
            _keys.resetClickables();

            int numKeys = Game1.getDungeonHandler().getKeys().Count;
            int keysWidth = Treasury.Equipment.Key.MAX_NUM_KEYS * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2);

            for (int x = 0; x < numKeys; x++)
            {
                Point xy = new Point(_x + _width / 2 - keysWidth / 2 + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER +
                    x * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2),
                    _y + _height - Drawing.DrawConstants.INVENTORYMENU_TITLE_FROM_TOP - _titleFontHeight - Drawing.DrawConstants.INVENTORYMENU_BETWEEN_BUFFER -
                    Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE - Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER -
                    Drawing.DrawConstants.TEXT_12_HEIGHT - Drawing.DrawConstants.INVENTORYMENU_TEXT_BUFFER);

                Clickables.Hovers.CollectedKey key = new Clickables.Hovers.CollectedKey(Game1.getDungeonHandler().getKeys()[x], xy);
                _keys.addClickableToBack(key);
            }
        }

        private void setupButtons()
        {
            _buttons.resetClickables();

            //FIXIT implement more
            
            Clickables.Button exitButton = new Clickables.Button(Game1.pic_functionality_exitButton,
                new Point(_x + _width / 2 - Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_WIDTH / 2, _y + Drawing.DrawConstants.INVENTORYMENU_BOTTOM_BUFFER),
                Drawing.DrawConstants.INVENTORYMENU_EXIT_BUTTON_WIDTH, Drawing.DrawConstants.INVENTORYMENU_EXIT_BUTTON_HEIGHT, () =>
                {
                    if (_heldItem != null)
                    {
                        rightClickedWithHeldItem();
                    }
                    else
                    {
                        Game1.closeMenu(this);
                    }
                }, new List<String>());
            _buttons.addClickableToBack(exitButton); //order doesn't matter
        }
    }
}
