using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus;

public class UnspecifiedConstant : Constant, IEquatable<UnspecifiedConstant>
{
    public Guid InstanceId { get; }

    private UnspecifiedConstant()
    {
        InstanceId = Guid.NewGuid();
    }

    public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

    public static UnspecifiedConstant Create() => new UnspecifiedConstant();

    public override bool Equals(object obj) => Equals(obj as UnspecifiedConstant);
    public override bool Equals(Operand other) => Equals(other as UnspecifiedConstant);
    public bool Equals(UnspecifiedConstant other) => other is not null && InstanceId.Equals(other.InstanceId);
    public override int GetHashCode() => InstanceId.GetHashCode();
}