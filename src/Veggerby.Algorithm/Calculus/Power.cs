using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus;

public class Power(Operand left, Operand right) : BinaryOperation(left, right), IEquatable<Power>
{
    public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

    public static Operand Create(Operand left, Operand right)
    {
        if (left is null)
        {
            throw new ArgumentNullException(nameof(left));
        }

        if (right is null)
        {
            throw new ArgumentNullException(nameof(right));
        }

        if (left.Equals(Constant.One) || right.Equals(Constant.Zero))
        {
            return Constant.One;
        }

        if (right.Equals(Constant.One))
        {
            return left;
        }

        return new Power(left, right);
    }

    public override bool Equals(object obj) => Equals(obj as Power);
    public override bool Equals(Operand other) => Equals(other as Power);
    public bool Equals(Power other) => other is not null && this.EqualsBinary(other);
    public override int GetHashCode() => base.GetHashCode();
}