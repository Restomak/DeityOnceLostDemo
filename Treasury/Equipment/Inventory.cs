using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Treasury.Equipment
{
    /// <summary>
    /// The player's inventory, which is stored in Game1, keeps track of all items
    /// that the player has on them (including Keys).
    /// </summary>
    public class Inventory
    {
        public const int INVENTORY_WIDTH = 4;
        public const int INVENTORY_WIDTH_UPGRADED = 5;
        public const int INVENTORY_WIDTH_FINAL = 6;
        public const int INVENTORY_HEIGHT = 4;

        public enum upgradeLevel
        {
            basic,
            upgraded,
            final
        }

        List<Item> _items;
        Rectangle _inventoryRect;
        upgradeLevel _upgradeLevel;

        public Inventory()
        {
            _items = new List<Item>();
            _upgradeLevel = upgradeLevel.basic;

            setupEmptyInventoryRect();
        }

        public List<Item> getItems()
        {
            return _items;
        }

        public Rectangle getSpaceRectangle()
        {
            return _inventoryRect;
        }

        public upgradeLevel getUpgradeLevel()
        {
            return _upgradeLevel;
        }

        /// <summary>
        /// Returns whether or not a specific grid location in the inventory is currently
        /// being occupied by an Item in its list.
        /// </summary>
        public bool doesSpaceHaveItem(Point point)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (point.X >= _items[i].getInventoryX() && point.X <= _items[i].getInventoryX() + _items[i].getWidth() - 1 &&
                    point.Y >= _items[i].getInventoryY() && point.Y <= _items[i].getInventoryY() + _items[i].getHeight() - 1)
                {
                    return true;
                }
            }

            return false;
        }



        public void setupEmptyInventoryRect()
        {
            int width = INVENTORY_WIDTH;

            switch (_upgradeLevel)
            {
                case upgradeLevel.upgraded:
                    width = INVENTORY_WIDTH_UPGRADED;
                    break;
                case upgradeLevel.final:
                    width = INVENTORY_WIDTH_FINAL;
                    break;
            }

            _inventoryRect = new Rectangle(0, 0, width, INVENTORY_HEIGHT);
        }

        /// <summary>
        /// Attempts to add an item to the inventory. If successful (which means it was
        /// properly placed within the bounds of the inventory's space and not colliding
        /// with another inventory item), it will return true. Otherwise, it will return
        /// false, meaning the item was not placed.
        /// </summary>
        public bool addItemToInventory(int xLeft, int yBottom, Item item)
        {
            Rectangle itemRect = new Rectangle(xLeft, yBottom, item.getWidth(), item.getHeight());

            //Make sure it's within inventory bounds
            if (!_inventoryRect.Contains(itemRect))
            {
                return false;
            }

            //Make sure it doesn't collide with other items in inventory
            for (int i = 0; i < _items.Count; i++)
            {
                Rectangle inventoryItemRect = new Rectangle(_items[i].getInventoryX(), _items[i].getInventoryY(), _items[i].getWidth(), _items[i].getHeight());

                if (inventoryItemRect.Intersects(itemRect))
                {
                    return false;
                }
            }

            //We're free to add it!
            _items.Add(item);
            item.addToInventory(xLeft, yBottom);
            return true;
        }

        /// <summary>
        /// Use only if addItemToInventory fails. Attempts to add an item to the inventory
        /// in a space where it only intersects with one other item. If successful (which
        /// means it was properly placed within the bounds of the inventory's space and
        /// colliding with exactly one other inventory item), it will return the swapped
        /// item. Otherwise, it will return null, meaning no swap can happen.
        /// </summary>
        public Item swapItemToInventory(int xLeft, int yBottom, Item item)
        {
            Rectangle itemRect = new Rectangle(xLeft, yBottom, item.getWidth(), item.getHeight());

            //Make sure it's within inventory bounds
            if (!_inventoryRect.Contains(itemRect))
            {
                return null;
            }

            //Make sure it doesn't collide with other items in inventory
            List<Item> intersectedItems = new List<Item>();
            for (int i = 0; i < _items.Count; i++)
            {
                Rectangle inventoryItemRect = new Rectangle(_items[i].getInventoryX(), _items[i].getInventoryY(), _items[i].getWidth(), _items[i].getHeight());

                //Not sure if intersect covers contains, so just in case the other two are here too
                if (inventoryItemRect.Intersects(itemRect) || inventoryItemRect.Contains(itemRect) || itemRect.Contains(inventoryItemRect))
                {
                    intersectedItems.Add(_items[i]);
                }
            }

            
            if (intersectedItems.Count == 0)
            {
                Game1.addToErrorLog("Should have used addItemToInventory rather than swapItemToInventory. Item not added to inventory.");
                return null;
            }
            else if (intersectedItems.Count > 1)
            {
                return null; //too many items
            }


            //We're free to swap them!
            _items.Add(item);
            item.addToInventory(xLeft, yBottom);

            _items.Remove(intersectedItems[0]);
            return intersectedItems[0];
        }

        public void changeUpgradeLevel(upgradeLevel newUpgradeLevel)
        {
            _upgradeLevel = newUpgradeLevel;
            setupEmptyInventoryRect();
        }

        public void removeFromInventory(Item item)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (item.GetType() == _items[i].GetType())
                {
                    _items.RemoveAt(i);
                    return;
                }
            }

            Game1.addToErrorLog("Tried to remove an item from inventory that wasn't there: " + item.getName());
        }
    }
}
