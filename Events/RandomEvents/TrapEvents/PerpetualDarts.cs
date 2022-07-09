using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events.RandomEvents.TrapEvents
{
    /// <summary>
    /// A TrapEvent with three choices:
    ///    - Go through: Take 15 damage
    ///    - Break machinery: Gain Weakened curse
    ///    - (with Deployable Cover) Protect yourself: Lose Deployable Cover
    /// </summary>
    class PerpetualDarts : TrapEvent
    {
        public const int TAKE_DAMAGE = 18;
        public const int MAX_HP = 8;

        public PerpetualDarts() : base(new List<String>()
        { "The grinding sounds of |[c: Dark Gray][s: Darker Gray][f: 12 bb]machinery| reach your ears as you approach the room, along with what sounds like a constant tumble of metal on metal.",
          "",
          "As you enter, you immediately spot the source of the noise. Dozens of |[s: Dark Red][f: 12 bb]tiny darts|, zipping endlessly from the right side of the room to the left,",
          "form a |[f: 12 bb]dangerous barrier| of |[c: Red][f: 12 bb]piercing steel| in the center of the room. Machinery lines the walls in an impressive display of mechanical engineering.",
          "",
          "On the far side of the room, you spot a chunk of protruding machinery with a |[c: Lawn Green][f: 12 bb]lever| jutting out of the side. Presumably, pulling on it will shut down",
          "the machinery, however you'll need to pass through the |[c: Red][f: 12 bb]hail of darts| in order to reach it.",
          "",
          "Or, you could |[f: 12 bb]smash apart the machinery|..."
        }, WEIGHT_TRIPWIRE_BLADED_HALLWAY)
        {
            _choices = new List<Choice>();
            Choice choice1_go = new Choice("[f: 12 bb]Gather yourself and step through |[f: 12 bb][c: Red](Take " + TAKE_DAMAGE + " damage)");
            Choice choice2_break = new Choice("[f: 12 bb]Smash apart the machinery |[f: 12 bb][c: Bright Purple](Gain Weakened)");
            MissableChoice choice3_cover = new MissableChoice("[f: 12 bb]Use Deployable Cover to shield yourself |[f: 12 bb][c: Green Yellow](Lose Deployable Cover)");

            _choices.Add(choice3_cover);
            _choices.Add(choice2_break);
            _choices.Add(choice1_go); //Backwards order to display properly

            choice1_go.setOnChoose(() =>
            {
                Game1.getChamp().takeDamage(TAKE_DAMAGE, false);
            });
            choice2_break.setOnChoose(() =>
            {
                Game1.getDungeonHandler().addRelic(new Treasury.Treasures.Curses.Weakened());
            });
            choice3_cover.setOnChoose(() =>
            {
                Game1.getInventory().removeFromInventory(choice3_cover.getRequiredItem());
            });

            choice2_break.setExtraInfo(Treasury.Treasures.Curses.Weakened.getExtraInfo());
            choice3_cover.setRequiredItem(new Treasury.Equipment.Items.DeployableCover()); //FIXIT there's probably a better way to do this than using extra memory
        }
    }
}
