using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    class AttackAndDefenseCard : BasicAttackCard, IDefendingCard
    {
        protected int _defense;

        public AttackAndDefenseCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int damage, int defense) : base(name, cardType, rarity, damage)
        {
            _defense = defense;
        }

        public int defense
        {
            get => _defense;
        }

        public override void onPlay()
        {
            dealDamage();
            gainDefense();
        }

        public void gainDefense()
        {
            Game1.getChamp().gainDefense(_defense);
        }

        public override List<String> getDescription(Characters.Champion champ)
        {
            List<String> desc = new List<string>();
            int defense = champ.getDexterity() + _defense;
            int damage = champ.getStrength() + _damage;

            desc.Add("Deal " + damage + " damage.");
            desc.Add("Gain " + defense + " defense.");

            return desc;
        }
    }
}
