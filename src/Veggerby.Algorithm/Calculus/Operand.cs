namespace Veggerby.Algorithm.Calculus
{
    public abstract class Operand
    {
        public abstract double Evaluate(OperationContext context);
        public abstract void Accept(IOperandVisitor visitor);
        public abstract Operand GetDerivative(Variable variable);

        public static Operand operator +(Operand left, Operand right)
        {
            return Addition.Create(left, right);
        }

        public static Operand operator -(Operand left, Operand right)
        {
            return Subtraction.Create(left, right);
        }

        public static Operand operator *(Operand left, Operand right)
        {
            return Multiplication.Create(left, right);
        }

        public static Operand operator /(Operand left, Operand right)
        {
            return Division.Create(left, right);
        }

        public static Operand operator ^(Operand left, Operand right)
        {
            return Power.Create(left, right);
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