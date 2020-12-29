namespace DirectedGraph
{
    public class Edge
    {
        public Node From { get; private set; }
        public Node To { get; private set; }
        public int Weight { get; private set; }

        public Edge(Node from, Node to, int weight = 0)
        {
            this.From = from;
            this.To = to;
            this.Weight = weight;
        }

        public override string ToString()
        {
            return $"{this.From} -> {this.To}";
        }
    }
}
