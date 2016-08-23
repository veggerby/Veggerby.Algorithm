using System;
using System.Linq;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Multiplication : BinaryOperation, ICommutativeBinaryOperation, IAssociativeBinaryOperation
    {
        private Multiplication(Operand left, Operand right) : base(left, right)
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

            if (left is Division && right is Division)
            {
                var l = (Division)left;
                var r = (Division)right;

                return Division.Create(
                    Multiplication.Create(l.Left, r.Left),
                    Multiplication.Create(l.Right, r.Right)
                );
            }

            if (left is Division)
            {
                var l = (Division)left;

                return Division.Create(
                    Multiplication.Create(l.Left, right),
                    l.Right
                );
            }

            if (right is Division)
            {
                var r = (Division)right;

                return Division.Create(
                    Multiplication.Create(left, r.Left),
                    r.Right
                );
            }

            if (left.Equals(right))
            {
                return Power.Create(left, 2);
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return (Constant)left * (Constant)right;
            }

            if (left.Equals(Constant.Zero) || right.Equals(Constant.Zero))
            {
                return Constant.Zero;
            }

            if (left.Equals(Constant.One))
            {
                return right;
            }

            if (right.Equals(Constant.One))
            {
                return left;
            }

            if (left.IsNegative() && right.IsNegative())
            {
                return Multiplication.Create(((Negative)left).Inner, ((Negative)right).Inner);
            }

            if (left.IsNegative())
            {
                return Negative.Create(Multiplication.Create(((Negative)left).Inner, right));
            }

            if (right.IsNegative())
            {
                return Negative.Create(Multiplication.Create(left, ((Negative)right).Inner));
            }

            if (left.Equals(Constant.MinusOne))
            {
                return Negative.Create(right);
            }

            if (right.Equals(Constant.MinusOne))
            {
                return Negative.Create(left);
            }

            if (!left.IsConstant() && right.IsConstant())
            {
                return Multiplication.Create(right, left);
            }

            var result = new Multiplication(left, right);

            var flat = result.FlattenCommutative().ToList();

            if (flat.Where(x => x.IsConstant()).Count() > 1)
            {
                var constants = flat.Where(x => x.IsConstant()).OfType<Constant>();
                Operand @const = constants.Aggregate((seed, x) => (Constant)(seed + x));
                return flat.Where(x => !x.IsConstant()).Aggregate(@const, (seed, x) => Multiplication.Create(seed, x));
            }

            var groups = flat.GroupBy(x => x).ToList();
            if (groups.Any(x => x.Count() > 1))
            {
                return groups.Aggregate((Operand)Constant.One, (seed, group) => Multiplication.Create(seed, Power.Create(group.Key, group.Count())));
            }

            return result;
        }
    }
}