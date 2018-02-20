using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Calculus
{
    public abstract class MultiOperation : Operand
    {
        public IEnumerable<Operand> Operands { get; }

        public override int MaxDepth => Operands.Max(x => x.MaxDepth) + 1;

        protected MultiOperation(params Operand[] operands)
        {
            Operands = operands;
        }

        public override int GetHashCode()
        {
            return Operands.Aggregate(GetType().GetHashCode(), (seed, x) => seed ^ x.GetHashCode());
        }
    }
}