using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Clickables
{
    /// <summary>
    /// Version of Decision that is used for MissableChoices instead of Choices, so that
    /// they can be displayed as grayed out or normal, depending on the conditions for
    /// being able to choose them.
    /// </summary>
    class MissableDecision : Decision
    {
        public MissableDecision(int choiceNum, Events.MissableChoice choice) : base(choiceNum, choice)
        {

        }

        public new Events.MissableChoice getChoice()
        {
            return (Events.MissableChoice)_choice;
        }



        /// <summary>
        /// Decision definition:
        /// Handles what happens in logic when the user hovers over the choice. It will
        /// simply enable the hover flag so that it can be displayed as glowing so the
        /// player knows it's clickable.
        /// 
        /// MissableDecision edit:
        /// Will only do the above if the player can select this choice.
        /// </summary>
        public override void onHover()
        {
            if (((Events.MissableChoice)_choice).canChoose())
            {
                _hovered = true;
                Game1.setHoveredClickable(this);
            }
        }

        /// <summary>
        /// Handles what happens in logic when the user has clicked the choice. Will
        /// call the Choice's onChoose function and then tell the EventHandler that
        /// a choice has been made.
        /// 
        /// MissableDecision edit:
        /// Will only do the above if the player can select this choice, and if so,
        /// will also consume the requirement for making the choice selectable.
        /// </summary>
        public override void onClick()
        {
            if (((Events.MissableChoice)_choice).canChoose())
            {
                ((Events.MissableChoice)_choice).consumeRequirement();
                Game1.getEventHandler().choiceChosen(_choiceNum);

                onHoverEnd();
            }
        }
    }
}
