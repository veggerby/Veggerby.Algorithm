using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class CanIntegrateOperandVisitor : IOperandVisitor<bool>
    {
        private readonly Variable _variable;

        private readonly IEnumerable<Operand> _stack;

        private static readonly Variable _constant = Variable.Create("c");

        public CanIntegrateOperandVisitor(Variable variable, IEnumerable<Operand> stack = null)
        {
            _variable = variable;
            _stack = stack?.ToList() ?? new List<Operand>();
        }

        private bool CanIntegrate(Operand operand)
        {
            if (_stack.Contains(operand) && _stack.Count() < 25) // abort when too deep
            {
                throw new ArgumentException("Circular call", nameof(operand));
            }

            var visitor = new CanIntegrateOperandVisitor(_variable, _stack.Concat(new [] { operand }));

            return operand
                .Reduce()
                .Accept(visitor);
        }

        private bool CanIntegrationByParts(Operand left, Operand right)
        {
            if (!CanIntegrate(right))
            {
                return false;
            }

            var leftDerivative = left.GetDerivative(_variable);
            var rightIntegral = right.GetIntegral(_variable);

            var operand = Multiplication.Create(leftDerivative, rightIntegral);

            return CanIntegrate(operand);
        }

        public bool Visit(Function operand)
        {
            return CanIntegrate(operand.Operand);
        }

        public bool Visit(FunctionReference operand)
        {
            return false;
        }

        public bool Visit(Variable operand)
        {
            return true;
        }

        public bool Visit(Subtraction operand)
        {
            return CanIntegrate(operand.Left) && CanIntegrate(operand.Right);
        }

        public bool Visit(Division operand)
        {
            return (operand.Left.IsConstant() && operand.Right.Equals(_variable));
        }

        public bool Visit(Factorial operand)
        {
            return false;
        }

        public bool Visit(Cosine operand)
        {
            return operand.Inner.Equals(_variable);
        }

        public bool Visit(Exponential operand)
        {
            return operand.Inner.Equals(_variable);
        }

        public bool Visit(LogarithmBase operand)
        {
            return false;
        }

        public bool Visit(Negative operand)
        {
            return CanIntegrate(operand.Inner);
        }

        public bool Visit(Logarithm operand)
        {
            return operand.Inner.Equals(_variable);
        }

        public bool Visit(Tangent operand)
        {
            return false;
        }

        public bool Visit(Sine operand)
        {
            return operand.Inner.Equals(_variable);
        }

        public bool Visit(Power operand)
        {
            return (operand.Left.Equals(_variable) && operand.Right.IsConstant() && operand.Right != Constant.MinusOne);
        }

        public bool Visit(Root operand)
        {
            return operand.Inner.Equals(_variable);
        }

        public bool Visit(Multiplication operand)
        {
            return operand
                .Operands
                .Any(x => CanIntegrationByParts(Multiplication.Create(operand.Operands.Where(y => !object.ReferenceEquals(x, y))), x));
        }

        public bool Visit(Addition operand)
        {
            return operand
                .Operands
                .All(x => CanIntegrate(x));
        }

        public bool Visit(NamedConstant operand)
        {
            return true;
        }

        public bool Visit(Constant operand)
        {
            return true;
        }

        public bool Visit(Fraction operand)
        {
            return true;
        }

        public bool Visit(Minimum operand)
        {
            return false;
        }

        public bool Visit(Maximum operand)
        {
            return false;
        }
    }
}