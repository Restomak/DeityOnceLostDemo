using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables.SpecialButtons
{
    /// <summary>
    /// Version of button that doesn't use a texture and instead draws text over the generic
    /// bar texture. Instantiated with a boolean for whether or not the drop down button can
    /// be clicked, and if unable to click it, it will simply display as grayed out with a
    /// red background.
    /// </summary>
    public class DropDownButton : Button
    {
        String _buttonText;
        bool _canBeClicked;

        public DropDownButton(String buttonText, Action function, bool canBeClicked = true) : //Location & width get calculated after creation so we pass as 0 for now
            base(Game1.pic_functionality_bar, new Point(0, 0), 0, DropDownButton.getDropDownButtonHeight(), function, new List<String>())
        {
            _buttonText = buttonText;
            _canBeClicked = canBeClicked;
        }

        public String getButtonText()
        {
            return _buttonText;
        }
        
        public Color getColor(bool isHighlighted)
        {            
            if (!isHighlighted)
            {
                return Color.DarkSlateGray;
            }

            if (!_canBeClicked)
            {
                return Color.DarkRed;
            }

            return Color.MidnightBlue;
        }

        public Color getTextColor()
        {
            if (!_canBeClicked)
            {
                return Color.DarkGray;
            }

            return Color.White;
        }



        public static SpriteFont getDropDownButtonFont()
        {
            return Game1.roboto_black_16;
        }
        public static int getDropDownButtonHeight()
        {
            return Drawing.DrawConstants.TEXT_16_HEIGHT;
        }
    }
}
