using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
namespace MyGame
{
    public class CardDeck
    {

        private Random _random = new Random();
        private List<Card> _deckOfCards;
        public Random Random { get => _random; set => _random = value; }

        public List<Card> DeckOfCard
        {
            get
            { return _deckOfCards; }
            set
            {
                _deckOfCards = value;
            }
        }

            public CardDeck()
        {
            _deckOfCards = new List<Card>();
            InitializeDeck();
        }

        public void InitializeDeck()
        {
            // Creates card objects. NEVER MAKES AGAIN. This function is the only one that has the word "new" in it. No where else.
            Array SuitArray = Enum.GetValues(typeof(Suit));//
            foreach (Suit s in SuitArray)
            {
                for (int i = 0; i <= 11; i++)
                {
                    Rank r = (Rank)i;
                    NormalCard NewCard = new NormalCard(s, r);

                    DeckOfCard.Add(NewCard);
                }
            }
           //DeckOfCard.Add(new JackOneEye(Suit.Heart));
           //DeckOfCard.Add(new JackOneEye(Suit.Spades));
           //DeckOfCard.Add(new JackTwoEyes(Suit.Clubs));
           //DeckOfCard.Add(new JackTwoEyes(Suit.Diamonds));
            // 52 cards in total.
            foreach (Suit s in SuitArray)
            {
                for (int i = 0; i <= 11; i++)
                {
                    Rank r = (Rank)i;
                    NormalCard NewCard = new NormalCard(s, r);

                    DeckOfCard.Add(NewCard);
                }
            }
            DeckOfCard.Add(new NormalCard(Suit.Diamonds, Rank.Five));
            DeckOfCard.Add(new NormalCard(Suit.Clubs, Rank.Five));
            DeckOfCard.Add(new NormalCard(Suit.Spades, Rank.Six));
            DeckOfCard.Add(new NormalCard(Suit.Diamonds, Rank.Seven));
            //DeckOfCard.Add(new JackOneEye(Suit.Heart));
            //DeckOfCard.Add(new JackOneEye(Suit.Spades));
            //// 100 cards in total.
        }
        public void ShuffleDeck()
        {
            // Randomises the deck..

            List<Card> _newDeck = new List<Card>();  //Create a temporary list of cards (empty)
            while (_deckOfCards.Count > 0)
            {
                int k = Random.Next(0,_deckOfCards.Count);  //get a random index in the ;current; deck
                _newDeck.Add(_deckOfCards[k]);     // add that card to the new deck
                _deckOfCards.RemoveAt(k);         // remove from the old deck
            }
            _deckOfCards = _newDeck;            // replace current deck with new "shuffled" deck

        }

        public Card DealCard()
        {
            // If _cards.Count > 0 //safety check...
            // Card cardToRemove = _cards[0];
            // _cards.RemoveAt(0);
            // return cardToRemove
            if (_deckOfCards.Count>0)
            {
                Card CardToRemove = _deckOfCards[0];
                _deckOfCards.RemoveAt(0);
                return CardToRemove;
            }
            return null;
        }

        public void AddCards(Stack<Card> cardsToAdd)
        {
            // _cards.AddRange(cardsToAdd);
            // OR you can do soethign like
            // Foreach (Card c in cardsToAdd)
            // _cards.add(c);
            foreach (Card c in cardsToAdd)
            {
                _deckOfCards.Add(c);
            }
        }
    }
}
