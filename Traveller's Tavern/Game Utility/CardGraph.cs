using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameutil
{
    /// <summary>
    /// A directed graph for mapping relationships between cards with other cards or players directly. 
    /// This graph is constructed of nodes that group cards by similar behavior or relationships.
    /// Edges represent the relationship one card has to another card or player, such as an effect,
    /// an event that can be responded to, etc.
    /// </summary>
    public class CardGraph<I, T>
    {
        private readonly Dictionary<I, Node> Nodes = new Dictionary<I, Node>();

        private class Edge
        {
            public T Data;

            public Node Destination;

            public Edge(T data)
            {
                Data = data;
            }
        }

        private class Node
        {
            public HashSet<Card> Cards = new HashSet<Card>();
            public List<Edge> Edges = new List<Edge>();
            public I Identifier;
        }
    }
}
