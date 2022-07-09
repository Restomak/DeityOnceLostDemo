using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.GodlyCards
{
    /// <summary>
    /// Guiding Light | Godly
    ///     (Costs 1 Divinity)
    /// Heal 3 HP.
    /// 2 random cards in your hand cost 0 Divinity for the combat.
    /// Dissipates.
    /// </summary>
    class GuidingLight : Card
    {
        public const String NAME = "Guiding Light";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.GODLY;
        public const int PLAYCOST_DIV = 1;
        public const int HEAL_AMOUNT = 3;
        public const int NUM_CARDS_TO_COST_0 = 2;

        public GuidingLight(int playCostDiv = PLAYCOST_DIV) : base(NAME, CARDTYPE, RARITY, CardEnums.TargetingType.champion, true)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, playCostDiv);
            addBaseCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
            _dissipates = true;
        }

        public override void onPlay()
        {
            healChampion();
            makeOtherCardsFree();
        }

        protected void healChampion()
        {
            Game1.getChamp().heal(HEAL_AMOUNT);
        }

        protected void makeOtherCardsFree()
        {
            int numCardsToMakeFree = NUM_CARDS_TO_COST_0;
            int emergencyExitCounter = 0;
            int randomIndex = 0;
            int changeIndex = 0;

            List<Card> freeCardPossibilities = new List<Card>();
            List<Card> cardsInHand = Game1.getChamp().getDeck().getHand();
            for (int i = 0; i < cardsInHand.Count; i++)
            {
                if (cardsInHand[i].getPlayCost(CardEnums.CostType.DIVINITY) != 0)
                {
                    freeCardPossibilities.Add(cardsInHand[i]);
                }
            }

            while (freeCardPossibilities.Count > 0 && numCardsToMakeFree > 0 && emergencyExitCounter < Deck.EMERGENCY_SHUFFLE_LOOP_BREAK_COUNT)
            {
                randomIndex = Game1.randint(0, freeCardPossibilities.Count - 1);
                changeIndex = Game1.getChamp().getDeck().getHand().IndexOf(freeCardPossibilities[randomIndex]);
                Game1.getChamp().getDeck().getHand()[changeIndex].changeCost(CardEnums.CostType.DIVINITY, 0);
                freeCardPossibilities.RemoveAt(randomIndex);

                numCardsToMakeFree--;
                emergencyExitCounter++;
            }
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            desc.Add("Heal " + HEAL_AMOUNT + " HP.");
            desc.Add(NUM_CARDS_TO_COST_0 + " random cards in");
            desc.Add("your hand cost 0");
            desc.Add("Divinity for the combat.");
            desc.Add("Dissipates.");

            return desc;
        }

        public override List<String> getStylizedDescription(Characters.Champion champ, int descFontSize, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]Heal " + HEAL_AMOUNT + " HP.");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]" + NUM_CARDS_TO_COST_0 + " random cards in");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]your hand cost 0");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]Divinity for the combat.");
            desc.Add(CardStylizing.dissipates(descFontSize));

            return desc;
        }

        public override Card getNewCard()
        {
            return new GuidingLight();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.GodlyCards.GuidingLight_Empowered();
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            return null;
        }
    }
}
