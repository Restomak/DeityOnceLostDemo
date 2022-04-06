using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat
{
    abstract class Unit
    {
        String _name;
        int _currentHP, _maxHP;
        bool _isEnemy;

        public Unit(String name, int hitPoints, bool enemy = false)
        {
            _name = name;
            _currentHP = hitPoints;
            _maxHP = hitPoints;
            _isEnemy = enemy;
        }
    }
}
