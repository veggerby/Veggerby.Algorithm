using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus;

public class Addition : MultiOperation, ICommutativeOperation, IAssociativeOperation, IEquatable<Addition>
{
    private Addition(params Operand[] operands) : base(operands)
    {
    }

    public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

    public static Operand Create(IEnumerable<Operand> operands)
    {
        if (operands is null)
        {
            throw new ArgumentNullException(nameof(operands));
        }

        if (operands.Count() == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(operands));
        }

        if (operands.Count() == 1)
        {
            return operands.Single();
        }

        return operands.Aggregate((seed, next) => Create(seed, next));
    }

    public static Operand Create(Operand left, Operand right)
    {
        if (left is null)
        {
            throw new ArgumentNullException(nameof(left));
        }

        if (right is null)
        {
            throw new ArgumentNullException(nameof(right));
        }

        if (right.IsNegative())
        {
            return Subtraction.Create(left, ((Negative)right).Inner);
        }

        if (right.IsConstant() && ((ValueConstant)right).Value < 0)
        {
            return Subtraction.Create(left, -((ValueConstant)right).Value);
        }

        if (left.IsNegative())
        {
            return Subtraction.Create(right, ((Negative)left).Inner);
        }

        if (left.IsConstant() && ((ValueConstant)left).Value < 0)
        {
            return Subtraction.Create(right, -((ValueConstant)left).Value);
        }

        var operands = new List<Operand>();

        if (left is Addition)
        {
            operands.AddRange(((Addition)left).Operands);
        }
        else
        {
            operands.Add(left);
        }

        if (right is Addition)
        {
            operands.AddRange(((Addition)right).Operands);
        }
        else
        {
            operands.Add(right);
        }

        if (operands.All(x => x.Equals(Constant.Zero)))
        {
            return Constant.Zero;
        }

        // remove zeros
        operands = operands.Where(x => !x.Equals(Constant.Zero)).ToList();

        // combine constants into one operand
        if (operands.Count(x => x.IsConstant()) > 1)
        {
            var constants = operands.Where(x => x.IsConstant()).Cast<ValueConstant>();
            var constant = constants.Aggregate((seed, next) => (ValueConstant)(seed + next));
            operands = new Operand[] { constant }.Concat(operands.Where(x => !x.IsConstant())).ToList();
        }

        if (operands.Count() == 1)
        {
            return operands.Single();
        }

        return new Addition(operands.ToArray());
    }

    public override bool Equals(object obj) => Equals(obj as Addition);
    public override bool Equals(Operand other) => Equals(other as Addition);
    public bool Equals(Addition other) => other is not null && this.EqualsCommutative(other);
    public override int GetHashCode() => base.GetHashCode();
}