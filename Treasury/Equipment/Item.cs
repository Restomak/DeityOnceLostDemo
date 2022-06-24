using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Treasury.Equipment
{
    public abstract class Item : Treasure
    {
        Texture2D _texture;
        int _width, _height, _inventoryX, _inventoryY;
        bool _usableOutOfCombat, _usableInCombat;

        public Item(Texture2D texture, int width, int height, bool usableOutOfCombat, bool usableInCombat) : base(treasureType.addInventoryItem)
        {
            _texture = texture;
            _width = width;
            _height = height;
            _usableOutOfCombat = usableOutOfCombat;
            _usableInCombat = usableInCombat;
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



        public void addToInventory(int x, int y)
        {
            _inventoryX = x;
            _inventoryY = y;
        }

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



        public override void onTaken()
        {
            //FIXIT add to inventory when implemented
            //open inventory menu for placing
            //_taken = function of adding to inventory;
        }

        public abstract void onUse();
    }
}
