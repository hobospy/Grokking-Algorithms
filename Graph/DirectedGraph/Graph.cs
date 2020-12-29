using System;
using System.Collections.Generic;
using System.Text;

namespace DirectedGraph
{
    public class Graph
    {
        public List<Edge> Edges { get; private set; }
        public List<Node> Nodes { get; private set; }

        public Graph(List<Edge> edges, List<Node> nodes)
        {
            this.Edges = edges;
            this.Nodes = nodes;

            foreach(Node node in nodes)
            {
                node.Graph = this;
            }
        }

        public void AddEdge(Edge edge)
        {
            this.Edges.Add(edge);
        }

        public void AddEdge(Node from, Node to)
        {
            this.Edges.Add(new Edge(from, to));
        }

        public void AddNode(Node node)
        {
            this.Nodes.Add(node);
            node.Graph = this;
        }

        public void RemoveEdge(Edge edge)
        {
            this.Edges.Remove(edge);
        }

        public void RemoveNode(Node node)
        {
            this.Edges.RemoveAll(e => e.From == node || e.To == node);
            this.Nodes.Remove(node);
        }
    }
}
