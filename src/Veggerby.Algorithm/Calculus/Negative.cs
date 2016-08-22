namespace Veggerby.Algorithm.Calculus
{
    public class Negative : UnaryOperation
    {
        private Negative(Operand inner) : base(inner)
        {
        }

        public override double Evaluate(OperationContext context)
        {
            return -Inner.Evaluate(context);
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public static Operand Create(Operand inner)
        {
            if (inner.IsConstant())
            {
                return Constant.Create(-((Constant)inner).Value);
            }

            return new Negative(inner);
        }
    }
}