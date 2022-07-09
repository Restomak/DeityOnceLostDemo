using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    /// <summary>
    /// Base class to make card design easier for cards that both deal damage to an enemy
    /// and draw more cards.
    /// </summary>
    abstract class AttackAndDrawCard : BasicAttackCard, IDrawCard
    {
        public AttackAndDrawCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int damage, int drawAmount) : base(name, cardType, rarity, damage)
        {
            iDrawAmount = drawAmount;
        }

        public int iDrawAmount { get; }

        public override void onPlay()
        {
            iDealDamage();
            iCardDraw();
        }

        public void iCardDraw()
        {
            Game1.getChamp().getDeck().drawNumCards(iDrawAmount);
        }
    }
}
