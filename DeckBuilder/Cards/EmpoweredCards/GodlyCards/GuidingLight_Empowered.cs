using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.GodlyCards
{
    /// <summary>
    /// Guiding Light | Godly
    ///     (Costs 0 Divinity)
    ///     (Empowered)
    /// Heal 3 HP.
    /// 2 random cards in your hand cost 0 Divinity for the combat.
    /// Dissipates.
    /// </summary>
    class GuidingLight_Empowered : RegularCards.GodlyCards.GuidingLight
    {
        public const int EMPOWERED_DIV_COST = 0;

        public GuidingLight_Empowered() : base(EMPOWERED_DIV_COST)
        {

        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override Card getNewCard()
        {
            return new GuidingLight_Empowered();
        }
    }
}
