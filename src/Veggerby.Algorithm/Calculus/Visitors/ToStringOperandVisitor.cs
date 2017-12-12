using System.Globalization;
using System.Linq;
using System.Text;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class ToStringOperandVisitor : IOperandVisitor<string>
    {
        private string VisitOperand(Operand operation, Operand operand)
        {
            var result = new StringBuilder();
            var operandPriority = operand.GetPriority();
            var operaitonPriority = operation.GetPriority();

            var useParenthesis = operand.CouldUseParenthesis() && (operandPriority != null && operandPriority > operaitonPriority);

            if (useParenthesis)
            {
                result.Append("(");
            }

            result.Append(operand.Accept(this));

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

        public string Visit(Variable operand)
        {
            return operand.Identifier;
        }

        public string Visit(Subtraction operand)
        {
            var left = VisitOperand(operand, operand.Left);
            var right = VisitOperand(operand, operand.Right);
            return $"{left}-{right}";
        }

        public string Visit(Division operand)
        {
            var left = VisitOperand(operand, operand.Left);
            var right = VisitOperand(operand, operand.Right);
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
            var left = VisitOperand(operand, operand.Left);
            var right = VisitOperand(operand, operand.Right);
            return $"{left}*{right}";
        }

        public string Visit(Addition operand)
        {
            var left = VisitOperand(operand, operand.Left);
            var right = VisitOperand(operand, operand.Right);
            return $"{left}+{right}";
        }

        public string Visit(NamedConstant operand)
        {
            return operand.Symbol;
        }

        public string Visit(Constant operand)
        {
            return operand.Value.ToString();
        }

        public string Visit(Fraction operand)
        {
            return $"{operand.Numerator}/{operand.Denominator}";
        }

        public string Visit(Minimum operand)
        {
            var left = operand.Left.Accept(this);
            var right = operand.Right.Accept(this);
            return $"min({left}, {right})";
        }

        public string Visit(Maximum operand)
        {
            var left = operand.Left.Accept(this);
            var right = operand.Right.Accept(this);
            return $"max({left}, {right})";
        }
    }
}