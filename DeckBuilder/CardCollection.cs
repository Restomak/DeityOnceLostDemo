using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    public class CardCollection
    {
        //The overall collection of all cards in the game

        private List<Card> _allCards;

        public CardCollection()
        {
            _allCards = new List<Card>();

            //Manually create all cards here until I figure out how to do it automatically

            //Default Cards
            _allCards.Add(new Cards.DefaultCards.Block());
            _allCards.Add(new Cards.DefaultCards.CautiousStrike());
            _allCards.Add(new Cards.DefaultCards.Dodge());
            _allCards.Add(new Cards.DefaultCards.Jab());
            _allCards.Add(new Cards.DefaultCards.Kick());
            _allCards.Add(new Cards.DefaultCards.Parry());
            _allCards.Add(new Cards.DefaultCards.Punch());

            //Common Cards
            _allCards.Add(new Cards.CommonCards.GatherStrength());
            _allCards.Add(new Cards.CommonCards.HeroicBlow());
            _allCards.Add(new Cards.CommonCards.RollingKick());
            _allCards.Add(new Cards.CommonCards.DodgeAndRoll());
            _allCards.Add(new Cards.CommonCards.Cleave());
            _allCards.Add(new Cards.CommonCards.ViciousSlam());
            _allCards.Add(new Cards.CommonCards.ExploitWeakness());

            //Rare Cards


            //Epic Cards


            //Godly Cards
            _allCards.Add(new Cards.GodlyCards.FistOfTheStars());


            //Void Cards

        }

        public List<Card> getCardMasterList()
        {
            return _allCards;
        }

        /// <summary>
        /// For manually adding a card into the game so that it can be found in engine searches & therefore obtainable in play
        /// </summary>
        public void createCard(Card newCard)
        {
            _allCards.Add(newCard);
        }
        


        public List<Card> getAllCardsByRarity(CardEnums.CardRarity rarity)
        {
            List<Card> returnList = new List<Card>();
            foreach (Card card in _allCards)
            {
                if (card.getCardRarity() == rarity)
                {
                    returnList.Add(card);
                }
            }
            return returnList;
        }

        public List<Card> getAllCardsByRarity(CardEnums.CardRarity rarity, CardEnums.CardType cardType)
        {
            List<Card> returnList = new List<Card>();
            foreach (Card card in _allCards)
            {
                if (card.getCardRarity() == rarity && card.getCardType() == cardType)
                {
                    returnList.Add(card);
                }
            }
            return returnList;
        }

        public List<Card> getAllCardsByType(CardEnums.CardType cardType)
        {
            List<Card> returnList = new List<Card>();
            foreach (Card card in _allCards)
            {
                if (card.getCardType() == cardType)
                {
                    returnList.Add(card);
                }
            }
            return returnList;
        }

        public List<Card> getAllCardsByType(CardEnums.CardType cardType, CardEnums.CardRarity rarity)
        {
            List<Card> returnList = new List<Card>();
            foreach (Card card in _allCards)
            {
                if (card.getCardRarity() == rarity && card.getCardType() == cardType)
                {
                    returnList.Add(card);
                }
            }
            return returnList;
        }
    }
}
