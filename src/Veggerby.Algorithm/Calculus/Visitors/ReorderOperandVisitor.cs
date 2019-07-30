using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class ReorderOperandVisitor : IOperandVisitor<Operand>
    {
        private Operand ReorderCommutativeBinaryOperation<TOperand>(TOperand operand, Func<IEnumerable<Operand>, Operand> factory) where TOperand : Operand, ICommutativeOperation
        {
            var ordered = operand.Operands.OrderBy(x => x, new CommutativeOperationComparer());
            return factory(ordered);
        }

        public Operand Visit(Function operand)
        {
            var innerOperand = operand.Operand.Accept(this);
            return Function.Create(operand.Identifier, innerOperand);
        }

        public Operand Visit(FunctionReference operand) => operand;

        public Operand Visit(Variable operand) => operand;

        public Operand Visit(Subtraction operand) => Subtraction.Create(operand.Left.Accept(this), operand.Right.Accept(this));

        public Operand Visit(Division operand) => Division.Create(operand.Left.Accept(this), operand.Right.Accept(this));

        public Operand Visit(Factorial operand) => Factorial.Create(operand.Inner.Accept(this));

        public Operand Visit(Cosine operand) => Sine.Create(operand.Inner.Accept(this));

        public Operand Visit(Exponential operand) => Exponential.Create(operand.Inner.Accept(this));

        public Operand Visit(LogarithmBase operand) => LogarithmBase.Create(operand.Base, operand.Inner.Accept(this));

        public Operand Visit(Negative operand) => Negative.Create(operand.Inner.Accept(this));

        public Operand Visit(Logarithm operand) => Logarithm.Create(operand.Inner.Accept(this));

        public Operand Visit(Tangent operand) => Tangent.Create(operand.Inner.Accept(this));

        public Operand Visit(Sine operand) => Sine.Create(operand.Inner.Accept(this));

        public Operand Visit(Power operand) => Power.Create(operand.Left.Accept(this), operand.Right.Accept(this));

        public Operand Visit(Root operand) => Root.Create(operand.Exponent, operand.Inner.Accept(this));

        public Operand Visit(Multiplication operand) => ReorderCommutativeBinaryOperation(operand, Multiplication.Create);

        public Operand Visit(Addition operand) => ReorderCommutativeBinaryOperation(operand, Addition.Create);

        public Operand Visit(NamedConstant operand) => operand;

        public Operand Visit(ValueConstant operand) => operand;

        public Operand Visit(UnspecifiedConstant operand) => operand;

        public Operand Visit(Fraction operand) => operand;

        public Operand Visit(Minimum operand) => ReorderCommutativeBinaryOperation(operand, Minimum.Create);

        public Operand Visit(Maximum operand) => ReorderCommutativeBinaryOperation(operand, Maximum.Create);
    }
}