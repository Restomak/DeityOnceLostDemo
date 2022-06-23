using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    class BasicMultiAttackCard : BasicAttackCard, IMultiAttack
    {
        protected int _numHits;

        public BasicMultiAttackCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int damage, int numHits) : base(name, cardType, rarity, damage)
        {
            _numHits = numHits;
        }

        public int numHits
        {
            get => _numHits;
        }

        public override void onPlay()
        {
            dealDamage();
        }

        public override void dealDamage()
        {
            if (_target != null)
            {
                for (int i = 0; i < _numHits; i++)
                {
                    _target.takeDamage(Game1.getChamp().getDamageAffectedByBuffs(_damage, _target));
                }
            }
            else
            {
                Game1.errorLog.Add("Attempted to use damage card without a target!");
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

            desc.Add("Deal " + damage + " damage " + numHits);
            desc.Add("times.");

            return desc;
        }
    }
}
