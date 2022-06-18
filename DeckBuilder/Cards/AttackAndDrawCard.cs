using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    class AttackAndDrawCard : BasicAttackCard, IDrawCard
    {
        protected int _drawAmount;

        public AttackAndDrawCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int damage, int drawAmount) : base(name, cardType, rarity, damage)
        {
            _drawAmount = drawAmount;
        }

        public int amount
        {
            get => _drawAmount;
        }

        public override void onPlay()
        {
            dealDamage();
            cardDraw();
        }

        public void cardDraw()
        {
            Game1.getChamp().getDeck().drawNumCards(_drawAmount);
        }

        public override List<String> getDescription(Characters.Champion champ)
        {
            List<String> desc = new List<string>();
            int damage = champ.getStrength() + _damage;
            String drawCardString = "Draw " + _drawAmount + " card";
            if (_drawAmount > 1)
            {
                drawCardString += "s";
            }
            drawCardString += ".";

            desc.Add("Deal " + damage + " damage.");
            desc.Add(drawCardString);

            return desc;
        }
    }
}
