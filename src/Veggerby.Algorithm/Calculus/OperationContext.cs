using System.Collections.Generic;

namespace Veggerby.Algorithm.Calculus
{
    public class OperationContext
    {
        public IDictionary<string, double> _variables = new Dictionary<string, double>();
        public IDictionary<string, Function> _functions = new Dictionary<string, Function>();

        public double GetVariable(string identifier)
        {
            return _variables[identifier];
        }

        public void Add(string identifier, double value)
        {
            _variables.Add(identifier, value);
        }

        public Function GetFunction(string identifier)
        {
            return _functions[identifier];
        }

        public void Add(Function function)
        {
            _functions.Add(function.Identifier, function);
        }
    }
}