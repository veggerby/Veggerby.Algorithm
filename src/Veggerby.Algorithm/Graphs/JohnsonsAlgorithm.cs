using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Graphs
{
    /// From http://www.sanfoundry.com/java-program-to-implement-johnsons-algorithm/
    public class JohnsonsAlgorithm
    {
        public IEnumerable<Path<T>> GetShortestPath<T>(Graph<T> graph, T source)
        {
            // https://en.wikipedia.org/wiki/Johnson%27s_algorithm
            // http://www.geeksforgeeks.org/johnsons-algorithm/

            var bellmanFord = new BellmanFord();
            var dijsktraShortestPath = new DijkstraShortestPath();

            // step 1: First, a new node q is added to the graph, connected by zero-weight edges to each of the other nodes.
            var augmentedGraph = ComputeAugmentedGraph(graph, source);

            // step 2: Second, the Bellman–Ford algorithm is used, starting from the new vertex q, to find for each vertex v the minimum weight h(v) of a path from q to v. If this step detects a negative cycle, the algorithm is terminated.
            var potential = bellmanFord.BellmanFordEvaluation(source, augmentedGraph);

            // step 3: Next the edges of the original graph are reweighted using the values computed by the Bellman–Ford algorithm: an edge from u to v, having length w(u,v), is given the new length w(u,v) + h(u) − h(v).
            var reweightedGraph = ReweightGraph(graph, potential);

            // step 4: Finally, q is removed, and Dijkstra's algorithm is used to find the shortest paths from each node s to every other vertex in the reweighted graph.
            var result = new List<Path<T>>();
            foreach (var from in graph.Vertices)
            {
                var paths = dijsktraShortestPath.CalculateShortestPaths(from, reweightedGraph);
                result.AddRange(paths.Select(x => GetOriginalPath(x, graph)));
            }

            return result;
        }

        private Path<T> GetOriginalPath<T>(Path<T> path, Graph<T> originalGraph)
        {
            var edges = new List<Edge<T>>();

            foreach (var edge in path.Edges)
            {
                var originalEdge = originalGraph.GetEdge(edge.From, edge.To);
                edges.Add(originalEdge);
            }

            return new Path<T>(edges);
        }

        private Graph<T> ComputeAugmentedGraph<T>(Graph<T> graph, T q)
        {
            var edges = graph.Vertices.Select(x => new Edge<T>(q, x, 0)).ToList();

            return new Graph<T>(graph.Vertices.Concat(new[] { q }), graph.Edges.Concat(edges));
        }

        private Graph<T> ReweightGraph<T>(Graph<T> graph, IDictionary<T, int> potential)
        {
            return new Graph<T>(
                graph.Vertices,
                graph.Edges
                    .Select(x => new Edge<T>(x.From, x.To, x.Weight + potential[x.From] - potential[x.To])));
        }
    }
}
