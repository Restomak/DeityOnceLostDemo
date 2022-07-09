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
    /// Used as buttons for event choices.
    /// </summary>
    class Decision : Clickable
    {
        protected Events.Choice _choice;
        protected int _choiceNum;

        public Decision(int choiceNum, Events.Choice choice)
        {
            _x = Game1.VIRTUAL_WINDOW_WIDTH / 2 - Drawing.DrawConstants.EVENT_CHOICE_WIDTH / 2;
            _y = (Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT) / 2 - Drawing.DrawConstants.EVENT_BACKGROUND_HEIGHT / 2 + Drawing.DrawConstants.EVENT_CHOICE_INTIAL_BUFFER +
                choiceNum * (Drawing.DrawConstants.EVENT_CHOICE_HEIGHT + Drawing.DrawConstants.EVENT_CHOICE_BUFFER);
            _width = Drawing.DrawConstants.EVENT_CHOICE_WIDTH;
            _height = Drawing.DrawConstants.EVENT_CHOICE_HEIGHT;

            _choiceNum = choiceNum;
            _choice = choice;

            _extraInfo = _choice.getExtraInfo();

            _extraInfoAtMouse = true;
        }

        public Events.Choice getChoice()
        {
            return _choice;
        }



        /// <summary>
        /// Handles what happens in logic when the user hovers over the choice. It will
        /// simply enable the hover flag so that it can be displayed as glowing so the
        /// player knows it's clickable.
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
        /// Handles what happens in logic when the user has clicked the choice. Will
        /// call the Choice's onChoose function and then tell the EventHandler that
        /// a choice has been made.
        /// </summary>
        public override void onClick()
        {
            Game1.getEventHandler().choiceChosen(_choiceNum);

            onHoverEnd();
        }
    }
}
