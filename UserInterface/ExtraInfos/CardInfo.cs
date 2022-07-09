using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.ExtraInfos
{
    /// <summary>
    /// Used when hovering over a Clickable needs to display a card.
    /// </summary>
    public class CardInfo : ExtraInfo
    {
        DeckBuilder.Card _card;

        public CardInfo(DeckBuilder.Card card)
        {
            _card = card;
        }

        public DeckBuilder.Card getCard()
        {
            return _card;
        }
    }
}
