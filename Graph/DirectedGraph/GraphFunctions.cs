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
    }
}
