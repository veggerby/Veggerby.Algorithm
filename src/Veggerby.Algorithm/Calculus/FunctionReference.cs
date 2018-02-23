using System;
using System.Collections.Generic;
using System.Linq;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class FunctionReference : Operand, IEquatable<FunctionReference>
    {
        public string Identifier { get; }
        public IEnumerable<Operand> Parameters { get; }

        protected FunctionReference(string identifier, IEnumerable<Operand> parameters)
        {
            Identifier = identifier;
            Parameters = parameters;
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public static Operand Create(string identifier, params Operand[] parameters)
        {
            return Create(identifier, parameters.AsEnumerable());
        }

        public static Operand Create(string identifier, IEnumerable<Operand> parameters)
        {
            if (string.IsNullOrEmpty(identifier))
            {
                throw new ArgumentException("Invalid identifier", nameof(identifier));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return new FunctionReference(identifier, parameters);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as FunctionReference);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as FunctionReference);
        }

        public bool Equals(FunctionReference other)
        {
            if (other == null)
            {
                return false;
            }

            return string.Equals(Identifier, other.Identifier) && Parameters.SequenceEqual(other.Parameters);
        }

        public override int GetHashCode()
        {
            return Parameters.Aggregate(Identifier.GetHashCode(), (seed, operand) => seed ^ operand.GetHashCode());
        }

    }
}