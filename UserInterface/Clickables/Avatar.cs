using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables
{
    public class Avatar : Clickable
    {
        public Avatar()
        {
            _x = Drawing.DrawConstants.COMBAT_CHAMPION_X;
            _y = Drawing.DrawConstants.COMBAT_CHAMPION_Y;
            _width = Drawing.DrawConstants.COMBAT_CHAMPION_WIDTH;
            _height = Drawing.DrawConstants.COMBAT_CHAMPION_HEIGHT;

            //doesn't need to store the champ since it's always passed to the DrawHandler anyway
        }

        /// <summary>
        /// Sets up the enemies in combat as a UserInterface so they're interactable
        /// </summary>
        public static void setupChampionUI(Avatar champUI, Characters.Champion champ, UserInterface hoverUI)
        {
            hoverUI.resetClickables();
            hoverUI.addClickableToFront(champUI);

            //HP bar
            Hovers.HPBar hpBar = new Hovers.HPBar(new Point(champUI._x + Drawing.DrawConstants.COMBAT_ENEMY_HP_WIDTHBUFFER, champUI._y - Drawing.DrawConstants.COMBAT_ENEMY_HP_BUFFER_TO_TOP - Drawing.DrawConstants.COMBAT_ENEMY_HP_HEIGHT),
                champUI._width - Drawing.DrawConstants.COMBAT_ENEMY_HP_WIDTHBUFFER * 2, Drawing.DrawConstants.COMBAT_ENEMY_HP_HEIGHT, champ, Hovers.HPBar.hpBarType.champion);
            hoverUI.addClickableToBack(hpBar); //order doesn't matter

            //FIXIT add buffs/debuffs
        }


        /// <summary>
        /// Handles what happens in logic when the user hovers over one the champion.
        /// The champion's name will appear above their head.
        /// </summary>
        public override void onHover()
        {
            _hovered = true;
            Game1.setHoveredClickable(this);
        }

        /// <summary>
        /// Handles what happens when the user is no longer hovering over this object.
        /// </summary>
        public override void onHoverEnd()
        {
            _hovered = false;
            Game1.setHoveredClickable(null);
        }

        /// <summary>
        /// Handles what happens in logic when the user clicks on the champion in combat.
        /// </summary>
        public override void onClick()
        {
            //FIXIT implement
        }
    }
}
