using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus;

public class Tangent : UnaryOperation, IEquatable<Tangent>
{
    private Tangent(Operand inner) : base(inner)
    {
    }

    public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

    public static Operand Create(Operand inner) => new Tangent(inner);

    public override bool Equals(object obj) => Equals(obj as Tangent);
    public override bool Equals(Operand other) => Equals(other as Tangent);
    public bool Equals(Tangent other) => other is not null && Inner.Equals(other.Inner);
    public override int GetHashCode() => base.GetHashCode();
}