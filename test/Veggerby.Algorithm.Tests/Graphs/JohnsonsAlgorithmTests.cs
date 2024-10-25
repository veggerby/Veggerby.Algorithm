using System.Linq;

using FluentAssertions;
using FluentAssertions.Execution;

using Veggerby.Algorithm.Graphs;

using Xunit;

namespace Veggerby.Algorithm.Tests.Graphs;

public class JohnsonsAlgorithmTests
{
    public readonly record struct Item(string Id);

    [Fact]
    public void Should_get_shortest_path_sample1()
    {
        /*
            based on example from: http://www.sanfoundry.com/java-program-to-implement-johnsons-algorithm/

            Weighted Matrix for the graph
            0 0 3 0
            2 0 0 0
            0 7 0 1
            6 0 0 0

            All pair shortest path is
                1	2	3	4
            1	0	10	3	4
            2	2	0	5	6
            3	7	7	0	1
            4	6	16	9	0
        */

        // arrange
        var graph = new Graph<int>(
            Enumerable.Range(0, 4),
            [
                new Edge<int>(0, 2, 3), // 0 -> 2 = 3
                new Edge<int>(1, 0, 2), // 1 -> 0 = 2
                new Edge<int>(2, 1, 7), // 2 -> 1 = 7
                new Edge<int>(2, 3, 1), // 2 -> 3 = 1
                new Edge<int>(3, 0, 6), // 3 -> 0 = 6
            ]);

        var expected = new[] {
            new Edge<int>(0, 1, 10),    // 0 -> 2 -> 1 = 10
            new Edge<int>(0, 2, 3),     // 0 -> 2 = 3
            new Edge<int>(0, 3, 4),     // 0 -> 2 -> 3 = 4
            new Edge<int>(1, 0, 2),     // 1 -> 0 = 2
            new Edge<int>(1, 2, 5),     // 1 -> 0 -> 2 = 5
            new Edge<int>(1, 3, 6),     // 1 -> 0 -> 2 -> 3 = 6
            new Edge<int>(2, 0, 7),     // 2 -> 3 -> 0 = 1 + 6 = 7 - not 2 -> 1 -> 0 = 7 + 2 = 9
            new Edge<int>(2, 1, 7),     // 2 -> 1 = 7
            new Edge<int>(2, 3, 1),     // 2 -> 3 = 1
            new Edge<int>(3, 0, 6),     // 3 -> 0 = 6
            new Edge<int>(3, 1, 16),    // 3 -> 0 -> 2 -> 1 = 3 + 6 + 7 = 16
            new Edge<int>(3, 2, 9),     // 3 -> 0 -> 2 = 3 + 6 = 9
        };

        // act
        var actual = JohnsonsAlgorithm.GetShortestPath(graph, int.MaxValue);

        // assert
        var actualEdges = actual.Select(x => new Edge<int>(x.From, x.To, x.Distance)).ToList();
        actualEdges.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Should_get_shortest_path_sample2()
    {
        var items = Enumerable.Range(0, 4).Select(x => new Item($"item-{x}"))
            .Concat([new Item("item-x"), new Item("item-y")])
            .ToList();

        var edges = Enumerable.Range(0, 4)
            .Select(x => new Edge<Item>(items[x], items[(x + 1) % 4], 2))
            .Concat(
                [
                    new Edge<Item>(items[0], items[2], 1),
                    new Edge<Item>(items[3], items[1], 1),
                    new Edge<Item>(items[4], items[5], 9)
                ]
            )
            .ToList();

        var graph = new Graph<Item>(items, edges);

        var q = new Item("q");

        // act
        var actual = JohnsonsAlgorithm.GetShortestPath(graph, q);

        // assert
        using var _ = new AssertionScope();

        actual.Should().NotBeEmpty();
        actual.Count().Should().Be(13);

        var paths = actual
            .Select(x => new Edge<Item>(x.From, x.To, x.Distance))
            .ToList();

        paths.Should().BeEquivalentTo([
            new Edge<Item>(new Item("item-0"), new Item("item-1"), 2), // 0-1
            new Edge<Item>(new Item("item-0"), new Item("item-2"), 1), // 0-2
            new Edge<Item>(new Item("item-0"), new Item("item-3"), 3), // 0-2-3
            new Edge<Item>(new Item("item-1"), new Item("item-0"), 6), // 1-2-3-0
            new Edge<Item>(new Item("item-1"), new Item("item-2"), 2), // 1-2
            new Edge<Item>(new Item("item-1"), new Item("item-3"), 4), // 1-2-3
            new Edge<Item>(new Item("item-2"), new Item("item-0"), 4), // 2-3-0
            new Edge<Item>(new Item("item-2"), new Item("item-1"), 3), // 2-3-1
            new Edge<Item>(new Item("item-2"), new Item("item-3"), 2), // 2-3
            new Edge<Item>(new Item("item-3"), new Item("item-0"), 2), // 3-0
            new Edge<Item>(new Item("item-3"), new Item("item-1"), 1), // 3-1
            new Edge<Item>(new Item("item-3"), new Item("item-2"), 3), // 3-1-2
            new Edge<Item>(new Item("item-x"), new Item("item-y"), 9), // 3-1-2
        ]);
    }
}