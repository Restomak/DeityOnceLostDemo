using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.EpicCards
{
    /// <summary>
    /// Draw Forth | Epic
    ///     (Costs 0 Divinity)
    /// Choose 1 card from your draw pile and add it to your hand.
    /// Dissipates.
    /// </summary>
    class DrawForth : Card
    {
        public const String NAME = "Draw Forth";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.EPIC;
        public const int PLAYCOST_DIV = 0;

        public DrawForth() : base(NAME, CARDTYPE, RARITY, CardEnums.TargetingType.champion, true)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
            _dissipates = true;
        }

        public override void onPlay()
        {
            if (Game1.getChamp().getDeck().getDrawPile().Count > 0)
            {
                Game1.addToMenus(new UserInterface.Menus.CombatCardChoiceMenu(Game1.getChamp().getDeck().getDrawPile(), UserInterface.Menus.CombatCardChoiceMenu.whereFrom.drawPile, () => { }));
            }
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            desc.Add("Choose 1 card from");
            desc.Add("your draw pile and");
            desc.Add("add it to your hand.");
            desc.Add("Dissipates.");

            return desc;
        }

        public override List<String> getStylizedDescription(Characters.Champion champ, int descFontSize, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]Choose 1 card from");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]your draw pile and");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]add it to your hand.");
            desc.Add(CardStylizing.dissipates(descFontSize));

            return desc;
        }

        public override Card getNewCard()
        {
            return new DrawForth();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.EpicCards.DrawForth_Empowered();
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(getDissipateExtraInfo());

            return extraInfo;
        }
    }
}
