using System;
using System.Collections.Generic;
using System.Linq;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Minimum : MultiOperation, ICommutativeOperation, IAssociativeOperation, IEquatable<Minimum>
    {
       public Minimum(params Operand[] operands) : base(operands)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

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

            return new Minimum(left, right);
        }

        public override bool Equals(object obj) 
        {
            return Equals(obj as Minimum);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as Minimum);
        }

        public bool Equals(Minimum other)
        {
            if (other == null)
            {
                return false;
            }

            return this.EqualsCommutative(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}