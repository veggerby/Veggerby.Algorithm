namespace Veggerby.Algorithm.Calculus;

public abstract class MultiOperation(params Operand[] operands) : Operand
{
    public IEnumerable<Operand> Operands { get; } = operands;

    public override int GetHashCode() => Operands.Aggregate(GetType().GetHashCode(), (seed, x) => seed ^ x.GetHashCode());
}