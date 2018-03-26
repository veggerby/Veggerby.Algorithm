using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class UnspecifiedConstant : Constant, IEquatable<UnspecifiedConstant>
    {
        public Guid InstanceId { get; }

        private UnspecifiedConstant()
        {
            InstanceId = Guid.NewGuid();
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public static UnspecifiedConstant Create()
        {
            return new UnspecifiedConstant();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as UnspecifiedConstant);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as UnspecifiedConstant);
        }

        public bool Equals(UnspecifiedConstant other)
        {
            if (other == null)
            {
                return false;
            }

            return InstanceId.Equals(other.InstanceId);
        }

        public override int GetHashCode()
        {
            return InstanceId.GetHashCode();
        }
    }
}