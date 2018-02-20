namespace Veggerby.Algorithm.Calculus
{
    public abstract class UnaryOperation : Operand, IUnaryOperation
    {
        public Operand Inner { get; }

        public override int MaxDepth => Inner.MaxDepth + 1;

        protected UnaryOperation(Operand inner)
        {
            Inner = inner;
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() ^ Inner.GetHashCode();
        }
    }
}