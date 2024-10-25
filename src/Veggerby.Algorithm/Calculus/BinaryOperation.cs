namespace Veggerby.Algorithm.Calculus;

public abstract class BinaryOperation : Operand, IBinaryOperation
{
    public Operand Left { get; }
    public Operand Right { get; }

    protected BinaryOperation(Operand left, Operand right)
    {
        if (left is null)
        {
            throw new ArgumentNullException(nameof(left));
        }

        if (right is null)
        {
            throw new ArgumentNullException(nameof(right));
        }

        Left = left;
        Right = right;
    }

    public override int GetHashCode() => GetType().GetHashCode() ^ Left.GetHashCode() ^ Right.GetHashCode();
}