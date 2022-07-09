using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.RareCards
{
    /// <summary>
    /// Quake Stomp | Rare
    ///     (Costs 2 Divinity)
    ///     (Empowered)
    /// Deal 17 damage.
    /// Choose up to 3 cards to discard from your draw pile and
    ///     then draw 2 cards.
    /// </summary>
    class QuakeStomp_Empowered : RegularCards.RareCards.QuakeStomp
    {
        public const int EMPOWERED_DAMAGE = 17;
        public const int EMPOWERED_CARD_DRAW = 2;

        public QuakeStomp_Empowered() : base(EMPOWERED_DAMAGE, EMPOWERED_CARD_DRAW)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new QuakeStomp_Empowered();
        }
    }
}
