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
            
            Array SuitArray = Enum.GetValues(typeof(Suit));
            foreach (Suit s in SuitArray)
            {
                for (int i = 0; i <= 11; i++)
                {
                    Rank r = (Rank)i;
                    Card NewCard = new Card(s, r);

                    DeckOfCard.Add(NewCard);
                }
            }
            // 48 cards in total.
            foreach (Suit s in SuitArray)
            {
                for (int i = 0; i <= 11; i++)
                {
                    Rank r = (Rank)i;
                    Card NewCard = new Card(s, r);

                    DeckOfCard.Add(NewCard);
                }
            }
            // 4 more cards to make it 100 cards
            for (int i=0;i<=4;i++)
            {
                Rank r0 = (Rank)i;
                Card NewCard2 = new Card(Suit.Clubs, r0);

                DeckOfCard.Add(NewCard2);
            }
            
        }
        public void ShuffleDeck()
        {
            List<Card> _newDeck = new List<Card>();  
            while (_deckOfCards.Count > 0)
            {
                int k = Random.Next(0,_deckOfCards.Count); 
                _newDeck.Add(_deckOfCards[k]);    
                _deckOfCards.RemoveAt(k);         
            }
            _deckOfCards = _newDeck;           
        }

        public Card DealCard()
        {
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
            foreach (Card c in cardsToAdd)
            {
                _deckOfCards.Add(c);
            }
        }
    }
}
