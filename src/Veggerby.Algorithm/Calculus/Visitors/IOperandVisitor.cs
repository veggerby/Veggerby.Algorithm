namespace Veggerby.Algorithm.Calculus.Visitors;

public interface IOperandVisitor<T>
{
    T Visit(Function operand);
    T Visit(FunctionReference operand);
    T Visit(ValueConstant operand);
    T Visit(NamedConstant operand);
    T Visit(UnspecifiedConstant operand);
    T Visit(Variable operand);
    T Visit(Addition operand);
    T Visit(Subtraction operand);
    T Visit(Multiplication operand);
    T Visit(Division operand);
    T Visit(Power operand);
    T Visit(Root operand);
    T Visit(Factorial operand);
    T Visit(Sine operand);
    T Visit(Cosine operand);
    T Visit(Tangent operand);
    T Visit(Exponential operand);
    T Visit(Logarithm operand);
    T Visit(LogarithmBase operand);
    T Visit(Negative operand);
    T Visit(Fraction operand);
    T Visit(Minimum operand);
    T Visit(Maximum operand);
}