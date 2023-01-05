using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameutil
{
    public class Drink
    {
        // The effect this drink has on a player's vitality - a drink can add or subtract vitality from a player
        public int VitalEffect { get; private set; }

        // The effect this drink has on a player's intoxication level
        public int IntoxEffect { get; private set; }

        // Indicates whether this drink is a chaser - if it is, the player drinks another drink
        public bool Chaser { get; private set; }
    }

    public enum DrinkType
    {

    }
}
