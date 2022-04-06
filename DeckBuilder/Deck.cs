using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
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

        //Used for checking cards that won't be discarded at the end of the turn (cards that automatically retain need not be added here, but it doesn't break if they are)
        private List<Card> _keepingInHand;

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

            _drawPile = shuffle(_deck);
            
            Game1.debugLog.Add("Deck start() called");
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
                Game1.errorLog.Add("Tried to remove a card that wasn't in the deck: " + card.getName());
            }
        }



        public List<Card> shuffle(List<Card> cards)
        {
            List<Card> shuffledCards = new List<Card>();
            int emergencyExitCounter = 0;
            int randomIndex;

            while (cards.Count > 0 && emergencyExitCounter < EMERGENCY_SHUFFLE_LOOP_BREAK_COUNT)
            {
                randomIndex = Game1.randint(0, cards.Count - 1);
                shuffledCards.Add(cards[randomIndex]);
                cards.RemoveAt(randomIndex);
            }

            Game1.debugLog.Add("Deck shuffled. emergencyExitCounter: " + emergencyExitCounter);
            return shuffledCards;
        }

        /// <summary>
        /// Draws cards one at a time to make sure last of draw pile is used up before shuffling discard pile back in
        /// </summary>
        public void drawNumCards(int count)
        {
            for (int i = count; i > 0; i--)
            {
                drawCard();
            }
        }

        public void drawCard()
        {
            if (_hand.Count < MAX_HAND_CAPACITY)
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
                    _hand.Add(_drawPile[0]);
                    _drawPile.RemoveAt(0);
                }
            }
        }

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
            for (int i = count; i > 0; i--)
            {
                if (cardsToRemove.Count > 0)
                {
                    _hand.RemoveAt(0);
                    cardsToRemove.RemoveAt(0);
                }
                else //should not happen
                {
                    Game1.errorLog.Add("Tried to end-of-turn discard more cards than exist: iterator at " + i);
                }
            }
        }



        //Functions for moving cards around between piles/hand
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
                Game1.errorLog.Add("Tried to move a card from hand to discard pile that wasn't in hand to begin with: " + card.getName());
            }
        }

        public void removeFromHand(Card card)
        {
            if (_hand.Contains(card))
            {
                card.onRemove(); //in case there's any mechanics for it being removed
                _removedCards.Add(card);
                _hand.Remove(card);
            }
            else
            {
                Game1.errorLog.Add("Tried to move a card from hand to removed cards that wasn't in hand to begin with: " + card.getName());
            }
        }

        public void moveFromHandToDraw(Card card)
        {
            if (_hand.Contains(card))
            {
                _drawPile.Add(card);
                _hand.Remove(card);
            }
            else
            {
                Game1.errorLog.Add("Tried to move a card from hand to draw pile that wasn't in hand to begin with: " + card.getName());
            }
        }

        public void pullCardFromDiscardToHand(Card card)
        {
            if (_discardPile.Contains(card))
            {
                _hand.Add(card);
                _discardPile.Remove(card);
            }
            else
            {
                Game1.errorLog.Add("Tried to move a card from discard pile to hand that wasn't in discard pile to begin with: " + card.getName());
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
                Game1.errorLog.Add("Tried to move a card from discard pile to draw pile that wasn't in discard pile to begin with: " + card.getName());
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
                Game1.errorLog.Add("Tried to move a card from discard pile to removed cards that wasn't in discard pile to begin with: " + card.getName());
            }
        }

        public void pullCardFromDrawToHand(Card card)
        {
            if (_drawPile.Contains(card))
            {
                _hand.Add(card);
                _drawPile.Remove(card);
            }
            else
            {
                Game1.errorLog.Add("Tried to move a card from draw pile to hand that wasn't in draw pile to begin with: " + card.getName());
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
                Game1.errorLog.Add("Tried to move a card from draw pile to discard pile that wasn't in draw pile to begin with: " + card.getName());
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
                Game1.errorLog.Add("Tried to move a card from draw pile to removed cards that wasn't in draw pile to begin with: " + card.getName());
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
                Game1.errorLog.Add("Tried to move a card from removed cards to discard pile that wasn't in removed cards to begin with: " + card.getName());
            }
        }

        public void bringRemovedBackToHand(Card card)
        {
            if (_removedCards.Contains(card))
            {
                _hand.Add(card);
                _removedCards.Remove(card);
            }
            else
            {
                Game1.errorLog.Add("Tried to move a card from removed cards to hand that wasn't in removed cards to begin with: " + card.getName());
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
    }
}