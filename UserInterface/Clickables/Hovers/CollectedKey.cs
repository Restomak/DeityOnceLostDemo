using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables.Hovers
{
    class CollectedKey : HoverInfo
    {
        Treasury.Equipment.Key _key;

        public CollectedKey(Treasury.Equipment.Key key, Point xy) : base(xy, Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE, Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE,
            new List<String>())
        {
            _key = key;

            _description.Add("The  " + key.getKeyColor().ToString() + " key for this floor.");
            _description.Add("With it, all " + key.getKeyColor().ToString() + " doors are");
            _description.Add("now unlocked.");
        }

        public Treasury.Equipment.Key getKey()
        {
            return _key;
        }
    }
}
