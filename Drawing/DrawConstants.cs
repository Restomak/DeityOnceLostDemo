using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Drawing
{
    class DrawConstants
    {
        //Text constants
        public const int TEXT_8_HEIGHT = 12;
        public const int TEXT_10_HEIGHT = 15;
        public const int TEXT_11_HEIGHT = 15;
        public const int TEXT_12_HEIGHT = 18;
        public const int TEXT_16_HEIGHT = 24;
        public const int TEXT_20_HEIGHT = 30;
        public const int TEXT_24_HEIGHT = 36;
        public const int DEBUG_LOG_X = 10;
        public const int DEBUG_LOG_Y_START = 10;
        public const int INFO_BOX_BUFFER_BETWEEN_LINES = 2;
        public const int INFO_BOX_BUFFER_EDGES = 8;



        //Run constants
        public const int TOPBAR_HEIGHT = 32;

        public const int TOPBAR_CHAMPION_X = 32;
        public const int TOPBAR_HP_X = 300;



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
        public const int COMBAT_REMOVEDCARDS_Y = 80;

        public const int COMBAT_DRAW_PILE_BUFFER = 2;

        public const int COMBAT_DIVINITY_X = Game1.VIRTUAL_WINDOW_WIDTH / 2 - 500;
        public const int COMBAT_DIVINITY_Y = 250;

        public const int COMBAT_HANDCARDS_AREAWIDTH = 1000;
        public const int COMBAT_HANDCARDS_X_FROMMID_LEFT = 500;
        public const int COMBAT_HANDCARDS_Y = -12;
        public const int COMBAT_HANDCARD_WIDTH = 141;
        public const int COMBAT_HANDCARD_HEIGHT = 195;
        public const int COMBAT_HANDCARD_GROW_WIDTH = 282;
        public const int COMBAT_HANDCARD_GROW_HEIGHT = 390;
        public const int COMBAT_HANDCARDS_GROW_Y = 0;
        public const int COMBAT_HANDCARDS_SPACE_BETWEEN_WHEN_LOW = 32;

        public const int COMBAT_ENEMY_Y = 350;
        public const int COMBAT_FLYING_ENEMY_Y = 500;
        public const int COMBAT_ENEMY_SPACE_X_FROMRIGHT = 50;
        public const int COMBAT_ENEMY_SPACE_WIDTH = 800;
        public const int COMBAT_ENEMY_NAME_BUFFER = 10;
        public const int COMBAT_ENEMY_HP_BUFFER_TO_TOP = 10;
        public const int COMBAT_ENEMY_HP_HEIGHT = 16;
        public const int COMBAT_ENEMY_HP_WIDTHBUFFER = 16;

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



        //Glow constants
        public const int HANDCARD_GLOW_FURTHEST = 12;
        public const int HANDCARD_GLOW_STEP = 1;
        public const float HANDCARD_GLOW_OPACITY = 0.1f;
        public const int HANDCARD_GLOW_NUM_STEPS = 12;

        public const int BUTTON_GLOW_FURTHEST = 6;
        public const int BUTTON_GLOW_STEP = 1;
        public const float BUTTON_GLOW_OPACITY = 0.15f;
        public const int BUTTON_GLOW_NUM_STEPS = 6;

        public const int CHOICE_GLOW_FURTHEST = 6;
        public const int CHOICE_GLOW_STEP = 1;
        public const float CHOICE_GLOW_OPACITY = 0.15f;
        public const int CHOICE_GLOW_NUM_STEPS = 6;



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



        //Animation constants
        public const int PULSE_MAX = 24;
        public const int PULSE_TIME_AT_MAX = 16;
        public const double PULSE_START = 0.25;
        public const double PULSE_STEP = 0.01;
        public const double PULSE_BOOST = 0.4;
    }
}
