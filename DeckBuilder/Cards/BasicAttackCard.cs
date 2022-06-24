using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
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

        public virtual void dealDamage()
        {
            if (_target != null)
            {
                _target.takeDamage(Game1.getChamp().getDamageAffectedByBuffs(_damage, _target));
            }
            else
            {
                Game1.addToErrorLog("Attempted to use damage card without a target!");
            }
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<string>();
            Combat.Unit descTarget = null;
            if (activeCard)
            {
                descTarget = Card.getTargetForDescription(_targetType);
            }
            int damage = champ.getDamageAffectedByBuffs(_damage, descTarget);

            desc.Add("Deal " + damage + " damage.");

            return desc;
        }
    }
}
