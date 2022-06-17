using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Clickables
{
    public class LootableTreasure : Clickable
    {
        Treasury.Treasure _treasure;

        public LootableTreasure(Treasury.Treasure treasure)
        {
            _treasure = treasure;
        }

        public Treasury.Treasure getTreasure()
        {
            return _treasure;
        }



        /// <summary>
        /// Handles what happens in logic when the user hovers over the treasure. It will glow,
        /// similarly to a Button.
        /// </summary>
        public override void onHover()
        {
            _hovered = true;
            Game1.setHoveredClickable(this);
        }

        public override void onHoverEnd()
        {
            _hovered = false;
            Game1.setHoveredClickable(null);
        }

        /// <summary>
        /// Handles what happens in logic when the user clicks on the treasure. It will perform
        /// the action defined in the Treasure's constructor (meaning it will depend on the type
        /// of Treasure what happens exactly on click).
        /// </summary>
        public override void onClick()
        {
            _treasure.onTaken();
            onHoverEnd(); //Regardless of what it does, this treasure is no longer selected nor hovered. If somehow it still should be, then the UI will fix that next game tick
        }
    }
}
