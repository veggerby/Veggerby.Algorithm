using System;
using System.Linq;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Addition : BinaryOperation, ICommutativeBinaryOperation, IAssociativeBinaryOperation
    {
        private Addition(Operand left, Operand right) : base(left, right)
        { 
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
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

            if (left.Equals(right))
            {
                return Multiplication.Create(2, left);
            }

            if (left.Equals(Constant.Zero))
            {
                return right;
            }

            if (right.Equals(Constant.Zero))
            {
                return left;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return (Constant)left + (Constant)right;
            }

            if (right.IsNegative())
            {
                return Subtraction.Create(left, ((Negative)right).Inner);
            }

            if (right.IsConstant() && ((Constant)right).Value < 0)
            {
                return Subtraction.Create(left, -((Constant)right).Value);
            }

            var result = new Addition(left, right);

            var flat = result.FlattenCommutative().ToList();

            if (flat.Where(x => x.IsConstant()).Count() > 1)
            {
                var constants = flat.Where(x => x.IsConstant()).OfType<Constant>();
                Operand @const = constants.Aggregate((seed, x) => (Constant)(seed + x));
                return flat.Where(x => !x.IsConstant()).Aggregate(@const, (seed, x) => Addition.Create(seed, x));
            }

            var groups = flat.GroupBy(x => x).ToList();

            if (groups.Any(x => x.Count() > 1))
            {
                return groups.Aggregate((Operand)Constant.Zero, (seed, group) => Addition.Create(seed, Multiplication.Create(group.Count(), group.Key)));
            }

            return result;
        }
    }
}