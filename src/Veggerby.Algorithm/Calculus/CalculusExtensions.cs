using System;
using System.Collections.Generic;
using System.Linq;

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
            if (operand is BinaryOperation || operand is Factorial)
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

        public static ISet<Operand> FlattenCommutative(this ICommutativeBinaryOperation operand, ISet<Operand> set = null)
        {
            var type = operand.GetType();

            if (set == null)
            {
                set = new HashSet<Operand>();
            }

            if (operand.Left.GetType() == type)
            {
                var left = (ICommutativeBinaryOperation)operand.Left;
                left.FlattenCommutative(set);
            }
            else
            {
                set.Add(operand.Left);
            }

            if (operand.Right.GetType() == type)
            {
                var right = (ICommutativeBinaryOperation)operand.Right;
                right.FlattenCommutative(set);
            }
            else 
            {
                set.Add(operand.Right);
            }

            return set;
        }
    }
}