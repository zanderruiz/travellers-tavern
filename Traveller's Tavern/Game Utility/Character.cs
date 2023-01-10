using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameutil
{
    public class Character
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public List<Card> CardDeck { get; private set; }

        /// <summary>
        /// Constructs a default Character, used for deserialization.
        /// </summary>
        public Character()
        {
            Name = "Default Name";
            Description = "Default Description";
            CardDeck= new List<Card>();
        }

        /// <summary>
        /// Constructs a new Character with the given name, description, and
        /// card deck.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="cardDeck"></param>
        public Character(string name, string description, List<Card> cardDeck)
        {
            Name = name;
            Description = description;
            CardDeck = cardDeck;
        }
    }
}
