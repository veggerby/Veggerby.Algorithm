using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class OrderOperandVisitor : IOperandVisitor<int>
    {
        public int Visit(Function operand)
        {
            return 11;
        }

        public int Visit(FunctionReference operand)
        {
            return 12;
        }

        public int Visit(Constant operand)
        {
            return 1;
        }

        public int Visit(NamedConstant operand)
        {
            return 3;
        }

        public int Visit(Variable operand)
        {
            return 4;
        }

        public int Visit(Addition operand)
        {
            return 6;
        }

        public int Visit(Subtraction operand)
        {
            return 7;
        }

        public int Visit(Multiplication operand)
        {
            return 8;
        }

        public int Visit(Division operand)
        {
            return 9;
        }

        public int Visit(Power operand)
        {
            return 10;
        }

        public int Visit(Root operand)
        {
            return 19;
        }

        public int Visit(Factorial operand)
        {
            return 13;
        }

        public int Visit(Sine operand)
        {
            return 16;
        }

        public int Visit(Cosine operand)
        {
            return 17;
        }

        public int Visit(Tangent operand)
        {
            return 18;
        }

        public int Visit(Exponential operand)
        {
            return 14;
        }

        public int Visit(Logarithm operand)
        {
            return 15;
        }

        public int Visit(LogarithmBase operand)
        {
            return 20;
        }

        public int Visit(Negative operand)
        {
            return 5;
        }

        public int Visit(Fraction operand)
        {
            return 2;
        }

        public int Visit(Minimum operand)
        {
            return 21;
        }

        public int Visit(Maximum operand)
        {
            return 22;
        }
    }
}