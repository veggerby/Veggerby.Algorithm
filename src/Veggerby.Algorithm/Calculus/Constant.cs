using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Constant : Operand, IEquatable<Constant>
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

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
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

            if (left.IsInteger() && right.IsInteger())
            {
                return Fraction.Create((int)left.Value, (int)right.Value);
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

        public static implicit operator double(Constant value)
        {
            return value.Value;
        }

        public static implicit operator int(Constant value)
        {
            return (int)value.Value;
        }

        public static Constant Create(double value)
        {
            return new Constant(value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Constant);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as Constant);
        }

        public bool Equals(Constant other)
        {
            if (other == null)
            {
                return false;
            }

            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}