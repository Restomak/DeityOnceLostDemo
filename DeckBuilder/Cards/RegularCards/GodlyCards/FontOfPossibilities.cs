using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.GodlyCards
{
    /// <summary>
    /// Font of Possibilities | Godly
    ///     (Costs 1 Divinity)
    /// Draw 4 cards.
    /// Empower 2 of the cards in your hand.
    /// </summary>
    class FontOfPossibilities : BasicDrawCard, IHandEmpower
    {
        public const String NAME = "Font of Possibilities";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.GODLY;
        public const int PLAYCOST_DIV = 1;
        public const int CARD_DRAW = 4;
        public const int EMPOWER_AMOUNT = 2;

        public FontOfPossibilities(int playCostDiv = PLAYCOST_DIV) : base(NAME, CARDTYPE, RARITY, CARD_DRAW)
        {
            iNumCards = EMPOWER_AMOUNT;

            addPlayCost(CardEnums.CostType.DIVINITY, playCostDiv);
            addBaseCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }

        public int iNumCards { get; }

        public override void onPlay()
        {
            iCardDraw();
            iHandEmpower();
        }

        public void iHandEmpower()
        {
            int numCardsToEmpower = iNumCards;
            int emergencyExitCounter = 0;
            int randomIndex = 0;
            int cardReplaceIndex = 0;

            List<Card> empowerPossibilities = new List<Card>();
            List<Card> cardsInHand = Game1.getChamp().getDeck().getHand();
            for (int i = 0; i < cardsInHand.Count; i++)
            {
                if (!cardsInHand[i].isEmpowered())
                {
                    empowerPossibilities.Add(cardsInHand[i]);
                }
            }

            while (empowerPossibilities.Count > 0 && numCardsToEmpower > 0 && emergencyExitCounter < Deck.EMERGENCY_SHUFFLE_LOOP_BREAK_COUNT)
            {
                randomIndex = Game1.randint(0, empowerPossibilities.Count - 1);
                cardReplaceIndex = Game1.getChamp().getDeck().getHand().IndexOf(empowerPossibilities[randomIndex]);
                Game1.getChamp().getDeck().getHand().RemoveAt(cardReplaceIndex);
                Game1.getChamp().getDeck().getHand().Insert(cardReplaceIndex, empowerPossibilities[randomIndex].getEmpoweredCard());
                empowerPossibilities.RemoveAt(randomIndex);

                numCardsToEmpower--;
                emergencyExitCounter++;
            }
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            String drawCardString = "Draw " + iDrawAmount + " card";
            if (iDrawAmount > 1)
            {
                drawCardString += "s";
            }
            drawCardString += ".";

            desc.Add(drawCardString);
            desc.Add("Empower " + iNumCards + " of the");
            desc.Add("cards in your hand.");

            return desc;
        }

        public override List<String> getStylizedDescription(Characters.Champion champ, int descFontSize, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            desc.Add(CardStylizing.basicCardDrawString(CARD_DRAW, iDrawAmount, descFontSize));
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]Empower " + iNumCards + " of the");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]cards in your hand.");

            return desc;
        }

        public override Card getNewCard()
        {
            return new FontOfPossibilities();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.GodlyCards.FontOfPossibilities_Empowered();
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(getEmpowerExtraInfo());

            return extraInfo;
        }
    }
}
