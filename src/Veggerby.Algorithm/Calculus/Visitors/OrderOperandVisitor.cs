namespace Veggerby.Algorithm.Calculus.Visitors
{
    public class OrderOperandVisitor : IOperandVisitor<int>
    {
        public int Visit(Function operand) => 12;

        public int Visit(FunctionReference operand) => 13;

        public int Visit(ValueConstant operand) => 1;

        public int Visit(NamedConstant operand) => 3;

        public int Visit(UnspecifiedConstant operand) => 4;

        public int Visit(Variable operand) => 5;

        public int Visit(Addition operand) => 7;

        public int Visit(Subtraction operand) => 8;

        public int Visit(Multiplication operand) => 9;

        public int Visit(Division operand) => 10;

        public int Visit(Power operand) => 11;

        public int Visit(Root operand) => 20;

        public int Visit(Factorial operand) => 14;

        public int Visit(Sine operand) => 17;

        public int Visit(Cosine operand) => 18;

        public int Visit(Tangent operand) => 19;

        public int Visit(Exponential operand) => 15;

        public int Visit(Logarithm operand) => 16;

        public int Visit(LogarithmBase operand) => 21;

        public int Visit(Negative operand) => 6;

        public int Visit(Fraction operand) => 2;

        public int Visit(Minimum operand) => 22;

        public int Visit(Maximum operand) => 23;
    }
}