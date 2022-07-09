using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    /// <summary>
    /// Base class to make card design easier for cards that attack multiple times.
    /// </summary>
    abstract class BasicMultiAttackCard : BasicAttackCard, IMultiAttack
    {
        public BasicMultiAttackCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int damage, int numHits) : base(name, cardType, rarity, damage)
        {
            iNumHits = numHits;
        }

        public int iNumHits { get; }

        public override void onPlay()
        {
            iDealDamage();
        }

        public override void iDealDamage()
        {
            if (_target != null)
            {
                for (int i = 0; i < iNumHits; i++)
                {
                    _target.takeDamage(Game1.getChamp().getDamageAffectedByBuffs(iDamage, _target));
                }
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
