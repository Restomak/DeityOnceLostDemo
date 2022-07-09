using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Treasures
{
    /// <summary>
    /// A type of Treasure that, when taken, creates a NewCardChoiceMenu for the
    /// player to select a card they wish to add to their deck.
    /// </summary>
    public class AddCardToDeck : Treasure
    {
        public const int EMERGENCY_SHUFFLE_LOOP_BREAK_COUNT = 500; //never have while loops without an emergency exit

        List<DeckBuilder.Card> _choices;

        public AddCardToDeck(List<DeckBuilder.Card> choices) : base (treasureType.addCard)
        {
            _choices = choices;

            _treasureText = "Add a card to your deck";
        }

        public override void onTaken()
        {
            Game1.addToMenus(new UserInterface.Menus.NewCardChoiceMenu(_choices, this, () => { }));
        }

        public void setTaken()
        {
            _taken = true;
        }

        public List<DeckBuilder.Card> getChoices()
        {
            return _choices;
        }



        /// <summary>
        /// Generates a random set of cards for use in the NewCardChoiceMenu. This function
        /// can be used to force these cards to be all common, or force them above a specific
        /// rarity (such as when a defeated miniboss provides a set of cards all rare quality
        /// or above). This function makes use of the weights defined in LootConstants.
        /// </summary>
        public static List<DeckBuilder.Card> getRandomCards(int numCards, bool forceAllCardsCommon = false, bool cardsMustBeUnique = true,
            DeckBuilder.CardEnums.CardRarity minimumRarity = DeckBuilder.CardEnums.CardRarity.COMMON)
        {
            List<DeckBuilder.Card> randomCards = new List<DeckBuilder.Card>();
            int emergencyExitCounter = 0;
            int randomIndex;

            if (!forceAllCardsCommon && (minimumRarity != DeckBuilder.CardEnums.CardRarity.COMMON || Game1.randChance(LootConstants.ADDCARD_CHANCE_NOT_ALL_COMMON)))
            {
                bool commonAllowed = true;
                bool rareAllowed = true;
                bool epicAllowed = true;
                bool godlyAllowed = true;

                if (minimumRarity == DeckBuilder.CardEnums.CardRarity.RARE)
                {
                    commonAllowed = false;
                }
                else if (minimumRarity == DeckBuilder.CardEnums.CardRarity.EPIC)
                {
                    commonAllowed = false;
                    rareAllowed = false;
                }
                else if (minimumRarity == DeckBuilder.CardEnums.CardRarity.GODLY)
                {
                    commonAllowed = false;
                    rareAllowed = false;
                    epicAllowed = false;
                }
                else if (minimumRarity == DeckBuilder.CardEnums.CardRarity.VOID)
                {
                    commonAllowed = false;
                    rareAllowed = false;
                    epicAllowed = false;
                    godlyAllowed = false;
                }

                List<DeckBuilder.Card> allCommonCards = Game1.getCardCollection().getAllCardsByRarity(DeckBuilder.CardEnums.CardRarity.COMMON);
                List<DeckBuilder.Card> allRareCards = Game1.getCardCollection().getAllCardsByRarity(DeckBuilder.CardEnums.CardRarity.RARE);
                List<DeckBuilder.Card> allEpicCards = Game1.getCardCollection().getAllCardsByRarity(DeckBuilder.CardEnums.CardRarity.EPIC);
                List<DeckBuilder.Card> allGodlyCards = Game1.getCardCollection().getAllCardsByRarity(DeckBuilder.CardEnums.CardRarity.GODLY);
                List<DeckBuilder.Card> allVoidCards = Game1.getCardCollection().getAllCardsByRarity(DeckBuilder.CardEnums.CardRarity.VOID);
                
                int randomRarity;
                List<DeckBuilder.Card> allCardsOfRarity;
                while (randomCards.Count < numCards && emergencyExitCounter < EMERGENCY_SHUFFLE_LOOP_BREAK_COUNT)
                {
                    //Randomize rarity
                    randomRarity = Game1.randint(1, LootConstants.ADDCARD_WEIGHT_COMMON + LootConstants.ADDCARD_WEIGHT_RARE +
                        LootConstants.ADDCARD_WEIGHT_EPIC + LootConstants.ADDCARD_WEIGHT_GODLY + LootConstants.ADDCARD_WEIGHT_VOID);

                    if (randomRarity <= LootConstants.ADDCARD_WEIGHT_COMMON && commonAllowed)
                    {
                        allCardsOfRarity = allCommonCards;
                    }
                    else if (randomRarity <= LootConstants.ADDCARD_WEIGHT_COMMON + LootConstants.ADDCARD_WEIGHT_RARE && rareAllowed)
                    {
                        allCardsOfRarity = allRareCards;
                    }
                    else if (randomRarity <= LootConstants.ADDCARD_WEIGHT_COMMON + LootConstants.ADDCARD_WEIGHT_RARE + LootConstants.ADDCARD_WEIGHT_EPIC && epicAllowed)
                    {
                        allCardsOfRarity = allEpicCards;
                    }
                    else if (randomRarity <= LootConstants.ADDCARD_WEIGHT_COMMON + LootConstants.ADDCARD_WEIGHT_RARE + LootConstants.ADDCARD_WEIGHT_EPIC +
                        LootConstants.ADDCARD_WEIGHT_GODLY && godlyAllowed)
                    {
                        allCardsOfRarity = allGodlyCards;
                    }
                    else if (randomRarity <= LootConstants.ADDCARD_WEIGHT_COMMON + LootConstants.ADDCARD_WEIGHT_RARE + LootConstants.ADDCARD_WEIGHT_EPIC +
                        LootConstants.ADDCARD_WEIGHT_GODLY + LootConstants.ADDCARD_WEIGHT_VOID)
                    {
                        allCardsOfRarity = allGodlyCards;//allVoidCards; //FIXIT fix when void cards added
                    }
                    else
                    {
                        Game1.addToErrorLog("getRandomCards generated random integer too large for total weights. Defaulting to allCommonCards");
                        allCardsOfRarity = allCommonCards;
                    }

                    //Add the card to the list
                    if (allCardsOfRarity.Count > 0)
                    {
                        randomIndex = Game1.randint(0, allCardsOfRarity.Count - 1);
                        if (!cardsMustBeUnique || !randomCards.Contains(allCardsOfRarity[randomIndex]))
                        {
                            randomCards.Add(allCardsOfRarity[randomIndex]);
                        }
                    }

                    emergencyExitCounter++;
                }
            }
            else
            {
                List<DeckBuilder.Card> allCommonCards = Game1.getCardCollection().getAllCardsByRarity(DeckBuilder.CardEnums.CardRarity.COMMON);

                while (randomCards.Count < numCards && emergencyExitCounter < EMERGENCY_SHUFFLE_LOOP_BREAK_COUNT)
                {
                    randomIndex = Game1.randint(0, allCommonCards.Count - 1);
                    if (!cardsMustBeUnique || !randomCards.Contains(allCommonCards[randomIndex]))
                    {
                        randomCards.Add(allCommonCards[randomIndex]);
                    }

                    emergencyExitCounter++;
                }
            }

            if (emergencyExitCounter >= EMERGENCY_SHUFFLE_LOOP_BREAK_COUNT)
            {
                Game1.addToErrorLog("getRandomCards emergencyExitCounter reached loop break count; exiting");
            }

            return randomCards;
        }
    }
}
