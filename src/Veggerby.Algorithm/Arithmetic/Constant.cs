using System.Globalization;

namespace Veggerby.Algorithm.Arithmetic
{
    public class Constant : IOperand
    {
        public double Value { get; }

        public Constant(double value)
        {
            Value = value;
        }

        public double Evaluate(OperationContext context)
        {
            return Value;
        }

        public void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture).TrimEnd('0', '.');
        }

        protected bool Equals(Constant other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Constant)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}