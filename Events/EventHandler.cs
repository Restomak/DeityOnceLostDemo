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
        bool eventFinished;

        public EventHandler()
        {
            _eventUI = new UserInterface.EventUI(this);
            eventFinished = false;
        }

        public void setCurrentRoom(Dungeon.Room currentRoom)
        {
            _currentRoom = currentRoom;
        }

        public void setupNewEvent(List<UserInterface.UserInterface> activeUI, Happening newEvent)
        {
            Game1.debugLog.Add("Setting up event");

            _event = newEvent;

            _eventUI = new UserInterface.EventUI(this);

            _eventUI.setupNewEvent(activeUI, this, newEvent);
        }

        public void handleEventLogic()
        {
            if (eventFinished) //used when an event brings up a menu before closing
            {
                _currentRoom.finishTopContent();
                Game1.returnToDungeon();
                eventFinished = false; //just in case
            }
        }

        public void choiceChosen(int chosenNum)
        {
            Happening nextEvent = _event.getChoices()[chosenNum].getResultingEvent();
            Combat.Encounter nextCombat = _event.getChoices()[chosenNum].getResultingEncounter();
            Treasury.Loot loot = _event.getChoices()[chosenNum].getResultingLoot();

            if (nextEvent != null)
            {
                Game1.startEvent(nextEvent);
            }
            else if (nextCombat != null)
            {
                Game1.enterNewCombat(nextCombat, _currentRoom);
            }
            else if (loot == null) //all three are null
            {
                _currentRoom.finishTopContent();
                Game1.returnToDungeon();
            }

            if (loot != null) //done separately since loot can happen simultaneously as one of the other two
            {
                //Check if it's an entire loot window or just a new card to deck
                if (loot.getTreasures().Count == 1)
                {
                    if (loot.getTreasures()[0].GetType() == typeof(Treasury.Treasures.AddCardToDeck))
                    {
                        Game1.addToMenus(new UserInterface.Menus.NewCardChoiceMenu(((Treasury.Treasures.AddCardToDeck)loot.getTreasures()[0]).getChoices(), (Treasury.Treasures.AddCardToDeck)loot.getTreasures()[0]));
                        if (nextEvent == null && nextCombat == null)
                        {
                            eventFinished = true;
                        }
                        return;
                    }
                    else if (loot.getTreasures()[0].GetType() == typeof(Treasury.Treasures.RemoveCardFromDeck))
                    {
                        Game1.addToMenus(new UserInterface.Menus.RemoveCardChoiceMenu((Treasury.Treasures.RemoveCardFromDeck)loot.getTreasures()[0]));
                        if (nextEvent == null && nextCombat == null)
                        {
                            eventFinished = true;
                        }
                        return;
                    }
                }

                Game1.addToMenus(new UserInterface.Menus.LootMenu(loot, UserInterface.Menus.LootMenu.CHEST_LOOT)); //FIXIT make an event loot constant?
                if (nextEvent == null && nextCombat == null)
                {
                    eventFinished = true;
                }
            }
        }
    }
}
