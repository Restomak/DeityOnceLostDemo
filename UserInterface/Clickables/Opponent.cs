using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Clickables
{
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
        public static void setupEnemyUI(UserInterface ui, Combat.Encounter encounter)
        {
            ui.resetClickables();

            for (int i = 0; i < encounter.getEnemies().Count; i++)
            {
                Combat.Enemy currentEnemy = encounter.getEnemies()[i];
                Opponent opponent = new Opponent(currentEnemy, i);
                
                //FIXIT the actual Encounters subclasses should be setting up enemy spacing (x & y) so it looks better
                //opponent._y = encounter.getEnemyY(i);
                //opponent._x = encounter.getEnemyX(i);

                //Temp: for testing
                if (!currentEnemy._isFlyer)
                {
                    opponent._y = Drawing.DrawConstants.COMBAT_ENEMY_Y;
                }
                else
                {
                    opponent._y = Drawing.DrawConstants.COMBAT_FLYING_ENEMY_Y;
                }
                opponent._x = Game1.VIRTUAL_WINDOW_WIDTH - Drawing.DrawConstants.COMBAT_ENEMY_SPACE_X_FROMRIGHT - Drawing.DrawConstants.COMBAT_ENEMY_SPACE_WIDTH / 2 - currentEnemy._width / 2;

                opponent._width = currentEnemy._width;
                opponent._height = currentEnemy._height;
                
                ui.addClickableToBack(opponent); //the first enemy is closest to front, so always add behind the line
            }
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
        /// Handles what happens in logic when the user clicks on an enemy in combat.
        /// </summary>
        public override void onClick()
        {
            //FIXIT implement
        }
    }
}
