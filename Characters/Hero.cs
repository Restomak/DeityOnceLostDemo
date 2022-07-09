using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Characters
{
    /// <summary>
    /// Every friendly character in the game that can be taken on a dungeon run is
    /// considered a Hero, including the Champion. The data in the Hero class
    /// represents the individuality of that character, and stores their name,
    /// pronouns, hitpoints, default deck, appearance, traits, buff they supply
    /// if used as a party member, and so on.
    /// </summary>
    public class Hero
    {
        List<DeckBuilder.Card> _defaultCards;
        String _name, _they, _them, _their, _theirs, _theyre, _theyve;
        int _maxHP;
        bool _dead;
        Treasury.Treasures.PartyBuff _partyMemberBuff;

        public Hero(Treasury.Treasures.PartyBuff partyMemberBuff = null, bool firstChampion = false)
        {
            _dead = false;

            _defaultCards = new List<DeckBuilder.Card>();

            generateRandomPronouns(firstChampion);
            generateRandomName(firstChampion);
            //generateRandomApperance();
            generateRandomTraits(); //for now just defaults stats to 0
            generateRandomDefaultDeck(firstChampion);
            generateRandomHP(firstChampion);

            if (partyMemberBuff == null)
            {
                generateRandomPartyMemberBuff();
            }
            else
            {
                _partyMemberBuff = partyMemberBuff;
            }
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
        public String getPronoun_theyre()
        {
            return _theyre;
        }
        public String getPronoun_theyve()
        {
            return _theyve;
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
        public void setPronouns_theyre(String theyre)
        {
            _theyre = theyre;
        }
        public void setPronouns_theyve(String theyve)
        {
            _theyve = theyve;
        }
        public bool isAlive()
        {
            return !_dead;
        }
        public int getMaxHP()
        {
            return _maxHP;
        }
        public List<DeckBuilder.Card> getDefaultCards()
        {
            return _defaultCards;
        }
        public Treasury.Treasures.PartyBuff getPartyMemberBuff()
        {
            return _partyMemberBuff;
        }

        //Playing with life
        public void kill()
        {
            _dead = true;
        }
        public void bringBackToLife()
        {
            _dead = false;
        }

        

        /// <summary>
        /// Called upon generation of the Hero, and determines their pronouns. Used before
        /// choosing a name, as the name has a chance to be dependent upon the pronouns
        /// that the Hero uses.
        /// </summary>
        private void generateRandomPronouns(bool firstChampion)
        {
            int randomPronoun = Game1.randint(1, HeroConstants.PRONOUN_WEIGHT_HE + HeroConstants.PRONOUN_WEIGHT_SHE + HeroConstants.PRONOUN_WEIGHT_THEY + HeroConstants.PRONOUN_WEIGHT_XE + HeroConstants.PRONOUN_WEIGHT_VE);
            if (randomPronoun <= HeroConstants.PRONOUN_WEIGHT_HE)
            {
                _they = HeroConstants.THEY_HEHIM;
                _them = HeroConstants.THEM_HEHIM;
                _their = HeroConstants.THEIR_HEHIM;
                _theirs = HeroConstants.THEIRS_HEHIM;
                _theyre = HeroConstants.THEYRE_HEHIM;
                _theyve = HeroConstants.THEYVE_HEHIM;
            }
            else if (randomPronoun <= HeroConstants.PRONOUN_WEIGHT_HE + HeroConstants.PRONOUN_WEIGHT_SHE)
            {
                _they = HeroConstants.THEY_SHEHER;
                _them = HeroConstants.THEM_SHEHER;
                _their = HeroConstants.THEIR_SHEHER;
                _theirs = HeroConstants.THEIRS_SHEHER;
                _theyre = HeroConstants.THEYRE_SHEHER;
                _theyve = HeroConstants.THEYVE_SHEHER;
            }
            else if (randomPronoun <= HeroConstants.PRONOUN_WEIGHT_HE + HeroConstants.PRONOUN_WEIGHT_SHE + HeroConstants.PRONOUN_WEIGHT_THEY)
            {
                _they = HeroConstants.THEY_THEYTHEM;
                _them = HeroConstants.THEM_THEYTHEM;
                _their = HeroConstants.THEIR_THEYTHEM;
                _theirs = HeroConstants.THEIRS_THEYTHEM;
                _theyre = HeroConstants.THEYRE_THEYTHEM;
                _theyve = HeroConstants.THEYVE_THEYTHEM;
            }
            else if (randomPronoun <= HeroConstants.PRONOUN_WEIGHT_HE + HeroConstants.PRONOUN_WEIGHT_SHE + HeroConstants.PRONOUN_WEIGHT_THEY + HeroConstants.PRONOUN_WEIGHT_XE)
            {
                _they = HeroConstants.THEY_XEXIR;
                _them = HeroConstants.THEM_XEXIR;
                _their = HeroConstants.THEIR_XEXIR;
                _theirs = HeroConstants.THEIRS_XEXIR;
                _theyre = HeroConstants.THEYRE_XEXIR;
                _theyve = HeroConstants.THEYVE_XEXIR;
            }
            else if (randomPronoun <= HeroConstants.PRONOUN_WEIGHT_HE + HeroConstants.PRONOUN_WEIGHT_SHE + HeroConstants.PRONOUN_WEIGHT_THEY + HeroConstants.PRONOUN_WEIGHT_XE + HeroConstants.PRONOUN_WEIGHT_VE)
            {
                _they = HeroConstants.THEY_VEVER;
                _them = HeroConstants.THEM_VEVER;
                _their = HeroConstants.THEIR_VEVER;
                _theirs = HeroConstants.THEIRS_VEVER;
                _theyre = HeroConstants.THEYRE_VEVER;
                _theyve = HeroConstants.THEYVE_VEVER;
            }
            else
            {
                Game1.addToErrorLog("New Hero created & pronoun roll out of bounds: defaulting to they/them");
                _they = HeroConstants.THEY_THEYTHEM;
                _them = HeroConstants.THEM_THEYTHEM;
                _their = HeroConstants.THEIR_THEYTHEM;
                _theirs = HeroConstants.THEIRS_THEYTHEM;
                _theyre = HeroConstants.THEYRE_THEYTHEM;
                _theyve = HeroConstants.THEYVE_THEYTHEM;
            }

            
            if (firstChampion)
            {
                _they = HeroConstants.THEY_SHEHER;
                _them = HeroConstants.THEM_SHEHER;
                _their = HeroConstants.THEIR_SHEHER;
                _theirs = HeroConstants.THEIRS_SHEHER;
                _theyre = HeroConstants.THEYRE_SHEHER;
                _theyve = HeroConstants.THEYVE_SHEHER;
            }
        }

        /// <summary>
        /// Called upon generation of the Hero, and determines their name. Has a chance of
        /// being dependant upon the Hero's pronouns.
        /// </summary>
        private void generateRandomName(bool firstChampion)
        {
            List<String> names = new List<String>();

            if (Game1.randChance(Names.CHANCE_OF_WHATEVER))
            {
                names.AddRange(Names.heroNames_masc);
                names.AddRange(Names.heroNames_femme);
                names.AddRange(Names.heroNames_nonbinary);
                names.AddRange(Names.heroNames_androgynous);
            }
            else
            {
                if (_they == HeroConstants.THEY_HEHIM)
                {
                    names.AddRange(Names.heroNames_masc);
                    names.AddRange(Names.heroNames_androgynous);

                    if (Game1.randChance(Names.CHANCE_OF_STEP))
                    {
                        names.AddRange(Names.heroNames_nonbinary);
                    }
                }
                else if (_they == HeroConstants.THEY_SHEHER)
                {
                    names.AddRange(Names.heroNames_femme);
                    names.AddRange(Names.heroNames_androgynous);

                    if (Game1.randChance(Names.CHANCE_OF_STEP))
                    {
                        names.AddRange(Names.heroNames_nonbinary);
                    }
                }
                else
                {
                    names.AddRange(Names.heroNames_nonbinary);
                    names.AddRange(Names.heroNames_androgynous);

                    if (Game1.randChance(Names.CHANCE_OF_STEP))
                    {
                        names.AddRange(Names.heroNames_masc);
                        names.AddRange(Names.heroNames_femme);
                    }
                }
            }


            if (firstChampion)
            {
                names = Names.heroNames_femme;
            }


            _name = names[Game1.randint(0, names.Count - 1)];
        }

        /// <summary>
        /// Called upon generation of the Hero, and determines the traits that make up
        /// their individuality. Currently not yet implemented.
        /// </summary>
        private void generateRandomTraits()
        {
            //FIXIT make this function when traits are added
        }

        /// <summary>
        /// Called upon generation of the Hero, and determines the Hero's default deck,
        /// to be used if they are selected as the Champion for the dungeon run. Heroes
        /// have many different possibilities for their default deck, providing another
        /// source of individuality and making them feel more real. Their deck represents
        /// how they fight without deific intervention.
        /// </summary>
        private void generateRandomDefaultDeck(bool firstChampion)
        {
            List<DeckBuilder.Card> allDefaultCards = Game1.getAllCards().getAllCardsByRarity(DeckBuilder.CardEnums.CardRarity.DEFAULT);
            List<DeckBuilder.Card> allDefaultAttackCards = Game1.getAllCards().getAllCardsByRarity(DeckBuilder.CardEnums.CardRarity.DEFAULT, DeckBuilder.CardEnums.CardType.ATTACK);
            List<DeckBuilder.Card> allDefaultSkillCards = Game1.getAllCards().getAllCardsByRarity(DeckBuilder.CardEnums.CardRarity.DEFAULT, DeckBuilder.CardEnums.CardType.SKILL);
            List<DeckBuilder.Card> allDefaultOtherCards = new List<DeckBuilder.Card>();
            foreach (DeckBuilder.Card card in allDefaultCards)
            {
                if (!allDefaultAttackCards.Contains(card) && !allDefaultSkillCards.Contains(card))
                {
                    allDefaultOtherCards.Add(card);
                }
            }
            int numDefaultCards = allDefaultCards.Count;
            int numDefaultAttackCards = allDefaultAttackCards.Count;
            int numDefaultSkillCards = allDefaultSkillCards.Count;
            int numDefaultOtherCards = allDefaultOtherCards.Count;

            if (numDefaultCards == 0)
            {
                Game1.addToErrorLog("No default cards exist when generating default deck");
            }

            if (HeroConstants.NUM_ATTACK_CARDS_IN_DEFAULT_DECK > 0)
            {
                bool specializedDeck = (!firstChampion && Game1.randint(1, 100) <= HeroConstants.CHANCE_AT_SPECIALIZED_DECK);
                DeckBuilder.Card specializedCard = allDefaultAttackCards[Game1.randint(0, numDefaultAttackCards - 1)];
                _defaultCards.Add(specializedCard.getNewCard());
                for (int i = 1; i < HeroConstants.NUM_ATTACK_CARDS_IN_DEFAULT_DECK; i++)
                {
                    if (specializedDeck && Game1.randint(1, 100) <= HeroConstants.CHANCE_AT_SPECIALIZED_CARD)
                    {
                        _defaultCards.Add(specializedCard.getNewCard());
                    }
                    else
                    {
                        _defaultCards.Add(allDefaultAttackCards[Game1.randint(0, numDefaultAttackCards - 1)].getNewCard());
                    }
                }
            }

            if (HeroConstants.NUM_SKILL_CARDS_IN_DEFAULT_DECK > 0)
            {
                bool specializedDeck = (!firstChampion && Game1.randint(1, 100) <= HeroConstants.CHANCE_AT_SPECIALIZED_DECK);
                DeckBuilder.Card specializedCard = allDefaultSkillCards[Game1.randint(0, numDefaultSkillCards - 1)];
                _defaultCards.Add(specializedCard.getNewCard());
                for (int i = 1; i < HeroConstants.NUM_SKILL_CARDS_IN_DEFAULT_DECK; i++)
                {
                    if (specializedDeck && Game1.randint(1, 100) <= HeroConstants.CHANCE_AT_SPECIALIZED_CARD)
                    {
                        _defaultCards.Add(specializedCard.getNewCard());
                    }
                    else
                    {
                        _defaultCards.Add(allDefaultSkillCards[Game1.randint(0, numDefaultSkillCards - 1)].getNewCard());
                    }
                }
            }

            if (HeroConstants.NUM_OTHER_CARD_ROLLS_IN_DEFAULT_DECK > 0)
            {
                //first check if any other default cards will even go in the deck
                int rolledOtherCards = 0;
                for (int i = 0; i < HeroConstants.NUM_OTHER_CARD_ROLLS_IN_DEFAULT_DECK; i++)
                {
                    if (Game1.randint(1, 100) <= HeroConstants.OTHER_CARD_CHANCE_IN_DEFAULT_DECK)
                    {
                        rolledOtherCards++;
                    }
                }

                //if yes, standard procedure
                if (rolledOtherCards > 0)
                {
                    bool specializedDeck = (!firstChampion && Game1.randint(1, 100) <= HeroConstants.CHANCE_AT_SPECIALIZED_DECK);
                    DeckBuilder.Card specializedCard = allDefaultOtherCards[Game1.randint(0, numDefaultOtherCards - 1)];
                    _defaultCards.Add(specializedCard.getNewCard());
                    for (int i = 1; i < rolledOtherCards; i++)
                    {
                        if (specializedDeck && Game1.randint(1, 100) <= HeroConstants.CHANCE_AT_SPECIALIZED_CARD)
                        {
                            _defaultCards.Add(specializedCard.getNewCard());
                        }
                        else
                        {
                            _defaultCards.Add(allDefaultOtherCards[Game1.randint(0, numDefaultOtherCards - 1)].getNewCard());
                        }
                    }
                }
            }

            int numCardsSoFar = _defaultCards.Count;
            int numCardsTotal = Game1.randint(HeroConstants.MIN_TOTAL_NUM_CARDS_IN_DEFAULT_DECK, HeroConstants.MAX_TOTAL_NUM_CARDS_IN_DEFAULT_DECK);
            //Add any random default card until deck is considered full
            for (int i = numCardsSoFar; i < numCardsTotal; i++)
            {
                _defaultCards.Add(allDefaultCards[Game1.randint(0, numDefaultCards - 1)].getNewCard());
            }

            String log = "New Hero default deck created: ";
            foreach (DeckBuilder.Card card in _defaultCards)
            {
                log += card.getName() + ", ";
            }
            Game1.debugLog.Add(log);
        }

        /// <summary>
        /// Called upon generation of the Hero, and determines their maximum hitpoints.
        /// Weighted to be towards the middle between minimum and maximum HP, but with
        /// a chance of extreme.
        /// </summary>
        private void generateRandomHP(bool firstChampion)
        {
            int maxHP_1 = Game1.randint(HeroConstants.HERO_MAX_HP_MIN, HeroConstants.HERO_MAX_HP_MAX);
            int maxHP_2 = Game1.randint(HeroConstants.HERO_MAX_HP_MIN, HeroConstants.HERO_MAX_HP_MAX);
            int maxHP_3 = Game1.randint(HeroConstants.HERO_MAX_HP_MIN, HeroConstants.HERO_MAX_HP_MAX);
            int maxHP_4 = Game1.randint(HeroConstants.HERO_MAX_HP_MIN, HeroConstants.HERO_MAX_HP_MAX);
            _maxHP = (maxHP_1 + maxHP_2 + maxHP_3 + maxHP_4) / 4; //tends towards the average rather than the extremes

            if (firstChampion)
            {
                if (_maxHP < (HeroConstants.HERO_MAX_HP_MIN + HeroConstants.HERO_MAX_HP_MIN + HeroConstants.HERO_MAX_HP_MAX) / 3)
                {
                    _maxHP = (HeroConstants.HERO_MAX_HP_MIN + HeroConstants.HERO_MAX_HP_MIN + HeroConstants.HERO_MAX_HP_MAX) / 3;
                }
                else if (_maxHP > (HeroConstants.HERO_MAX_HP_MIN + HeroConstants.HERO_MAX_HP_MAX + HeroConstants.HERO_MAX_HP_MAX) / 3)
                {
                    _maxHP = (HeroConstants.HERO_MAX_HP_MIN + HeroConstants.HERO_MAX_HP_MAX + HeroConstants.HERO_MAX_HP_MAX) / 3;
                }
            }
        }

        /// <summary>
        /// Called upon generation of the Hero if a specific party buff has not been 
        /// supplied to the constructor. Generates randomly from a list of all party
        /// buffs. Currently not yet implemented.
        /// </summary>
        private void generateRandomPartyMemberBuff()
        {
            //FIXIT implement
        }
    }
}
