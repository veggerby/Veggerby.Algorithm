using System.Collections.Generic;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class CommutativeOperationComparer : IComparer<Operand>
    {
        public int Compare(Operand x, Operand y)
        {
            if (x.IsConstant() && y.IsConstant())
            {
                return 0;
            }

            if (x.IsConstant())
            {
                return -1; // x < y
            }

            if (y.IsConstant())
            {
                return 1;
            }

            if (x.IsVariable() && y.IsVariable())
            {
                return 0;
            }

            if (x.IsVariable())
            {
                return -1;
            }

            if (y.IsVariable())
            {
                return 1;
            }

            var xPrio = x.GetPriority();
            var yPrio = y.GetPriority();

            if (xPrio != null && yPrio != null)
            {
                return -xPrio.Value.CompareTo(yPrio.Value); // opposite priority order, i.e. add first, factorial last
            }

            if (xPrio != null)
            {
                return -1;
            }

            if (yPrio != null)
            {
                return 1;
            }

            if (x is BinaryOperation && y is UnaryOperation)
            {
                return -1;
            }

            if (x is UnaryOperation && y is BinaryOperation)
            {
                return 1;
            }

            return x.GetType().Name.CompareTo(y.GetType().Name);
        }
    }
}