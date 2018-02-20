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

        public Operand Visit(FunctionReference operand)
        {
            return operand;
        }

        public Operand Visit(Variable operand)
        {
            return operand;
        }

        public Operand Visit(Subtraction operand)
        {
            return Subtraction.Create(operand.Left.Accept(this), operand.Right.Accept(this));
        }

        public Operand Visit(Division operand)
        {
            return Division.Create(operand.Left.Accept(this), operand.Right.Accept(this));
        }

        public Operand Visit(Factorial operand)
        {
            return Factorial.Create(operand.Inner.Accept(this));
        }

        public Operand Visit(Cosine operand)
        {
            return Sine.Create(operand.Inner.Accept(this));
        }

        public Operand Visit(Exponential operand)
        {
            return Exponential.Create(operand.Inner.Accept(this));
        }

        public Operand Visit(LogarithmBase operand)
        {
            return LogarithmBase.Create(operand.Base, operand.Inner.Accept(this));
        }

        public Operand Visit(Negative operand)
        {
            return Negative.Create(operand.Inner.Accept(this));
        }

        public Operand Visit(Logarithm operand)
        {
            return Logarithm.Create(operand.Inner.Accept(this));
        }

        public Operand Visit(Tangent operand)
        {
            return Tangent.Create(operand.Inner.Accept(this));
        }

        public Operand Visit(Sine operand)
        {
            return Sine.Create(operand.Inner.Accept(this));
        }

        public Operand Visit(Power operand)
        {
            return Power.Create(operand.Left.Accept(this), operand.Right.Accept(this));
        }

        public Operand Visit(Root operand)
        {
            return Root.Create(operand.Exponent, operand.Inner.Accept(this));
        }

        public Operand Visit(Multiplication operand)
        {
            return ReorderCommutativeBinaryOperation(operand, Multiplication.Create);
        }

        public Operand Visit(Addition operand)
        {
            return ReorderCommutativeBinaryOperation(operand, Addition.Create);
        }

        public Operand Visit(NamedConstant operand)
        {
            return operand;
        }

        public Operand Visit(Constant operand)
        {
            return operand;
        }

        public Operand Visit(Fraction operand)
        {
            return operand;
        }

        public Operand Visit(Minimum operand)
        {
            return ReorderCommutativeBinaryOperation(operand, Minimum.Create);
        }

        public Operand Visit(Maximum operand)
        {
            return ReorderCommutativeBinaryOperation(operand, Maximum.Create);
        }
    }
}