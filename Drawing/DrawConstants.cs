using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Drawing
{
    /// <summary>
    /// The purpose of this class is to store the massive amount of various constants
    /// used in drawing the various Clickables and other things in the game, with the
    /// exception of the virtual window width and height (which are stored in Game1).
    /// </summary>
    class DrawConstants
    {
        //Text constants
        public const int TEXT_8_HEIGHT = 12;
        public const int TEXT_10_HEIGHT = 15;
        public const int TEXT_11_HEIGHT = 15;
        public const int TEXT_12_HEIGHT = 18;
        public const int TEXT_14_HEIGHT = 21;
        public const int TEXT_15_HEIGHT = 23;
        public const int TEXT_16_HEIGHT = 24;
        public const int TEXT_18_HEIGHT = 27;
        public const int TEXT_20_HEIGHT = 30;
        public const int TEXT_21_HEIGHT = 32;
        public const int TEXT_24_HEIGHT = 36;
        public const int TEXT_28_HEIGHT = 42;
        public const int DEBUG_LOG_X = 10;
        public const int DEBUG_LOG_Y_START = 10;
        public const int INFO_BOX_BUFFER_BETWEEN_LINES = 2;
        public const int INFO_BOX_BUFFER_EDGES = 8;
        public const int SPACE_BETWEEN_INFO_BOXES = 4;



        //Top bar constants
        public const int TOPBAR_HEIGHT = 32;

        public const int TOPBAR_CHAMPION_X = 32;
        public const int TOPBAR_HP_X = 300;
        public const int TOPBAR_GOLD_X = 420;
        public const int TOPBAR_DECK_TEXT_X = 490;
        public const int TOPBAR_PARTY_X = 580;
        public const int TOPBAR_PARTY_1_X = 630;
        public const int TOPBAR_PARTY_2_X = 820;
        public const int TOPBAR_PARTY_3_X = 1010;
        public const int TOPBAR_INVENTORY_TEXT_X = 1200;

        public const int TOPBAR_DECK_ICON_X = 535;
        public const int TOPBAR_DECK_ICON_Y_BUFFER = 4;
        public const int TOPBAR_DECK_ICON_WIDTH = 29;
        public const int TOPBAR_DECK_ICON_HEIGHT = 24;

        public const int TOPBAR_INVENTORY_ICON_X = 1300;
        public const int TOPBAR_INVENTORY_ICON_Y_BUFFER = 4;
        public const int TOPBAR_INVENTORY_ICON_WIDTH = 24;
        public const int TOPBAR_INVENTORY_ICON_HEIGHT = 24;

        public const int TOPBAR_RELICS_STARTX = 16;
        public const int TOPBAR_RELICS_SPACE_FROM_TOP = 16;
        public const int TOPBAR_RELICS_SPACE_BETWEEN = 16;
        public const int TOPBAR_RELICS_SIZE = 48;
        public const int TOPBAR_RELICS_BUFFER = 1;



        //Combat constants
        public const int COMBAT_ENDTURNBUTTON_X_FROMRIGHT = 220;
        public const int COMBAT_ENDTURNBUTTON_Y = 200;
        public const int COMBAT_ENDTURNBUTTON_WIDTH = 120;
        public const int COMBAT_ENDTURNBUTTON_HEIGHT = 50;

        public const int COMBAT_CARDDOWN_WIDTH = 86;
        public const int COMBAT_CARDDOWN_HEIGHT = 42;

        public const int COMBAT_DRAWPILE_X = 20;
        public const int COMBAT_DRAWPILE_Y = 20;

        public const int COMBAT_DISCARDPILE_X_FROMRIGHT = 20;
        public const int COMBAT_DISCARDPILE_Y = 20;

        public const int COMBAT_REMOVEDCARDS_X_FROMRIGHT = 20;
        public const int COMBAT_REMOVEDCARDS_Y = 100;

        public const int COMBAT_DRAW_PILE_BUFFER = 2;

        public const int COMBAT_DIVINITY_X = Game1.VIRTUAL_WINDOW_WIDTH / 2 - 500;
        public const int COMBAT_DIVINITY_Y = 250;

        public const int COMBAT_HANDCARDS_AREAWIDTH = 1000;
        public const int COMBAT_HANDCARDS_X_FROMMID_LEFT = 500;
        public const int COMBAT_HANDCARDS_Y = -12;
        public const int COMBAT_HANDCARDS_GROW_Y = 0;
        public const int COMBAT_HANDCARDS_SPACE_BETWEEN_WHEN_LOW = 32;

        public const int COMBAT_ENEMY_Y = 350;
        public const int COMBAT_ENEMY_SPACE_X_FROMRIGHT = 50;
        public const int COMBAT_ENEMY_SPACE_WIDTH = 800;
        public const int COMBAT_ENEMY_NAME_BUFFER = 10;
        public const int COMBAT_ENEMY_HP_BUFFER_TO_TOP = 10;
        public const int COMBAT_ENEMY_HP_HEIGHT = 16;
        public const int COMBAT_ENEMY_HP_WIDTHBUFFER = 16;
        public const int COMBAT_ENEMY_DEFENSE_BUFFER = 2;

        public const int COMBAT_INTENTS_BUFFER = 30;
        public const int COMBAT_INTENTS_WIDTH = 56;
        public const int COMBAT_INTENTS_HEIGHT = 56;
        public const int COMBAT_INTENTS_BUFF_XADJUST = COMBAT_INTENTS_WIDTH / 2;
        public const int COMBAT_INTENTS_DEBUFF_XADJUST = -COMBAT_INTENTS_WIDTH / 2;
        public const int COMBAT_INTENTS_AOE_TOTALWIDTH = COMBAT_INTENTS_WIDTH * 3;
        public const int COMBAT_INTENTS_AOE_BUFFER = COMBAT_INTENTS_WIDTH / 4;
        public const int COMBAT_INTENTS_AOE_YBUFFER = COMBAT_INTENTS_HEIGHT / 4;
        public const int COMBAT_INTENTS_AOE_WIDTH = COMBAT_INTENTS_WIDTH / 2;
        public const int COMBAT_INTENTS_AOE_HEIGHT = COMBAT_INTENTS_HEIGHT / 2;
        public const int COMBAT_INTENTS_DAMAGE_BUFFER = 16;

        public const int COMBAT_DEFENSE_ICON_WIDTH = 28;
        public const int COMBAT_DEFENSE_ICON_HEIGHT = 28;
        public const int COMBAT_DEFENSE_ICON_HPBAR_OVERLAP = 4;

        public const int COMBAT_CHAMPION_X = 450;
        public const int COMBAT_CHAMPION_Y = 350;
        public const int COMBAT_CHAMPION_WIDTH = 200;
        public const int COMBAT_CHAMPION_HEIGHT = 240;

        public const int COMBAT_TARGET_CORNER_WIDTH = 32;
        public const int COMBAT_TARGET_CORNER_HEIGHT = 32;
        public const float COMBAT_TARGET_FADE_FADE = 0.25f;
        public const float COMBAT_TARGET_BACK_FADE = 0.5f;
        public const float COMBAT_TARGET_FADE_FADE_HOVERED = 0.5f;
        public const float COMBAT_TARGET_BACK_FADE_HOVERED = 0.75f;

        public const int COMBAT_PARTY_X_1 = 300;
        public const int COMBAT_PARTY_X_2 = 175;
        public const int COMBAT_PARTY_X_3 = 50;
        public const int COMBAT_PARTY_Y = 350;
        public const int COMBAT_PARTY_WIDTH = 100;
        public const int COMBAT_PARTY_HEIGHT = 120;

        public const int COMBAT_PARTY_NAME_BUFFER = 10;
        public const int COMBAT_PARTY_HP_BUFFER_TO_TOP = 10;
        public const int COMBAT_PARTY_HP_HEIGHT = 12;
        public const int COMBAT_PARTY_HP_WIDTHBUFFER = 8;

        public const int COMBAT_DEBUFF_WIDTH = 24;
        public const int COMBAT_DEBUFF_HEIGHT = 24;
        public const int COMBAT_DEBUFF_BORDER_BUFFER = 1;

        public const int COMBAT_INVENTORY_BUTTON_X = 32;
        public const int COMBAT_INVENTORY_BUTTON_Y = 100;
        public const int COMBAT_INVENTORY_BUTTON_WIDTH = 64;
        public const int COMBAT_INVENTORY_BUTTON_HEIGHT = 64;

        public const int COMBAT_TARGETING_RETICLE_AMOUNT = 28;
        public const int COMBAT_TARGETING_RETICLE_SIZE = 28;



        //Card constants
        public const float CARD_NAME_RARITY_FADE = 0.65f;
        public const int CARD_MAX_DESCRIPTION_HEIGHT = 5;

        public const int COMBAT_HANDCARD_WIDTH = 155;
        public const int COMBAT_HANDCARD_HEIGHT = 215;
        public const int COMBAT_HANDCARD_NAME_Y_BUFFER = -4;
        public const int COMBAT_HANDCARD_NAME_AREA_WIDTH = COMBAT_HANDCARD_WIDTH * 5 / 7;
        public const int COMBAT_HANDCARDS_DIVINITY_ADJUST_X = -8;
        public const int COMBAT_HANDCARDS_BLOOD_ADJUST_X = COMBAT_HANDCARDS_DIVINITY_ADJUST_X + COMBAT_HANDCARDS_COST_ICON_SIZE + 2; //space of 2 between divinity and blood
        public const int COMBAT_HANDCARDS_COST_ICON_SIZE = 32;
        public const int COMBAT_HANDCARDS_COST_ADJUST_Y_FROMTOP = -24;

        public const int COMBAT_HANDCARD_GROW_WIDTH = 310;
        public const int COMBAT_HANDCARD_GROW_HEIGHT = 430;
        public const int COMBAT_HANDCARD_GROW_NAME_Y_BUFFER = -8;
        public const int COMBAT_HANDCARD_GROW_NAME_AREA_WIDTH = COMBAT_HANDCARD_GROW_WIDTH * 5 / 7;
        public const int COMBAT_HANDCARDS_GROW_DIVINITY_ADJUST_X = -16;
        public const int COMBAT_HANDCARDS_GROW_BLOOD_ADJUST_X = COMBAT_HANDCARDS_GROW_DIVINITY_ADJUST_X + COMBAT_HANDCARDS_GROW_COST_ICON_SIZE + 4; //space of 4 between divinity and blood
        public const int COMBAT_HANDCARDS_GROW_COST_ICON_SIZE = 64;
        public const int COMBAT_HANDCARDS_GROW_COST_ADJUST_Y_FROMTOP = -48;

        public const int CARDSELECTIONMENU_CARD_WIDTH = 233;
        public const int CARDSELECTIONMENU_CARD_HEIGHT = 323;
        public const int CARDSELECTIONMENU_CARD_NAME_Y_BUFFER = -8;
        public const int CARDSELECTIONMENU_CARD_NAME_AREA_WIDTH = CARDSELECTIONMENU_CARD_WIDTH * 5 / 7;
        public const int CARDSELECTIONMENU_CARDS_DIVINITY_ADJUST_X = -16;
        public const int CARDSELECTIONMENU_CARDS_BLOOD_ADJUST_X = CARDSELECTIONMENU_CARDS_DIVINITY_ADJUST_X + CARDSELECTIONMENU_CARDS_COST_ICON_SIZE + 3; //space of 3 between divinity and blood
        public const int CARDSELECTIONMENU_CARDS_COST_ICON_SIZE = 64;
        public const int CARDSELECTIONMENU_CARDS_COST_ADJUST_Y_FROMTOP = -48;

        public const int CARDINSPECTIONMENU_CARD_WIDTH = 310;
        public const int CARDINSPECTIONMENU_CARD_HEIGHT = 430;
        public const int CARDINSPECTIONMENU_CARD_NAME_Y_BUFFER = -8;
        public const int CARDINSPECTIONMENU_CARD_NAME_AREA_WIDTH = CARDINSPECTIONMENU_CARD_WIDTH * 5 / 7;
        public const int CARDINSPECTIONMENU_CARD_DIVINITY_ADJUST_X = -16;
        public const int CARDINSPECTIONMENU_CARD_BLOOD_ADJUST_X = CARDINSPECTIONMENU_CARD_DIVINITY_ADJUST_X + CARDINSPECTIONMENU_CARD_COST_ICON_SIZE + 4; //space of 4 between divinity and blood
        public const int CARDINSPECTIONMENU_CARD_COST_ICON_SIZE = 64;
        public const int CARDINSPECTIONMENU_CARD_COST_ADJUST_Y_FROMTOP = -48;

        public const int CARDUPGRADEMENU_CARD_WIDTH = 310;
        public const int CARDUPGRADEMENU_CARD_HEIGHT = 430;
        public const int CARDUPGRADEMENU_CARD_NAME_Y_BUFFER = -8;
        public const int CARDUPGRADEMENU_CARD_NAME_AREA_WIDTH = CARDUPGRADEMENU_CARD_WIDTH * 5 / 7;
        public const int CARDUPGRADEMENU_CARD_DIVINITY_ADJUST_X = -16;
        public const int CARDUPGRADEMENU_CARD_BLOOD_ADJUST_X = CARDUPGRADEMENU_CARD_DIVINITY_ADJUST_X + CARDUPGRADEMENU_CARD_COST_ICON_SIZE + 4; //space of 4 between divinity and blood
        public const int CARDUPGRADEMENU_CARD_COST_ICON_SIZE = 64;
        public const int CARDUPGRADEMENU_CARD_COST_ADJUST_Y_FROMTOP = -48;

        public const int EXTRAINFO_CARD_WIDTH = 155;
        public const int EXTRAINFO_CARD_HEIGHT = 215;
        public const int EXTRAINFO_CARD_NAME_Y_BUFFER = -4;
        public const int EXTRAINFO_CARD_NAME_AREA_WIDTH = EXTRAINFO_CARD_WIDTH * 5 / 7;
        public const int EXTRAINFO_CARD_DIVINITY_ADJUST_X = -8;
        public const int EXTRAINFO_CARD_BLOOD_ADJUST_X = EXTRAINFO_CARD_DIVINITY_ADJUST_X + EXTRAINFO_CARD_COST_ICON_SIZE + 2; //space of 2 between divinity and blood
        public const int EXTRAINFO_CARD_COST_ICON_SIZE = 32;
        public const int EXTRAINFO_CARD_COST_ADJUST_Y_FROMTOP = -24;



        //Glow constants
        public const int CARD_GLOW_FURTHEST = 12;
        public const int CARD_GLOW_STEP = 1;
        public const float CARD_GLOW_OPACITY = 0.1f;
        public const int CARD_GLOW_NUM_STEPS = 12;

        public const int BUTTON_GLOW_FURTHEST = 6;
        public const int BUTTON_GLOW_STEP = 1;
        public const float BUTTON_GLOW_OPACITY = 0.15f;
        public const int BUTTON_GLOW_NUM_STEPS = 6;

        public const int CHOICE_GLOW_FURTHEST = 6;
        public const int CHOICE_GLOW_STEP = 1;
        public const float CHOICE_GLOW_OPACITY = 0.15f;
        public const int CHOICE_GLOW_NUM_STEPS = 6;

        public const int LOOT_GLOW_FURTHEST = 6;
        public const int LOOT_GLOW_STEP = 1;
        public const float LOOT_GLOW_OPACITY = 0.15f;
        public const int LOOT_GLOW_NUM_STEPS = 6;

        public const int TOPBARDECK_GLOW_FURTHEST = 3;
        public const int TOPBARDECK_GLOW_STEP = 1;
        public const float TOPBARDECK_GLOW_OPACITY = 0.15f;
        public const int TOPBARDECK_GLOW_NUM_STEPS = 3;
        public const int TOPBARDECK_GLOW_EXTRA_X_PER_STEP = 1;

        public const int ITEM_GLOW_FURTHEST = 3;
        public const int ITEM_GLOW_STEP = 1;
        public const float ITEM_GLOW_OPACITY = 0.2f;
        public const int ITEM_GLOW_NUM_STEPS = 3;



        //Dungeon constants
        public const int MAP_GRIDSPACE_WIDTH = 64;
        public const int MAP_GRIDSPACE_HEIGHT = 64;
        public const int MAP_GRID_CONNECTOR_SPACING = 32;
        public const float MAP_GRIDSPACE_HIGHLIGHT_FADE = 0.2f;

        public const int MAP_ROOM_CONTENTS_ICON_WIDTH = 32;
        public const int MAP_ROOM_CONTENTS_ICON_HEIGHT = 32;
        public const float MAP_ROOM_PLAYER_PULSE_FADE = 0.2f;
        public const float MAP_ROOM_PLAYER_PULSE_FADE_STEP_AT_MAX = 0.0125f;



        //Event constants
        public const int EVENT_BACKGROUND_WIDTH = 1200;
        public const int EVENT_BACKGROUND_HEIGHT = 675;

        public const int EVENT_CHOICE_WIDTH = 1000;
        public const int EVENT_CHOICE_HEIGHT = 32;
        public const int EVENT_CHOICE_BUFFER = 16;
        public const int EVENT_CHOICE_INTIAL_BUFFER = 40;
        public const int EVENT_CHOICE_TEXT_X_BUFFER = 16;

        public const int EVENT_TEXT_X_BUFFER = 48;
        public const int EVENT_TEXT_Y_BUFFER = 24; //includes the text - not just the space between
        public const int EVENT_TEXT_Y_START = 64;



        //Menu Constants
        public const int LOOTMENU_X = Game1.VIRTUAL_WINDOW_WIDTH / 2 - LOOTMENU_WIDTH / 2;
        public const int LOOTMENU_Y = Game1.VIRTUAL_WINDOW_HEIGHT / 2 - LOOTMENU_HEIGHT / 2;
        public const int LOOTMENU_WIDTH = 500;
        public const int LOOTMENU_HEIGHT = 700;
        public const int LOOTMENU_TITLE_X = LOOTMENU_X + 25;
        public const int LOOTMENU_TITLE_Y = LOOTMENU_Y + 650 - TEXT_24_HEIGHT / 2;
        public const int LOOTMENU_TREASURE_WIDTH = 450;
        public const int LOOTMENU_TREASURE_HEIGHT = 90;
        public const int LOOTMENU_TREASURE_BUFFER_X = 25;
        public const int LOOTMENU_TREASURE_BUFFER_Y = 10;
        public const int LOOTMENU_TREASURE_BUFFER_START_Y = 110;
        public const int LOOTMENU_MAX_DISPLAYED_TREASURES = 5;
        public const int LOOTMENU_SKIP_BUTTON_WIDTH = 120;
        public const int LOOTMENU_SKIP_BUTTON_HEIGHT = 50;
        public const int LOOTMENU_SKIP_BUTTON_Y = LOOTMENU_Y + 10;

        public const float LOOTMENU_TREASURE_UNSELECT_FADE = 0.2f;
        public const float LOOTMENU_TREASURE_SELECT_FADE = 1.0f;
        public const int LOOTMENU_TREASURE_ICON_WIDTH = 64;
        public const int LOOTMENU_TREASURE_ICON_HEIGHT = 64;
        public const int LOOTMENU_TREASURE_ICON_BUFFER = 13;

        public const int CARDSELECTIONMENU_X = 0;
        public const int CARDSELECTIONMENU_Y = 0;
        public const int CARDSELECTIONMENU_WIDTH = 1600;
        public const int CARDSELECTIONMENU_HEIGHT = Game1.VIRTUAL_WINDOW_HEIGHT - TOPBAR_HEIGHT;
        public const int CARDSELECTIONMENU_MIN_HEIGHT = 700;
        public const int CARDSELECTIONMENU_TITLE_Y_FROM_TOP = 32;
        public const float CARDCHOICE_BACKGROUND_FADE = 0.95f;
        public const int CARDSELECTIONMENU_Y_BUFFER = 32;
        public const int CARDSELECTIONMENU_CARD_SPACE_BETWEEN = 32;
        public const int CARDSELECTIONMENU_CARD_SPACE_BETWEEN_UNDER4 = 64;
        public const int CARDSELECTIONMENU_SKIP_BUTTON_WIDTH = 120;
        public const int CARDSELECTIONMENU_SKIP_BUTTON_HEIGHT = 50;
        public const int CARDSELECTIONMENU_MAX_CARDS_PER_ROW = 5;
        public const int CARDSELECTIONMENU_CARDS_START_X_1CARD = Game1.VIRTUAL_WINDOW_WIDTH / 2 - CARDSELECTIONMENU_CARD_WIDTH / 2;
        public const int CARDSELECTIONMENU_CARDS_START_X_2CARDS = Game1.VIRTUAL_WINDOW_WIDTH / 2 - CARDSELECTIONMENU_CARD_SPACE_BETWEEN / 2 - CARDSELECTIONMENU_CARD_WIDTH;
        public const int CARDSELECTIONMENU_CARDS_START_X_3CARDS = Game1.VIRTUAL_WINDOW_WIDTH / 2 - CARDSELECTIONMENU_CARD_WIDTH / 2 - (CARDSELECTIONMENU_CARD_WIDTH + CARDSELECTIONMENU_CARD_SPACE_BETWEEN);
        public const int CARDSELECTIONMENU_CARDS_START_X_4CARDS = Game1.VIRTUAL_WINDOW_WIDTH / 2 - CARDSELECTIONMENU_CARD_SPACE_BETWEEN / 2 - CARDSELECTIONMENU_CARD_WIDTH * 2 - CARDSELECTIONMENU_CARD_SPACE_BETWEEN;
        public const int CARDSELECTIONMENU_CARDS_START_X_5CARDS = Game1.VIRTUAL_WINDOW_WIDTH / 2 - CARDSELECTIONMENU_CARD_WIDTH / 2 - (CARDSELECTIONMENU_CARD_WIDTH + CARDSELECTIONMENU_CARD_SPACE_BETWEEN) * 2;
        public const int CARDSELECTIONMENU_CONFIRM_BUTTON_WIDTH = 120;
        public const int CARDSELECTIONMENU_CONFIRM_BUTTON_HEIGHT = 50;
        public const int CARDSELECTIONMENU_SPACE_BETWEEN_BUTTONS = 64;

        public const int INVENTORYMENU_GRIDSPACE_SIZE = 64;
        public const int INVENTORYMENU_BUTTONS_HEIGHT = 50;
        public const int INVENTORYMENU_GRIDSPACE_BUFFER = 1;
        public const int INVENTORYMENU_HORIZONTAL_BUFFER = 128;
        public const int INVENTORYMENU_BOTTOM_BUFFER = 32;
        public const int INVENTORYMENU_BETWEEN_BUFFER = 48;
        public const int INVENTORYMENU_TEXT_BUFFER = 4;
        public const int INVENTORYMENU_TOP_BUFFER = 48;
        public const int INVENTORYMENU_TITLE_FROM_TOP = 32;
        public const float INVENTORYMENU_BACKGROUND_FADE = 0.99f;
        public const int INVENTORYMENU_EXIT_BUTTON_WIDTH = 120;
        public const int INVENTORYMENU_EXIT_BUTTON_HEIGHT = 50;

        public const float INVENTORYMENU_HELD_ITEM_FADE = 0.85f;

        public const int ITEM_RIGHT_CLICK_MENU_HORIZONTAL_BUFFER = 8;

        public const int CARDINSPECTIONMENU_EXIT_BUTTON_WIDTH = 120;
        public const int CARDINSPECTIONMENU_EXIT_BUTTON_HEIGHT = 50;
        public const int CARDINSPECTIONMENU_BOTTOM_BUFFER = 32;
        public const int CARDINSPECTIONMENU_TOP_BUFFER = 32;
        public const int CARDINSPECTIONMENU_SPACE_BETWEEN = 32;
        public const int CARDINSPECTIONMENU_SPACE_BETWEEN_TEXT = 32;
        public const int CARDINSPECTIONMENU_CHECKBOX_BUFFER = 6;
        public const int CARDINSPECTIONMENU_WIDTH = Game1.VIRTUAL_WINDOW_WIDTH;
        public const int CARDINSPECTIONMENU_HEIGHT = CARDINSPECTIONMENU_BOTTOM_BUFFER + CARDINSPECTIONMENU_EXIT_BUTTON_HEIGHT +
            CARDINSPECTIONMENU_SPACE_BETWEEN * 2 + TEXT_12_HEIGHT + CARDINSPECTIONMENU_CARD_HEIGHT + CARDINSPECTIONMENU_TOP_BUFFER;
        public const int CARDINSPECTIONMENU_X = Game1.VIRTUAL_WINDOW_WIDTH / 2 - CARDINSPECTIONMENU_WIDTH / 2;
        public const int CARDINSPECTIONMENU_Y = (Game1.VIRTUAL_WINDOW_HEIGHT - TOPBAR_HEIGHT) / 2 - CARDINSPECTIONMENU_HEIGHT / 2;
        public const int CARDINSPECTIONMENU_BACKGROUND_HEIGHT = Game1.VIRTUAL_WINDOW_HEIGHT - TOPBAR_HEIGHT;
        public const int CARDINSPECTIONMENU_BACKGROUND_Y = 0;
        public const float CARDINSPECTIONMENU_BACKGROUND_FADE = 0.95f;

        public const int CARDUPGRADEMENU_CONFIRM_BUTTON_WIDTH = 120;
        public const int CARDUPGRADEMENU_CONFIRM_BUTTON_HEIGHT = 50;
        public const int CARDUPGRADEMENU_EXIT_BUTTON_WIDTH = 120;
        public const int CARDUPGRADEMENU_EXIT_BUTTON_HEIGHT = 50;
        public const int CARDUPGRADEMENU_BOTTOM_BUFFER = 32;
        public const int CARDUPGRADEMENU_SPACE_BETWEEN = 32;
        public const int CARDUPGRADEMENU_TITLE_BUFFER = 32;
        public const int CARDUPGRADEMENU_UPGRADE_ARROW_SIZE = 64;
        public const int CARDUPGRADEMENU_WIDTH = Game1.VIRTUAL_WINDOW_WIDTH;
        public const int CARDUPGRADEMENU_HEIGHT = CARDUPGRADEMENU_BOTTOM_BUFFER + CARDUPGRADEMENU_EXIT_BUTTON_HEIGHT +
            CARDUPGRADEMENU_SPACE_BETWEEN * 2 + CARDUPGRADEMENU_CARD_HEIGHT + TEXT_24_HEIGHT + CARDUPGRADEMENU_TITLE_BUFFER;
        public const int CARDUPGRADEMENU_X = Game1.VIRTUAL_WINDOW_WIDTH / 2 - CARDUPGRADEMENU_WIDTH / 2;
        public const int CARDUPGRADEMENU_Y = (Game1.VIRTUAL_WINDOW_HEIGHT - TOPBAR_HEIGHT) / 2 - CARDUPGRADEMENU_HEIGHT / 2;
        public const int CARDUPGRADEMENU_BACKGROUND_HEIGHT = Game1.VIRTUAL_WINDOW_HEIGHT - TOPBAR_HEIGHT;
        public const int CARDUPGRADEMENU_BACKGROUND_Y = 0;
        public const int CARDUPGRADEMENU_TITLE_Y = CARDUPGRADEMENU_Y + CARDUPGRADEMENU_HEIGHT - TEXT_24_HEIGHT - CARDUPGRADEMENU_TITLE_BUFFER;
        public const float CARDUPGRADEMENU_BACKGROUND_FADE = 0.95f;

        public const int MENU_SCROLLBAR_Y = 100;
        public const int MENU_SCROLLBAR_HEIGHT = Game1.VIRTUAL_WINDOW_HEIGHT - TOPBAR_HEIGHT - 200;
        public const int MENU_SCROLLBAR_WIDTH = 26;
        public const int MENU_SCROLLBAR_BUTTON_HEIGHT = 24;
        public const int MENU_SCROLLBAR_BUTTON_WIDTH = 24;
        public const int MENU_SCROLLBAR_CARDMENUS_X = Game1.VIRTUAL_WINDOW_WIDTH - 50 - MENU_SCROLLBAR_WIDTH;



        //Color constants
        public const int EVENT_CHOICE_BACKGROUND_RED = 35;
        public const int EVENT_CHOICE_BACKGROUND_GREEN = 20;
        public const int EVENT_CHOICE_BACKGROUND_BLUE = 99;

        public const int DARK_PURPLE_RED = 64;
        public const int DARK_PURPLE_GREEN = 0;
        public const int DARK_PURPLE_BLUE = 96;
        public const int DARKER_GRAY_RED = 64;
        public const int DARKER_GRAY_GREEN = 64;
        public const int DARKER_GRAY_BLUE = 64;
        public const int DARKER_RED_RED = 64;
        public const int DARKER_RED_GREEN = 0;
        public const int DARKER_RED_BLUE = 0;
        public const int DARKER_PURPLE_RED = 32;
        public const int DARKER_PURPLE_GREEN = 0;
        public const int DARKER_PURPLE_BLUE = 64;
        public const int RAREBLUE_RED = 0;
        public const int RAREBLUE_GREEN = 70;
        public const int RAREBLUE_BLUE = 255;
        public const int BLUE_PURPLE_RED = 96;
        public const int BLUE_PURPLE_GREEN = 0;
        public const int BLUE_PURPLE_BLUE = 224;
        public const int BRIGHT_PURPLE_RED = 160;
        public const int BRIGHT_PURPLE_GREEN = 0;
        public const int BRIGHT_PURPLE_BLUE = 160;



        //Animation constants
        public const int PULSE_MAX = 24;
        public const int PULSE_TIME_AT_MAX = 16;
        public const double PULSE_START = 0.25;
        public const double PULSE_STEP = 0.01;
        public const double PULSE_BOOST = 0.4;



        //Scroll Wheel
        public const int SCROLL_Y_WHEEL_TICK_AMOUNT = 32;

        //Checkboxes
        public const int CHECKBOX_SIZE = 16;
    }
}
