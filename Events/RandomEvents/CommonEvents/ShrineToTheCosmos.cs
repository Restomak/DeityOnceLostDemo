using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events.RandomEvents.CommonEvents
{
    class ShrineToTheCosmos : CommonRandomEvent
    {
        /* Shrine to the Cosmos
         * 
         * Choices:
         *    - Add to it: Remove a card from your deck
         *    - Ccontemplate: Heal 20% of max HP
         *    - Smash: --
         */
        public ShrineToTheCosmos() : base (new List<String>()
        { "An old stone shrine sits on a pedestal before you, with artwork depicting the cosmos.",
          "The craftsmanship is masterful, but doesn't include anything about yourself. You feel",
          "a desire to acknowledge this in some way.",
          "",
          "<DEV NOTE: event text is first draft (FIXIT)>"
        })
        {
            _choices = new List<Choice>();
            Choice choice1_add = new Choice("[f: 12 bb]Add a piece of yourself to the shrine |[f: 12 bb][c: Gold](Remove a card from your deck)");
            Choice choice2_contemplate = new Choice("[f: 12 bb]Contemplate your existence |[f: 12 bb][c: Gold](Heal [HP: 20%] hitpoints)"); //FIXIT I want this to dynamically say the amount
            Choice choice3_smash = new Choice("[f: 12 bb]Smash apart the shrine");
            _choices.Add(choice3_smash);
            _choices.Add(choice2_contemplate);
            _choices.Add(choice1_add); //Backwards order to display properly

            //Choice 1: remove a card
            Treasury.Loot choice1_add_loot = new Treasury.Loot(""); //the string doesn't matter since we won't see the loot screen; it'll skip to the card choice
            Treasury.Treasures.RemoveCardFromDeck choice1_add_removeCard = new Treasury.Treasures.RemoveCardFromDeck();
            choice1_add_loot.addTreasure(choice1_add_removeCard);
            choice1_add.setResult(choice1_add_loot);

            //Choice 1 result
            Happening choice1_result = new Happening(new List<String>()
            {
                "Feeling satisfied, you continue on.",
                "",
                "<DEV NOTE: choice 1 result text is first draft (FIXIT)>"
            });
            choice1_result.setChoices(new List<Choice>() { new Choice("[f: 12 bb]Continue.") });
            choice1_add.setResult(choice1_result);

            //Choice 2: heal
            choice2_contemplate.setOnChoose(() =>
            {
                Game1.getChamp().heal(Game1.getChamp().getMaxHP() / 5);
            });

            //Choice 2 result
            Happening choice2_result = new Happening(new List<String>()
            {
                "Feeling refreshed, you continue on.",
                "",
                "<DEV NOTE: choice 2 result text is first draft (FIXIT)>"
            });
            choice2_result.setChoices(new List<Choice>() { new Choice("[f: 12 bb]Continue.") });
            choice2_contemplate.setResult(choice2_result);

            //Choice 3 result (smash)
            Happening choice3_result = new Happening(new List<String>()
            {
                "Feeling vindicated, you continue on.",
                "",
                "<DEV NOTE: choice 3 result text is first draft (FIXIT)>"
            });
            choice3_result.setChoices(new List<Choice>() { new Choice("[f: 12 bb]Continue.") });
            choice3_smash.setResult(choice3_result);
        }
    }
}
