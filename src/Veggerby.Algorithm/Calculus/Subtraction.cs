using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Subtraction : BinaryOperation, IEquatable<Subtraction>
    {
        private Subtraction(Operand left, Operand right) : base(left, right)
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

            if (right.Equals(ValueConstant.Zero))
            {
                return left;
            }

            if (left.Equals(ValueConstant.Zero))
            {
                return Negative.Create(right);
            }

            return new Subtraction(left, right);
        }
        
        public override bool Equals(object obj) 
        {
            return Equals(obj as Subtraction);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as Subtraction);
        }

        public bool Equals(Subtraction other)
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