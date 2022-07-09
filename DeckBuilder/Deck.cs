using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    /// <summary>
    /// Deck and card pile storage and management.
    /// </summary>
    public class Deck
    {
        public const int EMERGENCY_SHUFFLE_LOOP_BREAK_COUNT = 500; //never have while loops without an emergency exit
        
        public const int MAX_HAND_CAPACITY = 12;
        public const int DEFAULT_DRAW_ON_TURN_START = 6;

        //The entire deck
        private List<Card> _deck;

        //The deck split into parts:
        private List<Card> _drawPile;
        private List<Card> _hand;
        private List<Card> _discardPile;
        private List<Card> _removedCards;

        //Used for checking cards that won't be discarded at the end of the turn (cards that automatically preserve need not be added here, but it doesn't break if they are)
        private List<Card> _keepingInHand;

        private Card _lastCardDrawn;

        public Deck(List<Card> startingCards)
        {
            _deck = new List<Card>();
            _drawPile = new List<Card>();
            _hand = new List<Card>();
            _discardPile = new List<Card>();
            _removedCards = new List<Card>();
            _keepingInHand = new List<Card>();

            foreach (Card card in startingCards)
            {
                _deck.Add(card);
            }
        }

        /// <summary>
        /// Initialize the deck. Use at the start of a new run
        /// </summary>
        public void start()
        {
            _drawPile.Clear();
            _hand.Clear();
            _discardPile.Clear();
            _removedCards.Clear();

            _drawPile = shuffle(_deck, true);

            _lastCardDrawn = null;

            Game1.debugLog.Add("Deck start() called. Deck size: " + _deck.Count);
        }

        /// <summary>
        /// if whileInPlay, also adds to _discardPile
        /// </summary>
        public void permanentlyAddToDeck(Card card, bool whileInPlay = false)
        {
            _deck.Add(card);

            if (whileInPlay)
            {
                _discardPile.Add(card);
            }
        }

        public void permanentlyRemoveFromDeck(Card card)
        {
            if (_deck.Contains(card))
            {
                _deck.Remove(card);
            }
            else
            {
                Game1.addToErrorLog("Tried to remove a card that wasn't in the deck: " + card.getName());
            }
        }



        /// <summary>
        /// Returns a shuffled list of Cards based on the passed list. If the generateNewCards
        /// flag is enabled, it will return a list of newly generated cards based on the passed
        /// list instead of using that list itself (required when copying cards out of the deck
        /// for combat, so that the cards in the deck remain unaffected by whatever happens in
        /// combat).
        /// </summary>
        public List<Card> shuffle(List<Card> cards, bool generateNewCards = false)
        {
            List<Card> unshuffledCards = new List<Card>();
            List<Card> shuffledCards = new List<Card>();
            int emergencyExitCounter = 0;
            int randomIndex;

            //Copy deck into unshuffled list
            for (int i = 0; i < cards.Count; i++)
            {
                if (!generateNewCards)
                {
                    unshuffledCards.Add(cards[i]);
                }
                else
                {
                    unshuffledCards.Add(cards[i].getNewCard());
                }
            }

            //Move unshuffled list into shuffled list
            while (unshuffledCards.Count > 0 && emergencyExitCounter < EMERGENCY_SHUFFLE_LOOP_BREAK_COUNT)
            {
                randomIndex = Game1.randint(0, unshuffledCards.Count - 1);
                shuffledCards.Add(unshuffledCards[randomIndex]);
                unshuffledCards.RemoveAt(randomIndex);

                emergencyExitCounter++;
            }

            //If emergency exit was hit, make sure the rest of the cards still get added in
            Game1.debugLog.Add("Deck shuffled. emergencyExitCounter: " + emergencyExitCounter);
            for (int i = 0; i < unshuffledCards.Count; i++)
            {
                shuffledCards.Add(cards[i]);
            }

            return shuffledCards;
        }

        /// <summary>
        /// Draws cards one at a time to make sure last of draw pile is used up before shuffling
        /// discard pile back in.
        /// </summary>
        public void drawNumCards(int count)
        {
            for (int i = count; i > 0; i--)
            {
                drawCard();
            }
        }

        /// <summary>
        /// If the draw pile is empty, it will shuffle the discard pile back into the draw pile
        /// before drawing. If the draw pile is also empty, nothing will happen.
        /// </summary>
        public void drawCard()
        {
            //first shuffle the draw pile if there's nothing in it
            if (_drawPile.Count == 0)
            {
                _drawPile = shuffle(_discardPile);
                _discardPile.Clear();
            }

            //if there's still nothing in it, don't draw anything
            if (_drawPile.Count > 0)
            {
                _lastCardDrawn = _drawPile[0];

                //Check if it goes in the hand or discard pile
                if (_hand.Count < MAX_HAND_CAPACITY)
                {
                    _hand.Add(_drawPile[0]);
                    _drawPile.RemoveAt(0);
                }
                else
                {
                    _discardPile.Add(_drawPile[0]);
                    _drawPile.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// Discards all cards in hand at the end of the turn, except those that have been
        /// Preserved.
        /// </summary>
        public void turnEndDiscardAll()
        {
            List<Card> cardsToRemove = new List<Card>();
            foreach (Card card in _hand)
            {
                if (_keepingInHand.Contains(card) || card.getRetainAlways())
                {
                    if (_keepingInHand.Contains(card) && !card.getRetainAlways())
                    {
                        _keepingInHand.Remove(card);
                    }
                    card.onKeep(); //in case there's any mechanics for it being kept in hand
                }
                else
                {
                    _discardPile.Add(card);
                    cardsToRemove.Add(card);
                }
            }

            int count = cardsToRemove.Count;
            int removeIndex;
            for (int i = count; i > 0; i--)
            {
                if (cardsToRemove.Count > 0)
                {
                    removeIndex = _hand.IndexOf(cardsToRemove[0]);
                    _hand.RemoveAt(removeIndex);
                    cardsToRemove.RemoveAt(0);
                }
                else //should not happen
                {
                    Game1.addToErrorLog("Tried to end-of-turn discard more cards than exist: iterator at " + i);
                }
            }

            //Some of the cards put in the list won't still be in hand; remove them
            cardsToRemove = new List<Card>();
            foreach (Card card in _keepingInHand)
            {
                if (!card.getRetainAlways())
                {
                    cardsToRemove.Add(card);
                }
            }
            count = cardsToRemove.Count;
            for (int i = count; i > 0; i--)
            {
                if (cardsToRemove.Count > 0)
                {
                    _keepingInHand.Remove(cardsToRemove[0]);
                    cardsToRemove.RemoveAt(0);
                }
            }
        }



        //Functions for moving cards around between piles/hand
        public void discardBecausePlayed(Card card, int positionInHand)
        {
            if (_hand.Contains(card) && _hand[positionInHand] == card)
            {
                _discardPile.Add(card);
                _hand.RemoveAt(positionInHand);
            }
            else
            {
                Game1.addToErrorLog("Tried to move a card from hand to discard pile that wasn't in hand to begin with: " + card.getName());
            }
        }

        public void discardFromHand(Card card)
        {
            if (_hand.Contains(card))
            {
                card.onDiscard(); //in case there's any mechanics for it being discarded
                _discardPile.Add(card);
                _hand.Remove(card);
            }
            else
            {
                Game1.addToErrorLog("Tried to move a card from hand to discard pile that wasn't in hand to begin with: " + card.getName());
            }
        }

        public void removeFromHand(Card card, int positionInHand)
        {
            if (_hand.Contains(card) && _hand[positionInHand] == card)
            {
                card.onRemove(); //in case there's any mechanics for it being removed
                _removedCards.Add(card);
                _hand.Remove(card);
            }
            else
            {
                Game1.addToErrorLog("Tried to move a card from hand to removed cards that wasn't in hand to begin with: " + card.getName());
            }
        }

        public void moveFromHandToDraw(Card card, int positionInHand)
        {
            if (_hand.Contains(card) && _hand[positionInHand] == card)
            {
                _drawPile.Add(card);
                _hand.Remove(card);
            }
            else
            {
                Game1.addToErrorLog("Tried to move a card from hand to draw pile that wasn't in hand to begin with: " + card.getName());
            }
        }

        public void pullCardFromDiscardToHand(Card card)
        {
            if (_discardPile.Contains(card))
            {
                addToHand(card);
                _discardPile.Remove(card);
            }
            else
            {
                Game1.addToErrorLog("Tried to move a card from discard pile to hand that wasn't in discard pile to begin with: " + card.getName());
            }
        }

        public void pullCardFromDiscardToDraw(Card card)
        {
            if (_discardPile.Contains(card))
            {
                _drawPile.Add(card);
                _discardPile.Remove(card);
            }
            else
            {
                Game1.addToErrorLog("Tried to move a card from discard pile to draw pile that wasn't in discard pile to begin with: " + card.getName());
            }
        }

        public void removeFromDiscard(Card card)
        {
            if (_discardPile.Contains(card))
            {
                _removedCards.Add(card);
                _discardPile.Remove(card);
            }
            else
            {
                Game1.addToErrorLog("Tried to move a card from discard pile to removed cards that wasn't in discard pile to begin with: " + card.getName());
            }
        }

        public void pullCardFromDrawToHand(Card card)
        {
            if (_drawPile.Contains(card))
            {
                addToHand(card);
                _drawPile.Remove(card);
            }
            else
            {
                Game1.addToErrorLog("Tried to move a card from draw pile to hand that wasn't in draw pile to begin with: " + card.getName());
            }
        }

        public void discardFromDraw(Card card)
        {
            if (_drawPile.Contains(card))
            {
                card.onDiscard(); //in case there's any mechanics for it being discarded
                _discardPile.Add(card);
                _drawPile.Remove(card);
            }
            else
            {
                Game1.addToErrorLog("Tried to move a card from draw pile to discard pile that wasn't in draw pile to begin with: " + card.getName());
            }
        }

        public void removeFromDraw(Card card)
        {
            if (_drawPile.Contains(card))
            {
                card.onRemove(); //in case there's any mechanics for it being removed
                _removedCards.Add(card);
                _drawPile.Remove(card);
            }
            else
            {
                Game1.addToErrorLog("Tried to move a card from draw pile to removed cards that wasn't in draw pile to begin with: " + card.getName());
            }
        }

        public void removeFromDraw(int count)
        {
            for (int i = count; i > 0; i--)
            {
                //only burn cards if any are there
                if (_drawPile.Count > 0)
                {
                    _drawPile[0].onRemove(); //in case there's any mechanics for it being removed
                    _removedCards.Add(_drawPile[0]);
                    _drawPile.RemoveAt(0);
                }
            }
        }

        public void bringRemovedBackToDiscard(Card card)
        {
            if (_removedCards.Contains(card))
            {
                _discardPile.Add(card);
                _removedCards.Remove(card);
            }
            else
            {
                Game1.addToErrorLog("Tried to move a card from removed cards to discard pile that wasn't in removed cards to begin with: " + card.getName());
            }
        }

        public void bringRemovedBackToHand(Card card)
        {
            if (_removedCards.Contains(card))
            {
                addToHand(card);
                _removedCards.Remove(card);
            }
            else
            {
                Game1.addToErrorLog("Tried to move a card from removed cards to hand that wasn't in removed cards to begin with: " + card.getName());
            }
        }

        public void addToHand(Card card)
        {
            if (_hand.Count == MAX_HAND_CAPACITY)
            {
                _discardPile.Add(card);
            }
            else
            {
                _hand.Add(card);
            }
        }

        public void preserveCardInHand(Card card)
        {
            if (_hand.Contains(card))
            {
                _keepingInHand.Add(card);
            }
            else
            {
                Game1.addToErrorLog("Tried to preserve a card in hand but can't find it in hand: " + card.getName());
            }
        }



        //Getters
        public List<Card> getDeck()
        {
            return _deck;
        }
        public List<Card> getDrawPile()
        {
            return _drawPile;
        }
        public List<Card> getHand()
        {
            return _hand;
        }
        public List<Card> getDiscardPile()
        {
            return _discardPile;
        }
        public List<Card> getRemovedCards()
        {
            return _removedCards;
        }
        public Card getLastDrawnCard()
        {
            return _lastCardDrawn;
        }



        /// <summary>
        /// Checks through the deck to see if it can find any cards that aren't upgraded yet. If it finds
        /// one, it returns false (not all cards are upgraded). If it can't find one, it returns true.
        /// </summary>
        public bool allCardsUpgraded()
        {
            for (int i = 0; i < _deck.Count; i++)
            {
                if (!_deck[i].isEmpowered() && !_deck[i].isBloodstained())
                {
                    return false;
                }
            }

            return true;
        }



        /// <summary>
        /// Sorts cards by rarity before shuffling within rarity. Used when viewing the draw
        /// pile, since the player isn't supposed to know the order cards will be drawn.
        /// </summary>
        public static List<Card> shuffleAndSortByRarity(List<Card> cards)
        {
            List<Card> defaultCards = new List<Card>();
            List<Card> commonCards = new List<Card>();
            List<Card> rareCards = new List<Card>();
            List<Card> epicCards = new List<Card>();
            List<Card> godlyCards = new List<Card>();
            List<Card> otherCards = new List<Card>();
            List<Card> allCards = new List<Card>();

            for (int i = 0; i < cards.Count; i++)
            {
                switch (cards[i].getCardRarity())
                {
                    case CardEnums.CardRarity.DEFAULT:
                        defaultCards.Insert(Game1.randint(0, defaultCards.Count), cards[i]);
                        break;
                    case CardEnums.CardRarity.COMMON:
                        commonCards.Insert(Game1.randint(0, commonCards.Count), cards[i]);
                        break;
                    case CardEnums.CardRarity.RARE:
                        rareCards.Insert(Game1.randint(0, rareCards.Count), cards[i]);
                        break;
                    case CardEnums.CardRarity.EPIC:
                        epicCards.Insert(Game1.randint(0, epicCards.Count), cards[i]);
                        break;
                    case CardEnums.CardRarity.GODLY:
                        godlyCards.Insert(Game1.randint(0, godlyCards.Count), cards[i]);
                        break;
                    default:
                        otherCards.Insert(Game1.randint(0, otherCards.Count), cards[i]);
                        break;
                }
            }

            allCards.AddRange(godlyCards);
            allCards.AddRange(epicCards);
            allCards.AddRange(rareCards);
            allCards.AddRange(commonCards);
            allCards.AddRange(defaultCards);
            allCards.AddRange(otherCards);

            return allCards;
        }



        /// <summary>
        /// Used when a champion dies; pass that champion's deck, and this function will strip away
        /// the default cards (unique to each Hero), and return a random number of their non-default
        /// cards (as some cards are lost on soul transfer).
        /// </summary>
        public static List<Card> getSplinterDeck(List<Card> originalDeck)
        {
            List<Card> newCards = new List<Card>();

            for (int i = 0; i < originalDeck.Count; i++)
            {
                if (originalDeck[i].getCardRarity() != CardEnums.CardRarity.DEFAULT && !Game1.randChance(Characters.HeroConstants.CHANCE_CARD_SPLINTERED_AWAY_ON_DEATH))
                {
                    newCards.Add(originalDeck[i]);
                }
            }

            return newCards;
        }
    }
}