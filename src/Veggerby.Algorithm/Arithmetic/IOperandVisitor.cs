namespace Veggerby.Algorithm.Arithmetic
{
    public interface IOperandVisitor
    {
        void Visit(Constant operand);
        void Visit(NamedConstant operand);
        void Visit(Variable operand);
        void Visit(Addition operand);
        void Visit(Subtraction operand);
        void Visit(Multiplication operand);
        void Visit(Division operand);
        void Visit(Power operand);
        void Visit(Factorial operand);
        void Visit(Sine operand);
        void Visit(Cosine operand);
        void Visit(Tangent operand);
    }
}