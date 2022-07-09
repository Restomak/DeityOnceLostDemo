using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Characters
{
    /// <summary>
    /// The unit class for the chosen champion of a dungeon run (or in the case of the
    /// tutorial dungeon, the one that is forced upon the player randomly). Stores many
    /// combat details unique to the champion, such as the current turn's Divinity.
    /// </summary>
    public class Champion : Combat.Unit
    {
        public const int DEFAULT_DIVINITY = 4;
        public const int CHAMPION_HERO_HP_MODIFIER = 3;

        int _divinity, _nextTurnDivinity, _runDivinity;
        Hero _hero;
        DeckBuilder.Deck _deck;
        int _cardDraw, _nextTurnCardDraw;
        bool _dealDoubleDamage;
        int _numCardsToPreserve;

        public Champion(Hero hero) : base (hero.getName(), hero.getMaxHP() * CHAMPION_HERO_HP_MODIFIER)
        {
            _hero = hero;
            _deck = new DeckBuilder.Deck(hero.getDefaultCards());
            _nextTurnDivinity = DEFAULT_DIVINITY;
            _runDivinity = DEFAULT_DIVINITY;
            _divinity = DEFAULT_DIVINITY;
            _nextTurnCardDraw = DeckBuilder.Deck.DEFAULT_DRAW_ON_TURN_START;
            _cardDraw = DeckBuilder.Deck.DEFAULT_DRAW_ON_TURN_START;

            //Buff stuff
            _dealDoubleDamage = false;
            _numCardsToPreserve = 0;
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

        public void setDoubleDamage(bool doubleDamage)
        {
            _dealDoubleDamage = doubleDamage;
        }

        public void setNumCardsToPreserve(int numCards)
        {
            _numCardsToPreserve = numCards;
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
        public int getNumCardsToPreserve()
        {
            return _numCardsToPreserve;
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



        public override void resetBuffs()
        {
            resetStrength();
            resetDexterity();
            resetResilience();
            _feeble = false;
            _sluggish = false;
            _vulnerable = false;

            _buffs.Clear();
        }

        public override void gainBuff(Combat.Buff buff)
        {
            //When blessings etc are implemented, affected buff code goes here

            base.gainBuff(buff);
        }

        /// <summary>
        /// Called by cards needing to determine the amount of defense the Champion would
        /// gain by using the card, so that the information can be displayed to the player.
        /// </summary>
        public int getDefenseAffectedByBuffs(int defense)
        {
            double newDefense = defense + _dexterity;

            if (_sluggish)
            {
                newDefense = newDefense * Combat.Buff.SLUGGISH_MODIFIER;
            }

            return (int)(Math.Round(newDefense));
        }

        /// <summary>
        /// Called by cards needing to determine the amount of damage that would be caused,
        /// either to a target or in general, so that the information can be displayed to
        /// the player.
        /// </summary>
        public int getDamageAffectedByBuffs(int damage, Combat.Unit target)
        {
            double newDamage = damage + _strength;

            if (_dealDoubleDamage)
            {
                newDamage *= 2.0;
            }

            if (!_feeble && (target == null || !target.vulnerable()))
            {
                return (int)(Math.Round(newDamage));
            }

            if (_feeble)
            {
                newDamage = newDamage * Combat.Buff.FEEBLE_MODIFIER;
            }

            if (target != null && target.vulnerable())
            {
                newDamage = newDamage * Combat.Buff.VULNERABLE_MODIFIER;
            }

            return (int)(Math.Round(newDamage));
        }



        public void tempSetStrengthTo0()
        {
            _strength = 0;
        }
        public void tempSetDexterityTo0()
        {
            _dexterity = 0;
        }
        public void tempSetResilienceTo0()
        {
            _resilience = 0;
        }

        

        /// <summary>
        /// Damage taken is affected by resilience by default. Things that bypass resilience should set affectedByResilience to false
        /// </summary>
        public override void takeDamage(int damage, bool affectedByResilience = true)
        {
            base.takeDamage(damage, affectedByResilience);

            if (_downed)
            {
                _hero.kill();
            }
        }

        /// <summary>
        /// Spends the champion's divinity (likely from a card). Can be used with a
        /// negative value to gain divinity.
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

        /// <summary>
        /// Affects the champion's maximum HP. When losing max HP, only affects current
        /// HP if it becomes over the maximum. When gaining max HP, also heals for that
        /// amount.
        /// </summary>
        public void affectMaxHP(int amount)
        {
            _maxHP += amount;

            if (amount < 0 && _currentHP > _maxHP)
            {
                _currentHP = _maxHP;
            }
            else if (amount > 0)
            {
                _currentHP += amount;
            }
        }
    }
}
