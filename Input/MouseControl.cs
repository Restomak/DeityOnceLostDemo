using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DeityOnceLost.Input
{
    class MouseControl
    {
        private MouseState mouseState;
        private double mouseX, mouseYFromBottom;
        private int oldScrollWheel;
        private bool leftClicked, rightClicked, leftClickHeld, rightClickHeld, scrollWheelUp, scrollWheelDown;

        public MouseControl()
        {
            leftClickHeld = false;
            rightClickHeld = false;
            scrollWheelUp = false;
            scrollWheelDown = false;
            oldScrollWheel = 0;
        }

        //Getters
        public Point getMousePosition()
        {
            return new Point((int)mouseX, (int)mouseYFromBottom);
        }
        public bool isLeftClicked()
        {
            return leftClicked;
        }
        public bool isRightClicked()
        {
            return rightClicked;
        }
        public bool isLeftHeld()
        {
            return leftClickHeld;
        }
        public bool isRightHeld()
        {
            return rightClickHeld;
        }
        public bool didScrollUp()
        {
            return scrollWheelUp;
        }
        public bool didScrollDown()
        {
            return scrollWheelDown;
        }


        /// <summary>
        /// Updates the mouse position & click/hold states. Mouse y is from the bottom of the screen because that's what I prefer.
        /// </summary>
        public void checkMouse(WindowControl windowControl)
        {
            mouseState = Mouse.GetState();
            Rectangle screenRect = windowControl.getScreenRect();

            if (screenRect.Contains(mouseState.Position))
            {
                //Mouse Position
                double widthScale = (double)screenRect.Width / (double)Game1.VIRTUAL_WINDOW_WIDTH;
                double heightScale = (double)screenRect.Height / (double)Game1.VIRTUAL_WINDOW_HEIGHT;

                double inWindowX = mouseState.X - screenRect.Left;
                double inWindowY = mouseState.Y - screenRect.Top;

                mouseX = inWindowX / widthScale;
                mouseYFromBottom = Game1.VIRTUAL_WINDOW_HEIGHT - inWindowY / heightScale;

                //Mouse Clicking
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (!leftClickHeld)
                    {
                        leftClicked = true;
                    }
                    else
                    {
                        leftClicked = false;
                    }
                    leftClickHeld = true;
                }
                else if (mouseState.LeftButton == ButtonState.Released)
                {
                    leftClicked = false;
                    leftClickHeld = false;
                }

                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    if (!rightClickHeld)
                    {
                        rightClicked = true;
                    }
                    else
                    {
                        rightClicked = false;
                    }
                    rightClickHeld = true;
                }
                else if (mouseState.RightButton == ButtonState.Released)
                {
                    rightClicked = false;
                    rightClickHeld = false;
                }

                //Mouse Scroll Wheel
                scrollWheelUp = false;
                scrollWheelDown = false;
                int newScrollWheel = mouseState.ScrollWheelValue;
                if (oldScrollWheel != newScrollWheel)
                {
                    if (oldScrollWheel < newScrollWheel)
                    {
                        scrollWheelUp = true;
                    }
                    else if (oldScrollWheel > newScrollWheel)
                    {
                        scrollWheelDown = true;
                    }

                    oldScrollWheel = newScrollWheel;
                }
            }
            else
            {
                //do not update mouse
            }
        }
    }
}
