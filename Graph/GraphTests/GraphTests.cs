using DirectedGraph;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GraphTests
{
    public class GetNeighbourTests
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

    public class BreadthFirstSearchTests
    {
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

    public class DijkstraTests
    {
        private static IEnumerable<TestCaseData> _testValidPathLists()
        {
            var testGraph = CreateWeightedTestGraph();

            yield return new TestCaseData(testGraph,
                                          "A",
                                          new ValueTuple<List<string>, uint>(new List<string>() { "Start", "B", "A" }, 5));
            yield return new TestCaseData(testGraph,
                                          "Finish",
                                          new ValueTuple<List<string>, uint>(new List<string>() { "Start", "B", "Finish" }, 5));



            List<Node> nodesExA = new List<Node>();
            nodesExA.Add(new Node<string>("Start"));
            nodesExA.Add(new Node<string>("A"));
            nodesExA.Add(new Node<string>("B"));
            nodesExA.Add(new Node<string>("C"));
            nodesExA.Add(new Node<string>("D"));
            nodesExA.Add(new Node<string>("Finish"));

            List<Edge> edgesExA = new List<Edge>();
            edgesExA.Add(new Edge(nodesExA[0], nodesExA[1], 5));
            edgesExA.Add(new Edge(nodesExA[0], nodesExA[2], 2));
            edgesExA.Add(new Edge(nodesExA[1], nodesExA[3], 4));
            edgesExA.Add(new Edge(nodesExA[1], nodesExA[4], 2));
            edgesExA.Add(new Edge(nodesExA[2], nodesExA[1], 8));
            edgesExA.Add(new Edge(nodesExA[2], nodesExA[4], 7));
            edgesExA.Add(new Edge(nodesExA[3], nodesExA[4], 6));
            edgesExA.Add(new Edge(nodesExA[3], nodesExA[5], 3));
            edgesExA.Add(new Edge(nodesExA[4], nodesExA[5], 1));

            var testGraphExA =  new Graph(edgesExA, nodesExA);

            yield return new TestCaseData(testGraphExA,
                                          "Finish",
                                          new ValueTuple<List<string>, uint>(new List<string>() { "Start", "A", "D", "Finish" }, 8));

            List<Node> nodesExB = new List<Node>();
            nodesExB.Add(new Node<string>("Start"));
            nodesExB.Add(new Node<string>("A"));
            nodesExB.Add(new Node<string>("B"));
            nodesExB.Add(new Node<string>("C"));
            nodesExB.Add(new Node<string>("Finish"));

            List<Edge> edgesExB = new List<Edge>();
            edgesExB.Add(new Edge(nodesExB[0], nodesExB[1], 10));
            edgesExB.Add(new Edge(nodesExB[1], nodesExB[2], 20));
            edgesExB.Add(new Edge(nodesExB[2], nodesExB[3], 1));
            edgesExB.Add(new Edge(nodesExB[2], nodesExB[4], 30));
            edgesExB.Add(new Edge(nodesExB[3], nodesExB[1], 1));

            var testGraphExB = new Graph(edgesExB, nodesExB);

            yield return new TestCaseData(testGraphExB,
                                          "Finish",
                                          new ValueTuple<List<string>, uint>(new List<string>() { "Start", "A", "B", "Finish" }, 60));

            List<Node> nodesExC = new List<Node>();
            nodesExC.Add(new Node<string>("Start"));
            nodesExC.Add(new Node<string>("A"));
            nodesExC.Add(new Node<string>("B"));
            nodesExC.Add(new Node<string>("C"));
            nodesExC.Add(new Node<string>("Finish"));

            List<Edge> edgesExC = new List<Edge>();
            edgesExC.Add(new Edge(nodesExC[0], nodesExC[1], 2));
            edgesExC.Add(new Edge(nodesExC[0], nodesExC[2], 2));
            edgesExC.Add(new Edge(nodesExC[1], nodesExC[3], 2));
            edgesExC.Add(new Edge(nodesExC[1], nodesExC[4], 2));
            edgesExC.Add(new Edge(nodesExC[2], nodesExC[1], 2));
            edgesExC.Add(new Edge(nodesExC[3], nodesExC[2], -1));
            edgesExC.Add(new Edge(nodesExC[3], nodesExC[4], 2));

            var testGraphExC = new Graph(edgesExC, nodesExC);

            yield return new TestCaseData(testGraphExC,
                                          "Finish",
                                          new ValueTuple<List<string>, uint>(new List<string>() { "Start", "A", "Finish" }, 4));
        }

        [Test]
        public void Dijkstra_ThrowsArgumentNullException_WhenGraphIsNull()
        {
            Graph testGraph = null;

            Assert.That(() => GraphFunctions.Dijkstra<string>(testGraph, ""), Throws.ArgumentNullException);
        }

        [Test]
        public void Dijkstra_ThrowsArgumentNullException_WhenSearchItemIsNull()
        {
            Graph testGraph = new Graph(new List<Edge>(), new List<Node>());

            Assert.That(() => GraphFunctions.Dijkstra<string>(testGraph, null), Throws.ArgumentNullException);
        }

        [Test]
        public void Dijkstra_ReturnsNull_WhenSearchItemIsNotFound()
        {
            var testGraph = CreateWeightedTestGraph();
            var expectedResult = new ValueTuple<List<string>, uint>(null, 0);

            var result = GraphFunctions.Dijkstra<string>(testGraph, "NotValid");

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCaseSource("_testValidPathLists")]
        public void Dijkstra_ReturnsShortestPath_WhenVariablesAreValid(Graph testGraph, string searchItem, ValueTuple<List<string>, uint> expectedResult)
        {
            var result = GraphFunctions.Dijkstra<string>(testGraph, searchItem);

            Assert.AreEqual(expectedResult, result);
        }

        private static Graph CreateWeightedTestGraph()
        {
            List<Node> nodes = new List<Node>();
            nodes.Add(new Node<string>("Start"));
            nodes.Add(new Node<string>("A"));
            nodes.Add(new Node<string>("B"));
            nodes.Add(new Node<string>("Finish"));

            List<Edge> edges = new List<Edge>();
            edges.Add(new Edge(nodes[0], nodes[1], 6));
            edges.Add(new Edge(nodes[0], nodes[2], 2));
            edges.Add(new Edge(nodes[1], nodes[3], 1));
            edges.Add(new Edge(nodes[2], nodes[1], 3));
            edges.Add(new Edge(nodes[2], nodes[3], 3));

            return new Graph(edges, nodes);
        }
    }

    public class GetNextLowestCostNeighbourTests
    {
        private static readonly Node<string>[] _testUnknownNodes =
        {
            new Node<string>("a"),
            new Node<string>("D"),
            new Node<string>("Finish")
        };

        [Test]
        public void GetNextLowestCostNeighbour_ThrowsNullArgumentException_WhenGraphIsNull()
        {
            Graph testGraph = null;

            Assert.That(() => GraphFunctions.GetNextLowestCostNeighbour(testGraph, new Node<string>("Payload")), Throws.ArgumentNullException);
        }

        [Test]
        public void GetNextLowestCostNeighbour_ThrowsNullArgumentException_WhenSearchItemIsNull()
        {
            Graph testGraph = new Graph(new List<Edge>(), new List<Node>());

            Assert.That(() => GraphFunctions.GetNextLowestCostNeighbour(testGraph, null), Throws.ArgumentNullException);
        }

        [Test]
        [TestCaseSource("_testUnknownNodes")]
        public void GetNextLowestCostNeighbour_ReturnsNull_WhenStartItemNotFound(Node<string> startNode)
        {
            var testGraph = CreateWeightedTestGraph();

            var result = GraphFunctions.GetNextLowestCostNeighbour(testGraph, startNode);

            Assert.AreEqual(new ValueTuple<Node, int?>(null, null), result);
        }

        [Test]
        [TestCase(0, new int[] { }, 2, 2)]
        [TestCase(1, new int[] { }, 3, 1)]
        [TestCase(2, new int[] { }, 1, 3)]
        [TestCase(0, new int[] { 2 }, 4, 2)]
        [TestCase(0, new int[] { 2, 4 }, 1, 6)]
        public void GetNextLowestCostNeighbour_ReturnsLowestNode_WhenStartNodeFound(int startNodeIndex, int[] searchedNodeIndexes, int expectedResultNodeIndex, int expectedResultWeight)
        {
            var testGraph = CreateWeightedTestGraph();
            ValueTuple<Node, int> expectedResult = new ValueTuple<Node, int>(testGraph.Nodes[expectedResultNodeIndex], expectedResultWeight);
            List<Node> searchedNodes = new List<Node>();
            foreach(var index in searchedNodeIndexes)
            {
                searchedNodes.Add(testGraph.Nodes[index]);
            }

            var result = GraphFunctions.GetNextLowestCostNeighbour(testGraph, testGraph.Nodes[startNodeIndex], searchedNodes);

            Assert.AreEqual(expectedResult, result);
        }

        private Graph CreateWeightedTestGraph()
        {
            List<Node> nodes = new List<Node>();
            nodes.Add(new Node<string>("Start"));
            nodes.Add(new Node<string>("A"));
            nodes.Add(new Node<string>("B"));
            nodes.Add(new Node<string>("Finish"));

            nodes.Add(new Node<string>("C"));

            List<Edge> edges = new List<Edge>();
            edges.Add(new Edge(nodes[0], nodes[1], 6));
            edges.Add(new Edge(nodes[0], nodes[2], 2));
            edges.Add(new Edge(nodes[1], nodes[3], 1));
            edges.Add(new Edge(nodes[2], nodes[1], 3));
            edges.Add(new Edge(nodes[2], nodes[3], 5));

            edges.Add(new Edge(nodes[0], nodes[4], 2));
            edges.Add(new Edge(nodes[4], nodes[2], 5));

            return new Graph(edges, nodes);
        }
    }

    public class FindLowestCostNodeTests
    {
        [Test]
        public void FindLowestCostNode_ThrowsNullArgumentException_WhenDictionaryIsNull()
        {
            Dictionary<Node, ValueTuple<uint, Node>> testDictionary = null;
            var testProcessedList = new List<Node>() { };

            Assert.That(() => GraphFunctions.FindLowestCostNode(testDictionary, testProcessedList), Throws.ArgumentNullException);
        }

        [Test]
        public void FindLowestCostNode_ThrowsArgumentException_WhenDictionaryIsEmpty()
        {
            var testDictionary = new Dictionary<Node, (uint, Node)>() { };
            var testProcessedList = new List<Node>() { };

            Assert.That(() => GraphFunctions.FindLowestCostNode(testDictionary, testProcessedList), Throws.ArgumentException);
        }

        [Test]
        public void FindLowestCostNode_ThrowsNullArgumentException_WhenProcessedListIsNull()
        {
            var testDictionary = new Dictionary<Node, (uint, Node)>();
            testDictionary.Add(new Node<string>("B"), (99, null));
            List<Node> testProcessedList = null;

            Assert.That(() => GraphFunctions.FindLowestCostNode(testDictionary, testProcessedList), Throws.ArgumentNullException);
        }

        [Test]
        public void FindLowestCoseNode_ReturnsFirstNode_WhenAllWeightsAreEqual()
        {
            var testNode = new Node<string>("A");
            var testDictionary = new Dictionary<Node, (uint, Node)>();
            testDictionary.Add(testNode, (99, null));
            testDictionary.Add(new Node<string>("B"), (99, null));
            testDictionary.Add(new Node<string>("C"), (99, null));
            testDictionary.Add(new Node<string>("D"), (99, null));
            testDictionary.Add(new Node<string>("E"), (99, null));
            var testProcessedList = new List<Node>() { };

            var result = GraphFunctions.FindLowestCostNode(testDictionary, testProcessedList);

            Assert.AreEqual(testNode, result);
        }

        [Test]
        public void FindLowestCoseNode_ReturnsNull_WhenAllItemsAreInProcessedList()
        {
            var testNode1 = new Node<string>("A");
            var testNode2 = new Node<string>("B");
            var testNode3 = new Node<string>("C");
            var testNode4 = new Node<string>("D");
            var testNode5 = new Node<string>("E");

            var testDictionary = new Dictionary<Node, (uint, Node)>();
            testDictionary.Add(testNode1, (99, null));
            testDictionary.Add(testNode2, (99, null));
            testDictionary.Add(testNode3, (99, null));
            testDictionary.Add(testNode4, (99, null));
            testDictionary.Add(testNode5, (99, null));

            var testProcessedList = new List<Node>() { testNode1, testNode2, testNode3, testNode4, testNode5 };

            var result = GraphFunctions.FindLowestCostNode(testDictionary, testProcessedList);

            Assert.AreEqual(null, result);
        }
    }

    public class ReturnShortestPathTests
    {
        private static IEnumerable<TestCaseData> _testNullArguments()
        {
            yield return new TestCaseData(null, "");
            yield return new TestCaseData(new Dictionary<Node, ValueTuple<uint, Node>>() { }, null);
            yield return new TestCaseData(null, null);
        }

        private static IEnumerable<TestCaseData> _testValidPathLists()
        {
            var testNodeA = new Node<string>("A");
            var testNodeB = new Node<string>("B");
            var testNodeC = new Node<string>("C");
            var testNodeD = new Node<string>("D");
            var testNodeE = new Node<string>("E");
            var testNodeF = new Node<string>("F");

            yield return new TestCaseData(new Dictionary<Node, ValueTuple<uint, Node>>()
                                            {
                                                { testNodeA, (0, null) },
                                                { testNodeB, (7, new Node<string>("A")) },
                                                { testNodeC, (3, new Node<string>("A")) },
                                                { testNodeD, (5, new Node<string>("C")) },
                                                { testNodeE, (6, new Node<string>("D")) }
                                            },
                                            "E",
                                            new ValueTuple<List<string>, uint>(new List<string>() { "A", "C", "D", "E" }, 6));
            yield return new TestCaseData(new Dictionary<Node, ValueTuple<uint, Node>>()
                                            {
                                                { testNodeA, (0, null) },
                                                { testNodeB, (1, new Node<string>("A")) },
                                                { testNodeC, (6, new Node<string>("A")) },
                                                { testNodeD, (13, new Node<string>("C")) },
                                                { testNodeE, (6, new Node<string>("B")) }
                                            },
                                            "E",
                                            new ValueTuple<List<string>, uint>(new List<string>() { "A", "B", "E" }, 6));
            yield return new TestCaseData(new Dictionary<Node, ValueTuple<uint, Node>>()
                                            {
                                                { testNodeA, (0, null) },
                                                { testNodeB, (1, new Node<string>("A")) },
                                                { testNodeC, (2, new Node<string>("B")) },
                                                { testNodeD, (3, new Node<string>("C")) },
                                                { testNodeE, (3, new Node<string>("A")) },
                                                { testNodeF, (10, new Node<string>("C")) }
                                            },
                                            "D",
                                            new ValueTuple<List<string>, uint>(new List<string>() { "A", "B", "C", "D" }, 3));
        }

        [Test]
        [TestCaseSource("_testNullArguments")]
        public void ReturnShortestPath_ThrowsNullArgumentException_WhenArgumentIsNull(Dictionary<Node, ValueTuple<uint, Node>> testDictionary, string endPoint)
        {
            Assert.That(() => GraphFunctions.ReturnShortestPath(testDictionary, endPoint), Throws.ArgumentNullException);
        }

        [Test]
        public void ReturnShortestPath_ThrowsArgumentException_WhenDictionaryIsEmpty()
        {
            var testDictionary = new Dictionary<Node, ValueTuple<uint, Node>>() { };

            Assert.That(() => GraphFunctions.ReturnShortestPath(testDictionary, ""), Throws.ArgumentException);
        }

        [Test]
        public void ReturnShortestPath_ReturnsEmptyList_WhenEndPointIsNotFound()
        {
            var testDictionary = new Dictionary<Node, (uint, Node)>();
            testDictionary.Add(new Node<string>("A"), (99, null));
            testDictionary.Add(new Node<string>("B"), (99, null));
            testDictionary.Add(new Node<string>("C"), (99, null));
            testDictionary.Add(new Node<string>("D"), (99, null));
            testDictionary.Add(new Node<string>("E"), (99, null));

            var result = GraphFunctions.ReturnShortestPath(testDictionary, "Z");

            Assert.IsNull(result.Item1);
        }

        [Test]
        [TestCaseSource("_testValidPathLists")]
        public void ReturnShortestPath_ReturnsCorrectPath_WhenEndPointIsFound(Dictionary<Node, ValueTuple<uint, Node>> testDictionary, string endPoint, ValueTuple<List<string>, uint> expectedResult)
        {
            var result = GraphFunctions.ReturnShortestPath(testDictionary, endPoint);

            Assert.AreEqual(expectedResult, result);
        }
    }
}