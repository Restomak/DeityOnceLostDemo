using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events
{
    /// <summary>
    /// A specific variant of event that is only created when the player uses the Firewood
    /// item and manages to rest successfully. The player will automatically heal 25% of
    /// max HP, and they will be presented with three choices:
    ///    - Heal another 25% of max HP
    ///    - Empower a card in the deck
    ///    - Gain a short but powerful blessing
    /// </summary>
    class FirewoodRest_Success : Happening
    {
        public FirewoodRest_Success() : base(new List<String>()
        { "You lay out the logs and prepare for your rest. Thankfully, nothing |[c: Red][f: 12 bb]disturbs| you, and with a little effort you manage to get the fire crackling.",
          "",
          "Your peaceful respite, although brief, is |[c: Lawn Green][f: 12 bb]rejuvenating|."
        })
        {
            Game1.getChamp().heal((int)Math.Round((double)Game1.getChamp().getMaxHP() * 0.25));

            _choices = new List<Choice>();
            Choice choice1_heal = new Choice("[c: Lawn Green][f: 12 bb]Heal an extra [HP: 25%] (25% of Max HP)");
            Choice choice2_empower = new Choice("[c: Powder Blue][f: 12 bb]Empower a card in your deck");
            Choice choice3_blessing = new Choice("[c: Gold][f: 12 bb]Gain a random short-lived but powerful blessing");
            _choices.Add(choice3_blessing);
            _choices.Add(choice2_empower);
            _choices.Add(choice1_heal); //Backwards order to display properly

            //Choice 1: heal
            choice1_heal.setOnChoose(() =>
            {
                Game1.getChamp().heal((int)Math.Round(Game1.getChamp().getMaxHP() * 0.25));
            });

            //Choice 2: empower a card
            choice2_empower.setOnChoose(() =>
            {
                //FIXIT when this is turned into a menu instead of an event, you will be able to back out of this option. For now, you cannot
                if (!Game1.getChamp().getDeck().allCardsUpgraded())
                {
                    Game1.addToMenus(new UserInterface.Menus.EmpowerCardChoiceMenu(() => { }, false));
                }
            });
            choice2_empower.setExtraInfo(new List<UserInterface.ExtraInfo>() { DeckBuilder.Card.getEmpowerExtraInfo() });

            //Choice 3
            choice3_blessing.setOnChoose(() =>
            {
                Treasury.Treasures.RestBlessing randomRestBlessing = null;
                int emergencyExitCounter = 8; //No reason to try too many times, it's not a huge deal if they have multiple of the same blessing. I just want to reduce that chance somewhat
                bool success = false;

                while (!success && emergencyExitCounter > 0)
                {
                    randomRestBlessing = Treasury.Treasures.RestBlessing._restBlessings[Game1.randint(0, Treasury.Treasures.RestBlessing._restBlessings.Count - 1)];
                    if (!Game1.getDungeonHandler().getRelics().Contains(randomRestBlessing))
                    {
                        success = true;
                        Game1.getDungeonHandler().addRelic(randomRestBlessing.getNewRestBlessing());
                    }
                    
                    emergencyExitCounter--;
                }

                if (!success)
                {
                    Game1.getDungeonHandler().addRelic(Treasury.Treasures.RestBlessing._restBlessings[Game1.randint(0,
                        Treasury.Treasures.RestBlessing._restBlessings.Count - 1)].getNewRestBlessing()); //one more random: one more try
                }
            });
        }
    }
}
