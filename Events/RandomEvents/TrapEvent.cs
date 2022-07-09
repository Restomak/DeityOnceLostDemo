using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events.RandomEvents
{
    /// <summary>
    /// A variant of event that generally has several choices with negative effects for the
    /// player to choose between, and possibly an out or two depending on the event. These
    /// events are stored as TrapEvents so that dungeons can generate random ones and still
    /// ensure this specific type of event is chosen in specific locations.
    /// </summary>
    public abstract class TrapEvent : RandomEvent
    {
        public const int WEIGHT_TRIPWIRE_BLADED_HALLWAY = 10;

        public TrapEvent(List<String> writing, int eventWeight) : base(writing, eventWeight)
        {

        }
    }
}
