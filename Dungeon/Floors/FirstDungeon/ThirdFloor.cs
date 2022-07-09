using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Dungeon.Floors.FirstDungeon
{
    class ThirdFloor : Floor
    {
        /* First Dungeon - Third Floor
         * 
         * This floor will explore the following mechanics:
         * • Treasure rooms
         * • Wirecutters item & Trap Events (the key is beyond)
         * • The Firewood item, & a suggestion to use it before the miniboss if low HP
         * • Resting (Firewood), which provides:
         *    - Healing (25% of max HP)
         *    - Choice of an extra benefit:
         *        ~ Healing another 25% of max HP
         *        ~ Rest-based blessing (powerful but lasts a short duration)
         *        ~ Empowering a card
         * • Fighting a miniboss
         * • Getting your first blessing
         * 
         * This floor will hint at a few more mechanics:
         * • Corpses & resurrection (find a Corpse, event explains, choose to take or not)
         * • Soul Stones (miniboss drops one)
         * • Deployable Cover and Trident items (optional treasure loot and optional event choice)
         * • Using items in combat (Deployable Cover or Trident items)
         * • Not all optional paths are beneficial (both single rooms hidden by doors have non-pushover combats)
         * • Curses (the option to take the Weakened curse in an optional event)
         * 
         * 
         * more things I could do:
         * • proper commenting
         * • buttons for certain menus (I & E for inventory, Q & C for deck)
         */

        public const int THIRD_FLOOR_MAP_WIDTH = 9;
        public const int THIRD_FLOOR_MAP_HEIGHT = 4;

        public const int DEFAULT_GOLD_FROM_COMBAT_MIN = 17;
        public const int DEFAULT_GOLD_FROM_COMBAT_MAX = 27;
        public const int MINIBOSS_GOLD_FROM_COMBAT_MIN = 43;
        public const int MINIBOSS_GOLD_FROM_COMBAT_MAX = 53;

        public const int DEFAULT_GOLD_FROM_CHEST_MIN = 22;
        public const int DEFAULT_GOLD_FROM_CHEST_MAX = 32;

        public const int RANDOM_ENCOUNTER_WEIGHT_TWO_FANBLADES = 6;
        public const int RANDOM_ENCOUNTER_WEIGHT_THREE_CRAWLERS = 5;
        public const int RANDOM_ENCOUNTER_WEIGHT_FOUR_CRAWLERS = 5;
        public const int RANDOM_ENCOUNTER_WEIGHT_SINGLE_FANBLADE = 3;
        public const int INITIAL_RANDOM_ENCOUNTER_AMOUNT = 5;

        public ThirdFloor() : base(THIRD_FLOOR_MAP_WIDTH, THIRD_FLOOR_MAP_HEIGHT, DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)
        {
            _remainingRandomEncounters = INITIAL_RANDOM_ENCOUNTER_AMOUNT; //set up max random encounters

            Point start = new Point(1, 2);
            Point end = new Point(8, 2);

            //We don't add a StartRoom to any floors in this dungeon since it's the first in the game; there's nowhere to escape to by running back to the entrance
            setStart(start);
            setEnd(end);

            //Start room
            Room startRoom = new Room();
            replaceRoom(startRoom, start);

            //End room
            Rooms.ExitRoom exitRoom = new Rooms.ExitRoom();
            replaceRoom(exitRoom, end);

            //Req treasure room 1
            setupReqTreasureRoom1(new Point(2, 1)); //wireclippers

            //Req combat room
            setupReqCombatRoom(new Point(3, 0));

            //Req key room
            setupReqKeyRoom(new Point(1, 0));

            //Req trap event room
            setupReqTrapEventRoom(new Point(3, 2));

            //Req treasure room 2
            setupReqTreasureRoom2(new Point(3, 3)); //firewood

            //Req story event room 1
            setupReqStoryEventRoom1(new Point(4, 3)); //corpse

            //Req miniboss room
            setupReqMinibossRoom(new Point(5, 2)); //greater soul stone

            //Req story event room 2
            setupReqStoryEventRoom2(new Point(7, 2));

            //Optional combat rooms
            setupOptionalCombatRooms(new Point(0, 1), new Point(1, 1), new Point(5, 0), new Point(4, 2));

            //Optional treasure room
            setupOptionalTreasureRoom(new Point(0, 0));

            //Optional trap event room
            setupOptionalTrapEventRoom(new Point(4, 0));

            //Optional event room
            setupOptionalEventRoom(new Point(5, 1)); //trident

            //Add window connectors
            Room.connectRooms(_rooms[0][0], Connector.direction.east, _rooms[1][0], new Connectors.SeeNotTraverse());
            Room.connectRooms(_rooms[2][3], Connector.direction.east, _rooms[3][3], new Connectors.SeeNotTraverse());
            Room.connectRooms(_rooms[5][1], Connector.direction.north, _rooms[5][2], new Connectors.SeeNotTraverse());

            //Add door connectors
            Room.connectRooms(_rooms[0][2], Connector.direction.east, _rooms[1][2], new Connectors.Door());
            Room.connectRooms(_rooms[1][2], Connector.direction.east, _rooms[2][2], new Connectors.Door());
            Room.connectRooms(_rooms[1][1], Connector.direction.east, _rooms[2][1], new Connectors.Door());
            Room.connectRooms(_rooms[2][1], Connector.direction.east, _rooms[3][1], new Connectors.Door());
            Room.connectRooms(_rooms[4][2], Connector.direction.east, _rooms[5][2], new Connectors.Door());
            Room.connectRooms(_rooms[4][3], Connector.direction.east, _rooms[5][3], new Connectors.Door());
            Room.connectRooms(_rooms[3][0], Connector.direction.north, _rooms[3][1], new Connectors.Door());
            Room.connectRooms(_rooms[3][2], Connector.direction.north, _rooms[3][3], new Connectors.LockedDoor(Treasury.Equipment.Key.keyColor.blue));

            //Remove connectors to make walls
            Room.removeConnector(_rooms[3][0], Connector.direction.east, _rooms[4][0]);
            Room.removeConnector(_rooms[0][1], Connector.direction.east, _rooms[1][1]);
            Room.removeConnector(_rooms[4][1], Connector.direction.east, _rooms[5][1]);
            Room.removeConnector(_rooms[2][2], Connector.direction.east, _rooms[3][2]);
            Room.removeConnector(_rooms[3][2], Connector.direction.east, _rooms[4][2]);
            Room.removeConnector(_rooms[1][0], Connector.direction.north, _rooms[1][1]);
            Room.removeConnector(_rooms[1][1], Connector.direction.north, _rooms[1][2]);
            Room.removeConnector(_rooms[2][0], Connector.direction.north, _rooms[2][1]);
            Room.removeConnector(_rooms[4][1], Connector.direction.north, _rooms[4][2]);
            Room.removeConnector(_rooms[4][2], Connector.direction.north, _rooms[4][3]);

            //Remove connectors so rooms aren't accessible & therefore don't exist on the map
            Room.removeConnector(_rooms[0][3], Connector.direction.south, _rooms[0][2]);
            Room.removeConnector(_rooms[1][3], Connector.direction.south, _rooms[1][2]);
            Room.removeConnector(_rooms[1][3], Connector.direction.east, _rooms[2][3]);
            Room.removeConnector(_rooms[6][3], Connector.direction.west, _rooms[5][3]);
            Room.removeConnector(_rooms[6][3], Connector.direction.south, _rooms[6][2]);
            Room.removeConnector(_rooms[7][3], Connector.direction.south, _rooms[7][2]);
            Room.removeConnector(_rooms[8][3], Connector.direction.south, _rooms[8][2]);
            Room.removeConnector(_rooms[6][0], Connector.direction.west, _rooms[5][0]);
            Room.removeConnector(_rooms[6][1], Connector.direction.west, _rooms[5][1]);
            Room.removeConnector(_rooms[6][1], Connector.direction.north, _rooms[6][2]);
            Room.removeConnector(_rooms[7][1], Connector.direction.north, _rooms[7][2]);
            Room.removeConnector(_rooms[8][1], Connector.direction.north, _rooms[8][2]);
        }

        private void setupReqTreasureRoom1(Point roomLoc)
        {
            //Treasures
            Treasury.Treasures.AddCardToDeck addCard = new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT));
            Treasury.Equipment.Items.Wirecutters wirecutters = new Treasury.Equipment.Items.Wirecutters();
            Treasury.Treasures.Gold gold = new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_CHEST_MIN, DEFAULT_GOLD_FROM_CHEST_MAX));

            List<Treasury.Treasure> treasures = new List<Treasury.Treasure>();
            treasures.Add(addCard);
            treasures.Add(gold);
            treasures.Add(wirecutters);

            Treasury.Loot loot = new Treasury.Loot(UserInterface.Menus.LootMenu.CHEST_LOOT, treasures);

            //The rest
            Rooms.TreasureRoom treasureRoom = new Rooms.TreasureRoom();
            treasureRoom.setRoomTreasure(loot);
            replaceRoom(treasureRoom, roomLoc);
        }

        private void setupReqCombatRoom(Point roomLoc)
        {
            //Combat
            Combat.Encounters.TwoFanblades combat = new Combat.Encounters.TwoFanblades();

            //Loot
            Treasury.Loot loot = new Treasury.Loot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
            loot.addTreasure(new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT)));
            loot.addTreasure(new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)));

            //Put it together
            combat.setLoot(loot);
            Rooms.CombatRoom combatRoom = new Rooms.CombatRoom(combat);
            replaceRoom(combatRoom, roomLoc);
        }

        private void setupReqTreasureRoom2(Point roomLoc)
        {
            //Treasures
            Treasury.Treasures.AddCardToDeck addCard = new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT));
            Treasury.Equipment.Items.Firewood firewood = new Treasury.Equipment.Items.Firewood();
            Treasury.Treasures.Gold gold = new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_CHEST_MIN, DEFAULT_GOLD_FROM_CHEST_MAX));

            List<Treasury.Treasure> treasures = new List<Treasury.Treasure>();
            treasures.Add(addCard);
            treasures.Add(gold);
            treasures.Add(firewood);

            Treasury.Loot loot = new Treasury.Loot(UserInterface.Menus.LootMenu.CHEST_LOOT, treasures);

            //The rest
            Rooms.TreasureRoom treasureRoom = new Rooms.TreasureRoom();
            treasureRoom.setRoomTreasure(loot);
            replaceRoom(treasureRoom, roomLoc);
        }

        private void setupReqStoryEventRoom1(Point roomLoc)
        {
            //Corpse
            Treasury.Equipment.Items.Corpse corpse = new Treasury.Equipment.Items.Corpse("Victim", new Characters.Hero(new Characters.PartyBuffs.Cardbearer()));
            Treasury.Loot loot = new Treasury.Loot("Remains:", new List<Treasury.Treasure>() { corpse });

            //Set up choices
            List<Events.Choice> choices = new List<Events.Choice>();
            Events.Choice choice1_bring = new Events.Choice(Story.StoryConstants.INTRO_F3_CORPSE_ROOM_CHOICE_1);
            Events.Choice choice2_leave = new Events.Choice(Story.StoryConstants.INTRO_F3_CORPSE_ROOM_CHOICE_2);
            choices.Add(choice2_leave);
            choices.Add(choice1_bring); //Backwards order to display properly

            choice1_bring.setResult(loot);
            choice1_bring.setExtraInfo(Treasury.Equipment.Items.Corpse.getExtraInfo("Victim"));

            //Event
            Events.Happening storyEvent = new Events.Happening(getReqStoryEventWriting1());
            storyEvent.setChoices(choices);

            //Room
            Rooms.StoryRoom storyEventRoom = new Rooms.StoryRoom();
            storyEventRoom.setRoomEvent(storyEvent);
            replaceRoom(storyEventRoom, roomLoc);
        }

        private void setupReqMinibossRoom(Point roomLoc)
        {
            //Combat
            Combat.Encounters.Minibosses.CrawlipedeEncounter miniboss = new Combat.Encounters.Minibosses.CrawlipedeEncounter();

            //Loot
            Treasury.Loot loot = new Treasury.Loot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
            loot.addTreasure(new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_MINIBOSS_CHOICE_AMOUNT,
                false, true, DeckBuilder.CardEnums.CardRarity.RARE)));
            loot.addTreasure(new Treasury.Treasures.Gold(Game1.randint(MINIBOSS_GOLD_FROM_COMBAT_MIN, MINIBOSS_GOLD_FROM_COMBAT_MAX)));
            loot.addTreasure(new Treasury.Equipment.Items.SoulStone(Treasury.Equipment.Items.SoulStone.soulSize.greater));

            //Put it together
            miniboss.setLoot(loot);
            Rooms.MinibossRoom minibossRoom = new Rooms.MinibossRoom(miniboss);
            replaceRoom(minibossRoom, roomLoc);
        }

        private void setupReqStoryEventRoom2(Point roomLoc)
        {
            //Blessing
            Treasury.Loot loot = new Treasury.Loot("", new List<Treasury.Treasure>() { new Treasury.Treasures.Blessings.HolySymbol() });

            //Set up choices
            List<Events.Choice> choices = new List<Events.Choice>();
            Events.Choice onlyChoice = new Events.Choice(Story.StoryConstants.INTRO_F3_MINIBOSS_DEFEATED_CHOICE);
            choices.Add(onlyChoice);

            onlyChoice.setResult(loot);
            onlyChoice.setExtraInfo(Treasury.Treasures.Blessings.HolySymbol.getExtraInfo());

            //Event
            Events.Happening storyEvent = new Events.Happening(getReqStoryEventWriting2());
            storyEvent.setChoices(choices);

            //Room
            Rooms.StoryRoom storyEventRoom = new Rooms.StoryRoom();
            storyEventRoom.setRoomEvent(storyEvent);
            replaceRoom(storyEventRoom, roomLoc);
        }

        private void setupReqKeyRoom(Point roomLoc)
        {
            //Treasures
            Treasury.Treasures.AddCardToDeck addCard = new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT));
            Treasury.Treasures.Gold gold = new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_CHEST_MIN, DEFAULT_GOLD_FROM_CHEST_MAX));

            //The rest
            Rooms.KeyRoom treasureRoom = new Rooms.KeyRoom(new Treasury.Equipment.Key(Treasury.Equipment.Key.keyColor.blue));
            treasureRoom.addRoomTreasure(addCard);
            treasureRoom.addRoomTreasure(gold);
            replaceRoom(treasureRoom, roomLoc);
        }

        private void setupReqTrapEventRoom(Point roomLoc)
        {
            Rooms.EventRoom trapRoom = new Rooms.EventRoom(new Events.RandomEvents.TrapEvents.TripwireBladedHallway());
            replaceRoom(trapRoom, roomLoc);
        }

        private void setupOptionalCombatRooms(Point roomLoc1, Point roomLoc2, Point roomLoc3, Point roomLoc4)
        {
            //Randomize them
            List<Point> roomLocs = new List<Point>();
            roomLocs.Add(roomLoc1);
            roomLocs.Insert(Game1.randint(0, 1), roomLoc2);
            roomLocs.Insert(Game1.randint(0, 2), roomLoc3);
            roomLocs.Insert(Game1.randint(0, 3), roomLoc4);

            /*____________________.Combat Room 1._____________________*/
            //Combat
            Combat.Encounters.TwoFanblades combat1 = new Combat.Encounters.TwoFanblades();

            //Loot
            Treasury.Loot loot1 = new Treasury.Loot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
            loot1.addTreasure(new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT)));
            loot1.addTreasure(new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)));

            //Put it together
            combat1.setLoot(loot1);
            Rooms.CombatRoom combatRoom1 = new Rooms.CombatRoom(combat1);
            replaceRoom(combatRoom1, roomLocs[0]);

            /*____________________.Combat Room 2._____________________*/
            //Combat
            Combat.Encounters.TwoFanblades combat2 = new Combat.Encounters.TwoFanblades();

            //Loot
            Treasury.Loot loot2 = new Treasury.Loot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
            loot2.addTreasure(new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT)));
            loot2.addTreasure(new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)));

            //Put it together
            combat2.setLoot(loot2);
            Rooms.CombatRoom combatRoom2 = new Rooms.CombatRoom(combat2);
            replaceRoom(combatRoom2, roomLocs[1]);

            /*____________________.Combat Room 3._____________________*/
            //Combat
            Combat.Encounters.ThreeCrawlers combat3 = new Combat.Encounters.ThreeCrawlers();

            //Loot
            Treasury.Loot loot3 = new Treasury.Loot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
            loot3.addTreasure(new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT)));
            loot3.addTreasure(new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)));

            //Put it together
            combat3.setLoot(loot3);
            Rooms.CombatRoom combatRoom3 = new Rooms.CombatRoom(combat3);
            replaceRoom(combatRoom3, roomLocs[2]);

            /*____________________.Combat Room 4._____________________*/
            //Combat
            Combat.Encounters.FourCrawlers combat4 = new Combat.Encounters.FourCrawlers();

            //Loot
            Treasury.Loot loot4 = new Treasury.Loot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
            loot4.addTreasure(new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT)));
            loot4.addTreasure(new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)));

            //Put it together
            combat4.setLoot(loot4);
            Rooms.CombatRoom combatRoom4 = new Rooms.CombatRoom(combat4);
            replaceRoom(combatRoom4, roomLocs[3]);
        }

        private void setupOptionalTreasureRoom(Point roomLoc)
        {
            //Treasures
            Treasury.Treasures.AddCardToDeck addCard = new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT));
            Treasury.Equipment.Items.DeployableCover deployableCover = new Treasury.Equipment.Items.DeployableCover();
            Treasury.Treasures.Gold gold = new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_CHEST_MIN, DEFAULT_GOLD_FROM_CHEST_MAX));

            List<Treasury.Treasure> treasures = new List<Treasury.Treasure>();
            treasures.Add(addCard);
            treasures.Add(gold);
            treasures.Add(deployableCover);

            Treasury.Loot loot = new Treasury.Loot(UserInterface.Menus.LootMenu.CHEST_LOOT, treasures);

            //The rest
            Rooms.TreasureRoom treasureRoom = new Rooms.TreasureRoom();
            treasureRoom.setRoomTreasure(loot);
            replaceRoom(treasureRoom, roomLoc);
        }

        private void setupOptionalTrapEventRoom(Point roomLoc)
        {
            Rooms.EventRoom trapRoom = new Rooms.EventRoom(new Events.RandomEvents.TrapEvents.PerpetualDarts());
            replaceRoom(trapRoom, roomLoc);
        }

        private void setupOptionalEventRoom(Point roomLoc)
        {
            Rooms.EventRoom eventRoom = new Rooms.EventRoom(new Events.RandomEvents.CommonEvents.BarracksTrident());
            replaceRoom(eventRoom, roomLoc);
        }

        private List<String> getReqStoryEventWriting1()
        {
            List<String> eventStory = new List<string>();

            eventStory.Add(Story.StoryConstants.INTRO_F3_CORPSE_ROOM_LINE_1);
            eventStory.Add(Story.StoryConstants.INTRO_F3_CORPSE_ROOM_LINE_2);
            eventStory.Add(Story.StoryConstants.INTRO_F3_CORPSE_ROOM_LINE_3);
            eventStory.Add(Story.StoryConstants.INTRO_F3_CORPSE_ROOM_LINE_4);
            eventStory.Add(Story.StoryConstants.INTRO_F3_CORPSE_ROOM_LINE_5);
            eventStory.Add(Story.StoryConstants.INTRO_F3_CORPSE_ROOM_LINE_6);
            eventStory.Add(Story.StoryConstants.INTRO_F3_CORPSE_ROOM_LINE_7);
            eventStory.Add(Story.StoryConstants.INTRO_F3_CORPSE_ROOM_LINE_8);
            eventStory.Add(Story.StoryConstants.INTRO_F3_CORPSE_ROOM_LINE_9);

            return eventStory;
        }

        private List<String> getReqStoryEventWriting2()
        {
            List<String> eventStory = new List<string>();

            eventStory.Add(Story.StoryConstants.INTRO_F3_MINIBOSS_DEFEATED_LINE_1);
            eventStory.Add(Story.StoryConstants.INTRO_F3_MINIBOSS_DEFEATED_LINE_2);
            eventStory.Add(Story.StoryConstants.INTRO_F3_MINIBOSS_DEFEATED_LINE_3);
            eventStory.Add(Story.StoryConstants.INTRO_F3_MINIBOSS_DEFEATED_LINE_4);
            eventStory.Add(Story.StoryConstants.INTRO_F3_MINIBOSS_DEFEATED_LINE_5);
            eventStory.Add(Story.StoryConstants.INTRO_F3_MINIBOSS_DEFEATED_LINE_6);
            eventStory.Add(Story.StoryConstants.INTRO_F3_MINIBOSS_DEFEATED_LINE_7);
            eventStory.Add(Story.StoryConstants.INTRO_F3_MINIBOSS_DEFEATED_LINE_8);
            eventStory.Add(Story.StoryConstants.INTRO_F3_MINIBOSS_DEFEATED_LINE_9);
            eventStory.Add(Story.StoryConstants.INTRO_F3_MINIBOSS_DEFEATED_LINE_10);
            eventStory.Add(Story.StoryConstants.INTRO_F3_MINIBOSS_DEFEATED_LINE_11);
            eventStory.Add(Story.StoryConstants.INTRO_F3_MINIBOSS_DEFEATED_LINE_12);
            eventStory.Add(Story.StoryConstants.INTRO_F3_MINIBOSS_DEFEATED_LINE_13);
            eventStory.Add(Story.StoryConstants.INTRO_F3_MINIBOSS_DEFEATED_LINE_14);
            eventStory.Add(Story.StoryConstants.INTRO_F3_MINIBOSS_DEFEATED_LINE_15);

            return eventStory;
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
            
            
            int rand = Game1.randint(1, RANDOM_ENCOUNTER_WEIGHT_TWO_FANBLADES + RANDOM_ENCOUNTER_WEIGHT_THREE_CRAWLERS + RANDOM_ENCOUNTER_WEIGHT_FOUR_CRAWLERS +
            RANDOM_ENCOUNTER_WEIGHT_SINGLE_FANBLADE);

            if (rand <= RANDOM_ENCOUNTER_WEIGHT_TWO_FANBLADES)
            {
                //Combat
                Combat.Encounters.TwoFanblades twoFanblades = new Combat.Encounters.TwoFanblades();

                //Loot
                Treasury.Loot twoFanbladesLoot = new Treasury.Loot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
                twoFanbladesLoot.addTreasure(new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT)));
                twoFanbladesLoot.addTreasure(new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)));

                twoFanblades.setLoot(twoFanbladesLoot);
                return twoFanblades;
            }
            else if (rand <= RANDOM_ENCOUNTER_WEIGHT_TWO_FANBLADES + RANDOM_ENCOUNTER_WEIGHT_THREE_CRAWLERS)
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
            else if (rand <= RANDOM_ENCOUNTER_WEIGHT_TWO_FANBLADES + RANDOM_ENCOUNTER_WEIGHT_THREE_CRAWLERS + RANDOM_ENCOUNTER_WEIGHT_FOUR_CRAWLERS)
            {
                //Combat
                Combat.Encounters.FourCrawlers fourCrawlers = new Combat.Encounters.FourCrawlers();

                //Loot
                Treasury.Loot fourCrawlersLoot = new Treasury.Loot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
                fourCrawlersLoot.addTreasure(new Treasury.Treasures.AddCardToDeck(Treasury.Treasures.AddCardToDeck.getRandomCards(Treasury.LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT)));
                fourCrawlersLoot.addTreasure(new Treasury.Treasures.Gold(Game1.randint(DEFAULT_GOLD_FROM_COMBAT_MIN, DEFAULT_GOLD_FROM_COMBAT_MAX)));

                fourCrawlers.setLoot(fourCrawlersLoot);
                return fourCrawlers;
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

        public override Combat.Encounter getRestEncounter()
        {
            return null; //It is impossible to rest more than once on this floor
        }

        protected override int getRemainingRestEncounters()
        {
            return 0; //It is impossible to rest than once on this floor
        }
    }
}
