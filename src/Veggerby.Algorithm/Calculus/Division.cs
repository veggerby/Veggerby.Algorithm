using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus;

public class Division : BinaryOperation, IEquatable<Division>
{
    private Division(Operand left, Operand right) : base(left, right)
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

        if (left is Fraction && right is Fraction)
        {
            return ((Fraction)left) / ((Fraction)right);
        }

        if (left.IsConstant() && right.IsConstant())
        {
            var l = (ValueConstant)left;
            var r = (ValueConstant)right;

            if (l.IsInteger() && r.IsInteger())
            {
                return Fraction.Create(l, r);
            }

            return l.Value / r.Value;
        }

        if (left.IsConstant() && right is Fraction)
        {
            return ((ValueConstant)left).Value / (Fraction)right;
        }

        if (left is Fraction && right.IsConstant())
        {
            return ((Fraction)left) / ((ValueConstant)right).Value;
        }

        if (right.Equals(Constant.One))
        {
            return left;
        }

        return new Division(left, right);
    }

    public override bool Equals(object obj) => Equals(obj as Division);
    public override bool Equals(Operand other) => Equals(other as Division);
    public bool Equals(Division other) => other is not null && this.EqualsBinary(other);
    public override int GetHashCode() => base.GetHashCode();
}