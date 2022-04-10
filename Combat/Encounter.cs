using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat
{
    abstract class Encounter
    {
        protected List<Enemy> _enemies;

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

        public abstract void initialize();
    }
}
