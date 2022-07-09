using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events.RandomEvents
{
    /// <summary>
    /// The most common variant of RandomEvent, though the randomization of them is not
    /// yet implemented as currently the demo only includes the first three floors of the
    /// game's tutorial dungeon.
    /// </summary>
    public abstract class CommonRandomEvent : RandomEvent
    {
        public const int DEFAULT_WEIGHT_COMMON = 10;

        public CommonRandomEvent(List<String> writing, int eventWeight = DEFAULT_WEIGHT_COMMON) : base(writing, eventWeight)
        {

        }
    }
}
