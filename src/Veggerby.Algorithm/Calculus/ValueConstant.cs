using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class ValueConstant : Constant, IConstantWithValue, IEquatable<ValueConstant>
    {
        public double Value { get; }

        protected ValueConstant(double value)
        {
            Value = value;
        }

        public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

        public static Operand operator +(ValueConstant left, ValueConstant right)
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

        public static Operand operator -(ValueConstant left, ValueConstant right)
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

        public static Operand operator *(ValueConstant left, ValueConstant right)
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

        public static Operand operator /(ValueConstant left, ValueConstant right)
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

        public static Operand operator ^(ValueConstant left, ValueConstant right)
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

        public static implicit operator double(ValueConstant value) => value.Value;

        public static implicit operator int(ValueConstant value) => (int)value.Value;

        public static ValueConstant Create(double value) => new ValueConstant(value);

        public override bool Equals(object obj) => Equals(obj as ValueConstant);
        public override bool Equals(Operand other) => Equals(other as ValueConstant);
        public bool Equals(ValueConstant other) => other != null && Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();
    }
}