using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables
{
    /// <summary>
    /// Opponents are the Clickables used for displaying enemy units on screen. When initialized,
    /// they will also set up the enemy's HP bar, buffs/debuffs, and intents.
    /// </summary>
    public class Opponent : Clickable
    {
        Combat.Enemy _enemy;
        int _position;

        public Opponent(Combat.Enemy enemy, int position)
        {
            _enemy = enemy;
            _position = position;
        }

        //Getters & Setters
        public void setEnemy(Combat.Enemy enemy)
        {
            _enemy = enemy;
        }
        public Combat.Enemy getEnemy()
        {
            return _enemy;
        }
        public void setPosition(int position)
        {
            _position = position;
        }
        public int getPosition()
        {
            return _position;
        }


        /// <summary>
        /// Sets up the enemies in combat as a UserInterface so they're interactable
        /// </summary>
        public static void setupEnemyUI(UserInterface ui, Combat.Encounter encounter, UserInterface hoverUI)
        {
            ui.resetClickables();
            hoverUI.resetClickables();

            for (int i = 0; i < encounter.getEnemies().Count; i++)
            {
                Combat.Enemy currentEnemy = encounter.getEnemies()[i];

                if (!currentEnemy.getDowned())
                {
                    Opponent opponent = new Opponent(currentEnemy, i);

                    opponent._x = currentEnemy._drawX;
                    opponent._y = currentEnemy._drawY;

                    opponent._width = currentEnemy._width;
                    opponent._height = currentEnemy._height;

                    ui.addClickableToBack(opponent); //the first enemy is closest to front, so always add behind the line
                    setupEnemyHoversUI(hoverUI, opponent);
                }
            }
        }

        /// <summary>
        /// Sets up the rest of the Clickables that are a part of each enemy:
        /// the HP bar, its buffs/debuffs, and its intents.
        /// </summary>
        private static void setupEnemyHoversUI(UserInterface hoverUI, Opponent enemy)
        {
            //HP bar
            Hovers.HPBar hpBar = new Hovers.HPBar(new Point(enemy._x + Drawing.DrawConstants.COMBAT_ENEMY_HP_WIDTHBUFFER,
                enemy._y - Drawing.DrawConstants.COMBAT_ENEMY_HP_BUFFER_TO_TOP - Drawing.DrawConstants.COMBAT_ENEMY_HP_HEIGHT),
                enemy._width - Drawing.DrawConstants.COMBAT_ENEMY_HP_WIDTHBUFFER * 2, Drawing.DrawConstants.COMBAT_ENEMY_HP_HEIGHT, enemy.getEnemy(), Hovers.HPBar.hpBarType.enemy);
            hoverUI.addClickableToBack(hpBar); //order doesn't matter

            //Buffs/debuffs
            if (enemy.getEnemy().getBuffs().Count > 0)
            {
                int row = 0;
                int index = 0;

                for (int i = 0; i < enemy.getEnemy().getBuffs().Count; i++)
                {
                    //Set up buff/debuff
                    Point loc = new Point(hpBar._x + index * (Drawing.DrawConstants.COMBAT_DEBUFF_WIDTH + Drawing.DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER * 2),
                        hpBar._y - Drawing.DrawConstants.COMBAT_DEBUFF_HEIGHT - Drawing.DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER * 2 -
                        row * (Drawing.DrawConstants.COMBAT_DEBUFF_HEIGHT + Drawing.DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER * 2) -
                        Drawing.DrawConstants.COMBAT_ENEMY_DEFENSE_BUFFER);

                    Hovers.StatusEffect status = new Hovers.StatusEffect(loc, Drawing.DrawConstants.COMBAT_DEBUFF_WIDTH + Drawing.DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER * 2,
                        Drawing.DrawConstants.COMBAT_DEBUFF_HEIGHT + Drawing.DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER * 2, enemy.getEnemy().getBuffs()[i]);
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

            enemy.getEnemy().getAIPattern().updateIntents(); //Make sure they're updated

            Hovers.EnemyIntent intentBox = new Hovers.EnemyIntent(new Point(enemy._x + enemy._width / 2 - Drawing.DrawConstants.COMBAT_INTENTS_AOE_TOTALWIDTH / 2,
                enemy._y + enemy._height + Drawing.DrawConstants.COMBAT_INTENTS_BUFFER),
                Drawing.DrawConstants.COMBAT_INTENTS_AOE_TOTALWIDTH, Drawing.DrawConstants.COMBAT_INTENTS_HEIGHT, enemy.getEnemy().getAIPattern());
            hoverUI.addClickableToBack(intentBox); //order doesn't matter
        }


        /// <summary>
        /// Handles what happens in logic when the user hovers over one of the enemies in
        /// combat. The enemy's name will appear above its head.
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
        /// Handles what happens in logic when the user clicks on an enemy in combat.
        /// </summary>
        public override void onClick()
        {
            //FIXIT implement
        }
    }
}
