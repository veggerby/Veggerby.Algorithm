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

        public static bool IsInteger(this Constant constant)
        {
            return constant.Value.IsInteger();
        }

        public static bool IsInteger(this double constant)
        {
            return (constant % 1 == 0);
        }

        public static IEnumerable<Operand> FlattenAssociative(this IAssociativeBinaryOperation operand, IList<Operand> set = null)
        {
            var type = operand.GetType();

            if (set == null)
            {
                set = new List<Operand>();
            }

            if (operand.Left.GetType() == type)
            {
                var left = (IAssociativeBinaryOperation)operand.Left;
                left.FlattenAssociative(set);
            }
            else
            {
                set.Add(operand.Left);
            }

            if (operand.Right.GetType() == type)
            {
                var right = (IAssociativeBinaryOperation)operand.Right;
                right.FlattenAssociative(set);
            }
            else
            {
                set.Add(operand.Right);
            }

            return set;
        }

        public static string ToMathJaxString(this Operand operand)
        {
            var visitor = new MathJaxOperandVisitor();
            operand.Accept(visitor);
            return visitor.Result;
        }

        public static double Evaluate(this Operand operand, OperationContext context)
        {
            var visitor = new EvaluateOperandVisitor(context);
            operand.Accept(visitor);
            return visitor.Result;
        }

        public static Operand GetDerivative(this Operand operand, Variable variable)
        {
            var visitor = new DerivativeOperandVisitor(variable);
            operand.Accept(visitor);
            return visitor.Result;
        }

        public static void PrintTree(this Operand operand, TextWriter writer)
        {
            var visitor = new PrintTreeOperandVisitor(writer);
            operand.Accept(visitor);
        }
    }
}