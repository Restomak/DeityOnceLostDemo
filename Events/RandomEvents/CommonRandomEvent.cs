using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events.RandomEvents
{
    public abstract class CommonRandomEvent : RandomEvent
    {
        public const int DEFAULT_WEIGHT_COMMON = 10;

        public CommonRandomEvent(List<String> writing, int eventWeight = DEFAULT_WEIGHT_COMMON) : base(writing, eventWeight)
        {

        }
    }
}
