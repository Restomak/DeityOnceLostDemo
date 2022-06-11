using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events
{
    public class EventHandler
    {
        Happening _event;
        UserInterface.EventUI _eventUI;
        Dungeon.Room _currentRoom;

        public EventHandler()
        {
            _eventUI = new UserInterface.EventUI(this);
        }

        public void setCurrentRoom(Dungeon.Room currentRoom)
        {
            _currentRoom = currentRoom;
        }

        public void setupNewEvent(List<UserInterface.UserInterface> activeUI, Happening newEvent)
        {
            Game1.debugLog.Add("Setting up event");

            _event = newEvent;

            _eventUI.setupNewEvent(activeUI, this, newEvent);
        }

        public void handleEventLogic()
        {
            //Not sure if I actually need to put anything here; it's all UI-based
        }

        public void choiceChosen(int chosenNum)
        {
            Happening nextEvent = _event.getChoices()[chosenNum].getResult();
            if (nextEvent != null)
            {
                Game1.startEvent(nextEvent);
            }
            else
            {
                _currentRoom.finishTopContent();
                Game1.returnToDungeon();
            }
        }
    }
}
