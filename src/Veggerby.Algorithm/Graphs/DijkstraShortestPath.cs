namespace Veggerby.Algorithm.Graphs;

/// From http://www.sanfoundry.com/java-program-to-implement-johnsons-algorithm/
/// http://stackoverflow.com/questions/10674468/finding-the-shortest-route-using-dijkstra-algorithm
public static class DijkstraShortestPath
{
    public static IEnumerable<Path<T>> CalculateShortestPaths<T>(T from, Graph<T> graph) where T : notnull, IEquatable<T>
    {
        var settled = new List<T>();
        var unsettled = new List<T>(graph.Vertices);
        var distances = graph.Vertices.ToDictionary(x => x, x => from.Equals(x) ? 0 : int.MaxValue);
        var parent = new Dictionary<T, T>();

        while (unsettled.Any())
        {
            if (TryGetNodeWithMinimumDistanceFromUnsettled(unsettled, distances, out var evaluationNode))
            {
                unsettled.Remove(evaluationNode);
                settled.Add(evaluationNode);
                EvaluateNeighbours(evaluationNode, settled, graph, unsettled, distances, parent);
            }
        }

        var result = new List<Path<T>>();
        foreach (var to in graph.Vertices)
        {
            if (!from.Equals(to))
            {
                if (TryGetShortestPath(from, to, parent, graph, out var path))
                {
                    result.Add(path);
                }
            }
        }

        return result;
    }

    private static bool TryGetShortestPath<T>(T from, T to, IDictionary<T, T> parent, Graph<T> graph, out Path<T> path) where T : notnull, IEquatable<T>
    {
        path = default;

        var shortestPath = new List<Edge<T>>();
        var current = to;

        while (!from.Equals(current))
        {
            // if there are no incoming edges to current node
            if (!parent.ContainsKey(current))
            {
                return false;
            }

            shortestPath.Add(graph.GetEdge(parent[current], current).Value);
            current = parent[current];
        }

        if (!shortestPath.Any())
        {
            return false;
        }

        shortestPath.Reverse();
        path = Path<T>.Create(shortestPath);
        return true;
    }

    private static bool TryGetNodeWithMinimumDistanceFromUnsettled<T>(IEnumerable<T> unsettled, IDictionary<T, int> distances, out T node)
    {
        var minDistance = unsettled
            .OrderBy(x => distances.ContainsKey(x) ? distances[x] : int.MaxValue)
            .ToList();

        if (!minDistance.Any())
        {
            node = default;
            return false;
        }

        node = minDistance.First();
        return true;
    }

    private static void EvaluateNeighbours<T>(T evaluationNode, IEnumerable<T> settled, Graph<T> graph, IList<T> unsettled, IDictionary<T, int> distances, IDictionary<T, T> parent) where T : notnull, IEquatable<T>
    {
        foreach (var destinationNode in graph.Vertices)
        {
            if (!settled.Contains(destinationNode))
            {
                var edge = graph.GetEdge(evaluationNode, destinationNode);
                if (edge is not null)
                {
                    var newDistance = distances[evaluationNode].InfinityAdd(edge.Value.Weight);
                    if (newDistance < distances[destinationNode])
                    {
                        distances[destinationNode] = newDistance;
                        parent[destinationNode] = evaluationNode;
                    }

                    unsettled.Add(destinationNode);
                }
            }
        }
    }
}