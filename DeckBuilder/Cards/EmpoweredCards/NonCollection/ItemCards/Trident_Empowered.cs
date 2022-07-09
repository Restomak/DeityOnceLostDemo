using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.NonCollection.ItemCards
{
    /// <summary>
    /// Trident | Default (item card)
    ///     (Costs 1 Divinity)
    ///     (Empowered)
    /// Deal 12 damage.
    /// Gain 4 Strength.
    /// Dissipates.
    /// </summary>
    class Trident_Empowered : RegularCards.NonCollection.ItemCards.Trident
    {
        public const int EMPOWERED_STRENGTH_AMOUNT = 4;

        public Trident_Empowered() : base(EMPOWERED_STRENGTH_AMOUNT)
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new Trident_Empowered();
        }
    }
}
