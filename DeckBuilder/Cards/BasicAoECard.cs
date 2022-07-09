using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    /// <summary>
    /// Base class to make card design easier for cards that deal damage to all enemies.
    /// </summary>
    abstract class BasicAoECard : Card, IDamagingCard
    {
        public BasicAoECard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int damage) : base(name, cardType, rarity, CardEnums.TargetingType.aoeEnemies)
        {
            iDamage = damage;
        }

        public int iDamage { get; }

        public override void onPlay()
        {
            iDealDamage();
        }

        public void iDealDamage()
        {
            List<Combat.Enemy> enemies = Game1.getCombatHandler().getCurrentEncounter().getEnemies();

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].takeDamage(Game1.getChamp().getDamageAffectedByBuffs(iDamage, enemies[i]));
            }
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            return null;
        }
    }
}
