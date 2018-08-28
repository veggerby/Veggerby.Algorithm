using System.Linq;
using Shouldly;
using Veggerby.Algorithm.Graphs;
using Xunit;

namespace Veggerby.Algorithm.Tests.Graphs
{
    public class JohnsonsAlgorithmTests
    {
        public class Item
        {
            public string Id { get; }

            public Item(string id)
            {
                Id = id;
            }

            protected bool Equals(Item other)
            {
                return string.Equals(Id, other.Id);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Item)obj);
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }
        }

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
            var algorithm = new JohnsonsAlgorithm();

            var graph = new Graph<int>(
                Enumerable.Range(0, 4),
                new [] {
                    new Edge<int>(0, 2, 3), // 0 -> 2 = 3
                    new Edge<int>(1, 0, 2), // 1 -> 0 = 2
                    new Edge<int>(2, 1, 7), // 2 -> 1 = 7
                    new Edge<int>(2, 3, 1), // 2 -> 3 = 1
                    new Edge<int>(3, 0, 6), // 3 -> 0 = 6
                });

            var expected = new [] {
                new Edge<int>(0, 1, 10),
                new Edge<int>(0, 2, 3),
                new Edge<int>(0, 3, 4),
                new Edge<int>(1, 0, 2),
                new Edge<int>(1, 2, 5),
                new Edge<int>(1, 3, 6),
                new Edge<int>(2, 0, 7),
                new Edge<int>(2, 1, 7),
                new Edge<int>(2, 3, 1),
                new Edge<int>(3, 0, 6),
                new Edge<int>(3, 1, 16),
                new Edge<int>(3, 2, 9),
            };

            // act
            var actual = algorithm.GetShortestPath(graph, int.MaxValue);

            // assert
            actual.Select(x => new Edge<int>(x.From, x.To, x.Distance)).ShouldBe(expected);
        }

        [Fact]
        public void Should_get_shortest_path_sample2()
        {
            var items = Enumerable.Range(0, 4).Select(x => new Item($"item-{x}"))
                .Concat(new [] { new Item("item-x"), new Item("item-y") })
                .ToList();

            var edges = Enumerable.Range(0, 4)
                .Select(x => new Edge<Item>(items[x], items[(x + 1) % 4], 2))
                .Concat(
                    new [] {
                        new Edge<Item>(items[0], items[2], 1),
                        new Edge<Item>(items[3], items[1], 1)
                    }
                );

            var graph = new Graph<Item>(items, edges);

            var q = new Item("q");

            var algorithm = new JohnsonsAlgorithm();

            // act
            var actual = algorithm.GetShortestPath(graph, q);

            // assert
            actual.ShouldNotBeEmpty();
            var paths = actual
                .Select(x => new Edge<Item>(x.From, x.To, x.Distance))
                .Select(x => $"{x.From.Id},{x.To.Id},{x.Weight}")
                .ToList();

            paths.Count().ShouldBe(12);
            paths.ShouldBe(new [] {
                "item-0,item-1,2", // 0-1
                "item-0,item-2,1", // 0-2
                "item-0,item-3,3", // 0-2-3
                "item-1,item-0,6", // 1-2-3-0
                "item-1,item-2,2", // 1-2
                "item-1,item-3,4", // 1-2-3
                "item-2,item-0,4", // 2-3-0
                "item-2,item-1,3", // 2-3-1
                "item-2,item-3,2", // 2-3
                "item-3,item-0,2", // 3-0
                "item-3,item-1,1", // 3-1
                "item-3,item-2,3", // 3-1-2
            });
        }
    }
}