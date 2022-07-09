using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    /// <summary>
    /// Base class to make card design easier for cards that both gain defense and draw
    /// some cards.
    /// </summary>
    abstract class DefenseAndDrawCard : BasicDefenseCard, IDrawCard
    {
        public DefenseAndDrawCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int defense, int drawAmount) : base(name, cardType, rarity, defense)
        {
            iDrawAmount = drawAmount;
        }

        public int iDrawAmount { get; }

        public override void onPlay()
        {
            iGainDefense();
            iCardDraw();
        }

        public void iCardDraw()
        {
            Game1.getChamp().getDeck().drawNumCards(iDrawAmount);
        }
    }
}
