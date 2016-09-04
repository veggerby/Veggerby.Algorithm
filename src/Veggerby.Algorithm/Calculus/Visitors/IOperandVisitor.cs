namespace Veggerby.Algorithm.Calculus.Visitors
{
    public interface IOperandVisitor
    {
        void Visit(Function operand);
        void Visit(FunctionReference operand);
        void Visit(Constant operand);
        void Visit(NamedConstant operand);
        void Visit(Variable operand);
        void Visit(Addition operand);
        void Visit(Subtraction operand);
        void Visit(Multiplication operand);
        void Visit(Division operand);
        void Visit(Power operand);
        void Visit(Root operand);
        void Visit(Factorial operand);
        void Visit(Sine operand);
        void Visit(Cosine operand);
        void Visit(Tangent operand);
        void Visit(Exponential operand);
        void Visit(Logarithm operand);
        void Visit(LogarithmBase operand);
        void Visit(Negative operand);
        void Visit(Fraction operand);
        void Visit(Minimum operand);
        void Visit(Maximum operand);
    }
}