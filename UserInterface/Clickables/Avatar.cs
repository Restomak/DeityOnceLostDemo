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
        /// Sets up the champion in combat as a UserInterface so they're interactable
        /// </summary>
        public static void setupChampionUI(Avatar champUI, Characters.Champion champ, UserInterface hoverUI)
        {
            hoverUI.resetClickables();
            hoverUI.addClickableToFront(champUI);

            //HP bar
            Hovers.HPBar hpBar = new Hovers.HPBar(new Point(champUI._x + Drawing.DrawConstants.COMBAT_ENEMY_HP_WIDTHBUFFER, champUI._y - Drawing.DrawConstants.COMBAT_ENEMY_HP_BUFFER_TO_TOP - Drawing.DrawConstants.COMBAT_ENEMY_HP_HEIGHT),
                champUI._width - Drawing.DrawConstants.COMBAT_ENEMY_HP_WIDTHBUFFER * 2, Drawing.DrawConstants.COMBAT_ENEMY_HP_HEIGHT, champ, Hovers.HPBar.hpBarType.champion);
            hoverUI.addClickableToBack(hpBar); //order doesn't matter
            
            if (champ.getBuffs().Count > 0)
            {
                int row = 0;
                int index = 0;

                for (int i = 0; i < champ.getBuffs().Count; i++)
                {
                    //Set up buff/debuff
                    Point loc = new Point(hpBar._x + index * (Drawing.DrawConstants.COMBAT_DEBUFF_WIDTH + Drawing.DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER * 2),
                        hpBar._y - Drawing.DrawConstants.COMBAT_DEBUFF_HEIGHT - Drawing.DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER * 2 -
                        row * (Drawing.DrawConstants.COMBAT_DEBUFF_HEIGHT + Drawing.DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER * 2) -
                        Drawing.DrawConstants.COMBAT_ENEMY_DEFENSE_BUFFER);

                    //Buffs/debuffs
                    Hovers.StatusEffect status = new Hovers.StatusEffect(loc, Drawing.DrawConstants.COMBAT_DEBUFF_WIDTH + Drawing.DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER * 2,
                        Drawing.DrawConstants.COMBAT_DEBUFF_HEIGHT + Drawing.DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER * 2, champ.getBuffs()[i]);
                    hoverUI.addClickableToBack(status); //order doesn't matter


                    //Set up next buff/debuff's draw location
                    index += 1;
                    if (index * Drawing.DrawConstants.COMBAT_DEBUFF_WIDTH + Drawing.DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER * 2 > hpBar._width)
                    {
                        index = 0;
                        row += 1;
                    }
                }
            }
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
