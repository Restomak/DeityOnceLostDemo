using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    abstract class BasicAttackCard : Card, ITargetingCard, IDamagingCard
    {
        protected int _damage;
        Combat.Unit _target;

        public BasicAttackCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int damage) : base(name, cardType, rarity)
        {
            _damage = damage;
        }

        public int damage
        {
            get => _damage;
        }

        public Combat.Unit target
        {
            get => _target;
            set => _target = value;
        }

        public override bool onPlay()
        {
            return dealDamage();
        }

        public void selectTarget()
        {
            //FIXIT: call on input bs for setting a target
        }

        public bool dealDamage()
        {
            if (_target != null)
            {
                //FIXIT: make the champion deal damage to the selected target
                return true;
            }
            else
            {
                return false;
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
