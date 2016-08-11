namespace Veggerby.Algorithm.Arithmetic
{
    public interface IOperandVisitor
    {
        void Visit(Constant operand);
        void Visit(Variable operand);
        void Visit(Addition operand);
        void Visit(Subtraction operand);
        void Visit(Multiplication operand);
        void Visit(Division operand);
    }
}