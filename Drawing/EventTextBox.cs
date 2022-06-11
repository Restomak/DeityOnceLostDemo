using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Drawing
{
    public class EventTextBox
    {
        List<String> _text;

        public EventTextBox(List<String> text)
        {
            _text = text;
        }

        public void draw(SpriteBatch sprites)
        {
            for (int i = 0; i < _text.Count; i++)
            {
                EventTextConverting.drawLineOfText(sprites, new Point(Game1.VIRTUAL_WINDOW_WIDTH / 2 - DrawConstants.EVENT_BACKGROUND_WIDTH / 2 + DrawConstants.EVENT_TEXT_X_BUFFER,
                    Game1.VIRTUAL_WINDOW_HEIGHT / 2 + DrawConstants.EVENT_BACKGROUND_HEIGHT / 2 - DrawConstants.EVENT_TEXT_Y_START - i * DrawConstants.EVENT_TEXT_Y_BUFFER), _text[i]);
            }
        }
    }
}
