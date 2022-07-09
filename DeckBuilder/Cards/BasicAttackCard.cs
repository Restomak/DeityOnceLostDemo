using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    /// <summary>
    /// Base class to make card design easier for cards that deal damage to an enemy.
    /// </summary>
    abstract class BasicAttackCard : Card, IDamagingCard
    {
        public BasicAttackCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int damage) : base(name, cardType, rarity, CardEnums.TargetingType.enemies)
        {
            iDamage = damage;
        }

        public int iDamage { get; }

        public override void onPlay()
        {
            iDealDamage();
        }

        public virtual void iDealDamage()
        {
            if (_target != null)
            {
                _target.takeDamage(Game1.getChamp().getDamageAffectedByBuffs(iDamage, _target));
            }
            else
            {
                Game1.addToErrorLog("Attempted to use damage card without a target!");
            }
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            return null;
        }
    }
}
