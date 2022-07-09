using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.RareCards
{
    /// <summary>
    /// Sacrifice | Rare
    ///     (Costs 3 Blood)
    /// Gain 3 Divinity.
    /// Draw 2 cards.
    /// </summary>
    class Sacrifice : Card, IDrawCard, IGainDivCard
    {
        public const String NAME = "Sacrifice";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.RARE;
        public const int PLAYCOST_BLOOD = 3;
        public const int DIVINITY_GAIN = 3;
        public const int CARD_DRAW = 2;

        public Sacrifice(int cardDraw = CARD_DRAW) : base(NAME, CARDTYPE, RARITY, CardEnums.TargetingType.champion, true)
        {
            iDrawAmount = cardDraw;
            iDivGain = DIVINITY_GAIN;

            addPlayCost(CardEnums.CostType.BLOOD, PLAYCOST_BLOOD);
        }

        public int iDrawAmount { get; }

        public int iDivGain { get; }

        public override void onPlay()
        {
            iGainDivinity();
            iCardDraw();
        }
        
        public void iGainDivinity()
        {
            Game1.getChamp().spendDivinity(-iDivGain);
        }

        public void iCardDraw()
        {
            Game1.getChamp().getDeck().drawNumCards(iDrawAmount);
        }
        
        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            desc.Add("Gain " + iDivGain + " Divinity.");
            desc.Add("Draw " + iDrawAmount + " cards.");

            return desc;
        }

        public override List<String> getStylizedDescription(Characters.Champion champ, int descFontSize, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]Gain " + iDivGain + " Divinity.");
            desc.Add(CardStylizing.basicCardDrawString(CARD_DRAW, iDrawAmount, descFontSize));

            return desc;
        }

        public override Card getNewCard()
        {
            return new Sacrifice();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.RareCards.Sacrifice_Empowered();
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(getBloodExtraInfo());
            extraInfo.Add(getDivinityExtraInfo());

            return extraInfo;
        }
    }
}
