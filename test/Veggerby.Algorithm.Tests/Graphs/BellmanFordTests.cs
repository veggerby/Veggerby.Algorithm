using System.Linq;
using Shouldly;
using Veggerby.Algorithm.Graphs;
using Xunit;

namespace Veggerby.Algorithm.Tests.Graphs
{
    public class BellmanFordTests
    {
        [Fact]
        public void Should_detect_negive_cycle()
        {
            // arrange
            var algorithm = new BellmanFord();
            var graph = new Graph<int>(
                Enumerable.Range(0, 4),
                new []
                {
                    new Edge<int>(0, 1, 1),
                    new Edge<int>(1, 2, -1),
                    new Edge<int>(2, 3, -1),
                    new Edge<int>(3, 0, -1)
                }
            );

            // act
            var actual = Should.Throw<GraphException>(() => algorithm.BellmanFordEvaluation<int>(0, graph));

            // assert
            actual.Message.ShouldBe("Graph contains negative egde cycle");
        }

        [Fact]
        public void Should_detect_negive_cycle_complex()
        {
            // example from https://www.geeksforgeeks.org/detect-negative-cycle-graph-bellman-ford/

            // arrange
            var algorithm = new BellmanFord();
            var graph = new Graph<int>(
                Enumerable.Range(1, 8),
                new []
                {
                    new Edge<int>(1, 2, 4),
                    new Edge<int>(1, 3, 4),
                    new Edge<int>(3, 6, -2),
                    new Edge<int>(3, 5, 5),
                    new Edge<int>(4, 1, 3),
                    new Edge<int>(4, 3, 2),
                    new Edge<int>(5, 4, 1),
                    new Edge<int>(5, 7, -2),
                    new Edge<int>(6, 2, 3),
                    new Edge<int>(6, 5, -3),
                    new Edge<int>(7, 6, 2),
                    new Edge<int>(7, 8, 2),
                    new Edge<int>(8, 5, -2)
                }
            );

            // act
            var actual = Should.Throw<GraphException>(() => algorithm.BellmanFordEvaluation<int>(1, graph));

            // assert
            actual.Message.ShouldBe("Graph contains negative egde cycle");
        }
    }
}