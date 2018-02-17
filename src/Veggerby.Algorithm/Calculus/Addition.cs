using System;
using System.Collections.Generic;
using System.Linq;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Addition : MultiOperation, ICommutativeOperation, IAssociativeOperation, IEquatable<Addition>
    {
        private Addition(params Operand[] operands) : base(operands)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public static Operand Create(IEnumerable<Operand> operands)
        {
            if (operands == null)
            {
                throw new ArgumentNullException(nameof(operands));
            }

            if (operands.Count() == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(operands));
            }

            if (operands.Count() == 1)
            {
                return operands.Single();
            }

            return operands.Aggregate((seed, next) => Create(seed, next));
        }

        public static Operand Create(Operand left, Operand right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            if (right.IsNegative())
            {
                return Subtraction.Create(left, ((Negative)right).Inner);
            }

            if (right.IsConstant() && ((Constant)right).Value < 0)
            {
                return Subtraction.Create(left, -((Constant)right).Value);
            }

            if (left.IsNegative())
            {
                return Subtraction.Create(right, ((Negative)left).Inner);
            }

            if (left.IsConstant() && ((Constant)left).Value < 0)
            {
                return Subtraction.Create(right, -((Constant)left).Value);
            }

            var operands = new List<Operand>();

            if (left is Addition)
            {
                operands.AddRange(((Addition)left).Operands);
            }
            else
            {
                operands.Add(left);
            }

            if (right is Addition)
            {
                operands.AddRange(((Addition)right).Operands);
            }
            else
            {
                operands.Add(right);
            }

            if (operands.All(x => x.Equals(Constant.Zero)))
            {
                return Constant.Zero;
            }

            // remove zeros
            operands = operands.Where(x => !x.Equals(Constant.Zero)).ToList();

            // combine constants into one operand
            if (operands.Count(x => x.IsConstant()) > 1)
            {
                var constants = operands.Where(x => x.IsConstant()).Cast<Constant>();
                var constant = constants.Aggregate((seed, next) => (Constant)(seed + next));
                operands = new Operand[] { constant }.Concat(operands.Where(x => !x.IsConstant())).ToList();
            }

            if (operands.Count() == 1)
            {
                return operands.Single();
            }

            return new Addition(operands.ToArray());
        }

        public override bool Equals(object obj) 
        {
            return Equals(obj as Addition);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as Addition);
        }

        public bool Equals(Addition other)
        {
            if (other == null)
            {
                return false;
            }

            return this.EqualsCommutative(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}