namespace Veggerby.Algorithm.Arithmetic
{
    public interface IBinaryOperation : IOperand
    {
        IOperand Left { get; }
        IOperand Right { get; }
    }
}