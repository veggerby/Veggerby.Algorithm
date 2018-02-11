using System;
using System.Collections.Generic;
using System.Linq;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Multiplication : MultiOperation, ICommutativeOperation, IAssociativeOperation
    {
        private Multiplication(params Operand[] operands) : base(operands)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public static Operand Create(IEnumerable<Operand> operands)
        {
            if (operands == null)
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
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
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
                var constants = operands.Where(x => x.IsConstant()).Cast<Constant>();
                var constant = constants.Aggregate((seed, next) => (Constant)(seed * next));
                operands = new Operand[] { constant }.Concat(operands.Where(x => !x.IsConstant())).ToList();
            }

            if (operands.Count() == 1)
            {
                return operands.Single();
            }

            return new Multiplication(operands.ToArray());
        }
    }
}