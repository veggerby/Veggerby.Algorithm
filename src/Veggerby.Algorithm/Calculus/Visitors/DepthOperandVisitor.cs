using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class DepthOperandVisitor : IOperandVisitor<int>
    {
        public int Visit(Function operand)
        {
            return operand.Operand.Accept(this) + 1;
        }

        public int Visit(FunctionReference operand)
        {
            return 1;
        }

        public int Visit(ValueConstant operand)
        {
            return 1;
        }

        public int Visit(UnspecifiedConstant operand)
        {
            return 1;
        }        

        public int Visit(NamedConstant operand)
        {
            return 1;
        }

        public int Visit(Variable operand)
        {
            return 1;
        }

        public int Visit(Addition operand)
        {
            return operand.Operands.Max(x => x.Accept(this)) + 1;
        }

        public int Visit(Subtraction operand)
        {
            return Math.Max(operand.Left.Accept(this), operand.Right.Accept(this)) + 1;
        }

        public int Visit(Multiplication operand)
        {
            return operand.Operands.Max(x => x.Accept(this)) + 1;
        }

        public int Visit(Division operand)
        {
            return Math.Max(operand.Left.Accept(this), operand.Right.Accept(this)) + 1;
        }

        public int Visit(Power operand)
        {
            return Math.Max(operand.Left.Accept(this), operand.Right.Accept(this)) + 1;
        }

        public int Visit(Root operand)
        {
            return operand.Inner.Accept(this) + 1;
        }

        public int Visit(Factorial operand)
        {
            return operand.Inner.Accept(this) + 1;
        }

        public int Visit(Sine operand)
        {
            return operand.Inner.Accept(this) + 1;
        }

        public int Visit(Cosine operand)
        {
            return operand.Inner.Accept(this) + 1;
        }

        public int Visit(Tangent operand)
        {
            return operand.Inner.Accept(this) + 1;
        }

        public int Visit(Exponential operand)
        {
            return operand.Inner.Accept(this) + 1;
        }

        public int Visit(Logarithm operand)
        {
            return operand.Inner.Accept(this) + 1;
        }

        public int Visit(LogarithmBase operand)
        {
            return operand.Inner.Accept(this) + 1;
        }

        public int Visit(Negative operand)
        {
            return operand.Inner.Accept(this) + 1;
        }

        public int Visit(Fraction operand)
        {
            return 1;
        }

        public int Visit(Minimum operand)
        {
            return operand.Operands.Max(x => x.Accept(this)) + 1;
        }

        public int Visit(Maximum operand)
        {
            return operand.Operands.Max(x => x.Accept(this)) + 1;
        }
    }
}