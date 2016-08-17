using System;

namespace Veggerby.Algorithm.Calculus
{
    public abstract class Operand
    {
        public abstract double Evaluate(OperationContext context);
        public abstract void Accept(IOperandVisitor visitor);
        public abstract Operand GetDerivative(Variable variable);

        public static Operand operator +(Operand left, Operand right)
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
                return 2 * left;
            }

            if (left.Equals(new Constant(0)))
            {
                return right;
            }

            if (right.Equals(new Constant(0)))
            {
                return left;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return (Constant)left + (Constant)right;
            }

            return new Addition(left, right);
        }

        public static Operand operator -(Operand left, Operand right)
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
                return 0;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return (Constant)left - (Constant)right;
            }

            return new Subtraction(left, right);
        }

        public static Operand operator *(Operand left, Operand right)
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
                return left ^ 2;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return (Constant)left * (Constant)right;
            }

            return new Multiplication(left, right);
        }

        public static Operand operator /(Operand left, Operand right)
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

        public static Operand operator ^(Operand left, Operand right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            if (left.Equals(new Constant(1)) || right.Equals(new Constant(0)))
            {
                return 1;
            }

            if (right.Equals(new Constant(1)))
            {
                return left;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return (Constant)left ^ (Constant)right;
            }

            return new Power(left, right);
        }

        public static implicit operator Operand(int value)
        {
            return new Constant(value);
        }

        public static implicit operator Operand(double value)
        {
            return new Constant(value);
        }
    }
}