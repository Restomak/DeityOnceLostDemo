using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Characters
{
    public class Champion
    {
        public const int DEFAULT_DIVINITY = 4;

        static int _divinity;
        static Hero _hero;
        static int _currentHP, _maxHP;
        static DeckBuilder.Deck deck;

        //stats
        static int _strength, _dexterity, _constitution;

        public Champion(Hero hero)
        {
            _maxHP = hero.getMaxHP() * 3;
            _currentHP = _maxHP;
            _hero = hero;
            deck = new DeckBuilder.Deck(hero.getDefaultCards());
            _divinity = DEFAULT_DIVINITY;
            _strength = 0;
            _dexterity = 0;
            _constitution = 0;
        }

        public void resetDivinity()
        {
            _divinity = DEFAULT_DIVINITY;
        }



        //Getters
        public DeckBuilder.Deck getDeck()
        {
            return deck;
        }
        public int getDivinity()
        {
            return _divinity;
        }
        public int getStrength()
        {
            return _strength;
        }
        public int getDexterity()
        {
            return _dexterity;
        }
        public int getConstitution()
        {
            return _constitution;
        }
        public Hero getHero()
        {
            return _hero;
        }
    }
}
