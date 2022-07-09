using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.RareCards
{
    /// <summary>
    /// Guidance | Rare
    ///     (Costs 1 Divinity)
    /// Draw 1 card and set its Divinity cost to 0 for the turn.
    /// </summary>
    class Guidance : BasicDrawCard
    {
        public const String NAME = "Guidance";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.RARE;
        public const int PLAYCOST_DIV = 1;
        public const int CARD_DRAW = 1;

        public Guidance(int playCostDiv = PLAYCOST_DIV) : base(NAME, CARDTYPE, RARITY, CARD_DRAW)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, playCostDiv);
            addBaseCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }

        public override void onPlay()
        {
            iCardDraw();
            reduceCost_lastDrawnCard();
        }

        protected void reduceCost_lastDrawnCard()
        {
            Game1.getChamp().getDeck().getLastDrawnCard().changeCost(CardEnums.CostType.DIVINITY, 0);
            Game1.getCombatHandler().makeCardFreeForTurn(Game1.getChamp().getDeck().getLastDrawnCard());
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<String>();
            
            desc.Add("Draw 1 card and set");
            desc.Add("its Divinity cost");
            desc.Add("to 0 for the turn.");

            return desc;
        }

        public override List<String> getStylizedDescription(Characters.Champion champ, int descFontSize, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]Draw 1 card and set");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]its Divinity cost");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]to 0 for the turn.");

            return desc;
        }

        public override Card getNewCard()
        {
            return new Guidance();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.RareCards.Guidance_Empowered();
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            return null;
        }
    }
}
