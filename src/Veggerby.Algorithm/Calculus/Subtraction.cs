using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus;

public class Subtraction : BinaryOperation, IEquatable<Subtraction>
{
    private Subtraction(Operand left, Operand right) : base(left, right)
    {
    }

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

        if (right.Equals(Constant.Zero))
        {
            return left;
        }

        if (left.Equals(Constant.Zero))
        {
            return Negative.Create(right);
        }

        return new Subtraction(left, right);
    }

    public override bool Equals(object obj) => Equals(obj as Subtraction);
    public override bool Equals(Operand other) => Equals(other as Subtraction);
    public bool Equals(Subtraction other) => other is not null && this.EqualsBinary(other);
    public override int GetHashCode() => base.GetHashCode();
}