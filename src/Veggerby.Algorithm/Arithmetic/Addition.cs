namespace Veggerby.Algorithm.Arithmetic
{
    public class Addition : BinaryOperation
    {
        public Addition(Operand left, Operand right) : base(left, right)
        { 
        }

        public override double Evaluate(OperationContext context)
        {
            return Left.Evaluate(context) + Right.Evaluate(context);
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override Operand GetDerivative(Variable variable)
        {
            var left = Left.GetDerivative(variable);
            var right = Right.GetDerivative(variable);

            return left != null && right != null 
                ? left + right
                : null;
        }

        protected override string ToString(string left, string right)
        {
            return $"{left}+{right}";
        }
    }
}