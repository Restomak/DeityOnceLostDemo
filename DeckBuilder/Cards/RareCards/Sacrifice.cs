using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RareCards
{
    class Sacrifice : Card, IDrawCard, IGainDivCard //don't need IDissipateCard since we're directly using Card's constructor
    {
        public const String NAME = "Sacrifice";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.RARE;
        public const int PLAYCOST_BLOOD = 3;
        public const int DIVINITY_GAIN = 3;
        public const int CARD_DRAW = 1;

        public Sacrifice() : base(NAME, CARDTYPE, RARITY, CardEnums.TargetingType.champion, true)
        {
            addPlayCost(CardEnums.CostType.BLOOD, PLAYCOST_BLOOD);
        }

        public int amount
        {
            get => CARD_DRAW;
        }

        public int divGain
        {
            get => DIVINITY_GAIN;
        }

        public override void onPlay()
        {
            gainDivinity();
            cardDraw();
        }
        
        public void gainDivinity()
        {
            Game1.getChamp().spendDivinity(-DIVINITY_GAIN);
        }

        public void cardDraw()
        {
            Game1.getChamp().getDeck().drawNumCards(CARD_DRAW);
        }
        
        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            desc.Add("Gain " + DIVINITY_GAIN + " Divinity.");
            desc.Add("Draw " + CARD_DRAW + " card.");
            desc.Add("Dissipates.");

            return desc;
        }
    }
}
