using System.Globalization;

namespace Veggerby.Algorithm.Calculus.Visitors;

public class LaTeXOperandVisitor(OperationContext context = null) : IOperandVisitor<string>
{
    private readonly OperationContext _context = context ?? new OperationContext();

    public string Visit(Function operand)
    {
        var variables = string.Join(", ", operand.Variables.Select(x => x.Identifier));
        var f = operand.Operand.Accept(this);
        return $@"{operand.Identifier}\left({variables}\right) = {f}";
    }

    public string Visit(FunctionReference operand)
    {
        var parameters = string.Join(", ", operand.Parameters.Select(x => x.Accept(this)));
        return $@"{operand.Identifier}\left({parameters}\right)";
    }

    public string Visit(Variable operand) => operand.Identifier;

    public string Visit(Subtraction operand)
    {
        var left = operand.Left.Accept(this);
        var right = operand.Right.Accept(this);
        return $"{{{left}}}-{{{right}}}";
    }

    public string Visit(Division operand)
    {
        var left = operand.Left.Accept(this);
        var right = operand.Right.Accept(this);
        return $@"\frac{{{left}}}{{{right}}}";
    }

    public string Visit(Factorial operand)
    {
        var inner = operand.Inner.Accept(this);
        return $"{{{inner}}}!";
    }

    public string Visit(Cosine operand)
    {
        var inner = operand.Inner.Accept(this);
        return $@"\cos\left({inner}\right)";
    }

    public string Visit(Exponential operand)
    {
        var inner = operand.Inner.Accept(this);
        return $"e^{{{inner}}}";
    }

    public string Visit(LogarithmBase operand)
    {
        var inner = operand.Inner.Accept(this);

        if (operand.Base == 10)
        {
            return $@"\log\left({inner}\right)";
        }

        return $@"\log_{operand.Base}\left({inner}\right)";
    }

    public string Visit(Negative operand)
    {
        var inner = operand.Inner.Accept(this);
        return $"-{inner}";
    }

    public string Visit(Logarithm operand)
    {
        var inner = operand.Inner.Accept(this);
        return $@"\ln\left({inner}\right)";
    }

    public string Visit(Tangent operand)
    {
        var inner = operand.Inner.Accept(this);
        return $@"\tan\left({inner}\right)";
    }

    public string Visit(Sine operand)
    {
        var inner = operand.Inner.Accept(this);
        return $@"\sin\left({inner}\right)";
    }

    public string Visit(Power operand)
    {
        var left = operand.Left.Accept(this);
        var right = operand.Right.Accept(this);
        return $"{{{left}}}^{{{right}}}";
    }

    public string Visit(Root operand)
    {
        var inner = operand.Inner.Accept(this);

        if (operand.Exponent == 2)
        {
            return $@"\sqrt{{{inner}}}";
        }

        return $@"\sqrt[{operand.Exponent}]{{{inner}}}";
    }

    public string Visit(Multiplication operand)
    {
        var values = operand
            .Operands
            .Select(x => x.Accept(this))
            .Select(x => $"{{{x}}}");

        return string.Join(@"\cdot", values);
    }

    public string Visit(Addition operand)
    {
        var values = operand
            .Operands
            .Select(x => x.Accept(this))
            .Select(x => $"{{{x}}}");

        return string.Join(@"+", values);
    }

    public string Visit(NamedConstant operand) => operand.Symbol;

    public string Visit(ValueConstant operand) => operand.Value.ToString(CultureInfo.InvariantCulture);

    public string Visit(UnspecifiedConstant operand) => _context.GetName(operand);

    public string Visit(Fraction operand) => $@"\frac{{{operand.Numerator}}}{{{operand.Denominator}}}";

    public string Visit(Minimum operand)
    {
        var values = operand
           .Operands
           .Select(x => x.Accept(this))
           .Select(x => $"{{{x}}}");

        var parameters = string.Join(", ", values);

        return $@"\min\left({parameters}\right)";
    }

    public string Visit(Maximum operand)
    {
        var values = operand
            .Operands
            .Select(x => x.Accept(this))
            .Select(x => $"{{{x}}}");

        var parameters = string.Join(", ", values);

        return $@"\max\left({parameters}\right)";
    }
}