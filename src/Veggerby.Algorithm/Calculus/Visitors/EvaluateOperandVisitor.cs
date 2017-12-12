using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class EvaluateOperandVisitor : IOperandVisitor<double>
    {
        private readonly OperationContext _context;

        public EvaluateOperandVisitor(OperationContext context)
        {
            _context = context;
        }

        private double Evaluate(Operand operand, OperationContext context = null)
        {
            if (context != null)
            {
                var visitor = new EvaluateOperandVisitor(context);
                return operand.Accept(visitor);
            }

            return operand.Accept(this);
        }

        public double Visit(Function operand)
        {
            return Evaluate(operand.Operand);
        }

        public double Visit(FunctionReference operand)
        {
            var f = _context.GetFunction(operand.Identifier);
            if (f == null)
            {
                throw new Exception("Unknown function");
            }

            if (f.Variables.Count() != operand.Parameters.Count())
            {
                throw new Exception("Invalid parameter count");
            }

            var parameters = operand.Parameters.Select(x => x.Evaluate(_context));

            var variables = f
                .Variables
                .Zip(operand.Parameters, (variable, parameter) => new KeyValuePair<string, double>(variable.Identifier, Evaluate(parameter)))
                .ToList();

            var context = new OperationContext(variables, _context.Functions);

            return Evaluate(f, context);
        }

        public double Visit(Variable operand)
        {
            return _context.GetVariable(operand.Identifier);
        }

        public double Visit(Subtraction operand)
        {
            return Evaluate(operand.Left) - Evaluate(operand.Right);
        }

        public double Visit(Division operand)
        {
            return Evaluate(operand.Left) / Evaluate(operand.Right);
        }

        public double Visit(Factorial operand)
        {
            var inner = Evaluate(operand.Inner);
            if (inner % 1 != 0)
            {
                throw new Exception("Non integer value");
            }

            var result = (int)inner;

            for (int i = 1; i < inner; i++)
            {
                result = result * i;
            }

            return result;
        }

        public double Visit(Cosine operand)
        {
            var inner = Evaluate(operand.Inner);
            return Math.Cos(inner);
        }

        public double Visit(Exponential operand)
        {
            var inner = Evaluate(operand.Inner);
            return Math.Exp(inner);
        }

        public double Visit(LogarithmBase operand)
        {
            var inner = Evaluate(operand.Inner);
            return Math.Log(inner) / Math.Log(operand.Base);
        }

        public double Visit(Negative operand)
        {
            var inner = Evaluate(operand.Inner);
            return -inner;
        }

        public double Visit(Logarithm operand)
        {
            var inner = Evaluate(operand.Inner);
            return Math.Log(inner);
        }

        public double Visit(Tangent operand)
        {
            var inner = Evaluate(operand.Inner);
            return Math.Tan(inner);
        }

        public double Visit(Sine operand)
        {
            var inner = Evaluate(operand.Inner);
            return Math.Sin(inner);
        }

        public double Visit(Power operand)
        {
            return Math.Pow(Evaluate(operand.Left), Evaluate(operand.Right));
        }

        public double Visit(Root operand)
        {
            return Math.Pow(Evaluate(operand.Inner), 1d / operand.Exponent);
        }

        public double Visit(Multiplication operand)
        {
            return Evaluate(operand.Left) * Evaluate(operand.Right);
        }

        public double Visit(Addition operand)
        {
            return Evaluate(operand.Left) + Evaluate(operand.Right);
        }

        public double Visit(NamedConstant operand)
        {
            return operand.Value;
        }

        public double Visit(Constant operand)
        {
            return operand.Value;
        }

        public double Visit(Fraction operand)
        {
            return 1d * operand.Numerator / operand.Denominator;
        }

        public double Visit(Minimum operand)
        {
            return Math.Min(Evaluate(operand.Left), Evaluate(operand.Right));
        }

        public double Visit(Maximum operand)
        {
            return Math.Max(Evaluate(operand.Left), Evaluate(operand.Right));
        }
    }
}