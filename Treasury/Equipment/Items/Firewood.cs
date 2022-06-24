using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Equipment.Items
{
    public class Firewood : Item
    {
        public const int WIDTH = 2;
        public const int HEIGHT = 1;

        public Firewood() : base(Game1.pic_functionality_bar, WIDTH, HEIGHT, true, false) //FIXIT get real texture
        {

        }

        public override void onUse()
        {

        }
    }
}
