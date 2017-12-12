using System;
using System.Linq;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class ReduceOperandVisitor : IOperandVisitor<Operand>
    {
        private Operand Reduce(Operand operand)
        {
            var visitor = new ReduceOperandVisitor();
            var result = operand.Accept(visitor);

            if (!operand.Equals(result))
            {
                return result;
            }

            return operand;
        }

        public Operand Visit(Function operand)
        {
            return Function.Create(operand.Identifier, Reduce(operand.Operand));
        }

        public Operand Visit(FunctionReference operand)
        {
            return FunctionReference.Create(operand.Identifier, operand.Parameters.Select(x => Reduce(x)));
        }

        public Operand Visit(Constant operand)
        {
            return operand;
        }

        public Operand Visit(NamedConstant operand)
        {
            return operand;
        }

        public Operand Visit(Variable operand)
        {
            return operand;
        }

        public Operand Visit(Addition operand)
        {
            var left = Reduce(operand.Left);
            var right = Reduce(operand.Right);

            if (left.Equals(right))
            {
                return Reduce(Multiplication.Create(2, left));
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
                return Reduce((Constant)left + (Constant)right);
            }

            if (right.IsNegative())
            {
                return Reduce(Subtraction.Create(left, ((Negative)right).Inner));
            }

            if (right.IsConstant() && ((Constant)right).Value < 0)
            {
                return Reduce(Subtraction.Create(left, -((Constant)right).Value));
            }

            var flat = operand.FlattenAssociative().ToList();

            if (flat.Where(x => x.IsConstant()).Count() > 1)
            {
                var constants = flat.Where(x => x.IsConstant()).OfType<Constant>();
                Operand @const = constants.Aggregate((seed, x) => (Constant)(seed + x));
                return Reduce(flat.Where(x => !x.IsConstant()).Aggregate(@const, (seed, x) => Addition.Create(seed, x)));
            }

            var groups = flat.GroupBy(x => x).ToList();

            if (groups.Any(x => x.Count() > 1))
            {
                return Reduce(groups.Aggregate((Operand)Constant.Zero, (seed, group) => Addition.Create(seed, Multiplication.Create(group.Count(), group.Key))));
            }

            return Addition.Create(left, right);
        }

        public Operand Visit(Subtraction operand)
        {
            var left = Reduce(operand.Left);
            var right = Reduce(operand.Right);

            if (left.Equals(right))
            {
                return Constant.Zero;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return Reduce((Constant)left - (Constant)right);
            }

            if (left.Equals(Constant.Zero))
            {
                return Reduce(Negative.Create(right));
            }

            if (right.IsNegative())
            {
                return Reduce(Addition.Create(left, ((Negative)right).Inner));
            }

            if (right.IsConstant() && ((Constant)right).Value < 0)
            {
                return Reduce(Addition.Create(left, -((Constant)right).Value));
            }

            return Subtraction.Create(left, right);
        }

        public Operand Visit(Multiplication operand)
        {
            var left = Reduce(operand.Left);
            var right = Reduce(operand.Right);

            if (left is Division && right is Division)
            {
                var l = (Division)left;
                var r = (Division)right;

                return Reduce(Division.Create(
                    Multiplication.Create(l.Left, r.Left),
                    Multiplication.Create(l.Right, r.Right)
                ));
            }

            if (left is Division)
            {
                var l = (Division)left;

                return Reduce(Division.Create(
                    Multiplication.Create(l.Left, right),
                    l.Right
                ));
            }

            if (right is Division)
            {
                var r = (Division)right;

                return Reduce(Division.Create(
                    Multiplication.Create(left, r.Left),
                    r.Right
                ));
            }

            if (left.Equals(right))
            {
                return Reduce(Power.Create(left, 2));
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return Reduce((Constant)left * (Constant)right);
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
                return Reduce(Multiplication.Create(((Negative)left).Inner, ((Negative)right).Inner));
            }

            if (left.IsNegative())
            {
                return Reduce(Negative.Create(Multiplication.Create(((Negative)left).Inner, right)));
            }

            if (right.IsNegative())
            {
                return Reduce(Negative.Create(Multiplication.Create(left, ((Negative)right).Inner)));
            }

            if (left.Equals(Constant.MinusOne))
            {
                return Reduce(Negative.Create(right));
            }

            if (right.Equals(Constant.MinusOne))
            {
                return Reduce(Negative.Create(left));
            }

            if (!left.IsConstant() && right.IsConstant())
            {
                return Reduce(Multiplication.Create(right, left));
            }

            var flat = operand.FlattenAssociative().ToList();

            if (flat.Where(x => x.IsConstant()).Count() > 1)
            {
                var constants = flat.Where(x => x.IsConstant()).OfType<Constant>();
                Operand @const = constants.Aggregate((seed, x) => (Constant)(seed + x));
                return Reduce(flat.Where(x => !x.IsConstant()).Aggregate(@const, (seed, x) => Multiplication.Create(seed, x)));
            }

            var groups = flat.GroupBy(x => x).ToList();
            if (groups.Any(x => x.Count() > 1))
            {
                return Reduce(groups.Aggregate((Operand)Constant.One, (seed, group) => Multiplication.Create(seed, Power.Create(group.Key, group.Count()))));
            }

            return Multiplication.Create(left, right);
        }

        public Operand Visit(Division operand)
        {
            var left = Reduce(operand.Left);
            var right = Reduce(operand.Right);

            if (left.Equals(right))
            {
                return Constant.One;
            }

            if (left is Fraction && right is Fraction)
            {
                return Reduce(((Fraction)left) / ((Fraction)right));
            }

            if (left.IsConstant() && right.IsConstant())
            {
                var l = (Constant)left;
                var r = (Constant)right;

                if (l.IsInteger() && r.IsInteger())
                {
                    return Reduce(Fraction.Create(l, r));
                }

                return Reduce(l.Value / r.Value);
            }

            if (left.IsConstant() && right is Fraction)
            {
                return Reduce(((Constant)left).Value / (Fraction)right);
            }

            if (left is Fraction && right.IsConstant())
            {
                return Reduce(((Fraction)left) / ((Constant)right).Value);
            }

            if (right.Equals(Constant.One))
            {
                return left;
            }

            if (left.IsNegative() && right.IsNegative())
            {
                return Reduce(Division.Create(((Negative)left).Inner, ((Negative)right).Inner));
            }

            if (left.IsNegative())
            {
                return Reduce(Negative.Create(Division.Create(((Negative)left).Inner, right)));
            }

            if (right.IsNegative())
            {
                return Reduce(Negative.Create(Division.Create(left, ((Negative)right).Inner)));
            }

            if (left is Division && right is Division)
            {
                return Reduce(Division.Create(
                    Multiplication.Create(((Division)left).Left, ((Division)right).Right),
                    Multiplication.Create(((Division)left).Right, ((Division)right).Left)));
            }

            if (left is Division)
            {
                return Reduce(Division.Create(
                    ((Division)left).Left,
                    Multiplication.Create(((Division)left).Right, right)));
            }

            if (right is Division)
            {
                return Reduce(Division.Create(
                    Multiplication.Create(left, ((Division)right).Right),
                    ((Division)right).Left));
            }

            return Division.Create(left, right);
        }

        public Operand Visit(Power operand)
        {
            var left = Reduce(operand.Left);
            var right = Reduce(operand.Right);

            if (left.Equals(Constant.One) || right.Equals(Constant.Zero))
            {
                return Constant.One;
            }

            if (right.Equals(Constant.One))
            {
                return left;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return Reduce((Constant)left ^ (Constant)right);
            }

            return Power.Create(left, right);
        }

        public Operand Visit(Root operand)
        {
            var inner = Reduce(operand.Inner);

            if (operand.Exponent == 1)
            {
                return inner;
            }

            return Root.Create(operand.Exponent, inner);
        }

        public Operand Visit(Factorial operand)
        {
            return Factorial.Create(Reduce(operand.Inner));
        }

        public Operand Visit(Sine operand)
        {
            return Sine.Create(Reduce(operand.Inner));
        }

        public Operand Visit(Cosine operand)
        {
            return Cosine.Create(Reduce(operand.Inner));
        }

        public Operand Visit(Tangent operand)
        {
            return Tangent.Create(Reduce(operand.Inner));
        }

        public Operand Visit(Exponential operand)
        {
            return Exponential.Create(Reduce(operand.Inner));
        }

        public Operand Visit(Logarithm operand)
        {
            return Logarithm.Create(Reduce(operand.Inner));
        }

        public Operand Visit(LogarithmBase operand)
        {
            return LogarithmBase.Create(operand.Base, Reduce(operand.Inner));
        }

        public Operand Visit(Negative operand)
        {
            var inner = Reduce(operand.Inner);

            if (inner.IsConstant())
            {
                return Constant.Create(-((Constant)inner).Value);
            }

            return Negative.Create(inner);
        }

        public Operand Visit(Fraction operand)
        {
            var numerator = operand.Numerator;
            var denominator = operand.Denominator;

            if (denominator < 0)
            {
                return Reduce(Fraction.Create(-numerator, -denominator)); // flip sign on both 1/-x -> -1/x and -1/-x -> 1/x
            }

            var gcd = GreatestCommonDivisor.Euclid(Math.Abs(numerator), Math.Abs(denominator));

            if (gcd > 1)
            {
                return Reduce(Fraction.Create(numerator / gcd, denominator / gcd));
            }

            if (denominator == 1)
            {
                return numerator;
            }

            return operand;
        }

        public Operand Visit(Minimum operand)
        {
            var left = Reduce(operand.Left);
            var right = Reduce(operand.Right);

            if (left.Equals(right))
            {
                return left;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return Math.Min((Constant)left, (Constant)right);
            }

            return Minimum.Create(left, right);
        }

        public Operand Visit(Maximum operand)
        {
            var left = Reduce(operand.Left);
            var right = Reduce(operand.Right);

            if (left.Equals(right))
            {
                return left;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return Math.Max((Constant)left, (Constant)right);
            }

            return Maximum.Create(left, right);
        }
    }
}