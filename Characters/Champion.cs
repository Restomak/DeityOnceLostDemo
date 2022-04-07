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
        int _strength, _dexterity, _resilience;

        public Champion(Hero hero)
        {
            _maxHP = hero.getMaxHP() * 3;
            _currentHP = _maxHP;
            _hero = hero;
            deck = new DeckBuilder.Deck(hero.getDefaultCards());
            _divinity = DEFAULT_DIVINITY;
            _strength = 0;
            _dexterity = 0;
            _resilience = 0;
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
            return _resilience;
        }
        public Hero getHero()
        {
            return _hero;
        }

        //Combat stat reset functions
        public void resetStrength()
        {
            _strength = 0; //FIXIT add check for things that alter it when they exist
        }
        public void resetDexterity()
        {
            _dexterity = 0; //FIXIT add check for things that alter it when they exist
        }
        public void resetResilience()
        {
            _resilience = 0; //FIXIT add check for things that alter it when they exist
        }



        /// <summary>
        /// Damage taken is affected by resilience by default. Things that bypass resilience should set affectedByResilience to false
        /// </summary>
        public void takeDamage(int damage, bool affectedByResilience = true)
        {
            if (affectedByResilience)
            {
                damage -= _resilience;
            }

            _currentHP -= damage;
            if (_currentHP <= 0)
            {
                _currentHP = 0;
                _hero.kill();

                //FIXIT insert logic for swapping champion to a party member & the resulting card loss
            }
        }
    }
}
