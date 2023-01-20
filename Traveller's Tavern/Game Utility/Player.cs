using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace gameutil
{
    public class Player
    {
        public int ID { get; private set; }

        public string Name { get; private set;  }

        public bool Alive = true;

        public Character Character { get; private set; }

        // Players start with default 10 gold in a normal game
        public int Gold = 10;

        // All players start with default 20 vitality
        public int Vitality = 20;

        // Intoxication level. Players start with 0 intoxication
        public int Intox = 0;

        public Stack<Card> CardDeck;

        public HashSet<Card> Hand;

        public Stack<Card> CardDiscard;

        public Stack<Drink> DrinkPile;

        public Player(int id, string name, Character character)
        {
            ID = id;
            Name = name;
            Character = character;

            Random rng = new Random();
            List<Card> shuffledCards = character.CardDeck.OrderBy(a => rng.Next()).ToList();
            CardDeck = new Stack<Card>(shuffledCards);

            Hand = new HashSet<Card>();
            CardDiscard = new Stack<Card>(shuffledCards);
            DrinkPile = new Stack<Drink>();
        }

        public Player(int id, string name, Character character, int gold, int vitality, int intox)
        {
            ID = id;
            Name = name;
            Character = character;

            Gold = gold;
            Vitality = vitality;
            Intox= intox;

            Random rng = new Random();
            List<Card> shuffledCards = character.CardDeck.OrderBy(a => rng.Next()).ToList();
            CardDeck = new Stack<Card>(shuffledCards);

            Hand = new HashSet<Card>();
            CardDiscard = new Stack<Card>(shuffledCards);
            DrinkPile = new Stack<Drink>();
        }

        /// <summary>
        /// Moves a card from the player's card deck to the player's hand.
        /// 
        /// If the player's hand already contains 7 or more cards, does not allow a card to be drawn.
        /// 
        /// If drawing this card results in the card deck being empty, the discard pile is reshuffled
        /// into the card deck.
        /// </summary>
        /// <returns> true if a card was drawn; false otherwise </returns>
        public bool DrawCard()
        {
            // If player's hand is already at 7, player can't draw anymore cards
            if (Hand.Count >= 7) return false;

            Hand.Add(CardDeck.Pop());

            // If card deck runs out after this draw, reshuffle the discard pile into the card deck
            if (CardDeck.Count == 0) ReshuffleDiscard();

            return true;
        }

        /// <summary>
        /// Moves the given card from the player's hand to the discard pile.
        /// 
        /// Throws an ArgumentException if the card to be discarded is not
        /// present in the player's hand.
        /// </summary>
        /// <param name="card"> The card to be discarded. </param>
        /// <exception cref="ArgumentException"> 
        /// Throws if the card to be discarded is not currently in the player's hand.
        /// </exception>
        public void DiscardCard(Card card)
        {
            if (!Hand.Remove(card))
            {
                throw new ArgumentException("The card attempting to be discarded is not" +
                    "currently in the player's hand.");
            }

            CardDiscard.Push(card);
        }

        /// <summary>
        /// Returns a list containing the top cards of the player's card deck equal to the
        /// amount given. If the amount of cards in the deck is less than the amount given,
        /// returns as many cards as are in the deck.
        /// </summary>
        /// <param name="numberOfCards">
        /// The number of cards to peek at from the card deck.
        /// </param>
        /// <returns> 
        /// A list of cards from the top of the pile up to the number given or the amount
        /// of cards currently in the deck.
        /// </returns>
        public List<Card> PeekCardDeck(int numberOfCards)
        {
            return PeekCards(numberOfCards, CardDeck);
        }

        /// <summary>
        /// Returns a list containing the top cards of the player's discard pile equal to the
        /// amount given. If the amount of cards in the discard pile is less than the amount
        /// given, returns as many cards as are in the discard pile.
        /// </summary>
        /// <param name="numberOfCards">
        /// The number of cards to peek at from the discard pile.
        /// </param>
        /// <returns> 
        /// A list of cards from the top of the discard pile up to the number given or the amount
        /// of cards currently in the pile.
        /// </returns>
        public List<Card> PeekDiscardPile(int numberOfCards)
        {
            return PeekCards(numberOfCards, CardDiscard);
        }

        /// <summary>
        /// Returns a list containing the top drinks of the player's drink pile equal to the
        /// amount given. If the amount of drinks in the drink pile is less than the amount
        /// given, returns as many drinks as are in the drink pile.
        /// </summary>
        /// <param name="numberOfCards">
        /// The number of drinks to peek at from the drink pile.
        /// </param>
        /// <returns> 
        /// A list of drinks from the top of the drink pile up to the number given or the amount
        /// of drinks currently in the pile.
        /// </returns>
        public List<Drink> PeekDrinkPile(int numberOfDrinks)
        {
            return PeekCards(numberOfDrinks, DrinkPile);
        }

        /// <summary>
        /// Helper method that returns a list containing the top items from the given
        /// stack equal to the amount given. If the amount of items in the stack is
        /// less than the amount given, returns as many items as are in the stack.
        /// </summary>
        /// <param name="numberOfCards">
        /// The number of items to peek at from the given stack.
        /// </param>
        /// <returns> 
        /// A list of items from the top of the stack to the number given or the amount
        /// of items currently in the stack.
        /// </returns>
        private List<T> PeekCards<T>(int numberOfCards, Stack<T> stack)
        {
            List<T> items = new List<T>();

            // If CardDiscard.Count > numberOfCards, return numberOfCards cards; else, return as many cards as are in the deck
            int cardOutputNum = stack.Count > numberOfCards ? numberOfCards : stack.Count;

            for (int i = 0; i < cardOutputNum; i++)
            {
                items.Add(stack.Pop());
            }

            for (int i = cardOutputNum - 1; i >= 0; i--)
            {
                stack.Push(items[i]);
            }

            return items;
        }


        public void ReshuffleDiscard()
        {
            Random rng = new Random();
            List<Card> shuffledCards = CardDiscard.OrderBy(a => rng.Next()).ToList();

            CardDiscard.Clear();

            for (int i = 0; i < shuffledCards.Count; i++)
            {
                CardDeck.Push(shuffledCards[i]);
            }
        }

        internal Player CreateCopy()
        {
            return new Player(ID, Name, Character, Gold, Vitality, Intox);
        }

        internal void RestoreState(Player prevState)
        {

        }
    }
}