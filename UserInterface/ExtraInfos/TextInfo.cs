using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.ExtraInfos
{
    /// <summary>
    /// Used when hovering over a Clickable needs to display only text.
    /// </summary>
    public class TextInfo : ExtraInfo
    {
        protected List<String> _description;

        public TextInfo(List<String> description)
        {
            _description = description;
        }

        public List<String> getDescription()
        {
            return _description;
        }
    }
}
