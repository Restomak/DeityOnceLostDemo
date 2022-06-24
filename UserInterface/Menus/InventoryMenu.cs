using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Menus
{
    class InventoryMenu : MenuUI
    {
        Treasury.Equipment.Inventory _inventory;
        UserInterface _gridSpaces;
        UserInterface _items;
        UserInterface _keys;
        UserInterface _buttons;

        public InventoryMenu(Treasury.Equipment.Inventory inventory) : base(0, 0, 0, 0, //all 0's because it needs to get calculated in the constructor
            Game1.pic_functionality_bar, Color.DarkSlateGray * Drawing.DrawConstants.INVENTORYMENU_BACKGROUND_FADE, "Inventory", 0, 0, Game1.roboto_black_24,
            Drawing.DrawConstants.TEXT_24_HEIGHT, Color.GreenYellow, Color.Black)
        {
            _inventory = inventory;

            _width = Drawing.DrawConstants.INVENTORYMENU_HORIZONTAL_BUFFER * 2 +
                (inventory.getSpaceRectangle().Width * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2));
            _height = Drawing.DrawConstants.INVENTORYMENU_BOTTOM_BUFFER + Drawing.DrawConstants.INVENTORYMENU_BUTTONS_HEIGHT + Drawing.DrawConstants.INVENTORYMENU_BETWEEN_BUFFER * 2 +
                Drawing.DrawConstants.INVENTORYMENU_TOP_BUFFER + Drawing.DrawConstants.INVENTORYMENU_TITLE_FROM_TOP + _titleFontHeight +
                ((inventory.getSpaceRectangle().Height + 1) * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2)) +
                Drawing.DrawConstants.TEXT_12_HEIGHT * 2 + Drawing.DrawConstants.INVENTORYMENU_TEXT_BUFFER * 2;
            
            _x = Game1.VIRTUAL_WINDOW_WIDTH / 2 - _width / 2;
            _y = (Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT) / 2 - _height / 2;

            _titleX = _x + _width / 2 - (int)(Game1.roboto_black_24.MeasureString("Inventory").X / 2.0f);
            _titleY = _y + _height - Drawing.DrawConstants.INVENTORYMENU_TITLE_FROM_TOP - _titleFontHeight;


            _gridSpaces = new UserInterface();
            _items = new UserInterface();
            _keys = new UserInterface();
            _buttons = new UserInterface();
            _wholeUI.Add(_items);
            _wholeUI.Add(_keys);
            _wholeUI.Add(_gridSpaces);
            _wholeUI.Add(_buttons);
        }

        public override bool addTopBar() { return true; }

        public override void updateUI()
        {
            setupInventoryClickables();
        }



        public void setupInventoryClickables()
        {
            setupGridSpaces();
            setupItems();
            setupKeys();
            setupButtons();
        }

        private void setupGridSpaces()
        {
            _gridSpaces.resetClickables();

            //Inventory
            Rectangle inventoryRect = _inventory.getSpaceRectangle();

            for (int x = 0; x < inventoryRect.Width; x++)
            {
                for (int y = 0; y < inventoryRect.Height; y++)
                {
                    Point xy = new Point(_x + Drawing.DrawConstants.INVENTORYMENU_HORIZONTAL_BUFFER + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER +
                        x * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2),
                        _y + Drawing.DrawConstants.INVENTORYMENU_BOTTOM_BUFFER + Drawing.DrawConstants.INVENTORYMENU_BUTTONS_HEIGHT + Drawing.DrawConstants.INVENTORYMENU_BETWEEN_BUFFER +
                        y * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2) + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER);

                    Clickables.Hovers.InventoryGrid gridSpace = new Clickables.Hovers.InventoryGrid(xy);
                    _gridSpaces.addClickableToBack(gridSpace);
                }
            }

            //Keys
            int keysWidth = Treasury.Equipment.Key.MAX_NUM_KEYS * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2);

            for (int x = 0; x < Treasury.Equipment.Key.MAX_NUM_KEYS; x++)
            {
                Point xy = new Point(_x + _width / 2 - keysWidth / 2 + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER +
                    x * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2),
                    _y + _height - Drawing.DrawConstants.INVENTORYMENU_TITLE_FROM_TOP - _titleFontHeight - Drawing.DrawConstants.INVENTORYMENU_BETWEEN_BUFFER -
                    Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE - Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2 + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER -
                    Drawing.DrawConstants.TEXT_12_HEIGHT - Drawing.DrawConstants.INVENTORYMENU_TEXT_BUFFER);

                Clickables.Hovers.InventoryGrid gridSpace = new Clickables.Hovers.InventoryGrid(xy);
                _gridSpaces.addClickableToBack(gridSpace);
            }

            //Text
            String itemsText = "Items:";
            Clickables.Hovers.DynamicText itemTextHover = new Clickables.Hovers.DynamicText(new Point(_x + Drawing.DrawConstants.INVENTORYMENU_HORIZONTAL_BUFFER,
                _y + Drawing.DrawConstants.INVENTORYMENU_BOTTOM_BUFFER + Drawing.DrawConstants.INVENTORYMENU_BUTTONS_HEIGHT +
                Drawing.DrawConstants.INVENTORYMENU_BETWEEN_BUFFER + Drawing.DrawConstants.INVENTORYMENU_TEXT_BUFFER +
                inventoryRect.Height * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2)),
                Game1.roboto_black_12, Drawing.DrawConstants.TEXT_12_HEIGHT, itemsText, new List<String>(), Color.GreenYellow);
            _gridSpaces.addClickableToBack(itemTextHover);

            String keyText = "Keys:";
            Clickables.Hovers.DynamicText keyTextHover = new Clickables.Hovers.DynamicText(new Point(_x + _width / 2 - keysWidth / 2,
                _y + _height - Drawing.DrawConstants.INVENTORYMENU_TITLE_FROM_TOP - _titleFontHeight - Drawing.DrawConstants.INVENTORYMENU_BETWEEN_BUFFER - Drawing.DrawConstants.TEXT_12_HEIGHT),
                Game1.roboto_black_12, Drawing.DrawConstants.TEXT_12_HEIGHT, keyText, new List<String>(), Color.GreenYellow);
            _gridSpaces.addClickableToBack(keyTextHover);
        }

        private void setupItems()
        {
            //FIXIT implement
        }

        private void setupKeys()
        {
            _keys.resetClickables();

            int numKeys = Game1.getDungeonHandler().getKeys().Count;
            int keysWidth = Treasury.Equipment.Key.MAX_NUM_KEYS * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2);

            for (int x = 0; x < numKeys; x++)
            {
                Point xy = new Point(_x + _width / 2 - keysWidth / 2 + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER +
                    x * (Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2),
                    _y + _height - Drawing.DrawConstants.INVENTORYMENU_TITLE_FROM_TOP - _titleFontHeight - Drawing.DrawConstants.INVENTORYMENU_BETWEEN_BUFFER -
                    Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_SIZE - Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER -
                    Drawing.DrawConstants.TEXT_12_HEIGHT - Drawing.DrawConstants.INVENTORYMENU_TEXT_BUFFER);

                Clickables.Hovers.CollectedKey key = new Clickables.Hovers.CollectedKey(Game1.getDungeonHandler().getKeys()[x], xy);
                _keys.addClickableToBack(key);
            }
        }

        private void setupButtons()
        {
            _buttons.resetClickables();

            //FIXIT implement more
            
            Clickables.Button exitButton = new Clickables.Button(Game1.pic_functionality_exitButton,
                new Point(_x + _width / 2 - Drawing.DrawConstants.CARDSELECTIONMENU_SKIP_BUTTON_WIDTH / 2, _y + Drawing.DrawConstants.INVENTORYMENU_BOTTOM_BUFFER),
                Drawing.DrawConstants.INVENTORYMENU_EXIT_BUTTON_WIDTH, Drawing.DrawConstants.INVENTORYMENU_EXIT_BUTTON_HEIGHT, () =>
                {
                    Game1.closeMenu(this);
                }, new List<String>());
            _buttons.addClickableToBack(exitButton); //order doesn't matter
        }
    }
}
