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
    /// Created as a Clickable but without any of the functionality of being clicked. Meant
    /// for aesthetic non-functional parts of the user interface that need to be drawn in a
    /// specific order in the stack.
    /// </summary>
    class AestheticOnly : Clickable
    {
        Texture2D _texture;
        Color _color;

        public AestheticOnly(Texture2D texture, Point xy, int width, int height, Color color)
        {
            _texture = texture;
            _x = xy.X;
            _y = xy.Y;
            _width = width;
            _height = height;
            _color = color;
        }
        
        public Texture2D getTexture()
        {
            return _texture;
        }
        public Color getColor()
        {
            return _color;
        }

        //None of these have any interaction
        public override void onHover() { }
        public override void onHoverEnd() { }
        public override void onClick() { }
    }
}
