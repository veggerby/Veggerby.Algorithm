using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Power : BinaryOperation, IEquatable<Power>
    {
        public Power(Operand left, Operand right) : base(left, right)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
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

            if (left.Equals(Constant.One) || right.Equals(Constant.Zero))
            {
                return Constant.One;
            }

            if (right.Equals(Constant.One))
            {
                return left;
            }

            return new Power(left, right);
        }

        public override bool Equals(object obj) 
        {
            return Equals(obj as Power);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as Power);
        }

        public bool Equals(Power other)
        {
            if (other == null)
            {
                return false;
            }

            return this.EqualsBinary(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}