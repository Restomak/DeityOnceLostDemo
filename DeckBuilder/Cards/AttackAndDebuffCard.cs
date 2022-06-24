using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    class AttackAndDebuffCard : BasicDebuffCard, IDamagingCard
    {
        protected int _damage;

        public AttackAndDebuffCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int damage,
            Combat.Buff.buffType buffType, int duration, int amount, bool hasDuration, bool hasAmount, bool decreasesEachTurn = true) :
            base(name, cardType, rarity, buffType, duration, amount, hasDuration, hasAmount)
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
            applyDebuff();
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
            List<String> desc = base.getDescription(champ);

            Combat.Unit descTarget = null;
            if (activeCard)
            {
                descTarget = Card.getTargetForDescription(_targetType);
            }
            int damage = champ.getDamageAffectedByBuffs(_damage, descTarget);

            desc.Insert(0, "Deal " + damage + " damage."); //put it in front

            return desc;
        }
    }
}
