using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Menus
{
    /// <summary>
    /// The menu used when the player encounters loot (either from a treasure room on the map,
    /// or after defeating the enemies in combat, etc). Each treasure is stored in the Loot
    /// object, and this menu sets them all up as LootableTreasure objects (the user interface
    /// version that the player can interact with).
    /// </summary>
    public class LootMenu : MenuUI
    {
        public const String COMBAT_LOOT = "Rewards:";
        public const String CHEST_LOOT = "Treasures:";

        Treasury.Loot _loot;
        UserInterface _treasuresAsClickables;

        public LootMenu(Treasury.Loot loot, String title) : base(Drawing.DrawConstants.LOOTMENU_X, Drawing.DrawConstants.LOOTMENU_Y, Drawing.DrawConstants.LOOTMENU_WIDTH, Drawing.DrawConstants.LOOTMENU_HEIGHT,
            Game1.pic_functionality_bar, Color.DarkSlateGray, title, Drawing.DrawConstants.LOOTMENU_TITLE_X, Drawing.DrawConstants.LOOTMENU_TITLE_Y, Game1.roboto_black_24, Drawing.DrawConstants.TEXT_24_HEIGHT,
            Color.Gold, Color.Black)
        {
            _loot = loot;

            _treasuresAsClickables = new UserInterface();
            _wholeUI.Add(_treasuresAsClickables);
        }

        public override bool addTopBar() { return true; }

        public override void updateUI()
        {
            setupTreasuresAsClickables();
        }

        public override void onEscapePressed()
        {
            checkForKeys();
            Game1.closeMenu(this);
        }

        /// <summary>
        /// Called when the menu is closed before all of the loot has been taken. Since picking up
        /// keys is mandatory, the menu will not close before automatically looting each key that
        /// was left behind (if any).
        /// </summary>
        public void checkForKeys()
        {
            List<Treasury.Treasure> treasures = _loot.getTreasures();
            
            for (int i = 0; i < treasures.Count; i++)
            {
                if (!treasures[i].isTaken() && treasures[i].GetType() == typeof(Treasury.Equipment.Key))
                {
                    treasures[i].onTaken(); //Take any key left behind
                }
            }
        }



        public void setupTreasuresAsClickables()
        {
            _treasuresAsClickables.resetClickables();
            _loot.removeTreasuresTaken();

            List<Treasury.Treasure> treasures = _loot.getTreasures();
            //If the list is empty, close the menu - we're done with it
            if (treasures.Count == 0)
            {
                Game1.closeMenu(this);
                return;
            }

            //If not empty, set them up as clickables
            for (int i = 0; i < treasures.Count && i < Drawing.DrawConstants.LOOTMENU_MAX_DISPLAYED_TREASURES; i++)
            {
                Clickables.LootableTreasure lootable = new Clickables.LootableTreasure(treasures[i]);

                if (treasures[i].GetType().IsSubclassOf(typeof(Treasury.Treasures.Relic)))
                {
                    lootable.setExtraInfo(((Treasury.Treasures.Relic)treasures[i]).getHoverExtraInfo());
                }
                else if (treasures[i].GetType().IsSubclassOf(typeof(Treasury.Equipment.Item)))
                {
                    lootable.setExtraInfo(((Treasury.Equipment.Item)treasures[i]).getHoverExtraInfo());
                }

                lootable._x = _x + Drawing.DrawConstants.LOOTMENU_TREASURE_BUFFER_X;
                lootable._y = _y + _height - Drawing.DrawConstants.LOOTMENU_TREASURE_HEIGHT - Drawing.DrawConstants.LOOTMENU_TREASURE_BUFFER_START_Y -
                    i * (Drawing.DrawConstants.LOOTMENU_TREASURE_HEIGHT + Drawing.DrawConstants.LOOTMENU_TREASURE_BUFFER_Y);
                lootable._width = Drawing.DrawConstants.LOOTMENU_TREASURE_WIDTH;
                lootable._height = Drawing.DrawConstants.LOOTMENU_TREASURE_HEIGHT;

                _treasuresAsClickables.addClickableToBack(lootable); //order doesn't matter
            }

            Clickables.Button skipButton = new Clickables.Button(Game1.pic_functionality_skipButton,
                new Point(Game1.VIRTUAL_WINDOW_WIDTH / 2 - Drawing.DrawConstants.LOOTMENU_SKIP_BUTTON_WIDTH / 2, Drawing.DrawConstants.LOOTMENU_SKIP_BUTTON_Y),
                Drawing.DrawConstants.LOOTMENU_SKIP_BUTTON_WIDTH, Drawing.DrawConstants.LOOTMENU_SKIP_BUTTON_HEIGHT, () =>
                {
                    checkForKeys();
                    Game1.closeMenu(this);
                }, new List<String>());
            _treasuresAsClickables.addClickableToBack(skipButton); //order doesn't matter
        }
    }
}
