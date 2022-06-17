using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Rooms
{
    public class MultiContentRoom : Room
    {
        List<Events.Happening> _roomEvents;
        List<Combat.Encounter> _roomEncounters;
        //FIXIT add list for treasure stuff

        public MultiContentRoom()
        {
            _roomEvents = new List<Events.Happening>();
            _roomEncounters = new List<Combat.Encounter>();
        }

        //Getters
        public override Combat.Encounter getRoomCombat()
        {
            if (_roomEncounters.Count > 0)
            {
                return _roomEncounters[0];
            }

            return null;
        }
        public override Events.Happening getRoomEvent()
        {
            if (_roomEvents.Count > 0)
            {
                return _roomEvents[0];
            }

            return null;
        }

        //Setters
        public void addRoomEvent(Events.Happening roomEvent, bool story)
        {
            _roomEvents.Add(roomEvent);

            if (story)
            {
                _roomContents.Add(roomContents.story);
            }
            else
            {
                _roomContents.Add(roomContents.happening);
            }
        }
        public void addRoomEncounter(Combat.Encounter roomEncounter, bool boss)
        {
            _roomEncounters.Add(roomEncounter);

            if (boss)
            {
                _roomContents.Add(roomContents.boss);
            }
            else
            {
                _roomContents.Add(roomContents.combat);
            }
        }

        /// <summary>
        /// Overrides the base function because it needs to also remove from the lists of encounters or events
        /// </summary>
        public override void finishTopContent()
        {
            if (_roomContents.Count > 0)
            {
                switch (_roomContents[0])
                {
                    case roomContents.combat:
                        _roomEncounters.RemoveAt(0);
                        break;
                    case roomContents.happening:
                    case roomContents.story:
                        _roomEvents.RemoveAt(0);
                        break;
                    case roomContents.treasure:
                        //FIXIT implement
                        break;
                    default:
                        Game1.errorLog.Add("Attempted to remove the top of _roomContents but the top of the list doesn't match what's possible in a MultiContentRoom: " + _roomContents[0].ToString());
                        break;
                }

                _roomContents.RemoveAt(0);
            }
            else
            {
                Game1.errorLog.Add("Attempted to remove the top of _roomContents but there was nothing in the list");
            }
        }
    }
}
