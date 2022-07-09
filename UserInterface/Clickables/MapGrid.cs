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
    /// Used to display a room in the dungeon on screen. Handled by the MapUI.
    /// </summary>
    public class MapGrid : Clickable
    {
        Dungeon.Room _room;
        Point _gridLocation;
        bool _highlighted;

        public MapGrid(Dungeon.Room room, Point gridLocation)
        {
            _room = room;
            _gridLocation = gridLocation;
            _highlighted = false;
        }

        //Getters
        public bool getHighlighted()
        {
            return _highlighted;
        }
        public Dungeon.Room getRoom()
        {
            return _room;
        }
        public Point getGridLocation()
        {
            return _gridLocation;
        }

        /// <summary>
        /// Handles what happens in logic when the user hovers over the room. Assuming the player is
        /// adjacent to the space and the Connector to it is open, it will light up, showing the user
        /// they can traverse to that space.
        /// </summary>
        public override void onHover()
        {
            if (playerCanTraverseToRoom())
            {
                _highlighted = true;
            }

            _hovered = true;
            Game1.setHoveredClickable(this);
        }

        public override void onHoverEnd()
        {
            _highlighted = false;
            _hovered = false;
            Game1.setHoveredClickable(null);
        }

        public override void onClick()
        {
            if (playerCanTraverseToRoom())
            {
                Game1.getDungeonHandler().movePlayer(_gridLocation);
                onHoverEnd();
            }
        }



        /// <summary>
        /// Called when the player hovers over or clicks on the MapGrid. Determines
        /// where the player would be moving from and checks if the space is both
        /// adjacent and traversable. If not, returns false.
        /// </summary>
        public bool playerCanTraverseToRoom()
        {
            Point playerLocation = Game1.getPlayerLocationOnMap();

            if (playerLocation.X == _gridLocation.X && playerLocation.Y + 1 == _gridLocation.Y) //North
            {
                if (_room.getConnector(Dungeon.Connector.direction.south) != null)
                {
                    return _room.getConnector(Dungeon.Connector.direction.south).canTraverse();
                }
            }
            if (playerLocation.X + 1 == _gridLocation.X && playerLocation.Y == _gridLocation.Y) //East
            {
                if (_room.getConnector(Dungeon.Connector.direction.west) != null)
                {
                    return _room.getConnector(Dungeon.Connector.direction.west).canTraverse();
                }
            }
            if (playerLocation.X == _gridLocation.X && playerLocation.Y - 1 == _gridLocation.Y) //South
            {
                if (_room.getConnector(Dungeon.Connector.direction.north) != null)
                {
                    return _room.getConnector(Dungeon.Connector.direction.north).canTraverse();
                }
            }
            if (playerLocation.X - 1 == _gridLocation.X && playerLocation.Y == _gridLocation.Y) //West
            {
                if (_room.getConnector(Dungeon.Connector.direction.east) != null)
                {
                    return _room.getConnector(Dungeon.Connector.direction.east).canTraverse();
                }
            }

            return false;
        }



        public static void setupRoomUI(UserInterface ui, List<List<Dungeon.Room>> rooms)
        {
            ui.resetClickables();
            //Point playerLocation = Game1.getPlayerLocationOnMap(); //doesn't matter yet but might matter if I add extremely big dungeons later
            int width = rooms.Count;
            int height = rooms[0].Count; //this only works if every dungeon is square-shaped in memory (which will probably be the case)
            int drawWidth = width * Drawing.DrawConstants.MAP_GRIDSPACE_WIDTH + (width - 1) * Drawing.DrawConstants.MAP_GRID_CONNECTOR_SPACING;
            int drawHeight = height * Drawing.DrawConstants.MAP_GRIDSPACE_HEIGHT + (height - 1) * Drawing.DrawConstants.MAP_GRID_CONNECTOR_SPACING;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    MapGrid room = new MapGrid(rooms[x][y], new Point(x, y));

                    room._x = Game1.VIRTUAL_WINDOW_WIDTH / 2 - drawWidth / 2 + x * (Drawing.DrawConstants.MAP_GRIDSPACE_WIDTH + Drawing.DrawConstants.MAP_GRID_CONNECTOR_SPACING);
                    room._y = (Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT - Drawing.DrawConstants.TOPBAR_RELICS_SPACE_FROM_TOP - Drawing.DrawConstants.TOPBAR_RELICS_SIZE) / 2 -
                        drawHeight / 2 + y * (Drawing.DrawConstants.MAP_GRIDSPACE_HEIGHT + Drawing.DrawConstants.MAP_GRID_CONNECTOR_SPACING);
                    room._width = Drawing.DrawConstants.MAP_GRIDSPACE_WIDTH;
                    room._height = Drawing.DrawConstants.MAP_GRIDSPACE_HEIGHT;

                    ui.addClickableToBack(room); //order doesn't matter
                }
            }
        }
    }
}
