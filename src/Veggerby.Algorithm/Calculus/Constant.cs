using System;
using System.Globalization;

namespace Veggerby.Algorithm.Calculus
{
    public class Constant : Operand
    {
        public static readonly Constant Zero = Constant.Create(0);
        public static readonly Constant One = Constant.Create(1);
        public static readonly Constant MinusOne = Constant.Create(-1);
        public static readonly NamedConstant Pi = NamedConstant.Create("π", Math.PI);
        public static readonly NamedConstant e = NamedConstant.Create("e", Math.E);

        public double Value { get; }

        protected Constant(double value)
        {
            Value = value;
        }

        public override double Evaluate(OperationContext context)
        {
            return Value;
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override Operand GetDerivative(Variable variable)
        {
            return 0;
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture).TrimEnd('0', '.');
        }

        protected bool Equals(Constant other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Constant)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static Operand operator +(Constant left, Constant right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return left.Value + right.Value;
        }

        public static Operand operator -(Constant left, Constant right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return left.Value - right.Value;
        }

        public static Operand operator *(Constant left, Constant right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return left.Value * right.Value;
        }

        public static Operand operator /(Constant left, Constant right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return left.Value / right.Value;
        }

        public static Operand operator ^(Constant left, Constant right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return Math.Pow(left.Value, right.Value);
        }

        public static Constant Create(double value)
        {
            return new Constant(value);
        }
    }
}