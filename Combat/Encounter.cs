using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat
{
    abstract class Encounter
    {
        List<Enemy> _enemies;

        public Encounter(List<Enemy> enemies)
        {
            _enemies = enemies;
        }

        //Getters
        public List<Enemy> getEnemies()
        {
            return _enemies;
        }
    }
}
