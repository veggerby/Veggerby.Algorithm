namespace Veggerby.Algorithm.Calculus
{
    public abstract class UnaryOperation : Operand, IUnaryOperation
    {
        public Operand Inner { get; }

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