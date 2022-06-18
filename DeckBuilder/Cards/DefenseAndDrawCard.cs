using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    class DefenseAndDrawCard : BasicDefenseCard, IDrawCard
    {
        protected int _drawAmount;

        public DefenseAndDrawCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int defense, int drawAmount) : base(name, cardType, rarity, defense)
        {
            _drawAmount = drawAmount;
        }

        public int amount
        {
            get => _drawAmount;
        }

        public override void onPlay()
        {
            gainDefense();
            cardDraw();
        }

        public void cardDraw()
        {
            Game1.getChamp().getDeck().drawNumCards(_drawAmount);
        }

        public override List<String> getDescription(Characters.Champion champ)
        {
            List<String> desc = new List<string>();
            int defense = champ.getDexterity() + _defense;
            String drawCardString = "Draw " + _drawAmount + " card";
            if (_drawAmount > 1)
            {
                drawCardString += "s";
            }
            drawCardString += ".";
            
            desc.Add("Gain " + defense + " defense.");
            desc.Add(drawCardString);

            return desc;
        }
    }
}
