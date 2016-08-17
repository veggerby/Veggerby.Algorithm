using System;

namespace Veggerby.Algorithm.Calculus
{
    public class LogarithmBase : UnaryOperation
    {
        public int Base { get; }
        public LogarithmBase(int @base, Operand inner) : base(inner)
        {
            Base = @base;
        }

        public override double Evaluate(OperationContext context)
        {
            var inner = Inner.Evaluate(context);
            return Math.Log(inner) / Math.Log(Base);
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override Operand GetDerivative(Variable variable)
        {
            var inner = Inner.GetDerivative(variable);

            if (inner == null)
            {
                return null;
            }

            // chain rule
            return inner / (new Logarithm(Base) * Inner);
        }

        protected bool Equals(LogarithmBase other)
        {
            return Base.Equals(other.Base) && base.Equals(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LogarithmBase)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Base.GetHashCode();
                hashCode = (hashCode*397) ^ Inner.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return Base == 10 ? $"log({Inner})" : $"log{Base}({Inner})";
        }
    }
}