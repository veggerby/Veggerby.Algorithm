namespace Veggerby.Algorithm.Arithmetic
{
    public class Multiplication : BinaryOperation
    {
        public Multiplication(Operand left, Operand right) : base(left, right)
        { 
        }

        public override double Evaluate(OperationContext context)
        {
            return Left.Evaluate(context) * Right.Evaluate(context);
        }

        public override Operand GetDerivative(Variable variable)
        {
            var left = Left.GetDerivative(variable);
            var right = Right.GetDerivative(variable);

            if (left == null || right == null)
            {
                return null;
            }

            // product rule
            return (left * Right + right * Left);
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override string ToString(string left, string right)
        {
            return $"{left}*{right}";
        }
    }
}