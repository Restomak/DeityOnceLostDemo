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

        public BasicDefenseCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int defense) : base(name, cardType, rarity)
        {
            _defense = defense;
        }

        public int defense
        {
            get => _defense;
        }

        public override bool onPlay()
        {
            return gainDefense();
        }

        public bool gainDefense()
        {
            //FIXIT: make the champion gain defense
            return true;
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
