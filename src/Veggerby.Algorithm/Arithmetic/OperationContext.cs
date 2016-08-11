using System.Collections.Generic;

namespace Veggerby.Algorithm.Arithmetic
{
    public class OperationContext
    {
        public IDictionary<string, double> _variables = new Dictionary<string, double>();

        public double Get(string identifier)
        {
            return _variables[identifier];
        }

        public void Add(string identifier, double value)
        {
            _variables.Add(identifier, value);
        }
    }
}