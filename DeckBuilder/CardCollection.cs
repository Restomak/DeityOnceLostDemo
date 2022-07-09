using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    /// <summary>
    /// Handles the collection of all cards in the game. Cards not added to the pool immediately
    /// are ones not available until certain conditions have been met, such as obtaining party
    /// members or completing the tutorial dungeon.
    /// </summary>
    public class CardCollection
    {
        private List<Card> _allCards;

        public CardCollection()
        {
            _allCards = new List<Card>();

            //Manually create all cards here until I figure out how to do it automatically

            //Default Cards
            _allCards.Add(new Cards.RegularCards.DefaultCards.Block());
            _allCards.Add(new Cards.RegularCards.DefaultCards.CautiousStrike());
            _allCards.Add(new Cards.RegularCards.DefaultCards.Dodge());
            _allCards.Add(new Cards.RegularCards.DefaultCards.Jab());
            _allCards.Add(new Cards.RegularCards.DefaultCards.Kick());
            _allCards.Add(new Cards.RegularCards.DefaultCards.Parry());
            _allCards.Add(new Cards.RegularCards.DefaultCards.Punch());

            //Common Cards
            _allCards.Add(new Cards.RegularCards.CommonCards.GatherStrength());
            _allCards.Add(new Cards.RegularCards.CommonCards.HeroicBlow());
            _allCards.Add(new Cards.RegularCards.CommonCards.RollingKick());
            _allCards.Add(new Cards.RegularCards.CommonCards.DodgeAndRoll());
            _allCards.Add(new Cards.RegularCards.CommonCards.Cleave());
            _allCards.Add(new Cards.RegularCards.CommonCards.ViciousSlam());
            _allCards.Add(new Cards.RegularCards.CommonCards.ExploitWeakness());
            _allCards.Add(new Cards.RegularCards.CommonCards.CheapShot());
            _allCards.Add(new Cards.RegularCards.CommonCards.FirstStrike());
            _allCards.Add(new Cards.RegularCards.CommonCards.SnapSlap());

            //Rare Cards
            _allCards.Add(new Cards.RegularCards.RareCards.Guidance());
            _allCards.Add(new Cards.RegularCards.RareCards.HuntersMark());
            _allCards.Add(new Cards.RegularCards.RareCards.QuakeStomp());
            _allCards.Add(new Cards.RegularCards.RareCards.RecurringBlows());

            //Epic Cards
            _allCards.Add(new Cards.RegularCards.EpicCards.DrawForth());

            //Godly Cards
            _allCards.Add(new Cards.RegularCards.GodlyCards.FistOfTheStars());
            _allCards.Add(new Cards.RegularCards.GodlyCards.FontOfPossibilities());
            _allCards.Add(new Cards.RegularCards.GodlyCards.GuidingLight());


            //Void Cards

        }

        public List<Card> getCardMasterList()
        {
            return _allCards;
        }

        /// <summary>
        /// Called when the player first gains party members, so that these cards don't exist in the loot tables until then.
        /// This is to avoid weird mechanics bugs for having cards that affect mechanics the player doesn't yet have.
        /// </summary>
        public void addCardsAfterParty()
        {
            //Epic Cards
            _allCards.Add(new Cards.RegularCards.EpicCards.TimeShift()); //has party mechanics
        }

        /// <summary>
        /// Called when the player finishes the first dungeon, so that these cards don't exist in the loot tables until then.
        /// This is to keep the game simple at first while learning, then allow it to branch out more drastically later on.
        /// </summary>
        public void addCardsAfterTutorial()
        {
            //Rare Cards
            _allCards.Add(new Cards.RegularCards.RareCards.Sacrifice()); //has a blood cost
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
