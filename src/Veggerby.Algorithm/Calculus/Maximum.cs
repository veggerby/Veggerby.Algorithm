using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus;

public class Maximum(params Operand[] operands) : MultiOperation(operands), ICommutativeOperation, IAssociativeOperation, IEquatable<Maximum>
{
    public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

    public static Operand Create(IEnumerable<Operand> operands)
    {
        if (operands is null)
        {
            throw new ArgumentNullException(nameof(operands));
        }

        if (operands.Count() == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(operands));
        }

        if (operands.Count() == 1)
        {
            return operands.Single();
        }

        var first = operands.First();
        return operands.Skip(1).Aggregate(first, (seed, next) => Create(seed, next));
    }

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

        return new Maximum(left, right);
    }

    public override bool Equals(object obj) => Equals(obj as Maximum);
    public override bool Equals(Operand other) => Equals(other as Maximum);
    public bool Equals(Maximum other) => other is not null && this.EqualsCommutative(other);
    public override int GetHashCode() => base.GetHashCode();
}