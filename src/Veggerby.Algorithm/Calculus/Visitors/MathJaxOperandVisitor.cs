using System.Text;

namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class MathJaxOperandVisitor : IOperandVisitor
    {
        private readonly StringBuilder _result = new StringBuilder();
        
        public string Result => _result.ToString();

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
    }
}