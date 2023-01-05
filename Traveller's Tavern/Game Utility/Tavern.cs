using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameutil
{
    internal class Tavern
    {
        public Dictionary<int, Player> Players;
        public Stack<Drink> DrinkDeck;
        public Stack<Drink> DrinkDiscard;
    }
}
