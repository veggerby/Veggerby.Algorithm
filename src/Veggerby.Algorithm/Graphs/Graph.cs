using System.Diagnostics.CodeAnalysis;

namespace Veggerby.Algorithm.Graphs;

public class Graph<T>(IEnumerable<T> vertices, IEnumerable<Edge<T>> edges) where T : notnull, IEquatable<T>
{
    [NotNull]
    public IEnumerable<T> Vertices { get; } = vertices?.ToList() ?? [];

    [NotNull]
    public IEnumerable<Edge<T>> Edges { get; } = edges?.ToList() ?? [];

    private Graph<T> Remove(T vertex) => new Graph<T>(
        Vertices.Where(x => !x.Equals(vertex)),
        Edges.Where(x => !x.From.Equals(vertex) && !x.To.Equals(vertex))
    );

    public Edge<T>? GetEdge(T from, T to) => Edges.Cast<Edge<T>?>().SingleOrDefault(x => x?.From.Equals(from) == true && x?.To.Equals(to) == true);

    public IEnumerable<Edge<T>> GetEdgesFrom(T from) => Edges.Where(x => x.From.Equals(from)).ToList().AsEnumerable();
    public IEnumerable<Edge<T>> GetEdgesTo(T to) => Edges.Where(x => x.To.Equals(to)).ToList().AsEnumerable();

    public int GetDegree(T vertex) => GetEdgesFrom(vertex).Count();

    public Graph<T> GetKCore(int degree)
    {
        var graph = this;
        while (graph.Vertices.Any() && graph.Vertices.Any(x => graph.GetDegree(x) < degree))
        {
            var lowDegree = graph.Vertices.First(x => graph.GetDegree(x) < degree);
            graph = graph.Remove(lowDegree);
        }

        return graph.Vertices.Any() ? graph : null;
    }

    private void TraverseVertexBreadthFirst(T vertex, IDictionary<T, bool> visited, Action<T> visitor)
    {
        var queue = new Queue<T>();

        visited[vertex] = true;
        queue.Enqueue(vertex);

        while (queue.Any())
        {
            var entry = queue.Dequeue();
            visitor(entry);

            var neighbors = GetEdgesFrom(entry);
            foreach (var neighbor in neighbors)
            {
                if (!visited[neighbor.To])
                {
                    visited[neighbor.To] = true;
                    queue.Enqueue(neighbor.To);
                }
            }
        }
    }

    public void TraverseBreadthFirst(Action<T> visitor)
    {
        var visited = Vertices.ToDictionary(x => x, x => false);
        do
        {
            var vertex = visited.First(x => !x.Value);
            TraverseVertexBreadthFirst(vertex.Key, visited, visitor);
        } while (visited.Any(x => !x.Value));
    }

    private void TraverseVertexDepthFirst(T vertex, IDictionary<T, bool> visited, Action<T> visitor)
    {
        visited[vertex] = true;
        visitor(vertex);

        var neighbors = GetEdgesFrom(vertex);
        foreach (var neighbor in neighbors)
        {
            if (!visited[neighbor.To])
            {
                TraverseVertexDepthFirst(neighbor.To, visited, visitor);
            }
        }
    }

    public void TraverseDepthFirst(Action<T> visitor)
    {
        var visited = Vertices.ToDictionary(x => x, x => false);
        var first = Vertices.First();
        TraverseVertexDepthFirst(first, visited, visitor);
    }
}