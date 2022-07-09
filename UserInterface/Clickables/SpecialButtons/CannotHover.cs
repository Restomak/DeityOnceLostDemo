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
    /// Version of button that overrides the onHover to do nothing. Meant for buttons that
    /// don't appear as buttons (such as the full-screen button with no texture that is used
    /// in the ItemRightClickMenu).
    /// </summary>
    public class CannotHover : Button
    {
        public CannotHover(Texture2D texture, Point xy, int width, int height, Action function) : 
            base(texture, xy, width, height, function, new List<String>())
        {
        }

        public override void onHover() { }

        public override void onHoverEnd() { }
    }
}
