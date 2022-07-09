using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    /// <summary>
    /// Base class to make card design easier for cards that both deal damage to an enemy
    /// and apply a debuff when doing so.
    /// </summary>
    abstract class AttackAndDebuffCard : BasicDebuffCard, IDamagingCard
    {
        public AttackAndDebuffCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int damage,
            Combat.Buff.buffType buffType, int duration, int amount, bool hasDuration, bool hasAmount, bool decreasesEachTurn = true) :
            base(name, cardType, rarity, buffType, duration, amount, hasDuration, hasAmount)
        {
            iDamage = damage;
        }

        public int iDamage { get; }

        public override void onPlay()
        {
            iDealDamage();
            iApplyDebuff();
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
    }
}
