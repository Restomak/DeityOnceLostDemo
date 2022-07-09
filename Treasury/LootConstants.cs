using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury
{
    class LootConstants
    {
        //The number of cards to choose between for adding a card to the deck, depending on the situation
        public const int ADDCARD_DEFAULT_CHOICE_AMOUNT = 3;
        public const int ADDCARD_DEFAULT_MINIBOSS_CHOICE_AMOUNT = 4;

        //The inverse of the chance that every card in an add card to deck treasure will be all common rarity
        public const double ADDCARD_CHANCE_NOT_ALL_COMMON = 0.33;

        //Weights used to determine the chance of each individual card rarity showing up in an add card to deck treasure (when not all common)
        public const int ADDCARD_WEIGHT_COMMON = 54;
        public const int ADDCARD_WEIGHT_RARE = 27;
        public const int ADDCARD_WEIGHT_EPIC = 9;
        public const int ADDCARD_WEIGHT_GODLY = 3;
        public const int ADDCARD_WEIGHT_VOID = 1;
    }
}
