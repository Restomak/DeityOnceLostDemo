using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    /// <summary>
    /// Base class to make card design easier for cards that draw more cards.
    /// </summary>
    abstract class BasicDrawCard : Card, IDrawCard
    {
        public BasicDrawCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int drawAmount) : base(name, cardType, rarity, CardEnums.TargetingType.champion)
        {
            iDrawAmount = drawAmount;
        }

        public int iDrawAmount { get; }

        public override void onPlay()
        {
            iCardDraw();
        }

        public void iCardDraw()
        {
            Game1.getChamp().getDeck().drawNumCards(iDrawAmount);
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            return null;
        }
    }
}
