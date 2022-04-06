using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Characters
{
    public class Hero
    {
        List<DeckBuilder.Card> _defaultCards;
        String _name, _they, _them, _their, _theirs;
        int _maxHP;

        public Hero()
        {
            _defaultCards = new List<DeckBuilder.Card>();

            generateRandomName();
            //generateRandomApperance();
            generateRandomPronouns();
            //generateRandomTraits();
            generateRandomDefaultDeck();
            generateRandomHP();
        }



        //Getters & Setters
        public String getName()
        {
            return _name;
        }
        public String getPronoun_they()
        {
            return _they;
        }
        public String getPronoun_them()
        {
            return _them;
        }
        public String getPronoun_their()
        {
            return _their;
        }
        public String getPronoun_theirs()
        {
            return _theirs;
        }
        public void setName(String name)
        {
            _name = name;
        }
        public void setPronouns_they(String they)
        {
            _they = they;
        }
        public void setPronouns_them(String them)
        {
            _them = them;
        }
        public void setPronouns_their(String their)
        {
            _their = their;
        }
        public void setPronouns_theirs(String theirs)
        {
            _theirs = theirs;
        }



        private void generateRandomName()
        {
            _name = Names.heroNames[Game1.randint(0, Names.heroNames.Count - 1)];
        }

        private void generateRandomPronouns()
        {
            int randomPronoun = Game1.randint(1, HeroConstants.PRONOUN_WEIGHT_HE + HeroConstants.PRONOUN_WEIGHT_SHE + HeroConstants.PRONOUN_WEIGHT_THEY + HeroConstants.PRONOUN_WEIGHT_XE + HeroConstants.PRONOUN_WEIGHT_VE);
            if (randomPronoun <= HeroConstants.PRONOUN_WEIGHT_HE)
            {
                _they = HeroConstants.THEY_HEHIM;
                _them = HeroConstants.THEM_HEHIM;
                _their = HeroConstants.THEIR_HEHIM;
                _theirs = HeroConstants.THEIRS_HEHIM;
            }
            else if (randomPronoun <= HeroConstants.PRONOUN_WEIGHT_HE + HeroConstants.PRONOUN_WEIGHT_SHE)
            {
                _they = HeroConstants.THEY_SHEHER;
                _them = HeroConstants.THEM_SHEHER;
                _their = HeroConstants.THEIR_SHEHER;
                _theirs = HeroConstants.THEIRS_SHEHER;
            }
            else if (randomPronoun <= HeroConstants.PRONOUN_WEIGHT_HE + HeroConstants.PRONOUN_WEIGHT_SHE + HeroConstants.PRONOUN_WEIGHT_THEY)
            {
                _they = HeroConstants.THEY_THEYTHEM;
                _them = HeroConstants.THEM_THEYTHEM;
                _their = HeroConstants.THEIR_THEYTHEM;
                _theirs = HeroConstants.THEIRS_THEYTHEM;
            }
            else if (randomPronoun <= HeroConstants.PRONOUN_WEIGHT_HE + HeroConstants.PRONOUN_WEIGHT_SHE + HeroConstants.PRONOUN_WEIGHT_THEY + HeroConstants.PRONOUN_WEIGHT_XE)
            {
                _they = HeroConstants.THEY_XEXIR;
                _them = HeroConstants.THEM_XEXIR;
                _their = HeroConstants.THEIR_XEXIR;
                _theirs = HeroConstants.THEIRS_XEXIR;
            }
            else if (randomPronoun <= HeroConstants.PRONOUN_WEIGHT_HE + HeroConstants.PRONOUN_WEIGHT_SHE + HeroConstants.PRONOUN_WEIGHT_THEY + HeroConstants.PRONOUN_WEIGHT_XE + HeroConstants.PRONOUN_WEIGHT_VE)
            {
                _they = HeroConstants.THEY_VEVER;
                _them = HeroConstants.THEM_VEVER;
                _their = HeroConstants.THEIR_VEVER;
                _theirs = HeroConstants.THEIRS_VEVER;
            }
            else
            {
                Game1.errorLog.Add("New Hero created & pronoun roll out of bounds: defaulting to they/them");
                _they = HeroConstants.THEY_THEYTHEM;
                _them = HeroConstants.THEM_THEYTHEM;
                _their = HeroConstants.THEIR_THEYTHEM;
                _theirs = HeroConstants.THEIRS_THEYTHEM;
            }
        }

        private void generateRandomDefaultDeck()
        {
            List<DeckBuilder.Card> allDefaultCards = Game1.getAllCards().getAllCardsByRarity(DeckBuilder.CardEnums.CardRarity.DEFAULT);
            List<DeckBuilder.Card> allDefaultAttackCards = Game1.getAllCards().getAllCardsByRarity(DeckBuilder.CardEnums.CardRarity.DEFAULT, DeckBuilder.CardEnums.CardType.ATTACK);
            List<DeckBuilder.Card> allDefaultSkillCards = Game1.getAllCards().getAllCardsByRarity(DeckBuilder.CardEnums.CardRarity.DEFAULT, DeckBuilder.CardEnums.CardType.SKILL);
            List<DeckBuilder.Card> allDefaultOtherCards = new List<DeckBuilder.Card>();
            foreach (DeckBuilder.Card card in allDefaultCards)
            {
                if (!allDefaultAttackCards.Contains(card) && !allDefaultSkillCards.Contains(card))
                {
                    allDefaultOtherCards.Add(card); //FIXIT make sure there's no weird reference errors
                }
            }
            int numDefaultCards = allDefaultCards.Count;
            int numDefaultAttackCards = allDefaultAttackCards.Count;
            int numDefaultSkillCards = allDefaultSkillCards.Count;
            int numDefaultOtherCards = allDefaultOtherCards.Count;

            if (numDefaultCards == 0)
            {
                Game1.errorLog.Add("No default cards exist when generating default deck");
            }

            if (HeroConstants.NUM_ATTACK_CARDS_IN_DEFAULT_DECK > 0)
            {
                bool specializedDeck = (Game1.randint(1, 100) <= HeroConstants.CHANCE_AT_SPECIALIZED_DECK);
                DeckBuilder.Card specializedCard = allDefaultAttackCards[Game1.randint(0, numDefaultAttackCards - 1)];
                _defaultCards.Add(specializedCard);
                for (int i = 1; i < HeroConstants.NUM_ATTACK_CARDS_IN_DEFAULT_DECK; i++)
                {
                    if (specializedDeck && Game1.randint(1, 100) <= HeroConstants.CHANCE_AT_SPECIALIZED_CARD)
                    {
                        _defaultCards.Add(specializedCard);
                    }
                    else
                    {
                        _defaultCards.Add(allDefaultAttackCards[Game1.randint(0, numDefaultAttackCards - 1)]);
                    }
                }
            }

            if (HeroConstants.NUM_SKILL_CARDS_IN_DEFAULT_DECK > 0)
            {
                bool specializedDeck = (Game1.randint(1, 100) <= HeroConstants.CHANCE_AT_SPECIALIZED_DECK);
                DeckBuilder.Card specializedCard = allDefaultSkillCards[Game1.randint(0, numDefaultSkillCards - 1)];
                _defaultCards.Add(specializedCard);
                for (int i = 1; i < HeroConstants.NUM_SKILL_CARDS_IN_DEFAULT_DECK; i++)
                {
                    if (specializedDeck && Game1.randint(1, 100) <= HeroConstants.CHANCE_AT_SPECIALIZED_CARD)
                    {
                        _defaultCards.Add(specializedCard);
                    }
                    else
                    {
                        _defaultCards.Add(allDefaultSkillCards[Game1.randint(0, numDefaultSkillCards - 1)]);
                    }
                }
            }

            if (HeroConstants.NUM_OTHER_CARD_ROLLS_IN_DEFAULT_DECK > 0)
            {
                //first check if any other default cards will even go in the deck
                int rolledOtherCards = 0;
                for (int i = 0; i < HeroConstants.NUM_OTHER_CARD_ROLLS_IN_DEFAULT_DECK; i++)
                {
                    if (Game1.randint(1, 100) <= HeroConstants.CHANCE_AT_SPECIALIZED_DECK)
                    {
                        rolledOtherCards++;
                    }
                }

                //if yes, standard procedure
                if (rolledOtherCards > 0)
                {
                    bool specializedDeck = (Game1.randint(1, 100) <= HeroConstants.CHANCE_AT_SPECIALIZED_DECK);
                    DeckBuilder.Card specializedCard = allDefaultOtherCards[Game1.randint(0, numDefaultOtherCards - 1)];
                    _defaultCards.Add(specializedCard);
                    for (int i = 1; i < rolledOtherCards; i++)
                    {
                        if (specializedDeck && Game1.randint(1, 100) <= HeroConstants.CHANCE_AT_SPECIALIZED_CARD)
                        {
                            _defaultCards.Add(specializedCard);
                        }
                        else
                        {
                            _defaultCards.Add(allDefaultOtherCards[Game1.randint(0, numDefaultOtherCards - 1)]);
                        }
                    }
                }
            }

            int numCardsSoFar = _defaultCards.Count;
            int numCardsTotal = Game1.randint(HeroConstants.MIN_TOTAL_NUM_CARDS_IN_DEFAULT_DECK, HeroConstants.MAX_TOTAL_NUM_CARDS_IN_DEFAULT_DECK);
            //Add any random default card until deck is considered full
            for (int i = numCardsSoFar; i < numCardsTotal; i++)
            {
                _defaultCards.Add(allDefaultCards[Game1.randint(0, numDefaultCards - 1)]);
            }

            String log = "New Hero default deck created: ";
            foreach (DeckBuilder.Card card in _defaultCards)
            {
                log += card.getName() + ", ";
            }
            Game1.debugLog.Add(log);
        }

        private void generateRandomHP()
        {
            int maxHP_1 = Game1.randint(HeroConstants.HERO_MAX_HP_MIN, HeroConstants.HERO_MAX_HP_MAX);
            int maxHP_2 = Game1.randint(HeroConstants.HERO_MAX_HP_MIN, HeroConstants.HERO_MAX_HP_MAX);
            _maxHP = (maxHP_1 + maxHP_2) / 2; //tends towards the average rather than the extremes
        }

        public List<DeckBuilder.Card> getDefaultCards()
        {
            return _defaultCards;
        }

        public int getMaxHP()
        {
            return _maxHP;
        }
    }
}
