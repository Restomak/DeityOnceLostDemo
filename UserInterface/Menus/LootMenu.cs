using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Menus
{
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
                    Game1.closeMenu(this);
                }, new List<String>());
            _treasuresAsClickables.addClickableToBack(skipButton); //order doesn't matter
        }
    }
}
