namespace Veggerby.Algorithm.Graphs;

/// From http://www.sanfoundry.com/java-program-to-implement-johnsons-algorithm/
public static class JohnsonsAlgorithm
{
    public static IEnumerable<Path<T>> GetShortestPath<T>(Graph<T> graph, T source) where T : notnull, IEquatable<T>
    {
        // https://en.wikipedia.org/wiki/Johnson%27s_algorithm
        // http://www.geeksforgeeks.org/johnsons-algorithm/

        // step 1: First, a new node q is added to the graph, connected by zero-weight edges to each of the other nodes.
        var augmentedGraph = ComputeAugmentedGraph(graph, source);

        // step 2: Second, the Bellman–Ford algorithm is used, starting from the new vertex q, to find for each vertex v the minimum weight h(v) of a path from q to v. If this step detects a negative cycle, the algorithm is terminated.
        var potential = BellmanFord.BellmanFordEvaluation(source, augmentedGraph);

        // step 3: Next the edges of the original graph are reweighted using the values computed by the Bellman–Ford algorithm: an edge from u to v, having length w(u,v), is given the new length w(u,v) + h(u) − h(v).
        var reweightedGraph = ReweightGraph(graph, potential);

        // step 4: Finally, q is removed, and Dijkstra's algorithm is used to find the shortest paths from each node s to every other vertex in the reweighted graph.
        var result = new List<Path<T>>();
        foreach (var from in graph.Vertices)
        {
            var paths = DijkstraShortestPath.CalculateShortestPaths(from, reweightedGraph);
            result.AddRange(paths.Select(x => GetOriginalPath(x, graph)));
        }

        return result;
    }

    private static Path<T> GetOriginalPath<T>(Path<T> path, Graph<T> originalGraph) where T : notnull, IEquatable<T>
    {
        var edges = new List<Edge<T>>();

        foreach (var edge in path.Edges)
        {
            var originalEdge = originalGraph.GetEdge(edge.From, edge.To);
            edges.Add(originalEdge.Value);
        }

        return Path<T>.Create(edges);
    }

    private static Graph<T> ComputeAugmentedGraph<T>(Graph<T> graph, T q) where T : notnull, IEquatable<T>
    {
        var edges = graph.Vertices.Select(x => new Edge<T>(q, x, 0)).ToList();

        return new Graph<T>(graph.Vertices.Concat([q]), graph.Edges.Concat(edges));
    }

    private static Graph<T> ReweightGraph<T>(Graph<T> graph, IDictionary<T, int> potential) where T : notnull, IEquatable<T>
    {
        return new Graph<T>(
            graph.Vertices,
            graph.Edges
                .Select(x => new Edge<T>(x.From, x.To, x.Weight + potential[x.From] - potential[x.To])));
    }
}