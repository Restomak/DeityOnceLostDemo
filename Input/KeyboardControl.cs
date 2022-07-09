using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DeityOnceLost.Input
{
    class KeyboardControl
    {
        private KeyboardState _keyboardState;

        private bool _escapePressed, _escapeHeld;
        private bool _continuePressed, _continueHeld;
        private bool _upPressed, _upHeld;
        private bool _rightPressed, _rightHeld;
        private bool _downPressed, _downHeld;
        private bool _leftPressed, _leftHeld;

        public KeyboardControl()
        {
            _escapePressed = false;
            _escapeHeld = false;
            _continuePressed = false;
            _continueHeld = false;
            _upPressed = false;
            _upHeld = false;
            _rightPressed = false;
            _rightHeld = false;
            _downPressed = false;
            _downHeld = false;
            _leftPressed = false;
            _leftHeld = false;
        }

        //Getters
        public bool isEscapePressed()
        {
            return _escapePressed;
        }
        public bool isContinuePressed()
        {
            return _continuePressed;
        }
        public bool isUpPressed()
        {
            return _upPressed;
        }
        public bool isRightPressed()
        {
            return _rightPressed;
        }
        public bool isDownPressed()
        {
            return _downPressed;
        }
        public bool isLeftPressed()
        {
            return _leftPressed;
        }



        /// <summary>
        /// Updates the keyboard keys' pressed/held states
        /// </summary>
        public void checkKeyboard()
        {
            _keyboardState = Keyboard.GetState();

            //escape
            if (_keyboardState.IsKeyDown(Keys.Escape))
            {
                if (!_escapeHeld)
                {
                    _escapePressed = true;
                }
                else
                {
                    _escapePressed = false;
                }
                _escapeHeld = true;
            }
            else
            {
                _escapePressed = false;
                _escapeHeld = false;
            }

            //continue
            if (_keyboardState.IsKeyDown(Keys.Space) || _keyboardState.IsKeyDown(Keys.Enter))
            {
                if (!_continueHeld)
                {
                    _continuePressed = true;
                }
                else
                {
                    _continuePressed = false;
                }
                _continueHeld = true;
            }
            else
            {
                _continuePressed = false;
                _continueHeld = false;
            }

            //up
            if (_keyboardState.IsKeyDown(Keys.Up) || _keyboardState.IsKeyDown(Keys.W))
            {
                if (!_upHeld)
                {
                    _upPressed = true;
                }
                else
                {
                    _upPressed = false;
                }
                _upHeld = true;
            }
            else
            {
                _upPressed = false;
                _upHeld = false;
            }

            //right
            if (_keyboardState.IsKeyDown(Keys.Right) || _keyboardState.IsKeyDown(Keys.D))
            {
                if (!_rightHeld)
                {
                    _rightPressed = true;
                }
                else
                {
                    _rightPressed = false;
                }
                _rightHeld = true;
            }
            else
            {
                _rightPressed = false;
                _rightHeld = false;
            }

            //down
            if (_keyboardState.IsKeyDown(Keys.Down) || _keyboardState.IsKeyDown(Keys.S))
            {
                if (!_downHeld)
                {
                    _downPressed = true;
                }
                else
                {
                    _downPressed = false;
                }
                _downHeld = true;
            }
            else
            {
                _downPressed = false;
                _downHeld = false;
            }

            //left
            if (_keyboardState.IsKeyDown(Keys.Left) || _keyboardState.IsKeyDown(Keys.A))
            {
                if (!_leftHeld)
                {
                    _leftPressed = true;
                }
                else
                {
                    _leftPressed = false;
                }
                _leftHeld = true;
            }
            else
            {
                _leftPressed = false;
                _leftHeld = false;
            }
        }
    }
}
