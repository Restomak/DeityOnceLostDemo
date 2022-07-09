using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables
{
    /// <summary>
    /// Version of AestheticOnly that displays a card rather than a passed texture.
    /// </summary>
    class AestheticOnly_InspectedCard : AestheticOnly
    {
        DeckBuilder.Card _card;

        public AestheticOnly_InspectedCard(DeckBuilder.Card card, Point xy, int width, int height) : base(null, xy, width, height, Color.White)
        {
            _card = card;
        }

        public DeckBuilder.Card getCard()
        {
            return _card;
        }
    }
}
