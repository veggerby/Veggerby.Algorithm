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

        public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

        public static Variable Create(string identifier) => new Variable(identifier);

        public override bool Equals(object obj) => Equals(obj as Variable);
        public override bool Equals(Operand other) => Equals(other as Variable);
        public bool Equals(Variable other) => other != null && string.Equals(Identifier, other.Identifier);
        public override int GetHashCode() => Identifier.GetHashCode();
    }
}