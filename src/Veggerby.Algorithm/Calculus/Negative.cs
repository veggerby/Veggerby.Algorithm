using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus;

public class Negative : UnaryOperation, IEquatable<Negative>
{
    private Negative(Operand inner) : base(inner)
    {
    }

    public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

    public static Operand Create(Operand inner)
    {
        if (inner.IsConstant() && inner is not NamedConstant)
        {
            return ValueConstant.Create(-((ValueConstant)inner).Value);
        }

        return new Negative(inner);
    }

    public override bool Equals(object obj) => Equals(obj as Negative);
    public override bool Equals(Operand other) => Equals(other as Negative);
    public bool Equals(Negative other) => other is not null && Inner.Equals(other.Inner);
    public override int GetHashCode() => base.GetHashCode();
}