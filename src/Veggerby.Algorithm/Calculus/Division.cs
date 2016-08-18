using System;

namespace Veggerby.Algorithm.Calculus
{
    public class Division : BinaryOperation
    {
        private Division(Operand left, Operand right) : base(left, right)
        { 
        }

        public override double Evaluate(OperationContext context)
        {
            return Left.Evaluate(context) / Right.Evaluate(context);
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override Operand GetDerivative(Variable variable)
        {
            var left = Left.GetDerivative(variable);
            var right = Right.GetDerivative(variable);

            if (left == null || right == null)
            {
                return null;
            }

            // division rule
            return Division.Create(
                Subtraction.Create(
                    Multiplication.Create(left, Right),
                    Multiplication.Create(right, Left)), 
                Power.Create(Left, 2));
        }

        protected override string ToString(string left, string right)
        {
            return $"{left}/{right}";
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

            if (left.Equals(right))
            {
                return 1;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return (Constant)left / (Constant)right;
            }

            return new Division(left, right);
        }
    }
}