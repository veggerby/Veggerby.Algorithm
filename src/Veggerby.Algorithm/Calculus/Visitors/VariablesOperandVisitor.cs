using System.Collections.Generic;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class VariablesOperandVisitor : IOperandVisitor
    {
        private readonly IList<Variable> _result = new List<Variable>();

        public IEnumerable<Variable> Result => _result;

        public void Visit(Function operand)
        {
            foreach (var variable in operand.Variables)
            {
                if (!_result.Contains(variable))
                {
                    _result.Add(variable);
                }
            }
        }

        public void Visit(Variable operand)
        {
            if (!_result.Contains(operand))
            {
                _result.Add(operand);
            }
        }

        public void Visit(Subtraction operand)
        {
            operand.Left.Accept(this);
            operand.Right.Accept(this);
        }

        public void Visit(Division operand)
        {
            operand.Left.Accept(this);
            operand.Right.Accept(this);
        }

        public void Visit(Root operand)
        {
            operand.Inner.Accept(this);
        }

        public void Visit(Sine operand)
        {
            operand.Inner.Accept(this);
        }

        public void Visit(Tangent operand)
        {
            operand.Inner.Accept(this);
        }

        public void Visit(Logarithm operand)
        {
            operand.Inner.Accept(this);
        }

        public void Visit(Negative operand)
        {
            operand.Inner.Accept(this);
        }

        public void Visit(Minimum operand)
        {
            operand.Left.Accept(this);
            operand.Right.Accept(this);
        }

        public void Visit(Maximum operand)
        {
            operand.Left.Accept(this);
            operand.Right.Accept(this);
        }

        public void Visit(Fraction operand)
        {
        }

        public void Visit(LogarithmBase operand)
        {
            operand.Inner.Accept(this);
        }

        public void Visit(Exponential operand)
        {
            operand.Inner.Accept(this);
        }

        public void Visit(Cosine operand)
        {
            operand.Inner.Accept(this);
        }

        public void Visit(Factorial operand)
        {
            operand.Inner.Accept(this);
        }

        public void Visit(Power operand)
        {
            operand.Left.Accept(this);
            operand.Right.Accept(this);
        }

        public void Visit(Multiplication operand)
        {
            operand.Left.Accept(this);
            operand.Right.Accept(this);
        }

        public void Visit(Addition operand)
        {
            operand.Left.Accept(this);
            operand.Right.Accept(this);
        }

        public void Visit(NamedConstant operand)
        {
        }

        public void Visit(Constant operand)
        {
        }
    }
}