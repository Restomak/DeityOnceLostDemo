using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Treasury.Equipment
{
    public class Key : Treasure
    {
        public const int MAX_NUM_KEYS = 4;

        public enum keyColor
        {
            red,
            blue,
            green,
            yellow
        }

        keyColor _keyColor;

        public Key(keyColor color) : base(treasureType.key)
        {
            _keyColor = color;

            _treasureText = "The " + color.ToString() + " key";
        }

        public keyColor getKeyColor()
        {
            return _keyColor;
        }

        public override void onTaken()
        {
            Game1.getDungeonHandler().addKey(this);
            _taken = true;
        }

        public override Color getIconColor()
        {
            return Key.getKeyColor(_keyColor);
        }



        public static Color getKeyColor(keyColor color)
        {
            switch(color)
            {
                case keyColor.red:
                    return Color.Red;
                case keyColor.blue:
                    return Color.Blue;
                case keyColor.green:
                    return Color.Green;
                case keyColor.yellow:
                    return Color.Yellow;
                default:
                    Game1.addToErrorLog("undefined keyColor: " + color.ToString());
                    return Color.White;
            }
        }
    }
}
