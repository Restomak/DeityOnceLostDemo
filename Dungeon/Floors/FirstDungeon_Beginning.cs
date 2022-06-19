﻿using System;
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

        public const int DEFAULT_GOLD_FROM_COMBAT_MIN = 12;
        public const int DEFAULT_GOLD_FROM_COMBAT_MAX = 22;

        public FirstDungeon_Beginning() : base (BEGINNING_MAP_WIDTH, BEGINNING_MAP_HEIGHT, DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)
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

            setupIntroStoryRoom(new Point(startX, startY));

            Rooms.ExitRoom exitRoom = new Rooms.ExitRoom();
            replaceRoom(exitRoom, new Point(endX, endY));

            setupSecondStoryRoom(new Point(eventX, eventY));

            setupThirdStoryRoom(new Point(combatX, combatY));

            //Remove the ability to go straight from start to exit by changing it to a connector you can scout through, but not traverse
            Room.connectRooms(_rooms[startX][startY], dirToEnd, _rooms[endX][endY], new Connectors.SeeNotTraverse());
        }

        private void setupIntroStoryRoom(Point location)
        {
            //Create the room & replace it in the 2D list
            Rooms.StoryRoom startRoom = new Rooms.StoryRoom();
            replaceRoom(startRoom, location);

            //Set up the event
            startRoom.setRoomEvent(new Events.Happening(getIntroStory()));
            List<Events.Choice> startRoomEventChoices = new List<Events.Choice>();
            startRoomEventChoices.Add(new Events.Choice(Story.StoryConstants.INTRO_CHOICE));
            startRoomEventChoices[0].setResult(new Events.Happening(getIntroStoryResult()));
            startRoomEventChoices[0].setOnChoose(() =>
            {
                Game1.setupRandomFirstChampion();
            });

            //Set up the follow-up event
            List<Events.Choice> startRoomResultChoices = new List<Events.Choice>();
            startRoomResultChoices.Add(new Events.Choice("[f: 12 bb]Continue."));
            startRoomEventChoices[0].getResultingEvent().setChoices(startRoomResultChoices);

            startRoom.getRoomEvent().setChoices(startRoomEventChoices);
        }

        private void setupSecondStoryRoom(Point location)
        {
            //Create the room & replace it in the 2D list
            Rooms.StoryRoom secondRoom = new Rooms.StoryRoom();
            replaceRoom(secondRoom, location);

            //Set up the event
            Events.Happening secondStoryEvent = new Events.Happening(getSecondStory());
            List<Events.Choice> secondRoomEventChoices = new List<Events.Choice>();
            Events.Choice secondStoryEvent_onlyChoice = new Events.Choice(Story.StoryConstants.INTRO_ROOM2_CHOICE);

            //Set up the follow-up event
            Events.Happening secondStoryResult = new Events.Happening(getSecondStoryResult());
            List<Events.Choice> secondRoomResultChoices = new List<Events.Choice>();
            Events.Choice fight = new Events.Choice("[c: Red][f: 12 bb]Fight!");

            //Set up the combat
            Combat.Encounter secondRoomCombat = new Combat.Encounters.SingleLabTestSlime(); //FIXIT SingleFanblade(); //FIXIT make sure loot can only be a new card

            //Set up the combat loot
            Treasury.Loot secondRoomLoot = new Treasury.Loot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
            secondRoomLoot.addTreasure(new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT)));
            secondRoomLoot.addTreasure(new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)));
            //FIXIT add money


            //Put it all together
            secondRoomCombat.setLoot(secondRoomLoot);
            fight.setResult(secondRoomCombat);
            secondRoomResultChoices.Add(fight);
            secondStoryResult.setChoices(secondRoomResultChoices);
            secondStoryEvent_onlyChoice.setResult(secondStoryResult);
            secondRoomEventChoices.Add(secondStoryEvent_onlyChoice);
            secondStoryEvent.setChoices(secondRoomEventChoices);
            secondRoom.setRoomEvent(secondStoryEvent);
        }

        private void setupThirdStoryRoom(Point location)
        {
            //Create the room & replace it in the 2D list
            Rooms.StoryRoom thirdRoom = new Rooms.StoryRoom();
            replaceRoom(thirdRoom, location);

            //Set up the event
            Events.Happening thirdStoryEvent = new Events.Happening(getThirdStory());
            List<Events.Choice> thirdRoomEventChoices = new List<Events.Choice>();
            Events.Choice thirdStoryEvent_onlyChoice = new Events.Choice("[c: Gold][f: 12 bb]Add a card to your deck.");

            //Set up the card choice
            Treasury.Loot thirdRoomLoot = new Treasury.Loot(""); //the string doesn't matter since we won't see the loot screen; it'll skip to the card choice
            List<DeckBuilder.Card> thirdRoomCardChoices = new List<DeckBuilder.Card>();
            thirdRoomCardChoices.Add(new DeckBuilder.Cards.GodlyCards.FistOfTheStars());
            thirdRoomCardChoices.Add(new DeckBuilder.Cards.GodlyCards.FistOfTheStars()); //FIXIT make godly-rarity cards for this
            thirdRoomCardChoices.Add(new DeckBuilder.Cards.GodlyCards.FistOfTheStars());
            Treasury.Treasures.AddCardToDeck thirdRoomCardAddition = new Treasury.Treasures.AddCardToDeck(thirdRoomCardChoices);


            //Put it all together
            thirdRoomLoot.addTreasure(thirdRoomCardAddition);
            thirdStoryEvent_onlyChoice.setResult(thirdRoomLoot);
            thirdRoomEventChoices.Add(thirdStoryEvent_onlyChoice);
            thirdStoryEvent.setChoices(thirdRoomEventChoices);
            thirdRoom.setRoomEvent(thirdStoryEvent);
        }

        private List<String> getIntroStory()
        {
            List<String> introStory = new List<string>();

            introStory.Add(Story.StoryConstants.INTRO_LINE_1);
            introStory.Add(Story.StoryConstants.INTRO_LINE_2);
            introStory.Add(Story.StoryConstants.INTRO_LINE_3);
            introStory.Add(Story.StoryConstants.INTRO_LINE_4);
            introStory.Add(Story.StoryConstants.INTRO_LINE_5);
            introStory.Add(Story.StoryConstants.INTRO_LINE_6);
            introStory.Add(Story.StoryConstants.INTRO_LINE_7);
            introStory.Add(Story.StoryConstants.INTRO_LINE_8);
            introStory.Add(Story.StoryConstants.INTRO_LINE_9);
            introStory.Add(Story.StoryConstants.INTRO_LINE_10);
            introStory.Add(Story.StoryConstants.INTRO_LINE_11);
            introStory.Add(Story.StoryConstants.INTRO_LINE_12);
            introStory.Add(Story.StoryConstants.INTRO_LINE_13);
            introStory.Add(Story.StoryConstants.INTRO_LINE_14);
            introStory.Add(Story.StoryConstants.INTRO_LINE_15);

            return introStory;
        }

        private List<String> getIntroStoryResult()
        {
            List<String> result = new List<string>();

            result.Add(Story.StoryConstants.INTRO_RESULT_LINE_1);
            result.Add(Story.StoryConstants.INTRO_RESULT_LINE_2);
            result.Add(Story.StoryConstants.INTRO_RESULT_LINE_3);
            result.Add(Story.StoryConstants.INTRO_RESULT_LINE_4);
            result.Add(Story.StoryConstants.INTRO_RESULT_LINE_5);
            result.Add(Story.StoryConstants.INTRO_RESULT_LINE_6);
            result.Add(Story.StoryConstants.INTRO_RESULT_LINE_7);
            result.Add(Story.StoryConstants.INTRO_RESULT_LINE_8);
            result.Add(Story.StoryConstants.INTRO_RESULT_LINE_9);
            result.Add(Story.StoryConstants.INTRO_RESULT_LINE_10);
            result.Add(Story.StoryConstants.INTRO_RESULT_LINE_11);
            result.Add(Story.StoryConstants.INTRO_RESULT_LINE_12);
            result.Add(Story.StoryConstants.INTRO_RESULT_LINE_13);
            result.Add(Story.StoryConstants.INTRO_RESULT_LINE_14);

            return result;
        }

        private List<String> getSecondStory()
        {
            List<String> secondStory = new List<string>();

            secondStory.Add(Story.StoryConstants.INTRO_ROOM2_LINE_1);
            secondStory.Add(Story.StoryConstants.INTRO_ROOM2_LINE_2);
            secondStory.Add(Story.StoryConstants.INTRO_ROOM2_LINE_3);
            secondStory.Add(Story.StoryConstants.INTRO_ROOM2_LINE_4);
            secondStory.Add(Story.StoryConstants.INTRO_ROOM2_LINE_5);
            secondStory.Add(Story.StoryConstants.INTRO_ROOM2_LINE_6);
            secondStory.Add(Story.StoryConstants.INTRO_ROOM2_LINE_7);

            return secondStory;
        }

        private List<String> getSecondStoryResult()
        {
            List<String> result = new List<string>();

            result.Add(Story.StoryConstants.INTRO_ROOM2_RESULT_LINE_1);
            result.Add(Story.StoryConstants.INTRO_ROOM2_RESULT_LINE_2);
            result.Add(Story.StoryConstants.INTRO_ROOM2_RESULT_LINE_3);
            result.Add(Story.StoryConstants.INTRO_ROOM2_RESULT_LINE_4);
            result.Add(Story.StoryConstants.INTRO_ROOM2_RESULT_LINE_5);

            return result;
        }

        private List<String> getThirdStory()
        {
            List<String> thirdStory = new List<string>();

            thirdStory.Add(Story.StoryConstants.INTRO_ROOM3_LINE_1);
            thirdStory.Add(Story.StoryConstants.INTRO_ROOM3_LINE_2);
            thirdStory.Add(Story.StoryConstants.INTRO_ROOM3_LINE_3);
            thirdStory.Add(Story.StoryConstants.INTRO_ROOM3_LINE_4);
            thirdStory.Add(Story.StoryConstants.INTRO_ROOM3_LINE_5);
            thirdStory.Add(Story.StoryConstants.INTRO_ROOM3_LINE_6);
            thirdStory.Add(Story.StoryConstants.INTRO_ROOM3_LINE_7);
            thirdStory.Add(Story.StoryConstants.INTRO_ROOM3_LINE_8);
            thirdStory.Add(Story.StoryConstants.INTRO_ROOM3_LINE_9);
            thirdStory.Add(Story.StoryConstants.INTRO_ROOM3_LINE_10);
            thirdStory.Add(Story.StoryConstants.INTRO_ROOM3_LINE_11);
            thirdStory.Add(Story.StoryConstants.INTRO_ROOM3_LINE_12);

            return thirdStory;
        }
    }
}
