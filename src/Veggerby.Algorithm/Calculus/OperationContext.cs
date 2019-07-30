using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Calculus
{
    public class OperationContext
    {
        private readonly IDictionary<string, double> _variables;
        private readonly IDictionary<string, Function> _functions;

        private readonly UnspecifiedConstantSymbolFactory _unspecifiedConstantSymbolFactory = new UnspecifiedConstantSymbolFactory();

        public IEnumerable<Function> Functions => _functions.Values;

        public OperationContext(IEnumerable<KeyValuePair<string, double>> variables = null, IEnumerable<Function> functions = null)
        {
            _variables = (variables ?? Enumerable.Empty<KeyValuePair<string, double>>()).ToDictionary(x => x.Key, x => x.Value);
            _functions = (functions ?? Enumerable.Empty<Function>()).ToDictionary(x => x.Identifier, x => x);
        }

        public double GetVariable(string identifier) => _variables[identifier];

        public void Add(string identifier, double value) => _variables.Add(identifier, value);

        public Function GetFunction(string identifier) => _functions[identifier];

        public void Add(Function function) => _functions.Add(function.Identifier, function);

        public string GetName(UnspecifiedConstant constant) => _unspecifiedConstantSymbolFactory.Get(constant.InstanceId);
    }
}