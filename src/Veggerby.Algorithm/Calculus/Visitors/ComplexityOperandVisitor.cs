namespace Veggerby.Algorithm.Calculus.Visitors;

public class ComplexityOperandVisitor : IOperandVisitor<int>
{
    public int Visit(Function operand) => operand.Operand.Accept(this) + 2;

    public int Visit(FunctionReference operand) => operand.Parameters.Sum(x => x.Accept(this)) + 2;

    public int Visit(ValueConstant operand) => 1;

    public int Visit(NamedConstant operand) => 1;

    public int Visit(UnspecifiedConstant operand) => 1;

    public int Visit(Variable operand) => 2;

    public int Visit(Addition operand) => operand.Operands.Sum(x => x.Accept(this)) + operand.Operands.Count() - 1;

    public int Visit(Subtraction operand) => operand.Left.Accept(this) + operand.Right.Accept(this) + 2;

    public int Visit(Multiplication operand) => operand.Operands.Sum(x => x.Accept(this)) + operand.Operands.Count() - 1;

    public int Visit(Division operand) => operand.Left.Accept(this) + operand.Right.Accept(this) + 3;

    public int Visit(Power operand) => operand.Left.Accept(this) + operand.Right.Accept(this) + 3;

    public int Visit(Root operand) => operand.Inner.Accept(this) + 3;

    public int Visit(Factorial operand) => operand.Inner.Accept(this) + 4;

    public int Visit(Sine operand) => operand.Inner.Accept(this) + 2;

    public int Visit(Cosine operand) => operand.Inner.Accept(this) + 2;

    public int Visit(Tangent operand) => operand.Inner.Accept(this) + 3;

    public int Visit(Exponential operand) => operand.Inner.Accept(this) + 2;

    public int Visit(Logarithm operand) => operand.Inner.Accept(this) + 2;

    public int Visit(LogarithmBase operand) => operand.Inner.Accept(this) + 3;

    public int Visit(Negative operand) => operand.Inner.Accept(this) + 2;

    public int Visit(Fraction operand) => 2;

    public int Visit(Minimum operand) => operand.Operands.Sum(x => x.Accept(this)) + (operand.Operands.Count() - 1) * 4;

    public int Visit(Maximum operand) => operand.Operands.Sum(x => x.Accept(this)) + (operand.Operands.Count() - 1) * 4;
}