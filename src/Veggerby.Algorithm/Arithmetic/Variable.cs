namespace Veggerby.Algorithm.Arithmetic
{
    public class Variable : Operand
    {
        public string Identifier { get; }

        public Variable(string identifier)
        {
            Identifier = identifier;
        }

        public override double Evaluate(OperationContext context)
        {
            return context.Get(Identifier);
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return Identifier;
        }
        
        protected bool Equals(Variable other)
        {
            return string.Equals(Identifier, other.Identifier);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Variable)obj);
        }

        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }
    }
}