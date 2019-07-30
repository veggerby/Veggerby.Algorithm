using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class VariablesOperandVisitor : IOperandVisitor<IEnumerable<Variable>>
    {
        public IEnumerable<Variable> Visit(Function operand) => operand.Variables.Distinct();

        public IEnumerable<Variable> Visit(FunctionReference operand) => operand.Parameters.SelectMany(x => x.Accept(this)).Distinct();

        public IEnumerable<Variable> Visit(Variable operand)
        {
            yield return operand;
        }

        public IEnumerable<Variable> Visit(Subtraction operand) => operand.Left.Accept(this).Concat(operand.Right.Accept(this)).Distinct();

        public IEnumerable<Variable> Visit(Division operand) => operand.Left.Accept(this).Concat(operand.Right.Accept(this)).Distinct();

        public IEnumerable<Variable> Visit(Root operand) => operand.Inner.Accept(this);

        public IEnumerable<Variable> Visit(Sine operand) => operand.Inner.Accept(this);

        public IEnumerable<Variable> Visit(Tangent operand) => operand.Inner.Accept(this);

        public IEnumerable<Variable> Visit(Logarithm operand) => operand.Inner.Accept(this);

        public IEnumerable<Variable> Visit(Negative operand) => operand.Inner.Accept(this);

        public IEnumerable<Variable> Visit(Minimum operand) => operand.Operands.SelectMany(x => x.Accept(this)).Distinct();

        public IEnumerable<Variable> Visit(Maximum operand) => operand.Operands.SelectMany(x => x.Accept(this)).Distinct();

        public IEnumerable<Variable> Visit(Fraction operand) => Enumerable.Empty<Variable>();

        public IEnumerable<Variable> Visit(LogarithmBase operand) => operand.Inner.Accept(this);

        public IEnumerable<Variable> Visit(Exponential operand) => operand.Inner.Accept(this);

        public IEnumerable<Variable> Visit(Cosine operand) => operand.Inner.Accept(this);

        public IEnumerable<Variable> Visit(Factorial operand) => operand.Inner.Accept(this);

        public IEnumerable<Variable> Visit(Power operand) => operand.Left.Accept(this).Concat(operand.Right.Accept(this)).Distinct();

        public IEnumerable<Variable> Visit(Multiplication operand) => operand.Operands.SelectMany(x => x.Accept(this)).Distinct();

        public IEnumerable<Variable> Visit(Addition operand) => operand.Operands.SelectMany(x => x.Accept(this)).Distinct();

        public IEnumerable<Variable> Visit(NamedConstant operand) => Enumerable.Empty<Variable>();

        public IEnumerable<Variable> Visit(ValueConstant operand) => Enumerable.Empty<Variable>();

        public IEnumerable<Variable> Visit(UnspecifiedConstant operand) => Enumerable.Empty<Variable>();
    }
}