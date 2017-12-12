using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Negative : UnaryOperation
    {
        private Negative(Operand inner) : base(inner)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public static Operand Create(Operand inner)
        {
            if (inner.IsConstant() && !(inner is NamedConstant))
            {
                return Constant.Create(-((Constant)inner).Value);
            }

            return new Negative(inner);
        }
    }
}