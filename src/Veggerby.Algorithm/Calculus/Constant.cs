using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public abstract class Constant : Operand
    {
        public static readonly ValueConstant Zero = ValueConstant.Create(0);
        public static readonly ValueConstant One = ValueConstant.Create(1);
        public static readonly ValueConstant MinusOne = ValueConstant.Create(-1);
        public static readonly NamedConstant Pi = NamedConstant.Create("π", Math.PI);
        public static readonly NamedConstant e = NamedConstant.Create("e", Math.E);

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

            if (left is UnspecifiedConstant || right is UnspecifiedConstant)
            {
                return UnspecifiedConstant.Create();
            }

            if (left is IConstantWithSymbol || right is IConstantWithSymbol)
            {
                return Addition.Create(left, right);
            }

            if (left is IConstantWithValue && right is IConstantWithValue)
            {
                return ((IConstantWithValue)left).Value + ((IConstantWithValue)right).Value;
            }

            return UnspecifiedConstant.Create();
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

            if (left is UnspecifiedConstant || right is UnspecifiedConstant)
            {
                return UnspecifiedConstant.Create();
            }

            if (left is IConstantWithSymbol || right is IConstantWithSymbol)
            {
                return Subtraction.Create(left, right);
            }

            if (left is IConstantWithValue && right is IConstantWithValue)
            {
                return ((IConstantWithValue)left).Value - ((IConstantWithValue)right).Value;
            }

            return UnspecifiedConstant.Create();
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

            if (left is UnspecifiedConstant || right is UnspecifiedConstant)
            {
                return UnspecifiedConstant.Create();
            }

            if (left is IConstantWithSymbol || right is IConstantWithSymbol)
            {
                return Multiplication.Create(left, right);
            }

            if (left is IConstantWithValue && right is IConstantWithValue)
            {
                return ((IConstantWithValue)left).Value * ((IConstantWithValue)right).Value;
            }

            return UnspecifiedConstant.Create();
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

            if (left is UnspecifiedConstant || right is UnspecifiedConstant)
            {
                return UnspecifiedConstant.Create();
            }

            if (left is IConstantWithSymbol || right is IConstantWithSymbol)
            {
                return Division.Create(left, right);
            }

            if (left is IConstantWithValue && right is IConstantWithValue)
            {
                if (left.IsInteger() && right.IsInteger())
                {
                    return Fraction.Create((int)left, (int)right);
                }

                return ((IConstantWithValue)left).Value / ((IConstantWithValue)right).Value;
            }

            return UnspecifiedConstant.Create();
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

            if (left is UnspecifiedConstant || right is UnspecifiedConstant)
            {
                return UnspecifiedConstant.Create();
            }

            if (left is IConstantWithSymbol || right is IConstantWithSymbol)
            {
                return Power.Create(left, right);
            }

            if (left is IConstantWithValue && right is IConstantWithValue)
            {
                return Math.Pow(left, right);
            }

            return UnspecifiedConstant.Create();
        }

        public static implicit operator double(Constant value)
        {
            if (value is IConstantWithValue)
            {
                return ((IConstantWithValue)value).Value;
            }

            throw new NotSupportedException();
        }

        public static implicit operator int(Constant value)
        {
            if (value is IConstantWithValue)
            {
                return (int)((IConstantWithValue)value).Value;
            }

            throw new NotSupportedException();       
        }
    }
}