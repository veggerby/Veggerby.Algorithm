using Veggerby.Algorithm.Calculus.Parser;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus;

public class Function : Operand, IEquatable<Function>
{
    public string Identifier { get; }
    public IEnumerable<Variable> Variables { get; }
    public Operand Operand { get; }

    protected Function(string identifier, Operand operand)
    {
        Identifier = identifier;
        Operand = operand;

        var visitor = new VariablesOperandVisitor();
        Variables = operand.Accept(visitor).OrderBy(x => x.Identifier).ToList();
    }

    public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

    public static Function Create(string identifier, Operand operand)
    {
        if (string.IsNullOrEmpty(identifier))
        {
            throw new ArgumentException("Invalid identifier", nameof(identifier));
        }

        if (operand is null)
        {
            throw new ArgumentNullException(nameof(operand));
        }

        return new Function(identifier, operand);
    }

    public static implicit operator Function(string value) => Create("f", FunctionParser.Parse(value));

    public override bool Equals(object obj) => Equals(obj as Function);
    public override bool Equals(Operand other) => Equals(other as Function);
    public bool Equals(Function other) => other is not null && Operand.Equals(other.Operand);
    public override int GetHashCode() => Operand.GetHashCode();
}