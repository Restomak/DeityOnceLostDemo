using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables.Hovers
{
    /// <summary>
    /// Version of HoverInfo that stores and displays a relic (blessing or curse).
    /// </summary>
    public class RelicDisplay : HoverInfo
    {
        public const int numRelicsUntilGoesOffScreen = Game1.VIRTUAL_WINDOW_WIDTH / (Drawing.DrawConstants.TOPBAR_RELICS_SIZE + Drawing.DrawConstants.TOPBAR_RELICS_SPACE_BETWEEN);

        Treasury.Treasures.Relic _relic;

        public RelicDisplay(Point xy, Treasury.Treasures.Relic relic) :
            base(xy, Drawing.DrawConstants.TOPBAR_RELICS_SIZE, Drawing.DrawConstants.TOPBAR_RELICS_SIZE, relic.getDescription())
        {
            _relic = relic;

            _extraInfo = _relic.getHoverInfo();
        }

        public Treasury.Treasures.Relic getRelic()
        {
            return _relic;
        }

        public bool grayedOut()
        {
            return !_relic.getActive();
        }


        
        public static void setupRelicsUI(UserInterface relicsUI)
        {
            List<Treasury.Treasures.Relic> relics = Game1.getDungeonHandler().getRelics();

            for (int i = 0; i < relics.Count; i++)
            {
                //Figure out where it's drawn
                Point xy = new Point(Drawing.DrawConstants.TOPBAR_RELICS_STARTX + (Drawing.DrawConstants.TOPBAR_RELICS_SIZE + Drawing.DrawConstants.TOPBAR_RELICS_SPACE_BETWEEN) * i,
                    Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT - Drawing.DrawConstants.TOPBAR_RELICS_SPACE_FROM_TOP - Drawing.DrawConstants.TOPBAR_RELICS_SIZE);

                //Make sure if you somehow have more than 25 relics, they don't go off screen
                if (i > numRelicsUntilGoesOffScreen)
                {
                    int newY = i / numRelicsUntilGoesOffScreen + 1;

                    xy.X = Drawing.DrawConstants.TOPBAR_RELICS_STARTX + (Drawing.DrawConstants.TOPBAR_RELICS_SIZE + Drawing.DrawConstants.TOPBAR_RELICS_SPACE_BETWEEN) * (i - numRelicsUntilGoesOffScreen);
                    xy.Y = Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT - (Drawing.DrawConstants.TOPBAR_RELICS_SPACE_FROM_TOP - Drawing.DrawConstants.TOPBAR_RELICS_SIZE) * newY;
                }

                //Make the Clickable
                RelicDisplay relicDisplay = new RelicDisplay(xy, relics[i]);

                //Set its width & height
                relicDisplay._width = Drawing.DrawConstants.TOPBAR_RELICS_SIZE;
                relicDisplay._height = Drawing.DrawConstants.TOPBAR_RELICS_SIZE;

                //Add it to the UI
                relicsUI.addClickableToBack(relicDisplay); //order doesn't matter
            }
        }
    }
}
