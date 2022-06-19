using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Treasures
{
    class Gold : Treasure
    {
        int _amount;

        public Gold(int amount) : base(treasureType.money)
        {
            _amount = amount;

            _treasureText = _amount + " gold";
        }

        public override void onTaken()
        {
            Game1.getDungeonHandler().addGold(_amount);
            _taken = true;
            Game1.updateTopBar();
        }
    }
}
