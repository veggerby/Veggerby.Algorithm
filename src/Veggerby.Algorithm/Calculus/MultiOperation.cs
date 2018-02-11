using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Calculus
{
    public abstract class MultiOperation : Operand
    {
        public IEnumerable<Operand> Operands { get; }

        protected MultiOperation(params Operand[] operands)
        {
            Operands = operands;
        }

        protected bool Equals(MultiOperation other)
        {
            if (this is ICommutativeOperation)
            {
                // TO DO optimize
                return Operands.OrderBy(x => x.ToString()).SequenceEqual(other.Operands.OrderBy(x => x.ToString()));
            }

            return Operands.SequenceEqual(other.Operands);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MultiOperation)obj);
        }

        public override int GetHashCode()
        {
            return Operands.Aggregate(GetType().Name.GetHashCode(), (seed, x) => seed ^ x.GetHashCode());
        }
    }
}