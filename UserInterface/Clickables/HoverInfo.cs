using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables
{
    class HoverInfo : Clickable
    {
        Action _hoverFunction;
        Texture2D _texture; //these need to supply their texture since there's no other way to determine which is which in the DrawHandler

        public HoverInfo(Texture2D texture, Point xy, int width, int height, Action function)
        {
            _texture = texture;
            _x = xy.X;
            _y = xy.Y;
            _width = width;
            _height = height;
            _hoverFunction = function;
        }

        //Getter
        public Texture2D getTexture()
        {
            return _texture;
        }
        
        /// <summary>
        /// Handles what happens in logic when the user hovers over the object. Will
        /// call the function passed to the constructor when the object was created.
        /// </summary>
        public override void onHover()
        {
            _hovered = true;
            Game1.setHoveredClickable(this);

            _hoverFunction();
        }

        /// <summary>
        /// Has no function.
        /// </summary>
        public override void onClick() { }
    }
}
