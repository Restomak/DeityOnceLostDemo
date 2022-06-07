using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Characters
{
    public class Champion : Combat.Unit
    {
        public const int DEFAULT_DIVINITY = 4;
        public const int CHAMPION_HERO_HP_MODIFIER = 3;

        static int _divinity, _nextTurnDivinity, _runDivinity;
        static Hero _hero;
        static DeckBuilder.Deck _deck;
        static int _cardDraw, _nextTurnCardDraw;

        public Champion(Hero hero) : base (hero.getName(), hero.getMaxHP() * CHAMPION_HERO_HP_MODIFIER)
        {
            _hero = hero;
            _deck = new DeckBuilder.Deck(hero.getDefaultCards());
            _nextTurnDivinity = DEFAULT_DIVINITY;
            _runDivinity = DEFAULT_DIVINITY;
            _divinity = DEFAULT_DIVINITY;
            _nextTurnCardDraw = DeckBuilder.Deck.DEFAULT_DRAW_ON_TURN_START;
            _cardDraw = DeckBuilder.Deck.DEFAULT_DRAW_ON_TURN_START;
        }

        public void resetDivinity()
        {
            _divinity = _nextTurnDivinity;
            resetNextTurnDivinity(); //because buffs/debuffs that affect divinity last only a single round (& if that changes I'll add a check)
        }

        public void resetNextTurnDivinity()
        {
            _nextTurnDivinity = _runDivinity;
        }

        public void resetRunDivinity()
        {
            _runDivinity = DEFAULT_DIVINITY;
        }

        public void resetNextTurnCardDraw()
        {
            _nextTurnCardDraw = _cardDraw;
        }

        public void resetRunCardDraw()
        {
            _cardDraw = DeckBuilder.Deck.DEFAULT_DRAW_ON_TURN_START;
        }



        //Getters
        public DeckBuilder.Deck getDeck()
        {
            return _deck;
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
        public int getRunDivinity()
        {
            return _runDivinity;
        }

        /// <summary>
        /// Returns the card draw for the next turn. If calling this while drawing cards
        /// on turn start, be sure to reset using the boolean.
        /// </summary>
        public int getCardDraw(bool reset)
        {
            int cardDraw = _nextTurnCardDraw;
            if (reset)
            {
                resetNextTurnDivinity();
            }
            return cardDraw;
        }

        //Combat stat reset functions
        public override void resetStrength()
        {
            _strength = 0; //FIXIT add check for things that alter it when they exist
        }
        public override void resetDexterity()
        {
            _dexterity = 0; //FIXIT add check for things that alter it when they exist
        }
        public override void resetResilience()
        {
            _resilience = 0; //FIXIT add check for things that alter it when they exist
        }


        
        /// <summary>
        /// Used when calculating damage shown on enemy intents
        /// </summary>
        public int calculateDamageIntakeFromEnemyAttack(int enemyDamage, bool affectedByResilience = true)
        {
            if (affectedByResilience)
            {
                enemyDamage -= _resilience;
            }

            return enemyDamage;
        }

        /// <summary>
        /// Damage taken is affected by resilience by default. Things that bypass resilience should set affectedByResilience to false
        /// </summary>
        public override void takeDamage(int damage, bool affectedByResilience = true)
        {
            if (affectedByResilience)
            {
                damage -= _resilience;
            }

            if (damage > 0)
            {
                if (_defense > 0)
                {
                    _defense -= damage;

                    if (_defense < 0)
                    {
                        damage = -_defense;
                    }
                    else
                    {
                        damage = 0;
                    }
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

        /// <summary>
        /// Spends the champion's divinity (likely from a card)
        /// </summary>
        public void spendDivinity(int amount)
        {
            _divinity -= amount;
        }

        /// <summary>
        /// Affects the amount of Divinity the champion will start with next round.
        /// </summary>
        public void affectDivinity(int amount)
        {
            _nextTurnDivinity += amount;
        }

        /// <summary>
        /// Affects the amount of Divinity the champion will start with each round for the whole run.
        /// </summary>
        public void affectDivinityForEntireRun(int amount)
        {
            _nextTurnDivinity += amount;
            _runDivinity += amount;
        }

        /// <summary>
        /// Affects the amount of card draw the champion will have the next round.
        /// </summary>
        public void affectCardDraw(int amount)
        {
            _nextTurnCardDraw += amount;
        }

        /// <summary>
        /// Affects the amount of card draw the champion will have for the whole run.
        /// </summary>
        public void affectCardDrawForEntireRun(int amount)
        {
            _nextTurnCardDraw += amount;
            _cardDraw += amount;
        }
    }
}
