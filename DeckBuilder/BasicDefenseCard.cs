using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    abstract class BasicDefenseCard : Card, IDefendingCard
    {
        protected int _defense;

        public BasicDefenseCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int defense) : base(name, cardType, rarity, CardEnums.TargetingType.champion)
        {
            _defense = defense;
        }

        public int defense
        {
            get => _defense;
        }

        public override void onPlay()
        {
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

            desc.Add("Gain " + defense + " defense.");

            return desc;
        }
    }
}
