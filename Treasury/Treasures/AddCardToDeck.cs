using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Treasures
{
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



        public static List<DeckBuilder.Card> getRandomCards(int numCards, bool forceAllCardsCommon = false, bool cardsMustBeUnique = true)
        {
            List<DeckBuilder.Card> randomCards = new List<DeckBuilder.Card>();
            int emergencyExitCounter = 0;
            int randomIndex;

            if (!forceAllCardsCommon && Game1.randChance(LootConstants.ADDCARD_CHANCE_NOT_ALL_COMMON))
            {
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

                    if (randomRarity < LootConstants.ADDCARD_WEIGHT_COMMON)
                    {
                        allCardsOfRarity = allCommonCards;
                    }
                    else if (randomRarity < LootConstants.ADDCARD_WEIGHT_COMMON + LootConstants.ADDCARD_WEIGHT_RARE)
                    {
                        allCardsOfRarity = allRareCards;
                    }
                    else if (randomRarity < LootConstants.ADDCARD_WEIGHT_COMMON + LootConstants.ADDCARD_WEIGHT_RARE + LootConstants.ADDCARD_WEIGHT_EPIC)
                    {
                        allCardsOfRarity = allRareCards;
                    }
                    else if (randomRarity < LootConstants.ADDCARD_WEIGHT_COMMON + LootConstants.ADDCARD_WEIGHT_RARE + LootConstants.ADDCARD_WEIGHT_EPIC +
                        LootConstants.ADDCARD_WEIGHT_GODLY)
                    {
                        allCardsOfRarity = allRareCards;
                    }
                    else if (randomRarity < LootConstants.ADDCARD_WEIGHT_COMMON + LootConstants.ADDCARD_WEIGHT_RARE + LootConstants.ADDCARD_WEIGHT_EPIC +
                        LootConstants.ADDCARD_WEIGHT_GODLY + LootConstants.ADDCARD_WEIGHT_VOID)
                    {
                        allCardsOfRarity = allRareCards;
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
