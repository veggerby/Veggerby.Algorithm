namespace Veggerby.Algorithm.Calculus.Visitors;

public class IntegralOperandVisitor : IOperandVisitor<Operand>
{
    private readonly Variable _variable;

    private readonly IEnumerable<Operand> _stack;

    private static readonly Variable _constant = Variable.Create("c");

    public IntegralOperandVisitor(Variable variable, IEnumerable<Operand> stack = null)
    {
        if (stack is not null && stack.Count() > 25) // abort when too deep
        {
            throw new ArgumentOutOfRangeException(nameof(stack), "Too many iterations");
        }

        _variable = variable;
        _stack = stack?.ToList() ?? new List<Operand>();
    }

    private Operand GetIntegral(Operand operand)
    {
        if (_stack.Contains(operand)) // abort when too deep
        {
            throw new ArgumentException("Circular call", nameof(operand));
        }

        var visitor = new IntegralOperandVisitor(_variable, _stack.Concat([operand]));

        return operand
            .Reduce()?
            .Accept(visitor)?
            .Reduce();
    }

    private Operand GetDerivative(Operand operand)
    {
        var visitor = new DerivativeOperandVisitor(_variable);
        return operand.Reduce()?.Accept(visitor)?.Reduce();
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

            if (leftDerivative is null || rightIntegral is null)
            {
                return null;
            }

            var rightOperation = Multiplication.Create(leftDerivative, rightIntegral);

            var rightPart = GetIntegral(rightOperation);

            if (rightPart is null)
            {
                return null;
            }

            return Subtraction.Create(
                Multiplication.Create(left, rightIntegral),
                rightPart
            );
        }
        catch (ArgumentException)
        {
            return null;
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

    public Operand Visit(Function operand)
    {
        var innerOperand = GetIntegral(operand.Operand);
        return Function.Create(operand.Identifier.ToUpperInvariant(), innerOperand);
    }

    public Operand Visit(FunctionReference operand) => null;

    public Operand Visit(Variable operand) => operand.Equals(_variable)
            ? PowerRule(1)
            : ConstantRule(operand);

    public Operand Visit(Subtraction operand)
    {
        var left = GetIntegral(operand.Left);
        var right = GetIntegral(operand.Right);

        return left is not null && right is not null
            ? Subtraction.Create(left, right)
            : null;
    }

    public Operand Visit(Division operand)
    {
        if (operand.Left.IsConstant() && operand.Right.Equals(_variable))
        {
            return Multiplication.Create(
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

    public Operand Visit(Factorial operand) => null;

    public Operand Visit(Cosine operand)
    {
        if (operand.Inner.Equals(_variable))
        {
            return Addition.Create(
                Sine.Create(_variable),
                _constant
            );
        }
        else
        {
            throw new NotSupportedException();
        }
    }

    public Operand Visit(Exponential operand)
    {
        if (operand.Inner.Equals(_variable))
        {
            return Addition.Create(
                Exponential.Create(_variable),
                _constant
            );
        }
        else
        {
            throw new NotSupportedException();
        }
    }

    public Operand Visit(LogarithmBase operand) => throw new NotImplementedException();

    public Operand Visit(Negative operand)
    {
        var inner = GetIntegral(operand.Inner);

        if (inner is null)
        {
            return null;
        }

        return Negative.Create(inner);
    }

    public Operand Visit(Logarithm operand)
    {
        if (operand.Inner.Equals(_variable))
        {
            return Addition.Create(
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

    public Operand Visit(Tangent operand) => throw new NotImplementedException();

    public Operand Visit(Sine operand)
    {
        if (operand.Inner.Equals(_variable))
        {
            return Addition.Create(
                Negative.Create(Cosine.Create(_variable)),
                _constant
            );
        }
        else
        {
            throw new NotSupportedException();
        }
    }

    public Operand Visit(Power operand)
    {
        if (operand.Left.Equals(_variable) && operand.Right.IsConstant() && operand.Right != Constant.MinusOne)
        {
            return PowerRule(((ValueConstant)operand.Right).Value);
        }
        else
        {
            throw new NotSupportedException();
        }
    }

    public Operand Visit(Root operand)
    {
        if (operand.Inner.Equals(_variable))
        {
            return PowerRule(1D / operand.Exponent);
        }
        else
        {
            throw new NotSupportedException();
        }
    }

    public Operand Visit(Multiplication operand)
    {
        var parts = operand
            .Operands
            .Select(x => new { Left = Multiplication.Create(operand.Operands.Where(y => !ReferenceEquals(x, y))), Right = x })
            .Distinct()
            .ToList();

        Operand simplest = null;
        int complexity = int.MaxValue;

        foreach (var part in parts)
        {
            var result = IntegrationByParts(part.Left, part.Right);
            if (result is null)
            {
                continue;
            }

            var c = result.GetComplexity();

            if (c < complexity)
            {
                complexity = c;
                simplest = result;
            }
        }

        return simplest;
    }

    public Operand Visit(Addition operand)
    {
        var operands = operand
            .Operands
            .Select(x => GetIntegral(x))
            .ToList();

        if (operands.Any(x => x is null))
        {
            return null;
        }

        return Addition.Create(operands);
    }

    public Operand Visit(NamedConstant operand) => ConstantRule(operand);

    public Operand Visit(ValueConstant operand) => ConstantRule(operand);

    public Operand Visit(UnspecifiedConstant operand) => ConstantRule(operand);

    public Operand Visit(Fraction operand) => ConstantRule(operand);

    public Operand Visit(Minimum operand) => null;

    public Operand Visit(Maximum operand) => null;
}