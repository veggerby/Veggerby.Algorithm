using Veggerby.Algorithm.Calculus.Parser;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus;

public abstract class Operand : IEquatable<Operand>
{
    public abstract T Accept<T>(IOperandVisitor<T> visitor);

    public abstract bool Equals(Operand other);

    public sealed override string ToString()
    {
        var visitor = new ToStringOperandVisitor();
        return this.Accept(visitor);
    }

    public static Operand operator +(Operand left, Operand right) => Addition.Create(left, right);

    public static Operand operator -(Operand left, Operand right) => Subtraction.Create(left, right);

    public static Operand operator *(Operand left, Operand right) => Multiplication.Create(left, right);

    public static Operand operator /(Operand left, Operand right) => Division.Create(left, right);

    public static Operand operator ^(Operand left, Operand right) => Power.Create(left, right);

    public static implicit operator Operand(int value) => ValueConstant.Create(value);

    public static implicit operator Operand(double value) => ValueConstant.Create(value);

    public static implicit operator Operand(string value) => FunctionParser.Parse(value);
}