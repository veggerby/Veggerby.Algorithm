using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Graphs
{
    /// based on https://gist.github.com/Sup3rc4l1fr4g1l1571c3xp14l1d0c10u5/3341dba6a53d7171fe3397d13d00ee3f
    public class TopologicalSort
    {
        public static List<T> Sort<T>(Graph<T> graph) where T : IEquatable<T>
        {
            // Empty list that will contain the sorted elements
            var sortedVertices = new List<T>();

            // temporary list of all edges
            var edges = new HashSet<Edge<T>>(graph.Edges);

            // Set of all nodes with no incoming edges
            var S = new HashSet<T>(graph.Vertices.Where(v => !edges.Any(e => e.To.Equals(v))));

            // while S is non-empty do
            while (S.Any())
            {
                //  remove a node n from S
                var n = S.First();
                S.Remove(n);

                // add n to tail of L
                sortedVertices.Add(n);

                // for each node m with an edge e from n to m do
                foreach (var e in edges.Where(e => e.From.Equals(n)).ToList())
                {
                    var m = e.To;

                    // remove edge e from the graph
                    edges.Remove(e);

                    // if m has no other incoming edges then
                    if (edges.All(me => me.To.Equals(m) == false))
                    {
                        // insert m into S
                        S.Add(m);
                    }
                }
            }

            // if graph has edges then
            if (edges.Any())
            {
                // return error (graph has at least one cycle)
                return null;
            }
            else
            {
                // return L (a topologically sorted order)
                return sortedVertices;
            }
        }
    }
}