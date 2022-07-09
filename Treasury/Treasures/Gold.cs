using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Treasures
{
    /// <summary>
    /// The type of treasure that represents gold, the standard in-game currency used
    /// for many things. Currently tracked by the DungeonHandler, I'm considering
    /// making it an inventory item that stacks to a certain amount, similarly to how
    /// it is done in Darkest Dungeon.
    /// </summary>
    public class Gold : Treasure
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

        public int getAmount()
        {
            return _amount;
        }
    }
}
