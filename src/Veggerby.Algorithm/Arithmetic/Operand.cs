namespace Veggerby.Algorithm.Arithmetic
{
    public abstract class Operand
    {
        public abstract double Evaluate(OperationContext context);
        public abstract void Accept(IOperandVisitor visitor);

        public static Operand operator +(Operand left, Operand right)
        {
            if (left.Equals(right))
            {
                return 2 * left;
            }

            return new Addition(left, right);
        }

        public static Operand operator -(Operand left, Operand right)
        {
            if (left.Equals(right))
            {
                return 0;
            }

            return new Subtraction(left, right);
        }

        public static Operand operator *(Operand left, Operand right)
        {
            if (left.Equals(right))
            {
                return left ^ 2;
            }

            return new Multiplication(left, right);
        }

        public static Operand operator /(Operand left, Operand right)
        {
            if (left.Equals(right))
            {
                return 1;
            }

            return new Division(left, right);
        }

        public static Operand operator ^(Operand left, Operand right)
        {
            if (left.Equals(new Constant(1)) || right.Equals(new Constant(0)))
            {
                return 1;
            }

            if (right.Equals(new Constant(1)))
            {
                return left;
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