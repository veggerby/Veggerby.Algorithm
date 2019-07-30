using System;
using System.Collections.Generic;
using System.Linq;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Multiplication : MultiOperation, ICommutativeOperation, IAssociativeOperation, IEquatable<Multiplication>
    {
        private Multiplication(params Operand[] operands) : base(operands)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

        public static Operand Create(IEnumerable<Operand> operands)
        {
            if (operands == null)
            {
                throw new ArgumentNullException(nameof(operands));
            }

            if (operands.Count() == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(operands));
            }

            if (operands.Count() == 1)
            {
                return operands.Single();
            }

            return operands.Aggregate((seed, next) => Create(seed, next));
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

            if (left.Equals(ValueConstant.MinusOne))
            {
                return Negative.Create(right);
            }

            if (right.Equals(ValueConstant.MinusOne))
            {
                return Negative.Create(left);
            }

            var operands = new List<Operand>();

            if (left is Multiplication)
            {
                operands.AddRange(((Multiplication)left).Operands);
            }
            else
            {
                operands.Add(left);
            }

            if (right is Multiplication)
            {
                operands.AddRange(((Multiplication)right).Operands);
            }
            else
            {
                operands.Add(right);
            }

            // if any is zero, multiplication is zero
            if (operands.Any(x => x.Equals(ValueConstant.Zero)))
            {
                return ValueConstant.Zero;
            }

            if (operands.All(x => x.Equals(ValueConstant.One)))
            {
                return ValueConstant.One;
            }

            // remove ones
            operands = operands.Where(x => !x.Equals(ValueConstant.One)).ToList();

            // combine constants into one operand
            if (operands.Count(x => x.IsConstant()) > 1)
            {
                var constants = operands.Where(x => x.IsConstant()).Cast<ValueConstant>();
                var constant = constants.Aggregate((seed, next) => (ValueConstant)(seed * next));
                operands = new Operand[] { constant }.Concat(operands.Where(x => !x.IsConstant())).ToList();
            }

            if (operands.Count() == 1)
            {
                return operands.Single();
            }

            return new Multiplication(operands.ToArray());
        }

        public override bool Equals(object obj) => Equals(obj as Multiplication);
        public override bool Equals(Operand other) => Equals(other as Multiplication);
        public bool Equals(Multiplication other) => other != null && this.EqualsCommutative(other);
        public override int GetHashCode() => base.GetHashCode();
    }
}