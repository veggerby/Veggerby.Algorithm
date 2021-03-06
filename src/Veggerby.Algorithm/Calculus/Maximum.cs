using System;
using System.Collections.Generic;
using System.Linq;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Maximum : MultiOperation, ICommutativeOperation, IAssociativeOperation, IEquatable<Maximum>
    {
        public Maximum(params Operand[] operands) : base(operands)
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

            var first = operands.First();
            return operands.Skip(1).Aggregate(first, (seed, next) => Create(seed, next));
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

            return new Maximum(left, right);
        }

        public override bool Equals(object obj) => Equals(obj as Maximum);
        public override bool Equals(Operand other) => Equals(other as Maximum);
        public bool Equals(Maximum other) => other != null && this.EqualsCommutative(other);
        public override int GetHashCode() => base.GetHashCode();
    }
}