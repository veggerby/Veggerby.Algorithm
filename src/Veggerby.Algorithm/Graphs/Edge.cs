using System.Diagnostics.CodeAnalysis;

namespace Veggerby.Algorithm.Graphs;

public readonly record struct Edge<T>([property: NotNull] T From, [property: NotNull] T To, int Weight = 1) where T : notnull, IEquatable<T>
{
    public override string ToString() => $"Edge: {From} -> {To} [{Weight}]";
};