namespace Veggerby.Algorithm.Calculus;

/// a * b = b * a
/// https://en.wikipedia.org/wiki/Commutative_property
public interface ICommutativeOperation
{
    IEnumerable<Operand> Operands { get; }
}