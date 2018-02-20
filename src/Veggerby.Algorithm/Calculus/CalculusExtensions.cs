using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public static class CalculusExtensions
    {
        private static readonly Type[] _priorities = new []
        {
            typeof(Factorial),
            typeof(Power),
            typeof(Division),
            typeof(Multiplication),
            typeof(Subtraction),
            typeof(Addition),
        };

        public static int? GetPriority(this Operand operand)
        {
            return _priorities.Contains(operand.GetType())
                ? Array.IndexOf(_priorities, operand.GetType())
                : (int?)null;
        }

        public static bool CouldUseParenthesis(this Operand operand)
        {
            if (operand is BinaryOperation || operand is Factorial || operand is MultiOperation)
            {
                return true;
            }

            return false;
        }

        public static bool IsConstant(this Operand operand)
        {
            return operand.GetType() == typeof(Constant);
        }

        public static bool IsVariable(this Operand operand)
        {
            return operand is Variable;
        }

        public static bool IsNegative(this Operand operand)
        {
            return operand is Negative;
        }

         public static bool IsInteger(this Constant constant)
        {
            return constant.Value.IsInteger();
        }

        public static bool IsInteger(this Operand operand)
        {
            return operand.IsConstant() && ((Constant)operand).IsInteger();
        }

        public static bool IsInteger(this double constant)
        {
            return (constant % 1 == 0);
        }

        public static bool EqualsCommutative<T>(this T t1, T t2) where T : ICommutativeOperation
        {
            if (t1 == null && t2 == null)
            {
                return true;
            }

            if (t1 == null || t2 == null)
            {
                return false;
            }

            return t1.Operands.OrderBy(x => x.ToString()).SequenceEqual(t2.Operands.OrderBy(x => x.ToString()));
        }

        public static bool EqualsBinary<T>(this T t1, T t2) where T : IBinaryOperation
        {
            if (t1 == null && t2 == null)
            {
                return true;
            }

            if (t1 == null || t2 == null)
            {
                return false;
            }

            return t1.Left.Equals(t2.Left) && t1.Right.Equals(t2.Right);
        }

        public static string ToLaTeXString(this Operand operand)
        {
            var visitor = new LaTeXOperandVisitor();
            return operand.Accept(visitor);
        }

        public static double Evaluate(this Operand operand, OperationContext context)
        {
            var visitor = new EvaluateOperandVisitor(context);
            return operand.Accept(visitor);
        }

        public static Operand GetDerivative(this Operand operand, Variable variable)
        {
            var visitor = new DerivativeOperandVisitor(variable);
            return operand.Accept(visitor);
        }

        public static Operand Reduce(this Operand operand)
        {
            var reduceVisitor = new ReduceOperandVisitor();
            var reorderVisitor = new ReorderOperandVisitor();
            return operand
                .Accept(reduceVisitor)
                .Accept(reorderVisitor);
        }
    }
}