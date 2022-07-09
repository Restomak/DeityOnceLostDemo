using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.ExtraInfos
{
    /// <summary>
    /// Used when hovering over a Clickable needs to display an icon in addition to text.
    /// The icon is displayed below the text, snapped to the left side of the text box.
    /// </summary>
    public class IconAndTextInfo : TextInfo
    {
        protected Texture2D _texture;
        public int _width, _height;
        bool _transparent;

        public IconAndTextInfo(Texture2D texture, int width, int height, List<String> description, bool transparent = false) : base(description)
        {
            _texture = texture;
            _transparent = transparent;

            _width = width;
            _height = height;
        }
        
        public Texture2D getTexture()
        {
            return _texture;
        }

        public bool isTransparent()
        {
            return _transparent;
        }
    }
}
