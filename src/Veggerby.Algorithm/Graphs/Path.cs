namespace Veggerby.Algorithm.Graphs;

public readonly record struct Path<T>(Edge<T>[] Edges) where T : notnull, IEquatable<T>
{
    public static Path<T> Create(IEnumerable<Edge<T>> edges)
    {
        AssertValidEdgeOrder(edges);
        return new Path<T>(edges.ToArray());
    }

    public static Path<T> Create(params Edge<T>[] edges) => Create(edges.AsEnumerable());

    public static Path<T> Create(params IEnumerable<Edge<T>>[] edges) => Create(edges.SelectMany(x => x));

    private static void AssertValidEdgeOrder(IEnumerable<Edge<T>> edges)
    {
        if (edges?.Any() != true)
        {
            throw new GraphException("No edges.");
        }

        if (edges.Count() == 1)
        {
            return;
        }

        Edge<T>? previous = null;
        var remainingEdges = edges.ToList();

        while (remainingEdges.Any())
        {
            var edge = remainingEdges.First();

            if (previous?.To.Equals(edge.From) == false)
            {
                throw new GraphException("Invalid edge order.");
            }

            remainingEdges.Remove(edge);
        }
    }

    public int Distance => Edges.Sum(x => x.Weight);
    public T From => Edges.First().From;
    public T To => Edges.Last().To;

    public override string ToString()
    {
        var path = string.Join(" ", Edges.Select(x => $"[{x.Weight}] {x.To}"));
        return $"Path: {From} {path}";
    }
}