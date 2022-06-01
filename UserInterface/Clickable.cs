using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface
{
    public abstract class Clickable
    {
        public int _x, _y, _width, _height, _layer;
        protected bool _clicked, _held;

        public bool mouseInBoundaries(int mouseX, int mouseY)
        {
            if (mouseX >= _x && mouseX <= _x + _width && mouseY >= _y && mouseY <= _y + _height)
            {
                return true;
            }

            return false;
        }
        
        public abstract void clickLogic();
    }
}
