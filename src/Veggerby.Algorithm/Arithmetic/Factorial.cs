using System;

namespace Veggerby.Algorithm.Arithmetic
{
    public class Factorial : UnaryOperation
    {
        public Factorial(Operand inner) : base(inner)
        {
        }

        public override double Evaluate(OperationContext context)
        {
            var inner = Inner.Evaluate(context);
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

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"{Inner}!";
        }
    }
}