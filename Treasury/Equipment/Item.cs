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
    /// Abstract base class for all inventory items, variants of Treasures that are
    /// stored in the player's Inventory, for use in or out of combat at a time of
    /// the player's choice.
    /// </summary>
    public abstract class Item : Treasure
    {
        /* Item ideas:
         * 
         * an item that can break down certain dungeon walls (usable in adjacent tile). gives me the ability to have connectors that lead to treasure rooms, etc. and having the item rewards you
         * 
         */

        Texture2D _texture;
        int _width, _height, _inventoryX, _inventoryY;
        bool _usableOutOfCombat, _usableInCombat;
        String _name;
        protected List<String> _description;

        public Item(Texture2D texture, int width, int height, bool usableOutOfCombat, bool usableInCombat, String name, List<String> description) : base(treasureType.addInventoryItem)
        {
            _texture = texture;
            _width = width;
            _height = height;
            _usableOutOfCombat = usableOutOfCombat;
            _usableInCombat = usableInCombat;
            _name = name;
            _description = description;

            _treasureText = name;
        }

        //Getters
        public Texture2D getTexture()
        {
            return _texture;
        }
        public int getWidth()
        {
            return _width;
        }
        public int getHeight()
        {
            return _height;
        }
        public int getInventoryX()
        {
            return _inventoryX;
        }
        public int getInventoryY()
        {
            return _inventoryY;
        }
        public String getName()
        {
            return _name;
        }
        public List<String> getDescription()
        {
            return _description;
        }



        /// <summary>
        /// Since the inventory isn't stored as a two-dimensional array or list and instead
        /// items are kept in a list, each item must store its location in the inventory.
        /// </summary>
        public void addToInventory(int x, int y)
        {
            _inventoryX = x;
            _inventoryY = y;
        }

        /// <summary>
        /// Returns whether or not the item can be used, depending on whether or not the
        /// player is currently in combat or idling on the dungeon map.
        /// </summary>
        public bool canUse()
        {
            if (_usableOutOfCombat && Game1.getGameState() == Game1.gameState.dungeon)
            {
                return true;
            }
            else if (_usableInCombat && Game1.getGameState() == Game1.gameState.combat)
            {
                return true;
            }

            return false;
        }



        /// <summary>
        /// When taken from a loot window, an InventoryMenu is automatically created so that the
        /// player can choose where to place their new item, if they decide to keep it.
        /// </summary>
        public override void onTaken()
        {
            //Open inventory menu
            UserInterface.Menus.InventoryMenu inventoryMenu = new UserInterface.Menus.InventoryMenu(Game1.getInventory());
            Game1.addToMenus(inventoryMenu);

            //Put it in hand
            inventoryMenu.pickUpItem(this);
        }

        /// <summary>
        /// Used only by the InventoryMenu to confirm it was successfully placed in the inventory
        /// so that it can disappear from the loot window.
        /// </summary>
        public void confirmTaken()
        {
            _taken = true;
        }

        public abstract void onUse();
        public abstract List<UserInterface.ExtraInfo> getHoverInfo();
        public abstract List<UserInterface.ExtraInfo> getHoverExtraInfo();
    }
}
