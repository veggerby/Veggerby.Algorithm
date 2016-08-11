namespace Veggerby.Algorithm.Arithmetic
{
    public abstract class Operand
    {
        public abstract double Evaluate(OperationContext context);
        public abstract void Accept(IOperandVisitor visitor);
    }
}