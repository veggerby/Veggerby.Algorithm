using System.Linq;
using System.Text;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class MathJaxOperandVisitor : IOperandVisitor
    {
        private readonly StringBuilder _result = new StringBuilder();

        public string Result => _result.ToString();

        public void Visit(Function operand)
        {
            _result.Append($@"{operand.Identifier}\left(");
            var variables = string.Join(", ", operand.Variables.Select(x => x.Identifier));
            _result.Append(variables);
            _result.Append("\right) = ");
            operand.Operand.Accept(this);
        }

        public void Visit(FunctionReference operand)
        {
            _result.Append($@"{operand.Identifier}\left(");

            var first = true;
            foreach (var parameter in operand.Parameters)
            {
                if (!first)
                {
                    _result.Append(", ");
                }

                parameter.Accept(this);
                first = false;
            }

            _result.Append("\right)");
        }

        public void Visit(Variable operand)
        {
            _result.Append(operand.Identifier);
        }

        public void Visit(Subtraction operand)
        {
            _result.Append(@"{");
            operand.Left.Accept(this);
            _result.Append(@"}-{");
            operand.Right.Accept(this);
            _result.Append(@"}");

        }

        public void Visit(Division operand)
        {
            _result.Append(@"\frac{");
            operand.Left.Accept(this);
            _result.Append("}{");
            operand.Right.Accept(this);
            _result.Append("}");
        }

        public void Visit(Factorial operand)
        {
        }

        public void Visit(Cosine operand)
        {
            _result.Append(@"\cos\left(");
            operand.Inner.Accept(this);
            _result.Append(@"\right)");
        }

        public void Visit(Exponential operand)
        {
            _result.Append(@"e^{");
            operand.Inner.Accept(this);
            _result.Append("}");
        }

        public void Visit(LogarithmBase operand)
        {
            _result.Append($@"\log_{operand.Base}\left(");
            operand.Inner.Accept(this);
            _result.Append(@"\right)");
        }

        public void Visit(Negative operand)
        {
            _result.Append("-");
            operand.Inner.Accept(this);
        }

        public void Visit(Logarithm operand)
        {
            _result.Append($@"\ln\left(");
            operand.Inner.Accept(this);
            _result.Append(@"\right)");
        }

        public void Visit(Tangent operand)
        {
            _result.Append(@"\tan\left(");
            operand.Inner.Accept(this);
            _result.Append(@"\right)");
        }

        public void Visit(Sine operand)
        {
            _result.Append(@"\sin\left(");
            operand.Inner.Accept(this);
            _result.Append(@"\right)");
        }

        public void Visit(Power operand)
        {
            _result.Append(@"{");
            operand.Left.Accept(this);
            _result.Append(@"}^{");
            operand.Right.Accept(this);
            _result.Append(@"}");
        }

        public void Visit(Root operand)
        {
            if (operand.Exponent == 2)
            {
                _result.Append(@"\sqrt{");
                operand.Inner.Accept(this);
                _result.Append("}");
            }
            else{
                _result.Append($@"\sqrt[{operand.Exponent}]{{");
                operand.Inner.Accept(this);
                _result.Append("}");
            }
        }

        public void Visit(Multiplication operand)
        {
            if (operand.Left.IsConstant())
            {
                operand.Left.Accept(this);
            }
            else
            {
                _result.Append(@"{");
                operand.Left.Accept(this);
                _result.Append(@"}\cdot");
            }

            if (operand.Right.IsConstant())
            {
                operand.Right.Accept(this);
            }
            else
            {
                _result.Append(@"{");
                operand.Right.Accept(this);
                _result.Append(@"}");
            }
        }

        public void Visit(Addition operand)
        {
            _result.Append(@"{");
            operand.Left.Accept(this);
            _result.Append(@"}+{");
            operand.Right.Accept(this);
            _result.Append(@"}");
        }

        public void Visit(NamedConstant operand)
        {
            _result.Append(operand.Symbol);
        }

        public void Visit(Constant operand)
        {
            _result.Append(operand.ToString());
        }

        public void Visit(Fraction operand)
        {
            _result.Append($"{operand.Numerator}/{operand.Denominator}");
        }


        public void Visit(Minimum operand)
        {
            _result.Append(@"\min\left({");
            operand.Left.Accept(this);
            _result.Append(@"}, {");
            operand.Right.Accept(this);
            _result.Append(@"}\right)");
        }

        public void Visit(Maximum operand)
        {
            _result.Append(@"\max\left({");
            operand.Left.Accept(this);
            _result.Append(@"}, {");
            operand.Right.Accept(this);
            _result.Append(@"}\right)");
        }
    }
}