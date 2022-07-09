using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events.RandomEvents.CommonEvents
{
    /// <summary>
    /// A common RandomEvent with three choices:
    ///    - Practice: Take 6 damage and Empower a card
    ///    - Take trident: Gain Trident item
    ///    - Leave: (does nothing)
    /// </summary>
    class BarracksTrident : CommonRandomEvent
    {
        public const int TAKE_DAMAGE = 6;

        public BarracksTrident() : base(new List<String>()
        { "A simple |[c: Brown][f: 12 bb]wooden post|, propped up in the center of the room, is covered in |[f: 12 bb]nicks| and |[f: 12 bb]cuts| and |[f: 12 bb]gashes|. Weapon racks filled with weapons line the",
          "walls, and the smell of oiled leather reaches your champion's nose. Many bits of hay lay scattered all around the room's floor.",
          "",
          "You hardly need to inspect the weapons to notice their poor condition. Shoddy and likely to cause more harm than good, they aren't worth taking with",
          "you, though you could use them briefly to |[c: Lawn Green][f: 12 bb]practice| and |[c: Powder Blue][f: 12 bb]refine| your champion's current skills.",
          "",
          "As you contemplate your next course of action, a |[f: 12 bb]gleam| from the corner of the room catches your eye. A |[c: Green Yellow][f: 12 bb]trident| in marvellous condition leans against",
          "the wall, |[s: Bright Purple][f: 12 bb]forgotten| by its owners.",
          "",
          "|[f: 12 bb]Tempting|."
        })
        {
            _choices = new List<Choice>();
            Choice choice1_practice = new Choice("[f: 12 bb]Practice and refine your champion's skills |[f: 12 bb][c: Red](Take " + TAKE_DAMAGE + " damage. |[f: 12 bb][c: Powder Blue]Empower a card in your deck)");
            Choice choice2_trident = new Choice("[f: 12 bb]Take the trident |[f: 12 bb][c: Green Yellow](Gain Trident)");
            Choice choice3_leave = new Choice("[f: 12 bb]Leave");
            _choices.Add(choice3_leave);
            _choices.Add(choice2_trident);
            _choices.Add(choice1_practice); //Backwards order to display properly

            //Choice 1: empower a card & take damage
            choice1_practice.setOnChoose(() =>
            {
                Game1.getChamp().takeDamage(TAKE_DAMAGE, false);

                if (!Game1.getChamp().getDeck().allCardsUpgraded())
                {
                    Game1.addToMenus(new UserInterface.Menus.EmpowerCardChoiceMenu(() => { }, false));
                }
            });

            //Choice 2: take trident
            Treasury.Loot choice2_trident_loot = new Treasury.Loot(UserInterface.Menus.LootMenu.CHEST_LOOT);
            Treasury.Equipment.Items.Trident trident = new Treasury.Equipment.Items.Trident();
            choice2_trident_loot.addTreasure(trident);
            choice2_trident.setResult(choice2_trident_loot);

            choice1_practice.setExtraInfo(new List<UserInterface.ExtraInfo>() { DeckBuilder.Card.getEmpowerExtraInfo() });
            choice2_trident.setExtraInfo(Treasury.Equipment.Items.Trident.getExtraInfo());
        }
    }
}
