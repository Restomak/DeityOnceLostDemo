using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.EmpoweredCards.EpicCards
{
    /// <summary>
    /// Draw Forth | Epic
    ///     (Costs 0 Divinity)
    ///     (Empowered)
    /// Choose 2 cards from your draw pile and add them to your hand.
    /// Dissipates.
    /// </summary>
    class DrawForth_Empowered : RegularCards.EpicCards.DrawForth
    {
        public const int EMPOWERED_CHOICES = 2;

        public DrawForth_Empowered() : base()
        {
            _empowered = true;
        }

        public override String getName(int nameFontSize)
        {
            return "[s: Powder Blue]" + base.getName(nameFontSize);
        }

        public override void onPlay()
        {
            Game1.addToMenus(new UserInterface.Menus.CombatCardChoiceMenu(Game1.getChamp().getDeck().getDrawPile(),
                UserInterface.Menus.CombatCardChoiceMenu.whereFrom.drawPile, () => { }, EMPOWERED_CHOICES));
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            desc.Add("Choose 2 cards from");
            desc.Add("your draw pile and");
            desc.Add("add them to your hand.");
            desc.Add("Dissipates.");

            return desc;
        }

        public override List<String> getStylizedDescription(Characters.Champion champ, int descFontSize, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]Choose |[f: " + descFontSize + " m]" + "[s: Black][c: Lawn Green]2|[f: " + descFontSize + " m]" + "[c: Black] cards from");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]your draw pile and");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]add them to your hand.");
            desc.Add(CardStylizing.dissipates(descFontSize));

            return desc;
        }

        public override Card getNewCard()
        {
            return new DrawForth_Empowered();
        }
    }
}
