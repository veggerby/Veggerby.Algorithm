using System.Globalization;
using System.Text;

namespace Veggerby.Algorithm.Calculus.Visitors;

public class ToStringOperandVisitor(OperationContext context = null) : IOperandVisitor<string>
{
    private readonly OperationContext _context = context ?? new OperationContext();

    private string VisitOperand(Operand parent, Operand child, bool checkAssociative = false)
    {
        var result = new StringBuilder();

        var childPriority = child.GetPriority();
        var parentPriority = parent.GetPriority();

        var useParenthesis = child.CouldUseParenthesis() && (childPriority is not null && childPriority > parentPriority);

        if (checkAssociative && !useParenthesis && parent.GetType() == child.GetType() && child is not IAssociativeOperation)
        {
            useParenthesis = true;
        }

        if (useParenthesis)
        {
            result.Append("(");
        }

        result.Append(child.Accept(this));

        if (useParenthesis)
        {
            result.Append(")");
        }

        return result.ToString();
    }

    public string Visit(Function operand)
    {
        var variables = string.Join(", ", operand.Variables.Select(x => x.Identifier));
        var f = operand.Operand.Accept(this);
        return $"{operand.Identifier}({variables})={f}";
    }

    public string Visit(FunctionReference operand)
    {
        var parameters = string.Join(", ", operand.Parameters.Select(x => x.Accept(this)));
        return $"{operand.Identifier}({parameters})";
    }

    public string Visit(Variable operand) => operand.Identifier;

    public string Visit(Subtraction operand)
    {
        var left = VisitOperand(operand, operand.Left);
        var right = VisitOperand(operand, operand.Right, true);
        return $"{left}-{right}";
    }

    public string Visit(Division operand)
    {
        var left = VisitOperand(operand, operand.Left);
        var right = VisitOperand(operand, operand.Right, true);
        return $"{left}/{right}";
    }

    public string Visit(Factorial operand)
    {
        var inner = VisitOperand(operand, operand.Inner);
        return $"{inner}!";
    }

    public string Visit(Cosine operand)
    {
        var inner = operand.Inner.Accept(this);
        return $"cos({inner})";
    }

    public string Visit(Exponential operand)
    {
        var inner = VisitOperand(operand, operand.Inner);
        return $"exp({inner})";
    }

    public string Visit(LogarithmBase operand)
    {
        var inner = operand.Inner.Accept(this);

        if (operand.Base == 10)
        {
            return $"log({inner})";
        }

        return $"log{operand.Base}({inner})";
    }

    public string Visit(Negative operand)
    {
        var inner = VisitOperand(operand, operand.Inner);
        return $"-{inner}";
    }

    public string Visit(Logarithm operand)
    {
        var inner = operand.Inner.Accept(this);
        return $"ln({inner})";
    }

    public string Visit(Tangent operand)
    {
        var inner = operand.Inner.Accept(this);
        return $"tan({inner})";
    }

    public string Visit(Sine operand)
    {
        var inner = operand.Inner.Accept(this);
        return $"sin({inner})";
    }

    public string Visit(Power operand)
    {
        var left = VisitOperand(operand, operand.Left);
        var right = VisitOperand(operand, operand.Right);
        return $"{left}^{right}";
    }

    public string Visit(Root operand)
    {
        var inner = operand.Inner.Accept(this);

        if (operand.Exponent == 2)
        {
            return $"sqrt({inner})";
        }

        return $"root({operand.Exponent}, {inner})";
    }

    public string Visit(Multiplication operand)
    {
        var values = operand.Operands.Select(x => VisitOperand(operand, x)).ToList();
        return string.Join("*", values);
    }

    public string Visit(Addition operand)
    {
        var values = operand.Operands.Select(x => VisitOperand(operand, x)).ToList();
        return string.Join("+", values);
    }

    public string Visit(NamedConstant operand) => operand.Symbol;

    public string Visit(ValueConstant operand) => operand.Value.ToString(CultureInfo.InvariantCulture);

    public string Visit(UnspecifiedConstant operand) => _context.GetName(operand);

    public string Visit(Fraction operand) => $"{operand.Numerator}/{operand.Denominator}";

    public string Visit(Minimum operand)
    {
        var values = operand.Operands.Select(x => VisitOperand(operand, x)).ToList();
        var parameters = string.Join(", ", values);
        return $"min({parameters})";
    }

    public string Visit(Maximum operand)
    {
        var values = operand.Operands.Select(x => VisitOperand(operand, x)).ToList();
        var parameters = string.Join(", ", values);
        return $"max({parameters})";
    }
}