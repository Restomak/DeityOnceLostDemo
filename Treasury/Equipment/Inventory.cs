using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Treasury.Equipment
{
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

        public void changeUpgradeLevel(upgradeLevel newUpgradeLevel)
        {
            _upgradeLevel = newUpgradeLevel;
            setupEmptyInventoryRect();
        }
    }
}
