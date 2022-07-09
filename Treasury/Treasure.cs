using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Treasury
{
    /// <summary>
    /// Base class for every possible treasure in the game, be it items or the abstract of
    /// adding a new card to your deck, or a relic- even a curse.
    /// </summary>
    public abstract class Treasure
    {
        public enum treasureType
        {
            addCard,
            removeCard,
            money,
            blessing,
            curse,
            partyBuff,
            addInventoryItem,
            key
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
                case treasureType.removeCard:
                    return Game1.pic_functionality_cardBackLootIcon;
                case treasureType.money:
                    return Game1.pic_functionality_goldLootIcon;
                case treasureType.blessing:
                case treasureType.curse:
                    return Treasures.Relic.getRelicIcon(this);
                case treasureType.addInventoryItem:
                    return Game1.pic_functionality_combatInventoryIcon;
                case treasureType.key:
                    return Game1.pic_item_key;
            }

            Game1.addToErrorLog("Treasure treasureType." + _treasureType.ToString() + " does not yet have an icon paired with it");
            return Game1.pic_functionality_bar;
        }

        public virtual Color getIconColor()
        {
            return Color.White;
        }



        public abstract void onTaken();
    }
}
