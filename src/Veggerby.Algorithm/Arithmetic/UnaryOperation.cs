namespace Veggerby.Algorithm.Arithmetic
{
    public abstract class UnaryOperation : Operand, IUnaryOperation
    {
        public Operand Inner { get; }

        public UnaryOperation(Operand inner)
        {
            Inner = inner;
        }

        protected bool Equals(UnaryOperation other)
        {
            return Inner.Equals(other.Inner);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UnaryOperation)obj);
        }

        public override int GetHashCode()
        {
            return Inner.GetHashCode();
        }
    }
}