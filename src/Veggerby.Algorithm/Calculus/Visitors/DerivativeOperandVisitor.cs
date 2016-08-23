using System;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class DerivativeOperandVisitor : IOperandVisitor
    {
        private readonly Variable _variable;

        public Operand Result { get; private set; }

        public DerivativeOperandVisitor(Variable variable)
        {
            _variable = variable;
        }

        private Operand GetDerivative(Operand operand)
        {
            var visitor = new DerivativeOperandVisitor(_variable);
            operand.Accept(visitor);
            return visitor.Result;
        }

        public void Visit(Variable operand)
        {
            Result = operand.Equals(_variable) ? 1 : 0;
        }

        public void Visit(Subtraction operand)
        {
            var left = GetDerivative(operand.Left);
            var right = GetDerivative(operand.Right);

            Result = left != null && right != null
                ? Subtraction.Create(left, right)
                : null;
        }

        public void Visit(Division operand)
        {
            var left = GetDerivative(operand.Left);
            var right = GetDerivative(operand.Right);

            Result = left != null && right != null
                ? Division.Create(
                    Subtraction.Create(
                        Multiplication.Create(left, operand.Right),
                        Multiplication.Create(right, operand.Left)),
                    Power.Create(operand.Right, 2))
                : null;
        }

        public void Visit(Factorial operand)
        {
            Result = null;
        }

        public void Visit(Cosine operand)
        {
            var inner = GetDerivative(operand.Inner);

            if (inner == null)
            {
                Result = null;
            }
            else
            {
            // chain rule
                Result = Multiplication.Create(Negative.Create(inner), Sine.Create(operand.Inner));
            }
        }

        public void Visit(Exponential operand)
        {
            var inner = GetDerivative(operand.Inner);

            if (inner == null)
            {
                Result = null;
            }
            else
            {
            // chain rule
                Result = Multiplication.Create(inner, Exponential.Create(operand.Inner));
            }
        }

        public void Visit(LogarithmBase operand)
        {
            var inner = GetDerivative(operand.Inner);

            if (inner == null)
            {
                Result = null;
            }
            else
            {
                Result = Division.Create(
                    inner,
                    Multiplication.Create(Logarithm.Create(operand.Base), operand.Inner));
            }
        }

        public void Visit(Negative operand)
        {
            var inner = GetDerivative(operand.Inner);

            if (inner == null)
            {
                Result = null;
            }
            else
            {
                Result = Negative.Create(inner);
            }
        }

        public void Visit(Logarithm operand)
        {
            var inner = GetDerivative(operand.Inner);

            if (inner == null)
            {
                Result = null;
            }
            else
            {
                Result = Division.Create(inner, operand.Inner);
            }
        }

        public void Visit(Tangent operand)
        {
            throw new NotImplementedException();
        }

        public void Visit(Sine operand)
        {
            var inner = GetDerivative(operand.Inner);

            if (inner == null)
            {
                Result = null;
            }
            else
            {
                Result = Multiplication.Create(inner, Cosine.Create(operand.Inner));
            }
        }

        public void Visit(Power operand)
        {
            if (operand.Right.IsConstant())
            {
                Result = Multiplication.Create(
                    operand.Right,
                    Power.Create(
                        operand.Left,
                        Subtraction.Create(operand.Right, 1)
                    ));
            }
            else
            {
                // exponential rule
                Result = Exponential.Create(
                    Multiplication.Create(operand.Right, Logarithm.Create(operand.Left)));
            }
        }

        public void Visit(Root operand)
        {
            var inner = GetDerivative(operand.Inner);

            if (inner == null)
            {
                Result = null;
            }
            else
            {
                Result = Multiplication.Create(
                    Division.Create(
                        Power.Create(operand.Inner, (Constant.One - operand.Exponent) / operand.Exponent),
                        operand.Exponent),
                    inner
                );
            }
        }

        public void Visit(Multiplication operand)
        {
            var left = GetDerivative(operand.Left);
            var right = GetDerivative(operand.Right);

            Result = left != null && right != null
                ?Addition.Create(
                    Multiplication.Create(left, operand.Right),
                    Multiplication.Create(right, operand.Left))
                : null;
        }

        public void Visit(Addition operand)
        {
            var left = GetDerivative(operand.Left);
            var right = GetDerivative(operand.Right);

            Result = left != null && right != null
                ? Addition.Create(left, right)
                : null;
        }

        public void Visit(NamedConstant operand)
        {
            Result = 0;
        }

        public void Visit(Constant operand)
        {
            Result = 0;
        }

        public void Visit(Fraction operand)
        {
            Result = 0;
        }
    }
}