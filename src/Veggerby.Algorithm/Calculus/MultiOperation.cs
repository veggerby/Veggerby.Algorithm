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

        public override int GetHashCode() => Operands.Aggregate(GetType().GetHashCode(), (seed, x) => seed ^ x.GetHashCode());
    }
}