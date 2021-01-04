using System;
using System.Collections.Generic;
using System.Linq;

namespace DirectedGraph
{
    public class GraphFunctions
    {
        public static Queue<T> GetNeighbours<T>(Graph graph, T searchItem)
        {
            Queue<T> returnValue = new Queue<T>();

            if (graph != null && searchItem != null)
            {
                var searchEdges = graph.Edges.Where(e => string.Equals(e.From.ToString(), searchItem));
                foreach (Edge edge in searchEdges)
                {
                    if (edge.To.ToString() is T toAsT)
                    {
                        returnValue.Enqueue(toAsT);
                    }
                }
            }
            else
            {
                throw new ArgumentNullException();
            }

            return returnValue;
        }

        public static bool BreadthFirstSearch<T>(Graph graph, T searchItem)
        {
            var returnValue = false;

            if (graph != null && searchItem != null)
            {
                Queue<T> searchQueue = new Queue<T>();
                List<T> searchedItems = new List<T>();

                if (graph.Nodes[0].ToString() is T nodeAsT)
                {
                    searchQueue.Enqueue(nodeAsT);

                    while(searchQueue.Count > 0)
                    {
                        var queuedItem = searchQueue.Dequeue();

                        if (!searchedItems.Contains(queuedItem))
                        {
                            if (queuedItem.Equals(searchItem))
                            {
                                returnValue = true;
                                break;
                            }
                            else
                            {
                                var items = GetNeighbours<T>(graph, queuedItem);

                                foreach(var item in items)
                                {
                                    searchQueue.Enqueue(item);
                                }

                                searchedItems.Add(queuedItem);
                            }
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentNullException();
            }

            return returnValue;
        }

        public static ValueTuple<Node, int?> GetNextLowestCostNeighbour(Graph graph, Node startNode, List<Node> reviewedNodes = null)
        {
            var returnValue = new ValueTuple<Node, int?>(null, null);

            if (graph != null && startNode != null)
            {
                var result = graph.Edges.Where(e => e.From == startNode).OrderBy(r => r.Weight).ToList();

                foreach(Edge edge in result)
                {
                    if (reviewedNodes == null || !reviewedNodes.Contains(edge.To))
                    {
                        returnValue.Item1 = edge.To;
                        returnValue.Item2 = edge.Weight;

                        break;
                    }
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
            return returnValue;
        }

        public static Node FindLowestCostNode(Dictionary<Node, ValueTuple<uint, Node>> dictionary, List<Node> processedNodes)
        {
            Node returnValue = null;

            if (dictionary != null && dictionary.Count > 0 && processedNodes != null)
            {
                uint lowestNodeCost = int.MaxValue;

                foreach(var item in dictionary)
                {
                    if (item.Value.Item1 < lowestNodeCost && !processedNodes.Contains(item.Key))
                    {
                        lowestNodeCost = item.Value.Item1;
                        returnValue = item.Key;
                    }
                }
            }
            else if (dictionary == null || processedNodes == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                throw new ArgumentException();
            }

            return returnValue;
        }

        public static ValueTuple<List<string>, uint> ReturnShortestPath(Dictionary<Node, ValueTuple<uint, Node>> dictionary, string endPoint)
        {
            var returnValue = new ValueTuple<List<string>, uint>();

            if (dictionary != null && dictionary.Count > 0 && endPoint != null)
            {
                var searchNode = dictionary.FirstOrDefault(d => d.Key != null ? string.Equals(d.Key.ToString(), endPoint) : false);

                if (searchNode.Key != null)
                {
                    returnValue.Item1 = new List<string>();
                    returnValue.Item2 = searchNode.Value.Item1;

                    while (searchNode.Key != null && searchNode.Value.Item2 != null)
                    {
                        returnValue.Item1.Insert(0, searchNode.Key.ToString());

                        searchNode = searchNode.Value.Item2 == null ? new KeyValuePair<Node, ValueTuple<uint, Node>>(null, (int.MaxValue, null)) : 
                                                                      dictionary.FirstOrDefault(d => string.Equals(d.Key.ToString(), searchNode.Value.Item2.ToString()));
                    }

                    returnValue.Item1.Insert(0, searchNode.Key.ToString());
                }
            }
            else if (dictionary == null || endPoint == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                throw new ArgumentException();
            }

            return returnValue;
        }

        public static ValueTuple<List<string>, uint> Dijkstra<T>(Graph graph, T endPoint)
        {
            var returnValue = new ValueTuple<List<string>, uint>();

            if (graph != null && endPoint != null)
            {
                var shortestPaths = new Dictionary<Node, ValueTuple<uint, Node>>();
                var processed = new List<Node>();

                // Get a list of all the nodes
                foreach(var node in graph.Nodes)
                {
                    shortestPaths.Add(node, (int.MaxValue, null));
                }

                shortestPaths[graph.Nodes[0]] = (0, null);
                var searchNode = FindLowestCostNode(shortestPaths, processed);

                List<Node> checkedNodes = new List<Node>();

                uint cost;
                while (searchNode != null)
                {
                    cost = shortestPaths[searchNode].Item1;
                    var neighbours = graph.Edges.Where(e => e.From == searchNode);

                    foreach(var edge in neighbours)
                    {
                        uint newCost = (uint)(cost + edge.Weight);
                        if (newCost < shortestPaths[edge.To].Item1)
                        {
                            shortestPaths[edge.To] = (newCost, searchNode);
                        }
                    }

                    processed.Add(searchNode);
                    searchNode = FindLowestCostNode(shortestPaths, processed);
                }

                returnValue = ReturnShortestPath(shortestPaths, endPoint.ToString());
            }
            else
            {
                throw new ArgumentNullException();
            }

            return returnValue;
        }
    }
}
