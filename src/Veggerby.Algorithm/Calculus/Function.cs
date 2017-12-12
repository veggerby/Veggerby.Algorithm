using System;
using System.Collections.Generic;
using System.Linq;
using Veggerby.Algorithm.Calculus.Parser;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Function : Operand
    {
        public string Identifier { get; }
        public IEnumerable<Variable> Variables { get; }
        public Operand Operand { get; }

        protected Function(string identifier, Operand operand)
        {
            Identifier = identifier;
            Operand = operand;

            var visitor = new VariablesOperandVisitor();
            Variables = operand.Accept(visitor).OrderBy(x => x.Identifier).ToList();
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        protected bool Equals(Function other)
        {
            return Operand.Equals(other.Operand);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Function)obj);
        }

        public override int GetHashCode()
        {
            return Operand.GetHashCode();
        }

        public static Function Create(string identifier, Operand operand)
        {
            if (string.IsNullOrEmpty(identifier))
            {
                throw new ArgumentException("Invalid identifier", nameof(identifier));
            }

            if (operand == null)
            {
                throw new ArgumentNullException(nameof(operand));
            }

            return new Function(identifier, operand);
        }

        public static implicit operator Function(string value)
        {
            return Function.Create(
                "f",
                FunctionParser.Parse(value));
        }
    }
}