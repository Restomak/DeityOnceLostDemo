using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events.RandomEvents
{
    /// <summary>
    /// A generic variant of event that includes many different types of events that
    /// can be randomly selected by a dungeon for specific locations.
    /// </summary>
    public abstract class RandomEvent : Happening
    {
        int _eventWeight;

        public RandomEvent(List<String> writing, int eventWeight) : base (writing)
        {
            _eventWeight = eventWeight;
        }

        public int getWeight()
        {
            return _eventWeight;
        }
    }
}
