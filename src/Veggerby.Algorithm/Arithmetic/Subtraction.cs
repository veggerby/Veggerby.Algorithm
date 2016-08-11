namespace Veggerby.Algorithm.Arithmetic
{
    public class Subtraction : BinaryOperation
    {
        public Subtraction(IOperand left, IOperand right) : base(left, right)
        { 
        }

        public override double Evaluate(OperationContext context)
        {
            return Left.Evaluate(context) - Right.Evaluate(context);
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"{Left}-{Right}";
        }
    }
}