using System;
using Veggerby.Algorithm.Calculus.Visitors;

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

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
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
    }
}