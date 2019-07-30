using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Graphs
{
    public class Graph<T>
    {
        public IEnumerable<T> Vertices { get; }
        public IEnumerable<Edge<T>> Edges { get; }

        public Graph(IEnumerable<T> vertices, IEnumerable<Edge<T>> edges)
        {
            Vertices = (vertices ?? Enumerable.Empty<T>()).ToList();
            Edges = (edges ?? Enumerable.Empty<Edge<T>>()).ToList();
        }

        private Graph<T> Remove(T vertice) => new Graph<T>(
                Vertices.Where(x => !x.Equals(vertice)),
                Edges.Where(x => !x.From.Equals(vertice) && !x.To.Equals(vertice))
            );

        public Edge<T> GetEdge(T from, T to) => Edges.SingleOrDefault(x => x.From.Equals(from) && x.To.Equals(to));

        public IEnumerable<Edge<T>> GetEdgesFrom(T from) => Edges.Where(x => x.From.Equals(from)).ToList().AsEnumerable();

        public int GetDegree(T vertice) => GetEdgesFrom(vertice).Count();

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

        private void TraverseVerticeBreadthFirst(T vertice, IDictionary<T, bool> visited, Action<T> visitor)
        {
            var queue = new Queue<T>();

            visited[vertice] = true;
            queue.Enqueue(vertice);

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
                var vertice = visited.First(x => !x.Value);
                TraverseVerticeBreadthFirst(vertice.Key, visited, visitor);
            } while (visited.Any(x => !x.Value));
        }

        private void TraverseVerticeDepthFirst(T vertice, IDictionary<T, bool> visited, Action<T> visitor)
        {
            visited[vertice] = true;
            visitor(vertice);

            var neighbors = GetEdgesFrom(vertice);
            foreach (var neighbor in neighbors)
            {
                if (!visited[neighbor.To])
                {
                    TraverseVerticeDepthFirst(neighbor.To, visited, visitor);
                }
            }
        }

        public void TraverseDepthFirst(Action<T> visitor)
        {
            var visited = Vertices.ToDictionary(x => x, x => false);
            var first = Vertices.First();
            TraverseVerticeDepthFirst(first, visited, visitor);
        }
    }
}