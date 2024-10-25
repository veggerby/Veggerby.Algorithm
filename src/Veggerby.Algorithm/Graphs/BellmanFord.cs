namespace Veggerby.Algorithm.Graphs;

public static class BellmanFord
{
    public static IDictionary<T, int> BellmanFordEvaluation<T>(T source, Graph<T> graph) where T : notnull, IEquatable<T>
    {
        var numberOfVertices = graph.Vertices.Count();

        if (!graph.Vertices.Contains(source))
        {
            throw new ArgumentOutOfRangeException(nameof(source));
        }

        // step 1: initialize distance to all vertices as infinite, except source is 0
        var distances = graph
            .Vertices
            .ToDictionary(x => x, x => source.Equals(x) ? 0 : int.MaxValue);

        /* step 2: This step calculates shortest distances. Do following |V|-1 times where |V| is the number of vertices in given graph.
	         * Do following for each edge u-v
         * - If dist[v] > dist[u] + weight of edge uv, then update dist[v] to dist[v] = dist[u] + weight of edge uv
         */

        var count = numberOfVertices - 1;
        while (count-- > 0)
        {
            foreach (var edge in graph.Edges)
            {
                if (distances[edge.To] > distances[edge.From].InfinityAdd(edge.Weight))
                {
                    distances[edge.To] = distances[edge.From].InfinityAdd(edge.Weight);
                }
            }
        }

        /* step 3: This step reports if there is a negative weight cycle in graph. Do following for each edge u-v
         * If dist[v] > dist[u] + weight of edge uv, then “Graph contains negative weight cycle”
         */

        foreach (var edge in graph.Edges)
        {
            if (distances[edge.To] > distances[edge.From].InfinityAdd(edge.Weight))
            {
                throw new GraphException("Graph contains negative egde cycle");
            }
        }

        return distances;
    }
}