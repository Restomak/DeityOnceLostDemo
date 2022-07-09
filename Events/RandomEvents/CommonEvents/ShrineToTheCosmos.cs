using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events.RandomEvents.CommonEvents
{
    /// <summary>
    /// A common RandomEvent with three choices:
    ///    - Add to it: Remove a card from your deck
    ///    - Contemplate: Heal 20% of max HP
    ///    - Smash: (does nothing)
    /// </summary>
    class ShrineToTheCosmos : CommonRandomEvent
    {
        public ShrineToTheCosmos() : base (new List<String>()
        { "An |[c: Dark Gray][s: Darker Gray][f: 12 bb]old stone shrine| sits on a pedestal before you, directly in the center of the room. The torchlight here is |[c: Monogame Orange][f: 12 bb]dimmed|, as if in reverence. Curious,",
          "you approach.",
          "",
          "As you reach the shrine, you notice that the craftsmanship involved is |[f: 12 bb]masterful|. It appears to be a beautiful portrayal of the cosmos. Among",
          "the stars and celestial bodies, there are numerous symbolic likenesses of |[s: Dark Red][f: 12 bb]various deities|. Looking at them triggers a |[s: Darker Purple][f: 12 bb]stirring| within you, some",
          "distant memory reacting to what lays before your eyes.",
          "",
          "Searching carefully, you are somehow certain that there is nothing in this carving that portrays |[f: 12 bb]you|.",
          "",
          "You are overcome with a desire to |[s: Darker Purple][c: Powder Blue][f: 12 bb]acknowledge| this in some way."
        })
        {
            _choices = new List<Choice>();
            Choice choice1_add = new Choice("[f: 12 bb]Add a piece of yourself to the shrine |[f: 12 bb][c: Gold](Remove a card from your deck)");
            Choice choice2_contemplate = new Choice("[f: 12 bb]Contemplate your existence |[f: 12 bb][c: Lawn Green](Heal [HP: 20%] hitpoints)");
            Choice choice3_smash = new Choice("[f: 12 bb]Smash apart the shrine");
            _choices.Add(choice3_smash);
            _choices.Add(choice2_contemplate);
            _choices.Add(choice1_add); //Backwards order to display properly

            //Choice 1: remove a card
            Treasury.Loot choice1_add_loot = new Treasury.Loot(""); //the string doesn't matter since we won't see the loot screen; it'll skip to the card choice
            Treasury.Treasures.RemoveCardFromDeck choice1_add_removeCard = new Treasury.Treasures.RemoveCardFromDeck();
            choice1_add_loot.addTreasure(choice1_add_removeCard);
            choice1_add.setResult(choice1_add_loot);

            //Choice 2: heal
            choice2_contemplate.setOnChoose(() =>
            {
                Game1.getChamp().heal((int)Math.Round(Game1.getChamp().getMaxHP() * 0.2));
            });
        }
    }
}
