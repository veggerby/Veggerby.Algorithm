using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Fraction : Operand, IEquatable<Fraction>
    {
        public int Numerator { get; }
        public int Denominator { get; }

        protected Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public static Operand operator +(Fraction left, Fraction right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            var num1 = left.Numerator * right.Denominator;
            var num2 = right.Numerator * left.Denominator;
            var dem = left.Denominator * right.Denominator;

            return Fraction.Create(num1 + num2, dem);
        }

        public static Operand operator -(Fraction left, Fraction right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            var num1 = left.Numerator * right.Denominator;
            var num2 = right.Numerator * left.Denominator;
            var dem = left.Denominator * right.Denominator;

            return Fraction.Create(num1 - num2, dem);
        }

        public static Operand operator *(Fraction left, Fraction right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return Fraction.Create(left.Numerator * right.Numerator, left.Denominator * right.Denominator);
        }

        public static Operand operator *(int left, Fraction right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return Fraction.Create(left * right.Numerator, right.Denominator);
        }

        public static Operand operator *(double left, Fraction right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            if (left.IsInteger())
            {
                return ((int)left) * right;
            }

            return left * right.Numerator / right.Denominator;
        }

        public static Operand operator *(Fraction left, int right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            return Fraction.Create(right * left.Numerator, left.Denominator);
        }

        public static Operand operator *(Fraction left, double right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right.IsInteger())
            {
                return left * ((int)right);
            }

            return right * left.Numerator / left.Denominator;
        }

        public static Operand operator /(Fraction left, Fraction right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return Fraction.Create(left.Numerator * right.Denominator, left.Denominator * right.Numerator);
        }

        public static Operand operator /(int left, Fraction right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return Fraction.Create(left * right.Denominator, right.Numerator);
        }

        public static Operand operator /(Fraction left, int right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            return Fraction.Create(left.Numerator, left.Denominator * right);
        }


        public static Operand operator /(double left, Fraction right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            if (left.IsInteger())
            {
                return ((int)left) / right;
            }

            return left * right.Denominator / right.Numerator;
        }

        public static Operand operator /(Fraction left, double right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right.IsInteger())
            {
                return left / ((int)right);
            }

            return left.Numerator / (left.Denominator * right);
        }

        public static Operand Create(int numerator, int denominator)
        {
            if (denominator == 1)
            {
                return Constant.Create(numerator);
            }

            return new Fraction(numerator, denominator);
        }

        public override bool Equals(object obj) 
        {
            return Equals(obj as Fraction);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as Fraction);
        }

        public bool Equals(Fraction other)
        {
            if (other == null)
            {
                return false;
            }

            return Numerator == other.Numerator && Denominator == other.Denominator;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Numerator.GetHashCode();
                hashCode = (hashCode*397) ^ Denominator.GetHashCode();
                return hashCode;
            }
        }
    }
}