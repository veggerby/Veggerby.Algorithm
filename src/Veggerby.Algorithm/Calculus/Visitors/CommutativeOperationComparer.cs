namespace Veggerby.Algorithm.Calculus.Visitors;

public class CommutativeOperationComparer : IComparer<Operand>
{
    public int Compare(Operand x, Operand y)
    {
        if (x is null)
        {
            throw new ArgumentNullException(nameof(x));
        }

        if (y is null)
        {
            throw new ArgumentNullException(nameof(y));
        }

        var complexityVisitor = new ComplexityOperandVisitor();
        var complexityX = x.Accept(complexityVisitor);
        var complexityY = y.Accept(complexityVisitor);

        if (complexityX < complexityY)
        {
            return -1;
        }

        if (complexityX > complexityY)
        {
            return 1;
        }

        var orderVisitor = new OrderOperandVisitor();
        var orderX = x.Accept(orderVisitor);
        var orderY = y.Accept(orderVisitor);

        return orderX.CompareTo(orderY);
    }
}