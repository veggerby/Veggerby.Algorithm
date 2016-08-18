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

        public override Operand GetDerivative(Variable variable)
        {
            return new Negative(Inner.GetDerivative(variable));
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return Inner.CouldUseParenthesis() ? "-({Inner})" : $"-{Inner}";
        }

        public static Operand Create(Operand inner)
        {
            return new Negative(inner);
        }
    }
}