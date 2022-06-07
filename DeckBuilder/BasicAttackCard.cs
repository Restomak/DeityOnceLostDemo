using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    abstract class BasicAttackCard : Card, IDamagingCard
    {
        protected int _damage;

        public BasicAttackCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int damage) : base(name, cardType, rarity, CardEnums.TargetingType.enemies)
        {
            _damage = damage;
        }

        public int damage
        {
            get => _damage;
        }

        public override void onPlay()
        {
            dealDamage();
        }

        public void dealDamage()
        {
            if (_target != null)
            {
                _target.takeDamage(_damage);
            }
            else
            {
                Game1.errorLog.Add("Attempted to use damage card without a target!");
            }
        }

        public override List<String> getDescription(Characters.Champion champ)
        {
            List<String> desc = new List<string>();
            int damage = champ.getStrength() + _damage;

            desc.Add("Deal " + damage + " damage.");

            return desc;
        }
    }
}
