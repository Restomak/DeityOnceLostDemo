using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    /// <summary>
    /// Base class to make card design easier for cards that both gain defense and deal damage
    /// to an enemy.
    /// </summary>
    abstract class AttackAndDefenseCard : BasicAttackCard, IDefendingCard
    {
        public AttackAndDefenseCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int damage, int defense) : base(name, cardType, rarity, damage)
        {
            iDefense = defense;
        }

        public int iDefense { get; }

        public override void onPlay()
        {
            iDealDamage();
            iGainDefense();
        }

        public void iGainDefense()
        {
            Game1.getChamp().gainDefense(iDefense);
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(getDefenseExtraInfo());

            return extraInfo;
        }
    }
}
