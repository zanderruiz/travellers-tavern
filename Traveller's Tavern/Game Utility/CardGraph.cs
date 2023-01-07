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
    public class CardGraph<I, T> where I : notnull where T : notnull
    {
        private readonly Dictionary<I, Node> Nodes = new();

        /// <summary>
        /// Adds a directional edge from the first node given by id1 to the node
        /// given in id2 with the given data on the edge.
        /// 
        /// Does not add duplicate edges. If an edge with the given data already
        /// exists from the first node to the second, ignores it and returns false.
        /// </summary>
        /// <param name="id1"> The identifier of the first node. </param>
        /// <param name="id2"> The identifier of the second node. </param>
        /// <param name="data"> The data contained in the new edge. </param>
        /// <returns> True if the edge was added; false if the edge already exists </returns>
        /// <exception cref="InvalidNodeException">
        /// Throws if either node given by id1 or id2 does not exist in this graph.
        /// </exception>
        public bool AddEdge(I id1, I id2, T data)
        {
            // Throw exception if either node does not exist in the graph
            if (!Nodes.ContainsKey(id1) || !Nodes.ContainsKey(id2))
            {
                throw new InvalidNodeException();
            }

            Node node1 = Nodes[id1];
            Node node2 = Nodes[id2];

            // If the given data is already one of node1's edges, dont add a duplicate edge and
            // return false
            foreach(Edge edge in node1.Edges)
            {
                if (edge.Data.Equals(data)) return false;
            }

            // At this point, both nodes are confirmed to exist, and the edge requested does not exist, so add the edge
            node1.Edges.Add(new Edge(data, node2));
            return true;
        }

        public bool TryGetEdge(I id1, I id2, ref T data)
        {
            // Return false if either node does not exist in the graph
            if (!Nodes.ContainsKey(id1) || !Nodes.ContainsKey(id2))
            {
                return false;
            }
        }

        public bool AddCardToNode(Card card, I id)
        {
            // Throw exception if the given node does not exist in the graph
            if (!Nodes.TryGetValue(id, out Node? node))
            {
                throw new InvalidNodeException();
            }

            // If the card is already in the node, just return false
            if (node!.Cards.Contains(card)) return false;

            // If card isn't in node yet, add it and return true
            node!.Cards.Add(card);
            return false;
        }

        public void AddNode(I id, )

        private class Edge
        {
            public T Data;

            public Node Destination;

            public Edge(T data, Node destination)
            {
                Data = data;
                Destination = destination;
            }
        }

        private class Node
        {
            public HashSet<Card> Cards = new HashSet<Card>();
            public List<Edge> Edges = new List<Edge>();
            public I Identifier;

            public Node(I identifier)
            {
                Identifier = identifier;
            }
        }
    }

    public class InvalidNodeException : Exception
    {
        public InvalidNodeException()
            : base("The requested node does not exist in the given graph.")
        {
        }

        public InvalidNodeException(string message)
            : base(message)
        {
        }

        public InvalidNodeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
