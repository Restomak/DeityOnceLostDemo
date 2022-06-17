using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Treasury
{
    public abstract class Treasure
    {
        public enum treasureType
        {
            addCard,
            addInventoryItem,
            money,
            relic
        }

        private treasureType _treasureType;
        protected bool _persistsIfLeaveRoom, _taken;
        protected String _treasureText;

        public Treasure(treasureType type)
        {
            _treasureType = type;
            _treasureText = "???";

            _persistsIfLeaveRoom = false; //by default, leaving behind a treasure makes it disappear
            _taken = false;
        }

        //Getters
        public treasureType getTreasureType()
        {
            return _treasureType;
        }
        public String getTreasureText()
        {
            return _treasureText;
        }
        public bool isTaken()
        {
            return _taken;
        }

        public Texture2D getIcon()
        {
            switch(_treasureType)
            {
                case treasureType.addCard:
                    break;
                case treasureType.addInventoryItem:
                    break;
                case treasureType.money:
                    break;
                case treasureType.relic:
                    break;
            }

            Game1.errorLog.Add("Treasure treasureType." + _treasureType.ToString() + " does not yet have an icon paired with it");
            return Game1.pic_functionality_bar;
        }



        public abstract void onTaken();
    }
}
