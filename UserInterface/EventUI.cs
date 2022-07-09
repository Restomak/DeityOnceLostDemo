using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface
{
    /// <summary>
    /// One of the major three gameState UIs along with CombatUI and MapUI. EventUI is used to
    /// store the choices as Decisions or MissableDecisions so that they can be seen and clicked
    /// by the player. It also stores the player's relics (blessings and curses) for display.
    /// </summary>
    public class EventUI
    {
        List<UserInterface> _wholeUI;

        UserInterface _buttons;
        UserInterface _relics;

        public EventUI(Events.EventHandler eventHandler)
        {
            _wholeUI = new List<UserInterface>();

            _buttons = new UserInterface();
            _relics = new UserInterface();

            //added in sorted fashion, top to bottom is front of the screen to back
            _wholeUI.Add(_buttons);
            _wholeUI.Add(_relics);
        }

        //Setters
        public void setAsActiveUI(List<UserInterface> activeUI)
        {
            activeUI.Clear();

            for (int i = 0; i < _wholeUI.Count; i++)
            {
                activeUI.Add(_wholeUI[i]);
            }

            Game1.addTopBar();
        }



        public void initializeChoiceButtons(Events.EventHandler eventHandler, Events.Happening newEvent)
        {
            for (int i = 0; i < newEvent.getChoices().Count; i++)
            {
                if (newEvent.getChoices()[i].GetType() == typeof(Events.MissableChoice))
                {
                    Clickables.MissableDecision choiceButton = new Clickables.MissableDecision(i, (Events.MissableChoice)newEvent.getChoices()[i]);
                    _buttons.addClickableToFront(choiceButton); //order doesn't matter
                }
                else
                {
                    Clickables.Decision choiceButton = new Clickables.Decision(i, newEvent.getChoices()[i]);
                    _buttons.addClickableToFront(choiceButton); //order doesn't matter
                }
            }
        }

        /// <summary>
        /// Called by the EventHandler to set up the new event's choices as clickable buttons (Decisions
        /// and MissableDecisions). Also makes the call to set up the player's relics as RelicDisplays.
        /// </summary>
        public void setupNewEvent(List<UserInterface> activeUI, Events.EventHandler eventHandler, Events.Happening newEvent)
        {
            setAsActiveUI(activeUI);

            initializeChoiceButtons(eventHandler, newEvent);
            Clickables.Hovers.RelicDisplay.setupRelicsUI(_relics);
        }
    }
}
