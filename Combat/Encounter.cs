using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat
{
    public abstract class Encounter
    {
        protected List<Enemy> _enemies;
        protected Treasury.Loot _loot;

        public Encounter()
        {
            _enemies = new List<Enemy>();

            initialize();
        }

        //Getters
        public List<Enemy> getEnemies()
        {
            return _enemies;
        }

        public void determineIntents(Characters.Champion champ, List<PartyMember> party)
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.getAIPattern().determineIntents(champ, party);
            }
        }

        public void resetDefense()
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.resetDefense();
            }
        }

        public bool areAllEnemiesDefeated()
        {
            foreach (Enemy enemy in _enemies)
            {
                if (!enemy.getDowned())
                {
                    return false;
                }
            }

            return true;
        }

        public Treasury.Loot getLoot()
        {
            if (_loot != null)
            {
                return _loot;
            }

            return Treasury.Loot.generateDefaultLoot(UserInterface.Menus.LootMenu.COMBAT_LOOT);
        }

        public void setLoot(Treasury.Loot loot)
        {
            _loot = loot;
        }

        public abstract void initialize();
    }
}
