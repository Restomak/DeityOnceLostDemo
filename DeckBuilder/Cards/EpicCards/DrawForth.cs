using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EpicCards
{
    class DrawForth : Card
    {
        public const String NAME = "Draw Forth";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.EPIC;
        public const int PLAYCOST_DIV = 0;

        public DrawForth() : base(NAME, CARDTYPE, RARITY, CardEnums.TargetingType.champion, true)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }

        public override void onPlay()
        {
            Game1.addToMenus(new UserInterface.Menus.CombatCardChoiceMenu(Game1.getChamp().getDeck().getDrawPile(), UserInterface.Menus.CombatCardChoiceMenu.whereFrom.drawPile, () => { }));
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
    }
}
