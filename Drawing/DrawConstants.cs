using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Drawing
{
    class DrawConstants
    {
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

        public const int COMBAT_DIVINITY_AMOUNT_X = 250;
        public const int COMBAT_DIVINITY_AMOUNT_Y = 210;

        public const int COMBAT_HANDCARDS_X_FROMMID_LEFT = 500;
        public const int COMBAT_HANDCARDS_Y = -12;
        public const int COMBAT_HANDCARDS_AREAWIDTH = 1000;

        public const int COMBAT_HANDCARD_WIDTH = 141;
        public const int COMBAT_HANDCARD_HEIGHT = 195;

        public const int COMBAT_ENEMY_Y = 350;
        public const int COMBAT_FLYING_ENEMY_Y = 500;
        public const int COMBAT_ENEMY_SPACE_X_FROMRIGHT = 50;
        public const int COMBAT_ENEMY_SPACE_WIDTH = 800;
        public const int COMBAT_ENEMY_NAME_BUFFER = 10;

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



        public const int TEXT_8_HEIGHT = 8;
        public const int TEXT_10_HEIGHT = 10;
        public const int TEXT_12_HEIGHT = 12;
        public const int TEXT_24_HEIGHT = 24;
        public const int DEBUG_LOG_X = 10;
        public const int DEBUG_LOG_Y_START = 10;
    }
}
