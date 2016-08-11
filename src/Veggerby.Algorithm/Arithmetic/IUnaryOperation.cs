namespace Veggerby.Algorithm.Arithmetic
{
    public interface IUnaryOperation : IOperand
    {
        IOperand Inner { get; }
    }
}