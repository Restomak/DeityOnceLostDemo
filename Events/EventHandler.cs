using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events
{
    /// <summary>
    /// The game's event handler, which manages the setting up of an event, as well as the
    /// engine logic used throughout the event, as well as the conclusion of an event.
    /// </summary>
    public class EventHandler
    {
        Happening _event;
        UserInterface.EventUI _eventUI;
        Dungeon.Room _currentRoom;
        bool _eventFinished;
        bool _fromRoom;

        public EventHandler()
        {
            _eventUI = new UserInterface.EventUI(this);
            _eventFinished = false;
        }

        public void setCurrentRoom(Dungeon.Room currentRoom)
        {
            _currentRoom = currentRoom;
        }

        /// <summary>
        /// Called when a new event needs to be setup. Calls the EventUI's setup function as well.
        /// </summary>
        public void setupNewEvent(List<UserInterface.UserInterface> activeUI, Happening newEvent, bool fromRoom)
        {
            Game1.debugLog.Add("Setting up event");

            _event = newEvent;
            _fromRoom = fromRoom;

            _eventUI = new UserInterface.EventUI(this);

            _eventUI.setupNewEvent(activeUI, this, newEvent);
        }

        /// <summary>
        /// The main game loop called during events. In this case, it merely checks if the event
        /// has yet finished, and if so, concludes it and returns the game's control to the
        /// DungeonHandler.
        /// </summary>
        public void handleEventLogic()
        {
            if (_eventFinished) //used when an event brings up a menu before closing
            {
                if (_fromRoom)
                {
                    _currentRoom.finishTopContent();
                }
                Game1.returnToDungeon();
                _eventFinished = false; //just in case
            }
        }

        /// <summary>
        /// Called when the player has selected a choice. It determines what's stored in the
        /// choice; be it another event, a combat, loot, or otherwise, and sets up the engine
        /// to handle what comes next.
        /// </summary>
        public void choiceChosen(int chosenNum)
        {
            _event.getChoices()[chosenNum].onChoose();

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
                if (_fromRoom)
                {
                    _currentRoom.finishTopContent();
                }
                Game1.returnToDungeon();
            }

            if (loot != null) //done separately since loot can happen simultaneously as one of the other two
            {
                //Check if it's an entire loot window or just one thing (eg. add/remove a card, or gain a relic/curse, etc)
                if (loot.getTreasures().Count == 1)
                {
                    if (loot.getTreasures()[0].GetType() == typeof(Treasury.Treasures.AddCardToDeck))
                    {
                        Game1.addToMenus(new UserInterface.Menus.NewCardChoiceMenu(((Treasury.Treasures.AddCardToDeck)loot.getTreasures()[0]).getChoices(),
                            (Treasury.Treasures.AddCardToDeck)loot.getTreasures()[0], () => { }));
                        if (nextEvent == null && nextCombat == null)
                        {
                            _eventFinished = true;
                        }
                        return;
                    }
                    else if (loot.getTreasures()[0].GetType() == typeof(Treasury.Treasures.RemoveCardFromDeck))
                    {
                        Game1.addToMenus(new UserInterface.Menus.RemoveCardChoiceMenu((Treasury.Treasures.RemoveCardFromDeck)loot.getTreasures()[0]));
                        if (nextEvent == null && nextCombat == null)
                        {
                            _eventFinished = true;
                        }
                        return;
                    }
                    else if (loot.getTreasures()[0].GetType().IsSubclassOf(typeof(Treasury.Treasures.Relic)))
                    {
                        Game1.getDungeonHandler().addRelic((Treasury.Treasures.Relic)loot.getTreasures()[0]);
                        if (nextEvent == null && nextCombat == null)
                        {
                            _eventFinished = true;
                        }
                        return;
                    }
                }

                Game1.addToMenus(new UserInterface.Menus.LootMenu(loot, UserInterface.Menus.LootMenu.CHEST_LOOT)); //FIXIT make an event loot constant?
                if (nextEvent == null && nextCombat == null)
                {
                    _eventFinished = true;
                }
            }
        }



        /// <summary>
        /// Keyboard control for pressing continue (default: space bar). If the current event
        /// only has one possible choice, it moves the engine along to that choice's result
        /// (as if the player had clicked on the choice normally).
        /// </summary>
        public void continuePressed()
        {
            if (_event.getChoices().Count == 1)
            {
                choiceChosen(0);

                Game1.setHoveredClickable(null);
            }
        }
    }
}
