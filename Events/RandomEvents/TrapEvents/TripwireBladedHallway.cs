using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events.RandomEvents.TrapEvents
{
    /// <summary>
    /// A TrapEvent with three choices:
    ///    - Step cautiously: Take 18 damage
    ///    - Charge through: Lose 6 max HP
    ///    - (with Wirecutters) Cut the tripwires: Lose Wirecutters
    /// </summary>
    class TripwireBladedHallway : TrapEvent
    {
        public const int TAKE_DAMAGE = 18;
        public const int MAX_HP = 6;

        public TripwireBladedHallway() : base(new List<String>()
        { "As you peer down the hallway, you notice a |[s: Dark Red][f: 12 bb]pair of blades| hidden within the uneven curves and jagged outcroppings of the wall. A thin, almost",
          "imperceptible |[c: Dark Red][f: 12 bb]tripwire| near your feet catches your eye |[c: Lawn Green][f: 12 bb]just in time|. It is looped up and around the blades in a way to set them falling upon you,",
          "should you |[c: Red][f: 12 bb]misstep|.",
          "",
          "The hallway is |[c: Dark Red][c: Red][f: 12 bb]trapped|.",
          "",
          "Without moving closer, you peer down the hallway once more, giving it a more thorough look. You notice |[s: Dark Red][f: 12 bb]dozens| more blades, all likely hooked.",
          "up to more nearly invisible tripwires.",
          "",
          "The hallway is |[f: 12 bb]excessively| trapped."
        }, WEIGHT_TRIPWIRE_BLADED_HALLWAY)
        {
            _choices = new List<Choice>();
            Choice choice1_cautious = new Choice("[f: 12 bb]Attempt to step cautiously around the tripwires |[f: 12 bb][c: Red](Take " + TAKE_DAMAGE + " damage)");
            Choice choice2_charge = new Choice("[f: 12 bb]Charge through and try to outrun the falling blades |[f: 12 bb][c: Red](Lose " + MAX_HP + " max HP)");
            MissableChoice choice3_wirecutters = new MissableChoice("[f: 12 bb]Use Wirecutters to cut the tripwires |[f: 12 bb][c: Green Yellow](Lose Wirecutters)");

            _choices.Add(choice3_wirecutters);
            _choices.Add(choice2_charge);
            _choices.Add(choice1_cautious); //Backwards order to display properly

            choice1_cautious.setOnChoose(() =>
            {
                Game1.getChamp().takeDamage(TAKE_DAMAGE, false);
            });
            choice2_charge.setOnChoose(() =>
            {
                Game1.getChamp().affectMaxHP(-MAX_HP);
            });
            choice3_wirecutters.setOnChoose(() =>
            {
                Game1.getInventory().removeFromInventory(choice3_wirecutters.getRequiredItem());
            });

            choice3_wirecutters.setRequiredItem(new Treasury.Equipment.Items.Wirecutters()); //FIXIT there's probably a better way to do this than using extra memory
        }
    }
}
