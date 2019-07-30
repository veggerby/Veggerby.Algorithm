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

        public Operand Visit(Function operand) => Function.Create(operand.Identifier, Reduce(operand.Operand));

        public Operand Visit(FunctionReference operand) => FunctionReference.Create(operand.Identifier, operand.Parameters.Select(x => Reduce(x)));

        public Operand Visit(ValueConstant operand) => operand;

        public Operand Visit(NamedConstant operand) => operand;

        public Operand Visit(UnspecifiedConstant operand) => operand;

        public Operand Visit(Variable operand) => operand;

        public Operand Visit(Addition operand)
        {
            var operands = operand
                .Operands
                .Select(x => Reduce(x))
                .Where(x => !x.Equals(ValueConstant.Zero))
                .GroupBy(x => x)
                .Select(x => x.Count() > 1 ? Multiplication.Create(x.Count(), x.Key) : x.Key)
                .OrderBy(x => x, new CommutativeOperationComparer())
                .ToList();

            // combine constants into one operand
            if (operands.Count(x => x.IsConstant()) > 1)
            {
                var constants = operands.Where(x => x.IsConstant()).Cast<ValueConstant>();
                var constant = constants.Aggregate((seed, next) => (ValueConstant)(seed + next));
                operands = new Operand[] { constant }.Concat(operands.Where(x => !x.IsConstant())).ToList();
            }

            if (operands.Any(x => x.IsNegative()))
            {
                return Reduce(Subtraction.Create(
                    Addition.Create(operands.Where(x => !x.IsNegative())),
                    Addition.Create(operands.OfType<Negative>().Select(x => x.Inner))
                ));
            }

            // consolidate addition and move substraction "out", e.g. c + (c - cos(x)) = (c + c) - cos(x)
            if (operands.Any(x => x is Subtraction))
            {
                var addition = Addition.Create(operands.Select(x => x is Subtraction ? ((Subtraction)x).Left : x));
                var substractions = operands.OfType<Subtraction>().Aggregate(addition, (seed, next) => Subtraction.Create(seed, next.Right));
                return Reduce(substractions);
            }

            return Addition.Create(operands);
        }

        public Operand Visit(Subtraction operand)
        {
            var left = Reduce(operand.Left);
            var right = Reduce(operand.Right);

            if (left.Equals(right))
            {
                return ValueConstant.Zero;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return Reduce((ValueConstant)left - (ValueConstant)right);
            }

            if (left.Equals(ValueConstant.Zero))
            {
                return Reduce(Negative.Create(right));
            }

            if (right is Subtraction) // x-(y-z) = (x+z)-y
            {
                var rightSubstraction = (Subtraction)right;
                return Reduce(Subtraction.Create(
                    Addition.Create(left, rightSubstraction.Right),
                    rightSubstraction.Left));
            }

            if (right.IsNegative())
            {
                return Reduce(Addition.Create(left, ((Negative)right).Inner));
            }

            if (right.IsConstant() && ((ValueConstant)right).Value < 0)
            {
                return Reduce(Addition.Create(left, -((ValueConstant)right).Value));
            }

            return Subtraction.Create(left, right);
        }

        public Operand Visit(Multiplication operand)
        {
            var operands = operand
                .Operands
                .Select(x => Reduce(x))
                .Where(x => !x.Equals(ValueConstant.One))
                .GroupBy(x => x)
                .Select(x => x.Count() > 1 ? Power.Create(x.Key, x.Count()) : x.Key)
                .OrderBy(x => x, new CommutativeOperationComparer())
                .ToList();

            if (operands.Any(x => x.Equals(ValueConstant.Zero)))
            {
                return ValueConstant.Zero;
            }

            // combine constants into one operand
            if (operands.Count(x => x.IsConstant()) > 1)
            {
                var constants = operands.Where(x => x.IsConstant()).Cast<ValueConstant>();
                var constant = constants.Aggregate((seed, next) => (ValueConstant)(seed * next));
                operands = new Operand[] { constant }.Concat(operands.Where(x => !x.IsConstant())).ToList();
            }

            var negativeCount = operands.Count(x => x.IsNegative());
            if (negativeCount > 0)
            {
                operands = operands.Select(x => x.IsNegative() ? ((Negative)x).Inner : x).ToList();
            }

            if (negativeCount % 2 == 1)
            {
                return Reduce(Negative.Create(Multiplication.Create(operands)));
            }

            // consolidate addition and move substraction "out", e.g. c + (c - cos(x)) = (c + c) - cos(x)
            if (operands.Any(x => x is Division))
            {
                var multiplication = Multiplication.Create(operands.Select(x => x is Division ? ((Division)x).Left : x));
                var divisions = operands.OfType<Division>().Aggregate(multiplication, (seed, next) => Division.Create(seed, next.Right));
                return Reduce(divisions);
            }

            return Multiplication.Create(operands);
        }

        public Operand Visit(Division operand)
        {
            var left = Reduce(operand.Left);
            var right = Reduce(operand.Right);

            if (left.Equals(right))
            {
                return ValueConstant.One;
            }

            if (left is Fraction && right is Fraction)
            {
                return Reduce(((Fraction)left) / ((Fraction)right));
            }

            if (left.IsConstant() && right.IsConstant())
            {
                var l = (ValueConstant)left;
                var r = (ValueConstant)right;

                if (l.IsInteger() && r.IsInteger())
                {
                    return Reduce(Fraction.Create(l, r));
                }

                return Reduce(l.Value / r.Value);
            }

            if (left.IsConstant() && right is Fraction)
            {
                return Reduce(((ValueConstant)left).Value / (Fraction)right);
            }

            if (left is Fraction && right.IsConstant())
            {
                return Reduce(((Fraction)left) / ((ValueConstant)right).Value);
            }

            if (right.Equals(ValueConstant.One))
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

            if (left.Equals(ValueConstant.One) || right.Equals(ValueConstant.Zero))
            {
                return ValueConstant.One;
            }

            if (right.Equals(ValueConstant.One))
            {
                return left;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return Reduce((ValueConstant)left ^ (ValueConstant)right);
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

        public Operand Visit(Factorial operand) => Factorial.Create(Reduce(operand.Inner));

        public Operand Visit(Sine operand) => Sine.Create(Reduce(operand.Inner));

        public Operand Visit(Cosine operand) => Cosine.Create(Reduce(operand.Inner));

        public Operand Visit(Tangent operand) => Tangent.Create(Reduce(operand.Inner));

        public Operand Visit(Exponential operand) => Exponential.Create(Reduce(operand.Inner));

        public Operand Visit(Logarithm operand) => Logarithm.Create(Reduce(operand.Inner));

        public Operand Visit(LogarithmBase operand) => LogarithmBase.Create(operand.Base, Reduce(operand.Inner));

        public Operand Visit(Negative operand)
        {
            var inner = Reduce(operand.Inner);

            if (inner.IsConstant())
            {
                return ValueConstant.Create(-((ValueConstant)inner).Value);
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
            var reduced = operand
                .Operands
                .Select(x => Reduce(x))
                .GroupBy(x => x)
                .Select(x => x.Key)
                .ToList();

            if (reduced.Count() == 1)
            {
                return reduced.Single();
            }

            if (reduced.All(x => x.IsConstant()))
            {
                return reduced.Aggregate(double.MaxValue, (seed, next) => Math.Min(seed, (ValueConstant)next));
            }

            return Minimum.Create(reduced);
        }

        public Operand Visit(Maximum operand)
        {
            var reduced = operand
                .Operands
                .Select(x => Reduce(x))
                .GroupBy(x => x)
                .Select(x => x.Key)
                .ToList();

            if (reduced.Count() == 1)
            {
                return reduced.Single();
            }

            if (reduced.All(x => x.IsConstant()))
            {
                return reduced.Aggregate(double.MinValue, (seed, next) => Math.Max(seed, (ValueConstant)next));
            }

            return Maximum.Create(reduced);
        }
    }
}