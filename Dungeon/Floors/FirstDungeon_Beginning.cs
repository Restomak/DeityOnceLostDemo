using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Dungeon.Floors
{
    class FirstDungeon_Beginning : Floor
    {
        public const int BEGINNING_MAP_WIDTH = 2;
        public const int BEGINNING_MAP_HEIGHT = 2;

        public FirstDungeon_Beginning() : base (BEGINNING_MAP_WIDTH, BEGINNING_MAP_HEIGHT)
        {
            //Randomize start point
            int startPoint = Game1.randint(1, 4); //four corners
            int startX = 0;
            int startY = 0;
            int endX = _width - 1;
            int endY = 0;
            Connector.direction dirToEnd = Connector.direction.east;
            int eventX = 0;
            int eventY = _height - 1;
            int combatX = _width - 1;
            int combatY = _height - 1;

            switch (startPoint)
            {
                case 1: //TR -> BR
                    startX = _width - 1;
                    startY = _height - 1;
                    endX = _width - 1;
                    dirToEnd = Connector.direction.south;
                    eventX = 0;
                    eventY = _height - 1;
                    combatX = 0;
                    combatY = 0;
                    break;
                case 2: //BR -> BL
                    startX = _width - 1;
                    startY = 0;
                    endX = 0;
                    dirToEnd = Connector.direction.west;
                    eventX = _width - 1;
                    eventY = _height - 1;
                    combatX = 0;
                    combatY = _height - 1;
                    break;
                case 3: //BL -> BR
                    startX = 0;
                    startY = 0;
                    endX = _width - 1;
                    dirToEnd = Connector.direction.east;
                    eventX = 0;
                    eventY = _height - 1;
                    combatX = _width - 1;
                    combatY = _height - 1;
                    break;
                case 4: //TL -> BL
                    startX = 0;
                    startY = _height - 1;
                    endX = 0;
                    dirToEnd = Connector.direction.south;
                    eventX = _width - 1;
                    eventY = _height - 1;
                    combatX = _width - 1;
                    combatY = 0;
                    break;
            }

            //We don't add a StartRoom to any floors in this dungeon since it's the first in the game; there's nowhere to escape to by running back to the entrance
            setStart(new Point(startX, startY));
            setEnd(new Point(endX, endY));

            Rooms.ExitRoom exitRoom = new Rooms.ExitRoom();
            replaceRoom(exitRoom, new Point(endX, endY));

            Rooms.StoryRoom eventRoom = new Rooms.StoryRoom();
            replaceRoom(eventRoom, new Point(eventX, eventY));

            Rooms.CombatRoom combatRoom = new Rooms.CombatRoom(new Combat.Encounters.SingleFanblade()); //FIXIT make sure loot can only be a new card
            replaceRoom(combatRoom, new Point(combatX, combatY));

            //Remove the ability to go straight from start to exit by changing it to a connector you can scout through, but not traverse
            Room.connectRooms(_rooms[startX][startY], dirToEnd, _rooms[endX][endY], new Connectors.SeeNotTraverse());
        }
    }
}
