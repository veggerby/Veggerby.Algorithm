namespace Veggerby.Algorithm.Calculus;

/// (x ∗ y) ∗ z = x ∗ (y ∗ z)
/// https://en.wikipedia.org/wiki/Associative_property
public interface IAssociativeOperation
{
    IEnumerable<Operand> Operands { get; }
}