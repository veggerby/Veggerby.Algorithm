namespace Veggerby.Algorithm.Arithmetic
{
    public interface IOperand
    {
        double Evaluate(OperationContext context);
        void Accept(IOperandVisitor visitor);
    }
}