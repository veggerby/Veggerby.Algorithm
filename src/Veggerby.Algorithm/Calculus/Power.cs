using System;

namespace Veggerby.Algorithm.Calculus
{
    public class Power : BinaryOperation
    {
        public Power(Operand left, Operand right) : base(left, right)
        { 
        }

        public override double Evaluate(OperationContext context)
        {
            return Math.Pow(Left.Evaluate(context), Right.Evaluate(context));
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override Operand GetDerivative(Variable variable)
        {
            if (Right.IsConstant())
            {
                return Multiplication.Create(
                    Right,
                    Power.Create(
                        Left, 
                        Subtraction.Create(Right, 1)
                    ));
            }

            // exponential rule
            return Exponential.Create(Multiplication.Create(Right, Logarithm.Create(Left)));
        }

        protected override string ToString(string left, string right)
        {
            return $"{left}^{right}";
        }

        public static Operand Create(Operand left, Operand right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            if (left.Equals(Constant.One) || right.Equals(Constant.Zero))
            {
                return 1;
            }

            if (right.Equals(Constant.One))
            {
                return left;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return (Constant)left ^ (Constant)right;
            }

            return new Power(left, right);
        }
    }
}