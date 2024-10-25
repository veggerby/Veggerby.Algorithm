using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus;

public class Multiplication : MultiOperation, ICommutativeOperation, IAssociativeOperation, IEquatable<Multiplication>
{
    private Multiplication(params Operand[] operands) : base(operands)
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

        if (left.Equals(Constant.MinusOne))
        {
            return Negative.Create(right);
        }

        if (right.Equals(Constant.MinusOne))
        {
            return Negative.Create(left);
        }

        var operands = new List<Operand>();

        if (left is Multiplication)
        {
            operands.AddRange(((Multiplication)left).Operands);
        }
        else
        {
            operands.Add(left);
        }

        if (right is Multiplication)
        {
            operands.AddRange(((Multiplication)right).Operands);
        }
        else
        {
            operands.Add(right);
        }

        // if any is zero, multiplication is zero
        if (operands.Any(x => x.Equals(Constant.Zero)))
        {
            return Constant.Zero;
        }

        if (operands.All(x => x.Equals(Constant.One)))
        {
            return Constant.One;
        }

        // remove ones
        operands = operands.Where(x => !x.Equals(Constant.One)).ToList();

        // combine constants into one operand
        if (operands.Count(x => x.IsConstant()) > 1)
        {
            var constants = operands.Where(x => x.IsConstant()).Cast<ValueConstant>();
            var constant = constants.Aggregate((seed, next) => (ValueConstant)(seed * next));
            operands = new Operand[] { constant }.Concat(operands.Where(x => !x.IsConstant())).ToList();
        }

        if (operands.Count() == 1)
        {
            return operands.Single();
        }

        return new Multiplication(operands.ToArray());
    }

    public override bool Equals(object obj) => Equals(obj as Multiplication);
    public override bool Equals(Operand other) => Equals(other as Multiplication);
    public bool Equals(Multiplication other) => other is not null && this.EqualsCommutative(other);
    public override int GetHashCode() => base.GetHashCode();
}