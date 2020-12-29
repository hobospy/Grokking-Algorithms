using DirectedGraph;
using NUnit.Framework;
using System.Collections.Generic;

namespace GraphTests
{
    public class Tests
    {
        [Test]
        public void GetNeighbours_ThrowsArgumentNullException_WhenGraphIsNull()
        {
            Graph testGraph = null;

            Assert.That(() => GraphFunctions.GetNeighbours<string>(testGraph, ""), Throws.ArgumentNullException);
        }

        [Test]
        public void GetNeighbours_ThrowsArgumentNullException_WhenSearchItemIsNull()
        {
            Graph testGraph = new Graph(new List<Edge>(), new List<Node>());

            Assert.That(() => GraphFunctions.GetNeighbours<string>(testGraph, null), Throws.ArgumentNullException);
        }

        [Test]
        public void GetNeighbours_ReturnsEmptyQueue_WhenGraphIsEmptyAndValidSearchItem()
        {
            Graph testGraph = new Graph(new List<Edge>(), new List<Node>());

            var result = GraphFunctions.GetNeighbours<string>(testGraph, "");

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        [TestCase("you", new string[] { "Alice", "Bob", "Claire" })]
        [TestCase("Claire", new string[] { "Jonny", "Thoh" })]
        [TestCase("Alice", new string[] { "Peggy" })]
        public void GetNeighbours_ReturnsCorrectQueue_WhenGraphAndSearchItemAreValid(string searchItem, string[] expectedResult)
        {
            Graph testGraph = CreateWeightFreeTestGraph();

            var result = GraphFunctions.GetNeighbours<string>(testGraph, searchItem);

            Assert.AreEqual(expectedResult, result.ToArray());
        }

        [Test]
        public void BreadthFirstSearch_ThrowsArgumentNullException_WhenGraphIsNull()
        {
            Graph testGraph = null;

            Assert.That(() => GraphFunctions.BreadthFirstSearch<string>(testGraph, ""), Throws.ArgumentNullException);
        }

        [Test]
        public void BreadthFirstSearch_ThrowsArgumentNullException_WhenSearchItemIsNull()
        {
            Graph testGraph = new Graph(new List<Edge>(), new List<Node>());

            Assert.That(() => GraphFunctions.BreadthFirstSearch<string>(testGraph, null), Throws.ArgumentNullException);
        }

        [Test]
        [TestCase("you")]
        [TestCase("Alice")]
        [TestCase("Bob")]
        [TestCase("Claire")]
        [TestCase("Anuj")]
        [TestCase("Peggy")]
        [TestCase("Jonny")]
        [TestCase("Thoh")]
        public void BreadthFirstSearch_ReturnsTrue_WhenGraphContainsSearchItem(string searchItem)
        {
            Graph testGraph = CreateWeightFreeTestGraph();

            var result = GraphFunctions.BreadthFirstSearch<string>(testGraph, searchItem);

            Assert.IsTrue(result);
        }

        [Test]
        [TestCase("")]
        [TestCase("YoU")]
        [TestCase("a")]
        [TestCase("ZZ")]
        [TestCase("Claires")]
        public void BreadthFirstSearch_ReturnsFalse_WhenGraphDoesNotContainSearchItem(string searchItem)
        {
            Graph testGraph = CreateWeightFreeTestGraph();

            var result = GraphFunctions.BreadthFirstSearch<string>(testGraph, searchItem);

            Assert.IsFalse(result);
        }

        private Graph CreateWeightFreeTestGraph()
        {
            List<Node> nodes = new List<Node>();
            nodes.Add(new Node<string>("you"));
            nodes.Add(new Node<string>("Alice"));
            nodes.Add(new Node<string>("Bob"));
            nodes.Add(new Node<string>("Claire"));
            nodes.Add(new Node<string>("Anuj"));
            nodes.Add(new Node<string>("Peggy"));
            nodes.Add(new Node<string>("Jonny"));
            nodes.Add(new Node<string>("Thoh"));

            List<Edge> edges = new List<Edge>();
            edges.Add(new Edge(nodes[0], nodes[1]));
            edges.Add(new Edge(nodes[0], nodes[2]));
            edges.Add(new Edge(nodes[0], nodes[3]));
            edges.Add(new Edge(nodes[1], nodes[5]));
            edges.Add(new Edge(nodes[2], nodes[5]));
            edges.Add(new Edge(nodes[2], nodes[4]));
            edges.Add(new Edge(nodes[3], nodes[6]));
            edges.Add(new Edge(nodes[3], nodes[7]));

            return new Graph(edges, nodes);
        }
    }
}