namespace Veggerby.Algorithm.Calculus
{
    public class Division : BinaryOperation
    {
        public Division(Operand left, Operand right) : base(left, right)
        { 
        }

        public override double Evaluate(OperationContext context)
        {
            return Left.Evaluate(context) / Right.Evaluate(context);
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override Operand GetDerivative(Variable variable)
        {
            var left = Left.GetDerivative(variable);
            var right = Right.GetDerivative(variable);

            if (left == null || right == null)
            {
                return null;
            }

            // division rule
            return (left * Right - right * Left) / (Left ^ 2);
        }

        protected override string ToString(string left, string right)
        {
            return $"{left}/{right}";
        }
    }
}