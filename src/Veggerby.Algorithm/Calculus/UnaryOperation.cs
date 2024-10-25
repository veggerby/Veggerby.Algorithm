namespace Veggerby.Algorithm.Calculus;

public abstract class UnaryOperation(Operand inner) : Operand, IUnaryOperation
{
    public Operand Inner { get; } = inner;

    public override int GetHashCode() => GetType().GetHashCode() ^ Inner.GetHashCode();
}