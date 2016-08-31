using System;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class IntegralOperandVisitor : IOperandVisitor
    {
        private readonly Variable _variable;

        private static readonly Variable _constant = Variable.Create("c");

        public Operand Result { get; private set; }

        public IntegralOperandVisitor(Variable variable)
        {
            _variable = variable;
        }

        private Operand GetIntegral(Operand operand)
        {
            var visitor = new IntegralOperandVisitor(_variable);
            operand.Accept(visitor);
            return visitor.Result;
        }

        private Operand GetDerivative(Operand operand)
        {
            var visitor = new DerivativeOperandVisitor(_variable);
            operand.Accept(visitor);
            return visitor.Result;
        }

        private Operand ConstantRule(Operand constant)
        {
            return Addition.Create(
                Multiplication.Create(constant, _variable),
                _constant
            );
        }

        private Operand PowerRule(double power)
        {
            return Addition.Create(
                    Division.Create(
                        Power.Create(_variable, power + 1),
                        power + 1),
                    _constant);
        }

        private Operand IntegrationByParts(Operand left, Operand right)
        {
            try
            {
                var leftDerivative = GetDerivative(left);
                var rightIntegral = GetIntegral(right);

                if (leftDerivative == null || rightIntegral == null)
                {
                    return null;
                }

                var rightPart = GetIntegral(
                    Multiplication.Create(leftDerivative, rightIntegral)
                );

                if (rightPart == null)
                {
                    return null;
                }

                return Subtraction.Create(
                    Multiplication.Create(left, rightIntegral),
                    rightPart
                );
            }
            catch (NotImplementedException)
            {
                return null;
            }
            catch (NotSupportedException)
            {
                return null;
            }
        }

        public void Visit(Variable operand)
        {
            Result = operand.Equals(_variable)
                ? PowerRule(1)
                : ConstantRule(operand);
        }

        public void Visit(Subtraction operand)
        {
            var left = GetIntegral(operand.Left);
            var right = GetIntegral(operand.Right);

            Result = left != null && right != null
                ? Subtraction.Create(left, right)
                : null;
        }

        public void Visit(Division operand)
        {
            if (operand.Left.IsConstant() && operand.Right.Equals(_variable))
            {
                Result = Multiplication.Create(
                    operand.Left,
                    Addition.Create(
                        Logarithm.Create(_variable),
                        _constant
                    )
                );
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void Visit(Factorial operand)
        {
            Result = null;
        }

        public void Visit(Cosine operand)
        {
            if (operand.Inner.Equals(_variable))
            {
                Result = Addition.Create(
                    Sine.Create(_variable),
                    _constant
                );
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void Visit(Exponential operand)
        {
            if (operand.Inner.Equals(_variable))
            {
                Result = Addition.Create(
                    Exponential.Create(_variable),
                    _constant
                );
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void Visit(LogarithmBase operand)
        {
            throw new NotImplementedException();
        }

        public void Visit(Negative operand)
        {
            var inner = GetIntegral(operand.Inner);

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
            if (operand.Inner.Equals(_variable))
            {
                Result = Addition.Create(
                    Subtraction.Create(
                        Multiplication.Create(_variable, Logarithm.Create(_variable)),
                        _variable),
                    _constant);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void Visit(Tangent operand)
        {
            throw new NotImplementedException();
        }

        public void Visit(Sine operand)
        {
            if (operand.Inner.Equals(_variable))
            {
                Result = Addition.Create(
                    Negative.Create(Cosine.Create(_variable)),
                    _constant
                );
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void Visit(Power operand)
        {
            if (operand.Left.Equals(_variable) && operand.Right.IsConstant() && operand.Right != -1)
            {
                Result = PowerRule(((Constant)operand.Right).Value);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void Visit(Root operand)
        {
            if (operand.Inner.Equals(_variable))
            {
                Result = PowerRule(1D / operand.Exponent);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void Visit(Multiplication operand)
        {
            Result = IntegrationByParts(operand.Left, operand.Right)
                ?? IntegrationByParts(operand.Right, operand.Left);
        }

        public void Visit(Addition operand)
        {
            var left = GetIntegral(operand.Left);
            var right = GetIntegral(operand.Right);

            Result = left != null && right != null
                ? Addition.Create(left, right)
                : null;
        }

        public void Visit(NamedConstant operand)
        {
            Result = ConstantRule(operand);
        }

        public void Visit(Constant operand)
        {
            Result = ConstantRule(operand);
        }

        public void Visit(Fraction operand)
        {
            Result = ConstantRule(operand);
        }

        public void Visit(Minimum operand)
        {
            Result = null;
        }

        public void Visit(Maximum operand)
        {
            Result = null;
        }
    }
}