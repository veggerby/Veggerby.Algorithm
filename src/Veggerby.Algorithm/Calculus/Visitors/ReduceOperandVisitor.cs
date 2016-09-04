using System;
using System.Linq;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class ReduceOperandVisitor : IOperandVisitor
    {
        public Operand Result { get; private set; }

        public void Visit(Function operand)
        {
            Result = Function.Create(operand.Identifier, operand.Operand.Reduce());
        }

        public void Visit(Constant operand)
        {
            Result = operand;
        }

        public void Visit(NamedConstant operand)
        {
            Result = operand;
        }

        public void Visit(Variable operand)
        {
            Result = operand;
        }

        public void Visit(Addition operand)
        {
            var left = operand.Left;
            var right = operand.Right;

            if (left.Equals(right))
            {
                Result = Multiplication.Create(2, left);
                return;
            }

            if (left.Equals(Constant.Zero))
            {
                Result = right;
                return;
            }

            if (right.Equals(Constant.Zero))
            {
                Result = left;
                return;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                Result = (Constant)left + (Constant)right;
                return;
            }

            if (right.IsNegative())
            {
                Result = Subtraction.Create(left, ((Negative)right).Inner);
                return;
            }

            if (right.IsConstant() && ((Constant)right).Value < 0)
            {
                Result = Subtraction.Create(left, -((Constant)right).Value);
                return;
            }

            var flat = operand.FlattenAssociative().ToList();

            if (flat.Where(x => x.IsConstant()).Count() > 1)
            {
                var constants = flat.Where(x => x.IsConstant()).OfType<Constant>();
                Operand @const = constants.Aggregate((seed, x) => (Constant)(seed + x));
                Result = flat.Where(x => !x.IsConstant()).Aggregate(@const, (seed, x) => Addition.Create(seed, x));
                return;
            }

            var groups = flat.GroupBy(x => x).ToList();

            if (groups.Any(x => x.Count() > 1))
            {
                Result = groups.Aggregate((Operand)Constant.Zero, (seed, group) => Addition.Create(seed, Multiplication.Create(group.Count(), group.Key)));
                return;
            }

            Result = operand;
        }

        public void Visit(Subtraction operand)
        {
            var left = operand.Left;
            var right = operand.Right;

            if (left.Equals(right))
            {
                Result = 0;
                return;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                Result = (Constant)left - (Constant)right;
                return;
            }

            if (left.Equals(Constant.Zero))
            {
                Result = Negative.Create(right);
                return;
            }

            if (right.IsNegative())
            {
                Result = Addition.Create(left, ((Negative)right).Inner);
                return;
            }

            if (right.IsConstant() && ((Constant)right).Value < 0)
            {
                Result = Addition.Create(left, -((Constant)right).Value);
                return;
            }

            Result = operand;
        }

        public void Visit(Multiplication operand)
        {
            var left = operand.Left;
            var right = operand.Right;

            if (left is Division && right is Division)
            {
                var l = (Division)left;
                var r = (Division)right;

                Result = Division.Create(
                    Multiplication.Create(l.Left, r.Left),
                    Multiplication.Create(l.Right, r.Right)
                );
                return;
            }

            if (left is Division)
            {
                var l = (Division)left;

                Result = Division.Create(
                    Multiplication.Create(l.Left, right),
                    l.Right
                );
                return;
            }

            if (right is Division)
            {
                var r = (Division)right;

                Result = Division.Create(
                    Multiplication.Create(left, r.Left),
                    r.Right
                );
                return;
            }

            if (left.Equals(right))
            {
                Result = Power.Create(left, 2);
                return;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                Result = (Constant)left * (Constant)right;
                return;
            }

            if (left.Equals(Constant.Zero) || right.Equals(Constant.Zero))
            {
                Result = Constant.Zero;
                return;
            }

            if (left.Equals(Constant.One))
            {
                Result = right;
                return;
            }

            if (right.Equals(Constant.One))
            {
                Result = left;
                return;
            }

            if (left.IsNegative() && right.IsNegative())
            {
                Result = Multiplication.Create(((Negative)left).Inner, ((Negative)right).Inner);
                return;
            }

            if (left.IsNegative())
            {
                Result = Negative.Create(Multiplication.Create(((Negative)left).Inner, right));
                return;
            }

            if (right.IsNegative())
            {
                Result = Negative.Create(Multiplication.Create(left, ((Negative)right).Inner));
                return;
            }

            if (left.Equals(Constant.MinusOne))
            {
                Result = Negative.Create(right);
                return;
            }

            if (right.Equals(Constant.MinusOne))
            {
                Result = Negative.Create(left);
                return;
            }

            if (!left.IsConstant() && right.IsConstant())
            {
                Result = Multiplication.Create(right, left);
                return;
            }

            var flat = operand.FlattenAssociative().ToList();

            if (flat.Where(x => x.IsConstant()).Count() > 1)
            {
                var constants = flat.Where(x => x.IsConstant()).OfType<Constant>();
                Operand @const = constants.Aggregate((seed, x) => (Constant)(seed + x));
                Result = flat.Where(x => !x.IsConstant()).Aggregate(@const, (seed, x) => Multiplication.Create(seed, x));
                return;
            }

            var groups = flat.GroupBy(x => x).ToList();
            if (groups.Any(x => x.Count() > 1))
            {
                Result = groups.Aggregate((Operand)Constant.One, (seed, group) => Multiplication.Create(seed, Power.Create(group.Key, group.Count())));
                return;
            }

            Result = operand;
        }

        public void Visit(Division operand)
        {
            var left = operand.Left;
            var right = operand.Right;

            if (left.Equals(right))
            {
                Result = Constant.One;
                return;
            }

            if (left is Fraction && right is Fraction)
            {
                Result = ((Fraction)left) / ((Fraction)right);
                return;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                var l = (Constant)left;
                var r = (Constant)right;

                if (l.IsInteger() && r.IsInteger())
                {
                    Result = Fraction.Create(l, r);
                    return;
                }

                Result = l.Value / r.Value;
                return;
            }

            if (left.IsConstant() && right is Fraction)
            {
                Result = ((Constant)left).Value / (Fraction)right;
                return;
            }

            if (left is Fraction && right.IsConstant())
            {
                Result = ((Fraction)left) / ((Constant)right).Value;
                return;
            }

            if (right.Equals(Constant.One))
            {
                Result = left;
                return;
            }

            if (left.IsNegative() && right.IsNegative())
            {
                Result = Division.Create(((Negative)left).Inner, ((Negative)right).Inner);
                return;
            }

            if (left.IsNegative())
            {
                Result = Negative.Create(Division.Create(((Negative)left).Inner, right));
                return;
            }

            if (right.IsNegative())
            {
                Result = Negative.Create(Division.Create(left, ((Negative)right).Inner));
                return;
            }

            if (left is Division && right is Division)
            {
                Result = Division.Create(
                    Multiplication.Create(((Division)left).Left, ((Division)right).Right),
                    Multiplication.Create(((Division)left).Right, ((Division)right).Left));
                return;
            }

            if (left is Division)
            {
                Result = Division.Create(
                    ((Division)left).Left,
                    Multiplication.Create(((Division)left).Right, right));
                return;
            }

            if (right is Division)
            {
                Result = Division.Create(
                    Multiplication.Create(left, ((Division)right).Right),
                    ((Division)right).Left);
                return;
            }

            Result = operand;
        }

        public void Visit(Power operand)
        {
            var left = operand.Left;
            var right = operand.Right;

            if (left.Equals(Constant.One) || right.Equals(Constant.Zero))
            {
                Result = 1;
                return;
            }

            if (right.Equals(Constant.One))
            {
                Result = left;
                return;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                Result = (Constant)left ^ (Constant)right;
                return;
            }

            Result = operand;
        }

        public void Visit(Root operand)
        {
            if (operand.Exponent == 1)
            {
                Result = operand.Inner;
                return;
            }

            Result = operand;
        }

        public void Visit(Factorial operand)
        {
            Result = operand;
        }

        public void Visit(Sine operand)
        {
            Result = operand;
        }

        public void Visit(Cosine operand)
        {
            Result = operand;
        }

        public void Visit(Tangent operand)
        {
            Result = operand;
        }

        public void Visit(Exponential operand)
        {
            Result = operand;
        }

        public void Visit(Logarithm operand)
        {
            Result = operand;
        }

        public void Visit(LogarithmBase operand)
        {
            Result = operand;
        }

        public void Visit(Negative operand)
        {
            var inner = operand.Inner;

            if (inner.IsConstant())
            {
                Result = Constant.Create(-((Constant)inner).Value);
                return;
            }

            Result = operand;
        }

        public void Visit(Fraction operand)
        {
            var numerator = operand.Numerator;
            var denominator = operand.Denominator;

            if (denominator < 0)
            {
                Result = Fraction.Create(-numerator, -denominator); // flip sign on both 1/-x -> -1/x and -1/-x -> 1/x
                return;
            }

            var gcd = GreatestCommonDivisor.Euclid(Math.Abs(numerator), Math.Abs(denominator));

            if (gcd > 1)
            {
                Result = Fraction.Create(numerator / gcd, denominator / gcd);
                return;
            }

            if (denominator == 1)
            {
                Result = numerator;
                return;
            }

            Result = operand;
        }

        public void Visit(Minimum operand)
        {
            var left = operand.Left;
            var right = operand.Right;

            if (left.Equals(right))
            {
                Result = left;
                return;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                Result = Math.Min((Constant)left, (Constant)right);
                return;
            }

            Result = operand;        }

        public void Visit(Maximum operand)
        {
            var left = operand.Left;
            var right = operand.Right;

            if (left.Equals(right))
            {
                Result = left;
                return;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                Result = Math.Max((Constant)left, (Constant)right);
                return;
            }

            Result = operand;
        }
    }
}