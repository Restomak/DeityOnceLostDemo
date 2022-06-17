using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury
{
    public class Loot
    {
        List<Treasure> _treasures;
        UserInterface.Menus.LootMenu _lootMenu;

        public Loot(String lootTitle)
        {
            _treasures = new List<Treasure>();

            _lootMenu = new UserInterface.Menus.LootMenu(this, lootTitle);
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



        public static Loot generateDefaultLoot(String lootTitle)
        {
            Loot defaultLoot = new Loot(lootTitle);
            //FIXIT add chance for other treasures when implemented
            //FIXIT add money
            List<DeckBuilder.Card> defaultRandomCards = Treasures.AddCardToDeck.getRandomCards(LootConstants.ADDCARD_DEFAULT_CHOICE_AMOUNT);

            defaultLoot.addTreasure(new Treasures.AddCardToDeck(defaultRandomCards));

            return defaultLoot;
        }
    }
}
