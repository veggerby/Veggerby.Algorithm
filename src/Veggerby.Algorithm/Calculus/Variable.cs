using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Variable : Operand, IEquatable<Variable>
    {
        public readonly static Variable x = Variable.Create("x");
        public readonly static Variable y = Variable.Create("y");

        public string Identifier { get; }

        private Variable(string identifier)
        {
            Identifier = identifier;
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public static Variable Create(string identifier)
        {
            return new Variable(identifier);
        }        
        
        public override bool Equals(object obj) 
        {
            return Equals(obj as Variable);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as Variable);
        }

        public bool Equals(Variable other)
        {
            if (other == null)
            {
                return false;
            }

            return string.Equals(Identifier, other.Identifier);
        }

        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }
    }
}