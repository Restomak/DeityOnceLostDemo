using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury
{
    /// <summary>
    /// Used for storing one or more Treasures that are the result of doing something in
    /// the game (winning a combat, opening a chest, even certain event choices).
    /// </summary>
    public class Loot
    {
        List<Treasure> _treasures;
        UserInterface.Menus.LootMenu _lootMenu;

        public Loot(String lootTitle)
        {
            _treasures = new List<Treasure>();

            _lootMenu = new UserInterface.Menus.LootMenu(this, lootTitle);
        }
        public Loot(String lootTitle, List<Treasure> treasures)
        {
            _treasures = treasures;

            _lootMenu = new UserInterface.Menus.LootMenu(this, lootTitle);

            _lootMenu.setupTreasuresAsClickables();
        }

        //Getters
        public List<Treasure> getTreasures()
        {
            return _treasures;
        }
        public UserInterface.Menus.LootMenu getMenu()
        {
            return _lootMenu;
        }



        public void addTreasure(Treasure treasure)
        {
            _treasures.Add(treasure);
            _lootMenu.setupTreasuresAsClickables();
        }

        /// <summary>
        /// Iterates through the list of stored Treasures and checks their isTaken flag.
        /// Removes any that are set to true, as those have already been looted by the player.
        /// </summary>
        public void removeTreasuresTaken()
        {
            List<Treasure> toRemove = new List<Treasure>();

            for (int i = 0; i < _treasures.Count; i++)
            {
                if (_treasures[i].isTaken())
                {
                    toRemove.Add(_treasures[i]);
                }
            }

            if (toRemove.Count > 0)
            {
                for (int i = 0; i < toRemove.Count; i++)
                {
                    _treasures.Remove(toRemove[i]);
                }
            }
        }



        /// <summary>
        /// Generates a default random Loot that includes (at least currently) only gold and an
        /// AddCardToDeck. The amount of gold is determined by each individual dungeon floor.
        /// </summary>
        public static Loot generateDefaultLoot(String lootTitle)
        {
            Loot defaultLoot = new Loot(lootTitle);
            //FIXIT add chance for other treasures when implemented - maybe items. perhaps have a different function for tutorial dungeon than other dungeons
            List<DeckBuilder.Card> defaultRandomCards = Treasures.AddCardToDeck.getRandomCards(LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT);
            Treasures.Gold money = new Treasures.Gold(Game1.randint(Game1.getDungeonHandler().getCurrentFloor().getDefaultGoldFromCombat_Min(),
                Game1.getDungeonHandler().getCurrentFloor().getDefaultGoldFromCombat_Max()));

            defaultLoot.addTreasure(new Treasures.AddCardToDeck(defaultRandomCards));
            defaultLoot.addTreasure(money);

            String log = "Generated default loot. Cards choices: ";
            foreach (DeckBuilder.Card card in defaultRandomCards)
            {
                log += card.getName() + ", ";
            }
            Game1.debugLog.Add(log);

            return defaultLoot;
        }
    }
}
