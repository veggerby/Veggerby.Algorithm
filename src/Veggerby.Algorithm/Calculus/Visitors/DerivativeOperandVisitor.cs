namespace Veggerby.Algorithm.Calculus.Visitors;

public class DerivativeOperandVisitor(Variable variable) : IOperandVisitor<Operand>
{
    private readonly Variable _variable = variable;

    private Operand GetDerivative(Operand operand) => operand.Accept(this);

    public Operand Visit(Function operand)
    {
        var innerOperand = GetDerivative(operand.Operand);
        return Function.Create($"{operand.Identifier}'", innerOperand);
    }

    public Operand Visit(FunctionReference operand) => null;

    public Operand Visit(Variable operand) => operand.Equals(_variable) ? 1 : 0;

    public Operand Visit(Subtraction operand)
    {
        var left = GetDerivative(operand.Left);
        var right = GetDerivative(operand.Right);

        return left is not null && right is not null
            ? Subtraction.Create(left, right)
            : null;
    }

    public Operand Visit(Division operand)
    {
        var left = GetDerivative(operand.Left);
        var right = GetDerivative(operand.Right);

        return left is not null && right is not null
            ? Division.Create(
                Subtraction.Create(
                    Multiplication.Create(left, operand.Right),
                    Multiplication.Create(right, operand.Left)),
                Power.Create(operand.Right, 2))
            : null;
    }

    public Operand Visit(Factorial operand) => null;

    public Operand Visit(Cosine operand)
    {
        var inner = GetDerivative(operand.Inner);

        if (inner is null)
        {
            return null;
        }

        // chain rule
        return Multiplication.Create(Negative.Create(inner), Sine.Create(operand.Inner));
    }

    public Operand Visit(Exponential operand)
    {
        var inner = GetDerivative(operand.Inner);

        if (inner is null)
        {
            return null;
        }

        // chain rule
        return Multiplication.Create(inner, Exponential.Create(operand.Inner));
    }

    public Operand Visit(LogarithmBase operand)
    {
        var inner = GetDerivative(operand.Inner);

        if (inner is null)
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

        if (inner is null)
        {
            return null;
        }

        return Negative.Create(inner);
    }

    public Operand Visit(Logarithm operand)
    {
        var inner = GetDerivative(operand.Inner);

        if (inner is null)
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

        if (inner is null)
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

        if (inner is null)
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
        var operandLeft = operand.Operands.First();
        var operandRight = Multiplication.Create(operand.Operands.Skip(1));

        var left = GetDerivative(operandLeft);
        var right = GetDerivative(operandRight);

        return left is not null && right is not null
            ? Addition.Create(
                Multiplication.Create(left, operandRight),
                Multiplication.Create(right, operandLeft))
            : null;
    }

    public Operand Visit(Addition operand)
    {
        var operandLeft = operand.Operands.First();
        var operandRight = Addition.Create(operand.Operands.Skip(1));

        var left = GetDerivative(operandLeft);
        var right = GetDerivative(operandRight);

        return left is not null && right is not null
            ? Addition.Create(left, right)
            : null;
    }

    public Operand Visit(NamedConstant operand) => Constant.Zero;

    public Operand Visit(ValueConstant operand) => Constant.Zero;

    public Operand Visit(UnspecifiedConstant operand) => Constant.Zero;

    public Operand Visit(Fraction operand) => Constant.Zero;

    public Operand Visit(Minimum operand) => null;

    public Operand Visit(Maximum operand) => null;
}