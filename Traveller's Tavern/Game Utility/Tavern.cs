using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameutil
{
    public class Tavern
    {
        public Dictionary<int, Player> Players { get; private set; }
        public Stack<Drink> DrinkDeck { get; private set; }
        public Stack<Drink> DrinkDiscard;

        public Tavern()
        {
            // TODO: CHANGE LATER TO SET UP THE TAVERN CORRECTLY
            Players = new Dictionary<int, Player>();
            DrinkDeck = new Stack<Drink>();
            DrinkDiscard= new Stack<Drink>();
        }

        private Tavern(IEnumerable<Player> players, Stack<Drink> drinkDeck, Stack<Drink> drinkDiscard)
        {
            Players = new Dictionary<int, Player>();

            foreach (Player player in players)
            {
                Players.Add(player.ID, player.CreateCopy());
            }

            DrinkDeck = new Stack<Drink>(drinkDeck.Reverse());
            DrinkDiscard = new Stack<Drink>(drinkDiscard.Reverse());
        }

        public Tavern CreateSaveState()
        {
            return new Tavern(Players.Values, DrinkDeck, DrinkDiscard);
        }


        public Tavern RestoreState(Tavern prevState)
        {
            // Store the current state to return later
            Tavern currentState = CreateSaveState();

            // Restore the state of each player 
            foreach (Player player in prevState.Players.Values)
            {
                Players[player.ID].RestoreState(player);
            }

            // Restore the drink deck and drink discard pile to their previous states
            DrinkDeck = new Stack<Drink>(prevState.DrinkDeck.Reverse());
            DrinkDiscard = new Stack<Drink>(prevState.DrinkDiscard.Reverse());

            // Return the state the tavern was in before restoring the previous state
            return currentState;
        }
    }
}
