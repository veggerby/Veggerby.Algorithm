using System;
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
            return operand is Constant;
        }

        public static bool IsVariable(this Operand operand)
        {
            return operand is Variable;
        }
    }
}