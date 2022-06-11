using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface
{
    public class EventUI
    {
        List<UserInterface> _wholeUI;

        UserInterface _buttons;

        public EventUI(Events.EventHandler eventHandler)
        {
            _wholeUI = new List<UserInterface>();

            _buttons = new UserInterface();

            //added in sorted fashion, top to bottom is front of the screen to back
            _wholeUI.Add(_buttons);
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
                Clickables.Decision choiceButton = new Clickables.Decision(i, newEvent.getChoices()[i]);

                _buttons.addClickableToFront(choiceButton); //order doesn't matter
            }
        }

        public void setupNewEvent(List<UserInterface> activeUI, Events.EventHandler eventHandler, Events.Happening newEvent)
        {
            setAsActiveUI(activeUI);

            initializeChoiceButtons(eventHandler, newEvent);
        }
    }
}
