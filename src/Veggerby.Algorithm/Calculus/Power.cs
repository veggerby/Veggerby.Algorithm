using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Power : BinaryOperation, IEquatable<Power>
    {
        public Power(Operand left, Operand right) : base(left, right)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

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

            if (left.Equals(ValueConstant.One) || right.Equals(ValueConstant.Zero))
            {
                return ValueConstant.One;
            }

            if (right.Equals(ValueConstant.One))
            {
                return left;
            }

            return new Power(left, right);
        }

        public override bool Equals(object obj) => Equals(obj as Power);
        public override bool Equals(Operand other) => Equals(other as Power);
        public bool Equals(Power other) => other != null && this.EqualsBinary(other);
        public override int GetHashCode() => base.GetHashCode();
    }
}