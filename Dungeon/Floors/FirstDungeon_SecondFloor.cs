using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Dungeon.Floors
{
    class FirstDungeon_SecondFloor : Floor
    {
        /* First Dungeon - Second Floor
         * 
         * This floor will explore the following mechanics:
         * • Door connectors, showing the importance of scouting (doors prevent it)
         * • Locked door connectors, that you need a key to open
         * • Backtracking on the map (the key is in a dead end)
         * • Multiple paths, meaning decision-making on routing (optional combat)
         * • Gaining party members
         * • Combat:
         *    - More than one enemy
         *    - Enemy debuffs
         *    - Having party members in combat
         * • Events with multiple choices
         * • Key rooms, that grant a key and sometimes extra treasure (in this case, it will have gold)
         * 
         * This floor will hint at a few more mechanics:
         * • Treasure room (if they don't avoid it)
         * • Random encounters (if they do a lot of backtracking)
         * • A few randomized rooms (not noticeable unless it's not their first time through)
         * 
         * Floor layout:
         * • 4x4, but with a couple unused rooms so it doesn't have a square shape
         * 
         */

        public const int SECOND_FLOOR_MAP_WIDTH = 4;
        public const int SECOND_FLOOR_MAP_HEIGHT = 4;

        public const int DEFAULT_GOLD_FROM_COMBAT_MIN = 12;
        public const int DEFAULT_GOLD_FROM_COMBAT_MAX = 22;

        public const int DEFAULT_GOLD_FROM_CHEST_MIN = 22;
        public const int DEFAULT_GOLD_FROM_CHEST_MAX = 32;

        public const int RANDOM_ENCOUNTER_WEIGHT_TWO_CRAWLERS = 3;
        public const int RANDOM_ENCOUNTER_WEIGHT_THREE_CRAWLERS = 2;
        public const int RANDOM_ENCOUNTER_WEIGHT_SINGLE_FANBLADE = 1;
        public const int INITIAL_RANDOM_ENCOUNTER_AMOUNT = 3;

        public FirstDungeon_SecondFloor() : base(SECOND_FLOOR_MAP_WIDTH, SECOND_FLOOR_MAP_HEIGHT, DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)
        {
            _remainingRandomEncounters = INITIAL_RANDOM_ENCOUNTER_AMOUNT; //set up max random encounters

            Point start = new Point(0, 2);
            Point end = new Point(0, 1);

            //We don't add a StartRoom to any floors in this dungeon since it's the first in the game; there's nowhere to escape to by running back to the entrance
            setStart(start);
            setEnd(end);

            //Start room
            Room startRoom = new Room();
            replaceRoom(startRoom, start);

            //End room
            Rooms.ExitRoom exitRoom = new Rooms.ExitRoom();
            replaceRoom(exitRoom, end);

            //Req combat room
            setupReqCombatRoom(); //crawler battle right after start

            //Optional combat room
            setupOptionalCombatRoom(); //two fanblades, skippable

            //Req combat room 2
            setupReqCombatRoom2(); //crawler battle right after the locked door

            //Combat & two events, but locations randomized
            setupRandomRooms();

            //Party member story room
            setupPartyStoryRoom();

            //Key room
            setupKeyRoom();

            //Add window connectors
            Room.connectRooms(_rooms[start.X][start.Y], Connector.direction.south, _rooms[end.X][end.Y], new Connectors.SeeNotTraverse());
            Room.connectRooms(_rooms[start.X][start.Y], Connector.direction.east, _rooms[1][2], new Connectors.SeeNotTraverse());
            Room.connectRooms(_rooms[1][2], Connector.direction.east, _rooms[2][2], new Connectors.SeeNotTraverse());

            //Add door connectors
            Room.connectRooms(_rooms[2][0], Connector.direction.east, _rooms[3][0], new Connectors.Door());
            Room.connectRooms(_rooms[0][0], Connector.direction.east, _rooms[1][0], new Connectors.LockedDoor(Treasury.Equipment.Key.keyColor.red));
            Room.connectRooms(_rooms[end.X][end.Y], Connector.direction.south, _rooms[0][0], new Connectors.Door());

            //Remove connectors to make walls
            Room.removeConnector(_rooms[0][1], Connector.direction.east, _rooms[1][1]);
            Room.removeConnector(_rooms[1][0], Connector.direction.north, _rooms[1][1]);
            Room.removeConnector(_rooms[3][1], Connector.direction.west, _rooms[2][1]);

            //Remove connectors so rooms aren't accessible & therefore don't exist on the map
            Room.removeConnector(_rooms[3][2], Connector.direction.south, _rooms[3][1]);
            Room.removeConnector(_rooms[3][2], Connector.direction.west, _rooms[2][2]);
            Room.removeConnector(_rooms[3][3], Connector.direction.west, _rooms[2][3]);
        }

        private void setupReqCombatRoom()
        {
            //Combat
            Combat.Encounters.TwoCrawlers crawlerCombat = new Combat.Encounters.TwoCrawlers();
            
            //Loot
            Treasury.Loot loot = new Treasury.Loot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
            loot.addTreasure(new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT)));
            loot.addTreasure(new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)));

            //Put it together
            crawlerCombat.setLoot(loot);
            Rooms.CombatRoom reqCombatRoom = new Rooms.CombatRoom(crawlerCombat);

            replaceRoom(reqCombatRoom, new Point(0, 3));
        }

        private void setupOptionalCombatRoom()
        {
            //Combat
            Combat.Encounters.TwoFanblades twoFanblades = new Combat.Encounters.TwoFanblades();

            //Loot
            Treasury.Loot loot = new Treasury.Loot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
            loot.addTreasure(new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT)));
            loot.addTreasure(new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)));

            //Put it together
            twoFanblades.setLoot(loot);
            Rooms.CombatRoom optionalCombatRoom = new Rooms.CombatRoom(twoFanblades);

            replaceRoom(optionalCombatRoom, new Point(1, 2));
        }

        private void setupReqCombatRoom2()
        {
            //Combat
            Combat.Encounters.ThreeCrawlers crawlerCombat = new Combat.Encounters.ThreeCrawlers();

            //Loot
            Treasury.Loot loot = new Treasury.Loot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
            loot.addTreasure(new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT)));
            loot.addTreasure(new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)));

            //Put it together
            crawlerCombat.setLoot(loot);
            Rooms.CombatRoom reqCombatRoom = new Rooms.CombatRoom(crawlerCombat);

            replaceRoom(reqCombatRoom, new Point(0, 0));
        }

        private void setupRandomRooms()
        {
            /*____________________.Combat Room._____________________*/
            //Combat
            Combat.Encounters.ThreeCrawlers crawlers = new Combat.Encounters.ThreeCrawlers();

            //Loot
            Treasury.Loot crawlersLoot = new Treasury.Loot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
            crawlersLoot.addTreasure(new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT)));
            crawlersLoot.addTreasure(new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)));

            //Put it together
            crawlers.setLoot(crawlersLoot);
            Rooms.CombatRoom combatRoom = new Rooms.CombatRoom(crawlers);


            /*____________________.Story Event Room._____________________*/
            //Event
            Events.Happening storyEvent = new Events.Happening(getEventStory());
            List<Events.Choice> storyEventChoices = new List<Events.Choice>();
            Events.Choice storyEvent_choice1 = new Events.Choice(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_CHOICE_1);
            Events.Choice storyEvent_choice2 = new Events.Choice(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_CHOICE_2);

            //Choice 1: remove a card
            Treasury.Loot storyEventLoot_choice1 = new Treasury.Loot(""); //the string doesn't matter since we won't see the loot screen; it'll skip to the card choice
            Treasury.Treasures.RemoveCardFromDeck choice1_removeCard = new Treasury.Treasures.RemoveCardFromDeck();
            storyEventLoot_choice1.addTreasure(choice1_removeCard);
            storyEvent_choice1.setResult(storyEventLoot_choice1);

            //Choice 1 result
            Events.Happening storyEvent_result1 = new Events.Happening(getEventStoryResult_choice1());
            storyEvent_result1.setChoices(new List<Events.Choice>() { new Events.Choice("[f: 12 bb]Continue.") });
            storyEvent_choice1.setResult(storyEvent_result1);

            //Choice 2: add a card
            Treasury.Loot storyEventLoot_choice2 = new Treasury.Loot(""); //the string doesn't matter since we won't see the loot screen; it'll skip to the card choice
            List<DeckBuilder.Card> choice2_cardChoices = Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT);
            Treasury.Treasures.AddCardToDeck choice2_addCard = new Treasury.Treasures.AddCardToDeck(choice2_cardChoices);
            storyEventLoot_choice2.addTreasure(choice2_addCard);
            storyEvent_choice2.setResult(storyEventLoot_choice2);

            //Choice 2 result
            Events.Happening storyEvent_result2 = new Events.Happening(getEventStoryResult_choice2());
            storyEvent_result2.setChoices(new List<Events.Choice>() { new Events.Choice("[f: 12 bb]Continue.") });
            storyEvent_choice2.setResult(storyEvent_result2);

            //Put it together
            List<Events.Choice> storyChoices = new List<Events.Choice>() { storyEvent_choice2, storyEvent_choice1 }; //Backwards order to display properly
            storyEvent.setChoices(storyChoices);
            Rooms.StoryRoom storyEventRoom = new Rooms.StoryRoom();
            storyEventRoom.setRoomEvent(storyEvent);


            /*____________________.Misc Event Room._____________________*/
            Rooms.EventRoom shrineRoom = new Rooms.EventRoom(new Events.RandomEvents.CommonEvents.ShrineToTheCosmos());


            /*____________________.Randomizing & Setup._____________________*/
            List<Room> rooms = new List<Room>();

            rooms.Add(combatRoom);
            rooms.Insert(Game1.randint(0, 1), storyEventRoom);
            rooms.Insert(Game1.randint(0, 2), shrineRoom);

            replaceRoom(rooms[0], new Point(1, 3));
            replaceRoom(rooms[1], new Point(2, 1));
            replaceRoom(rooms[2], new Point(1, 0));
        }

        private void setupPartyStoryRoom()
        {
            //Event
            Events.Happening partyStoryEvent = new Events.Happening(getPartyStoryWriting());
            Events.Choice onlyChoice = new Events.Choice(Story.StoryConstants.INTRO_F2_PARTY_ROOM_CHOICE);
            onlyChoice.setOnChoose(() =>
            {
                Game1.setupRandomFirstPartyMembers();
            });
            partyStoryEvent.setChoices(new List<Events.Choice>() { onlyChoice });

            //Put it together
            Rooms.StoryRoom partyStoryRoom = new Rooms.StoryRoom();
            partyStoryRoom.setRoomEvent(partyStoryEvent);
            replaceRoom(partyStoryRoom, new Point(3, 0));
        }

        private void setupKeyRoom()
        {
            //Treasures
            Treasury.Treasures.Gold roomGold = new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_CHEST_MIN, DEFAULT_GOLD_FROM_CHEST_MAX));

            //Room
            Rooms.KeyRoom keyRoom = new Rooms.KeyRoom(new Treasury.Equipment.Key(Treasury.Equipment.Key.keyColor.red));
            keyRoom.addRoomTreasure(roomGold);

            replaceRoom(keyRoom, new Point(3, 1));
        }

        private List<String> getEventStory()
        {
            List<String> eventStory = new List<string>();

            eventStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_LINE_1);
            eventStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_LINE_2);
            eventStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_LINE_3);
            eventStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_LINE_4);
            eventStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_LINE_5);
            eventStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_LINE_6);
            eventStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_LINE_7);
            eventStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_LINE_8);
            eventStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_LINE_9);

            return eventStory;
        }

        private List<String> getEventStoryResult_choice1()
        {
            List<String> resultingStory = new List<string>();

            resultingStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_RESULT_C1_1);
            resultingStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_RESULT_C1_2);
            resultingStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_RESULT_C1_3);
            resultingStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_RESULT_C1_4);

            return resultingStory;
        }

        private List<String> getEventStoryResult_choice2()
        {
            List<String> resultingStory = new List<string>();

            resultingStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_RESULT_C2_1);
            resultingStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_RESULT_C2_2);
            resultingStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_RESULT_C2_3);
            resultingStory.Add(Story.StoryConstants.INTRO_F2_RANDOM_ROOM_RESULT_C2_4);

            return resultingStory;
        }

        private List<String> getPartyStoryWriting()
        {
            List<String> storyText = new List<string>();

            storyText.Add(Story.StoryConstants.INTRO_F2_PARTY_ROOM_LINE_1);
            storyText.Add(Story.StoryConstants.INTRO_F2_PARTY_ROOM_LINE_2);
            storyText.Add(Story.StoryConstants.INTRO_F2_PARTY_ROOM_LINE_3);
            storyText.Add(Story.StoryConstants.INTRO_F2_PARTY_ROOM_LINE_4);
            storyText.Add(Story.StoryConstants.INTRO_F2_PARTY_ROOM_LINE_5);
            storyText.Add(Story.StoryConstants.INTRO_F2_PARTY_ROOM_LINE_6);
            storyText.Add(Story.StoryConstants.INTRO_F2_PARTY_ROOM_LINE_7);
            storyText.Add(Story.StoryConstants.INTRO_F2_PARTY_ROOM_LINE_8);
            storyText.Add(Story.StoryConstants.INTRO_F2_PARTY_ROOM_LINE_9);
            storyText.Add(Story.StoryConstants.INTRO_F2_PARTY_ROOM_LINE_10);
            storyText.Add(Story.StoryConstants.INTRO_F2_PARTY_ROOM_LINE_11);
            storyText.Add(Story.StoryConstants.INTRO_F2_PARTY_ROOM_LINE_12);
            storyText.Add(Story.StoryConstants.INTRO_F2_PARTY_ROOM_LINE_13);
            storyText.Add(Story.StoryConstants.INTRO_F2_PARTY_ROOM_LINE_14);

            return storyText;
        }



        public override Combat.Encounter getRandomEncounter()
        {
            if (getRemainingRandomEncounters() == 0)
            {
                return null; //None left
            }
            else
            {
                _remainingRandomEncounters -= 1;
            }


            int rand = Game1.randint(1, RANDOM_ENCOUNTER_WEIGHT_TWO_CRAWLERS + RANDOM_ENCOUNTER_WEIGHT_THREE_CRAWLERS + RANDOM_ENCOUNTER_WEIGHT_SINGLE_FANBLADE);

            if (rand <= RANDOM_ENCOUNTER_WEIGHT_TWO_CRAWLERS)
            {
                //Combat
                Combat.Encounters.TwoCrawlers twoCrawlers = new Combat.Encounters.TwoCrawlers();

                //Loot
                Treasury.Loot twoCrawlersLoot = new Treasury.Loot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
                twoCrawlersLoot.addTreasure(new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT)));
                twoCrawlersLoot.addTreasure(new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)));

                twoCrawlers.setLoot(twoCrawlersLoot);
                return twoCrawlers;
            }
            else if (rand <= RANDOM_ENCOUNTER_WEIGHT_TWO_CRAWLERS + RANDOM_ENCOUNTER_WEIGHT_THREE_CRAWLERS)
            {
                //Combat
                Combat.Encounters.ThreeCrawlers threeCrawlers = new Combat.Encounters.ThreeCrawlers();

                //Loot
                Treasury.Loot threeCrawlersLoot = new Treasury.Loot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
                threeCrawlersLoot.addTreasure(new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT)));
                threeCrawlersLoot.addTreasure(new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)));

                threeCrawlers.setLoot(threeCrawlersLoot);
                return threeCrawlers;
            }
            else
            {
                //Combat
                Combat.Encounters.SingleFanblade fanblade = new Combat.Encounters.SingleFanblade();

                //Loot
                Treasury.Loot fanbladeLoot = new Treasury.Loot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
                fanbladeLoot.addTreasure(new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT)));
                fanbladeLoot.addTreasure(new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)));

                fanblade.setLoot(fanbladeLoot);
                return fanblade;
            }
        }

        protected override int getRemainingRandomEncounters()
        {
            return _remainingRandomEncounters;
        }
    }
}
