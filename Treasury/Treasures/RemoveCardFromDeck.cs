using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Treasures
{
    class RemoveCardFromDeck : Treasure
    {
        public RemoveCardFromDeck() : base(treasureType.removeCard)
        {
            _treasureText = "Remove a card from your deck";
        }

        public override void onTaken()
        {
            Game1.addToMenus(new UserInterface.Menus.RemoveCardChoiceMenu(this));
        }

        public void setTaken()
        {
            _taken = true;
        }
    }
}
