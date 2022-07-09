using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Clickables
{
    /// <summary>
    /// Used by the InventoryMenu to display items in inventory.
    /// </summary>
    public class InventoryItem : Clickable
    {
        Treasury.Equipment.Item _item;
        Menus.InventoryMenu _inventoryMenu;

        public InventoryItem(Menus.InventoryMenu inventoryMenu, Treasury.Equipment.Item item) : base()
        {
            _inventoryMenu = inventoryMenu;
            _item = item;

            _width = item.getWidth() * Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + (item.getWidth() - 1) * Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2;
            _height = item.getHeight() * Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + (item.getHeight() - 1) * Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2;

            _extraInfo = _item.getHoverInfo();
        }

        public Treasury.Equipment.Item getItem()
        {
            return _item;
        }



        /// <summary>
        /// Handles what happens in logic when the user hovers over the object. Will
        /// cause the item to glow, indicating that it can be picked up.
        /// </summary>
        public override void onHover()
        {
            _hovered = true;
            Game1.setHoveredClickable(this);
        }

        /// <summary>
        /// Handles what happens when the user is no longer hovering over this object.
        /// </summary>
        public override void onHoverEnd()
        {
            _hovered = false;
            Game1.setHoveredClickable(null);
        }

        /// <summary>
        /// Handles what happens when the user clicks on the object. Will pick up the
        /// item so that it can be moved.
        /// </summary>
        public override void onClick()
        {
            onHoverEnd();
            Game1.getInventory().removeFromInventory(_item);
            _inventoryMenu.pickUpItem(this);
        }

        /// <summary>
        /// Handles what happens when the user right clicks on the object. In the case
        /// of an inventory item, will bring up the ItemRightClickMenu for using or
        /// discarding the item, or closing the menu
        /// </summary>
        public override void onRightClick()
        {
            onHoverEnd();
            Game1.getInventory().removeFromInventory(getItem());
            _inventoryMenu.pickUpItem(this, false);
            Game1.addToMenus(new Menus.ItemRightClickMenu(_inventoryMenu, true));
        }
    }
}
