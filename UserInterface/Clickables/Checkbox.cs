using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables
{
    /// <summary>
    /// A checkbox-style Clickable that is comprised of text as well as the actual checkbox. After
    /// creation, can be assigned a list of other Checkboxes that will be unchecked once this gets
    /// checked (clicked when set to false).
    /// </summary>
    public class Checkbox : Clickable
    {
        List<Checkbox> _uncheckables;
        SpriteFont _font;
        String _text;
        Color _textColor;
        Color _shadowColor;
        bool _hasShadowColor;
        bool _isChecked;

        public Checkbox(SpriteFont font, String text, Color textColor, Color shadowColor, bool hasShadowColor, bool isChecked = false)  : base()
        {
            _uncheckables = new List<Checkbox>();

            _font = font;
            _text = text;
            _textColor = textColor;
            _shadowColor = shadowColor;
            _hasShadowColor = hasShadowColor;
            _isChecked = isChecked;
        }

        //Getters
        public SpriteFont getFont()
        {
            return _font;
        }
        public String getText()
        {
            return _text;
        }
        public Color getTextColor()
        {
            return _textColor;
        }
        public Color getShadowColor()
        {
            return _shadowColor;
        }
        public bool hasShadowColor()
        {
            return _hasShadowColor;
        }
        public bool isChecked()
        {
            return _isChecked;
        }



        //Setters
        public void setChecked(bool isChecked)
        {
            _isChecked = isChecked;
        }
        public void setUncheckables(List<Checkbox> uncheckables)
        {
            _uncheckables = uncheckables;
        }
        public void addToUncheckables(Checkbox uncheckable)
        {
            _uncheckables.Add(uncheckable);
        }



        /// <summary>
        /// Handles what happens in logic when the user hovers over this object.
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
        /// Handles what happens in logic when the user has clicked the checkbox. Will
        /// alternate the flag of _isChecked
        /// </summary>
        public override void onClick()
        {
            _isChecked = !_isChecked;
            if (_isChecked)
            {
                foreach (Checkbox uncheck in _uncheckables)
                {
                    uncheck.setChecked(false);
                }
            }

            onHoverEnd();
        }
    }
}
