namespace gameutil
{
    public class Player
    {
        public int ID { get; private set; }

        public string Name { get; private set;  }

        public bool Alive = true;

        // Players start with default 10 gold in a normal game
        public int Gold = 10;

        // All players start with default 20 vitality
        public int Vitality = 20;

        // Intoxication level. Players start with 0 intoxication
        public int Intox = 0;

        public Stack<Card> CardDeck;

        public Stack<Card> Hand;

        public Stack<Card> CardDiscard;

        public Stack<Drink> DrinkDeck;

        public Stack<Drink> DrinkDiscard;
    }
}