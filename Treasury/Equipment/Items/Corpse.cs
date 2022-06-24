using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Equipment.Items
{
    public class Corpse : Item
    {
        public const int WIDTH = 3;
        public const int HEIGHT = 2;

        public Corpse() : base(Game1.pic_functionality_bar, WIDTH, HEIGHT, false, false) //FIXIT get real texture
        {

        }

        public override void onUse()
        {
            //Cannot use
        }
    }
}
