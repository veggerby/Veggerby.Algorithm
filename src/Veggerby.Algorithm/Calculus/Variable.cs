using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Variable : Operand
    {
        public readonly static Variable x = Variable.Create("x");
        public readonly static Variable y = Variable.Create("y");

        public string Identifier { get; }

        private Variable(string identifier)
        {
            Identifier = identifier;
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
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

        public static Variable Create(string identifier)
        {
            return new Variable(identifier);
        }
    }
}