using System;
using System.Collections.Generic;
using System.Linq;
using Veggerby.Algorithm.Calculus.Parser;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Function : Operand, IEquatable<Function>
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

        public override bool Equals(object obj) 
        {
            return Equals(obj as Function);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as Function);
        }

        public bool Equals(Function other)
        {
            if (other == null)
            {
                return false;
            }

            return Operand.Equals(other.Operand);
        }

        public override int GetHashCode()
        {
            return Operand.GetHashCode();
        }

    }
}