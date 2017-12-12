using System;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class DerivativeOperandVisitor : IOperandVisitor<Operand>
    {
        private readonly Variable _variable;

        public DerivativeOperandVisitor(Variable variable)
        {
            _variable = variable;
        }

        private Operand GetDerivative(Operand operand)
        {
            return operand.Accept(this);
        }

        public Operand Visit(Function operand)
        {
            var innerOperand = GetDerivative(operand.Operand);
            return Function.Create($"{operand.Identifier}'", innerOperand);
        }

        public Operand Visit(FunctionReference operand)
        {
            return null;
        }

        public Operand Visit(Variable operand)
        {
            return operand.Equals(_variable) ? 1 : 0;
        }

        public Operand Visit(Subtraction operand)
        {
            var left = GetDerivative(operand.Left);
            var right = GetDerivative(operand.Right);

            return left != null && right != null
                ? Subtraction.Create(left, right)
                : null;
        }

        public Operand Visit(Division operand)
        {
            var left = GetDerivative(operand.Left);
            var right = GetDerivative(operand.Right);

            return left != null && right != null
                ? Division.Create(
                    Subtraction.Create(
                        Multiplication.Create(left, operand.Right),
                        Multiplication.Create(right, operand.Left)),
                    Power.Create(operand.Right, 2))
                : null;
        }

        public Operand Visit(Factorial operand)
        {
            return null;
        }

        public Operand Visit(Cosine operand)
        {
            var inner = GetDerivative(operand.Inner);

            if (inner == null)
            {
                 return null;
            }

            // chain rule
            return Multiplication.Create(Negative.Create(inner), Sine.Create(operand.Inner));
        }

        public Operand Visit(Exponential operand)
        {
            var inner = GetDerivative(operand.Inner);

            if (inner == null)
            {
                return null;
            }

            // chain rule
            return Multiplication.Create(inner, Exponential.Create(operand.Inner));
        }

        public Operand Visit(LogarithmBase operand)
        {
            var inner = GetDerivative(operand.Inner);

            if (inner == null)
            {
                return null;
            }

            return Division.Create(
                inner,
                Multiplication.Create(Logarithm.Create(operand.Base), operand.Inner));
        }

        public Operand Visit(Negative operand)
        {
            var inner = GetDerivative(operand.Inner);

            if (inner == null)
            {
                return null;
            }

            return Negative.Create(inner);
        }

        public Operand Visit(Logarithm operand)
        {
            var inner = GetDerivative(operand.Inner);

            if (inner == null)
            {
                return null;
            }

            return Division.Create(inner, operand.Inner);
        }

        public Operand Visit(Tangent operand)
        {
            throw new NotImplementedException();
        }

        public Operand Visit(Sine operand)
        {
            var inner = GetDerivative(operand.Inner);

            if (inner == null)
            {
                return null;
            }

            return Multiplication.Create(inner, Cosine.Create(operand.Inner));
        }

        public Operand Visit(Power operand)
        {
            if (operand.Right.IsConstant())
            {
                return Multiplication.Create(
                    operand.Right,
                    Power.Create(
                        operand.Left,
                        Subtraction.Create(operand.Right, 1)
                    ));
            }

            // exponential rule
            return Exponential.Create(
                Multiplication.Create(operand.Right, Logarithm.Create(operand.Left)));
        }

        public Operand Visit(Root operand)
        {
            var inner = GetDerivative(operand.Inner);

            if (inner == null)
            {
                return null;
            }

            return Multiplication.Create(
                Division.Create(
                    Power.Create(operand.Inner, (Constant.One - operand.Exponent) / operand.Exponent),
                    operand.Exponent),
                inner
            );
        }

        public Operand Visit(Multiplication operand)
        {
            var left = GetDerivative(operand.Left);
            var right = GetDerivative(operand.Right);

            return left != null && right != null
                ? Addition.Create(
                    Multiplication.Create(left, operand.Right),
                    Multiplication.Create(right, operand.Left))
                : null;
        }

        public Operand Visit(Addition operand)
        {
            var left = GetDerivative(operand.Left);
            var right = GetDerivative(operand.Right);

            return left != null && right != null
                ? Addition.Create(left, right)
                : null;
        }

        public Operand Visit(NamedConstant operand)
        {
            return Constant.Zero;
        }

        public Operand Visit(Constant operand)
        {
            return Constant.Zero;
        }

        public Operand Visit(Fraction operand)
        {
            return Constant.Zero;
        }

        public Operand Visit(Minimum operand)
        {
            return null;
        }

        public Operand Visit(Maximum operand)
        {
            return null;
        }
    }
}