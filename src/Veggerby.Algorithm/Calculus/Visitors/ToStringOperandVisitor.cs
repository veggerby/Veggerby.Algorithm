using System.Globalization;
using System.Text;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class ToStringOperandVisitor : IOperandVisitor
    {
        private readonly StringBuilder _result = new StringBuilder();
        
        public string Result => _result.ToString();

        private void VisitOperand(Operand operation, Operand operand)
        {
            var operandPriority = operand.GetPriority();
            var operaitonPriority = operation.GetPriority();

            var useParenthesis = operand.CouldUseParenthesis() && (operandPriority != null && operandPriority > operaitonPriority);

            if (useParenthesis) 
            {
                _result.Append("(");
            }            

            operand.Accept(this);

            if (useParenthesis) 
            {
                _result.Append(")");
            }            
        }

        public void Visit(Variable operand)
        {
            _result.Append(operand.Identifier);
        }

        public void Visit(Subtraction operand)
        {
            VisitOperand(operand, operand.Left);
            _result.Append("-");
            VisitOperand(operand, operand.Right);
        }

        public void Visit(Division operand)
        {
            VisitOperand(operand, operand.Left);
            _result.Append("/");
            VisitOperand(operand, operand.Right);
        }

        public void Visit(Factorial operand)
        {
            VisitOperand(operand, operand.Inner);
            _result.Append("!");
        }

        public void Visit(Cosine operand)
        {
            _result.Append("cos(");
            operand.Inner.Accept(this);
            _result.Append(")");
        }

        public void Visit(Exponential operand)
        {
            _result.Append("exp(");
            operand.Inner.Accept(this);
            _result.Append(")");
        }

        public void Visit(LogarithmBase operand)
        {
            if (operand.Base == 10)
            {
                _result.Append("log(");
            }
            else 
            {
                _result.Append($"log{operand.Base}(");
            }
            
            operand.Inner.Accept(this);
            _result.Append(")");            
        }

        public void Visit(Negative operand)
        {
            _result.Append("-");
            VisitOperand(operand, operand.Inner);
        }

        public void Visit(Logarithm operand)
        {
            _result.Append("ln(");
            operand.Inner.Accept(this);
            _result.Append(")");
        }

        public void Visit(Tangent operand)
        {
            _result.Append("tan(");
            operand.Inner.Accept(this);
            _result.Append(")");
        }

        public void Visit(Sine operand)
        {
            _result.Append("sin(");
            operand.Inner.Accept(this);
            _result.Append(")");
        }

        public void Visit(Power operand)
        {
            VisitOperand(operand, operand.Left);
            _result.Append("^");
            VisitOperand(operand, operand.Right);
        }

        public void Visit(Root operand)
        {
            if (operand.Exponent == 2)
            {
                _result.Append("sqrt(");
                operand.Inner.Accept(this);
                _result.Append(")");
            }
            else
            {
                _result.Append($"root({operand.Exponent}, ");
                operand.Inner.Accept(this);
                _result.Append(")");
            }
        }

        public void Visit(Multiplication operand)
        {
            VisitOperand(operand, operand.Left);
            _result.Append("*");
            VisitOperand(operand, operand.Right);
        }

        public void Visit(Addition operand)
        {
            VisitOperand(operand, operand.Left);
            _result.Append("+");
            VisitOperand(operand, operand.Right);
        }

        public void Visit(NamedConstant operand)
        {
            _result.Append(operand.Symbol);
        }

        public void Visit(Constant operand)
        {
            var result = operand.Value.ToString(CultureInfo.InvariantCulture).TrimEnd('0', '.');
            _result.Append(result);
        }

        public void Visit(Fraction operand)
        {
            _result.Append($"{operand.Numerator}/{operand.Denominator}");
        }

    }
}
