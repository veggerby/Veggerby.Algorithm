using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class EvaluateOperandVisitor : IOperandVisitor
    {
        private readonly OperationContext _context;

        public double Result { get; private set; }

        public EvaluateOperandVisitor(OperationContext context)
        {
            _context = context;
        }

        private double Evaluate(Operand operand, OperationContext context = null)
        {
            var visitor = new EvaluateOperandVisitor(context ?? _context);
            operand.Accept(visitor);
            return visitor.Result;
        }

        public void Visit(Function operand)
        {
            Result = Evaluate(operand.Operand);
        }

        public void Visit(FunctionReference operand)
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

            Result = Evaluate(f, context);
        }

        public void Visit(Variable operand)
        {
            Result = _context.GetVariable(operand.Identifier);
        }

        public void Visit(Subtraction operand)
        {
            Result = Evaluate(operand.Left) - Evaluate(operand.Right);
        }

        public void Visit(Division operand)
        {
            Result = Evaluate(operand.Left) / Evaluate(operand.Right);
        }

        public void Visit(Factorial operand)
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

            Result = result;
        }

        public void Visit(Cosine operand)
        {
            var inner = Evaluate(operand.Inner);
            Result = Math.Cos(inner);
        }

        public void Visit(Exponential operand)
        {
            var inner = Evaluate(operand.Inner);
            Result = Math.Exp(inner);
        }

        public void Visit(LogarithmBase operand)
        {
            var inner = Evaluate(operand.Inner);
            Result = Math.Log(inner) / Math.Log(operand.Base);
        }

        public void Visit(Negative operand)
        {
            var inner = Evaluate(operand.Inner);
            Result = -inner;
        }

        public void Visit(Logarithm operand)
        {
            var inner = Evaluate(operand.Inner);
            Result = Math.Log(inner);
        }

        public void Visit(Tangent operand)
        {
            var inner = Evaluate(operand.Inner);
            Result = Math.Tan(inner);
        }

        public void Visit(Sine operand)
        {
            var inner = Evaluate(operand.Inner);
            Result = Math.Sin(inner);
        }

        public void Visit(Power operand)
        {
            Result = Math.Pow(Evaluate(operand.Left), Evaluate(operand.Right));
        }

        public void Visit(Root operand)
        {
            Result = Math.Pow(Evaluate(operand.Inner), 1d / operand.Exponent);
        }

        public void Visit(Multiplication operand)
        {
            Result = Evaluate(operand.Left) * Evaluate(operand.Right);
        }

        public void Visit(Addition operand)
        {
            Result = Evaluate(operand.Left) + Evaluate(operand.Right);
        }

        public void Visit(NamedConstant operand)
        {
            Result = operand.Value;
        }

        public void Visit(Constant operand)
        {
            Result = operand.Value;
        }

        public void Visit(Fraction operand)
        {
            Result = 1d * operand.Numerator / operand.Denominator;
        }

        public void Visit(Minimum operand)
        {
            Result = Math.Min(Evaluate(operand.Left), Evaluate(operand.Right));
        }

        public void Visit(Maximum operand)
        {
            Result = Math.Max(Evaluate(operand.Left), Evaluate(operand.Right));
        }
    }
}