using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Dungeon.Locations
{
    public class FirstDungeon : Dungeon
    {
        public FirstDungeon()
        {
            _floors.Add(new Floors.FirstDungeon_Beginning());
        }
    }
}
