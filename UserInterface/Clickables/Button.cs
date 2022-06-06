using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables
{
    public class Button : Clickable
    {
        Action _buttonFunction;
        Texture2D _texture; //buttons need to supply their texture since there's no other way to determine which button is which in the DrawHandler

        public Button(Texture2D texture, Point xy, int width, int height, Action function)
        {
            _texture = texture;
            _x = xy.X;
            _y = xy.Y;
            _width = width;
            _height = height;
            _buttonFunction = function;
        }

        //Getter
        public Texture2D getTexture()
        {
            return _texture;
        }

        /// <summary>
        /// Handles what happens in logic when the user hovers over the button. It will
        /// simply enable the hover flag so that it can be displayed as glowing so the
        /// player knows it's clickable.
        /// </summary>
        public override void onHover()
        {
            _hovered = true;
            Game1.setHoveredClickable(this);
        }

        /// <summary>
        /// Handles what happens in logic when the user has clicked the button. Will
        /// call the function passed to the constructor when the button was created.
        /// </summary>
        public override void onClick()
        {
            _buttonFunction();
        }
    }
}
