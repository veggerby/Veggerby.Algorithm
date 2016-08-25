namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class PrintTreeOperandVisitor : IOperandVisitor
    {
        private string _indent = string.Empty;

        private void Indent()
        {
            _indent = _indent + "  ";
        }

        private void Outdent()
        {
            _indent = _indent.Substring(2);
        }

        private void VisitBinary(IBinaryOperation operand)
        {
            System.Console.WriteLine($"{_indent}{operand.GetType().Name} {operand}");
            Indent();
            operand.Left.Accept(this);
            operand.Right.Accept(this);
            Outdent();
        }

        private void VisitUnary(IUnaryOperation operand)
        {
            System.Console.WriteLine($"{_indent}{operand.GetType().Name} {operand}");
            Indent();
            operand.Inner.Accept(this);
            Outdent();
        }

        public void Visit(Variable operand)
        {
            System.Console.WriteLine($"{_indent}Variable: {operand.Identifier}");
        }

        public void Visit(Subtraction operand)
        {
            VisitBinary(operand);
        }

        public void Visit(Division operand)
        {
            VisitBinary(operand);
        }

        public void Visit(Factorial operand)
        {
            VisitUnary(operand);
        }

        public void Visit(Cosine operand)
        {
            VisitUnary(operand);
        }

        public void Visit(Exponential operand)
        {
            VisitUnary(operand);
        }

        public void Visit(LogarithmBase operand)
        {
            VisitUnary(operand);
        }

        public void Visit(Negative operand)
        {
            VisitUnary(operand);
        }

        public void Visit(Logarithm operand)
        {
            VisitUnary(operand);
        }

        public void Visit(Tangent operand)
        {
            VisitUnary(operand);
        }

        public void Visit(Sine operand)
        {
            VisitUnary(operand);
        }

        public void Visit(Power operand)
        {
            VisitBinary(operand);
        }

         public void Visit(Root operand)
        {
            VisitUnary(operand);
        }

        public void Visit(Multiplication operand)
        {
            VisitBinary(operand);
        }

        public void Visit(Addition operand)
        {
            VisitBinary(operand);
        }

        public void Visit(NamedConstant operand)
        {
            System.Console.WriteLine($"{_indent}Constant: {operand.Symbol}");
        }

        public void Visit(Constant operand)
        {
            System.Console.WriteLine($"{_indent}Constant: {operand.Value}");
        }

        public void Visit(Fraction operand)
        {
            System.Console.WriteLine($"{_indent}Fraction: {operand.Numerator}/{operand.Denominator}");
        }

        public void Visit(Minimum operand)
        {
            VisitBinary(operand);
        }

        public void Visit(Maximum operand)
        {
            VisitBinary(operand);
        }
    }
}