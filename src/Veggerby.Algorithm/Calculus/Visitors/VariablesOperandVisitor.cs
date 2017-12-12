using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class VariablesOperandVisitor : IOperandVisitor<IEnumerable<Variable>>
    {
        public IEnumerable<Variable> Visit(Function operand)
        {
            return operand.Variables.Distinct();
        }

        public IEnumerable<Variable> Visit(FunctionReference operand)
        {
            return operand.Parameters.SelectMany(x => x.Accept(this)).Distinct();
        }

        public IEnumerable<Variable> Visit(Variable operand)
        {
            yield return operand;
        }

        public IEnumerable<Variable> Visit(Subtraction operand)
        {
            return operand.Left.Accept(this).Concat(operand.Right.Accept(this)).Distinct();
        }

        public IEnumerable<Variable> Visit(Division operand)
        {
            return operand.Left.Accept(this).Concat(operand.Right.Accept(this)).Distinct();
        }

        public IEnumerable<Variable> Visit(Root operand)
        {
            return operand.Inner.Accept(this);
        }

        public IEnumerable<Variable> Visit(Sine operand)
        {
            return operand.Inner.Accept(this);
        }

        public IEnumerable<Variable> Visit(Tangent operand)
        {
            return operand.Inner.Accept(this);
        }

        public IEnumerable<Variable> Visit(Logarithm operand)
        {
            return operand.Inner.Accept(this);
        }

        public IEnumerable<Variable> Visit(Negative operand)
        {
            return operand.Inner.Accept(this);
        }

        public IEnumerable<Variable> Visit(Minimum operand)
        {
            return operand.Left.Accept(this).Concat(operand.Right.Accept(this)).Distinct();
        }

        public IEnumerable<Variable> Visit(Maximum operand)
        {
            return operand.Left.Accept(this).Concat(operand.Right.Accept(this)).Distinct();
        }

        public IEnumerable<Variable> Visit(Fraction operand)
        {
            return Enumerable.Empty<Variable>();
        }

        public IEnumerable<Variable> Visit(LogarithmBase operand)
        {
            return operand.Inner.Accept(this);
        }

        public IEnumerable<Variable> Visit(Exponential operand)
        {
            return operand.Inner.Accept(this);
        }

        public IEnumerable<Variable> Visit(Cosine operand)
        {
            return operand.Inner.Accept(this);
        }

        public IEnumerable<Variable> Visit(Factorial operand)
        {
            return operand.Inner.Accept(this);
        }

        public IEnumerable<Variable> Visit(Power operand)
        {
            return operand.Left.Accept(this).Concat(operand.Right.Accept(this)).Distinct();
        }

        public IEnumerable<Variable> Visit(Multiplication operand)
        {
            return operand.Left.Accept(this).Concat(operand.Right.Accept(this)).Distinct();
        }

        public IEnumerable<Variable> Visit(Addition operand)
        {
            return operand.Left.Accept(this).Concat(operand.Right.Accept(this)).Distinct();
        }

        public IEnumerable<Variable> Visit(NamedConstant operand)
        {
            return Enumerable.Empty<Variable>();
        }

        public IEnumerable<Variable> Visit(Constant operand)
        {
            return Enumerable.Empty<Variable>();
        }
    }
}