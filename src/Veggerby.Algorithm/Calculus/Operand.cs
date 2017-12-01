using Veggerby.Algorithm.Calculus.Parser;
using Veggerby.Algorithm.Calculus.Visitors;

#pragma warning disable CS0660 // warning CS0660: 'Operand' defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // warning CS0661: 'Operand' defines operator == or operator != but does not override Object.GetHashCode()

namespace Veggerby.Algorithm.Calculus
{
    public abstract class Operand
    {
        public abstract void Accept(IOperandVisitor visitor);

        public sealed override string ToString()
        {
            var visitor = new ToStringOperandVisitor();
            this.Accept(visitor);
            return visitor.Result;
        }

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
            return Constant.Create(value);
        }

        public static implicit operator Operand(double value)
        {
            return Constant.Create(value);
        }

        public static implicit operator Operand(string value)
        {
            return FunctionParser.Parse(value);
        }
    }
}

#pragma warning restore CS0661
#pragma warning restore CS0660