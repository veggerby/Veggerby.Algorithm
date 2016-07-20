namespace Veggerby.Algorithm.Graphs
{
    public class GraphException : System.Exception
    {
        public GraphException() { }
        public GraphException(string message) : base(message) { }
        public GraphException(string message, System.Exception inner) : base(message, inner) { }
    }
}