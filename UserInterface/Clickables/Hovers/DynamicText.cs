using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables.Hovers
{
    /// <summary>
    /// Version of HoverInfo that displays text. Has an _onUpdate Action that is passed to
    /// the constructor that allows the text to dynamically change on updates.
    /// </summary>
    class DynamicText : HoverInfo
    {
        SpriteFont _font;
        String _displayText;
        Action _onUpdate;
        Color _color, _shadowColor;

        public DynamicText(Point xy, SpriteFont font, int fontHeight, String displayText, List<String> description, Color color) : base(xy, 0, fontHeight, description)
        {
            _font = font;
            _width = (int)font.MeasureString(displayText).X;
            _displayText = displayText;
            _color = color;
            _shadowColor = Color.Black;
        }
        public DynamicText(Point xy, SpriteFont font, int fontHeight, String displayText, List<String> description, Color color, Color shadowColor) : base(xy, 0, fontHeight, description)
        {
            _font = font;
            _width = (int)font.MeasureString(displayText).X;
            _displayText = displayText;
            _color = color;
            _shadowColor = shadowColor;
        }

        //Getters
        public SpriteFont getFont()
        {
            return _font;
        }

        public String getDisplayText()
        {
            return _displayText;
        }

        public Color getColor()
        {
            return _color;
        }

        public Color getShadowColor()
        {
            return _shadowColor;
        }



        //Setters
        public void setOnUpdate(Action onUpdate)
        {
            _onUpdate = onUpdate;
        }



        public void updateString(String displayText)
        {
            _displayText = displayText;
        }

        public void onUpdate()
        {
            if (_onUpdate != null)
            {
                _onUpdate();
            }
        }
    }
}
