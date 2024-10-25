using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus;

public class Logarithm : UnaryOperation, IEquatable<Logarithm>
{
    private Logarithm(Operand inner) : base(inner)
    {
    }

    public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

    public static Operand Create(Operand inner) => new Logarithm(inner);

    public override bool Equals(object obj) => Equals(obj as Logarithm);
    public override bool Equals(Operand other) => Equals(other as Logarithm);
    public bool Equals(Logarithm other) => other is not null && Inner.Equals(other.Inner);
    public override int GetHashCode() => base.GetHashCode();
}