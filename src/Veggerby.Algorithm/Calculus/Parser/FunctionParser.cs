using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Veggerby.Algorithm.Calculus.Parser
{
    public class FunctionParser
    {
        private static readonly Regex _number = new Regex(@"^(?<number>([\-+]?[0-9]+(\.[0-9]+)?((e|E)[\-+]?[0-9]+)?|pi|π))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex _operation = new Regex(@"^(?<operation>\+|-|\*|\/|\^)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex _function = new Regex(@"^(?<function>sin|cos|tan|exp|ln|log|log2|!)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex _variable = new Regex(@"^(?<variable>[a-z][a-z0-9]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private enum OperandType
        {
            Number,
            Variable,
            Operation,
            Function,
            ParenthesisOpen,
            ParenthesisClose,
            None
        }

        public static Operand Parse(string function) 
        {
            var root = new Group(null);

            var currentGroup = root;

            var remain = function;

            var parenthesisValue = 0;

            var previousType = OperandType.None;

            while (!string.IsNullOrEmpty(remain))
            {
                bool isMatch = false;

                var m = _number.Match(remain);
                if (m.Success && (previousType == OperandType.None || previousType == OperandType.Operation || previousType == OperandType.ParenthesisOpen))
                {
                    previousType = OperandType.Number;
                    isMatch = true;

                    var node = new UnstructuredNode(currentGroup, m.Groups["number"].Value);
                    currentGroup.Add(node);

                    remain = remain.Substring(m.Length);
                }

                m = _operation.Match(remain);
                if (m.Success && previousType != OperandType.Operation)
                {
                    previousType = OperandType.Operation;
                    isMatch = true;

                    var node = new UnstructuredNode(currentGroup, m.Groups["operation"].Value);
                    currentGroup.Add(node);

                    remain = remain.Substring(m.Length);
                }

                m = _function.Match(remain);
                if (m.Success && previousType != OperandType.Function)
                {
                    previousType = OperandType.Function;
                    isMatch = true;

                    var node = new UnstructuredNode(currentGroup, m.Groups["function"].Value);
                    currentGroup.Add(node);

                    remain = remain.Substring(m.Length);
                }

                m = _variable.Match(remain);
                if (m.Success)
                {
                    previousType = OperandType.Variable;
                    isMatch = true;

                    var node = new UnstructuredNode(currentGroup, m.Groups["variable"].Value);
                    currentGroup.Add(node);

                    remain = remain.Substring(m.Length);
                }

                if (remain.StartsWith("(") || remain.StartsWith(")"))
                {
                    isMatch = true;
                    var value = remain.Substring(0, 1);
                    if (value == "(")
                    {
                        previousType = OperandType.ParenthesisOpen;
                        parenthesisValue++;

                        var group = new Group(currentGroup);
                        currentGroup.Add(group);
                        currentGroup = group;
                    }
                    else if (value == ")")
                    {
                        previousType = OperandType.ParenthesisClose;
                        parenthesisValue--;
                        currentGroup = (Group)currentGroup.Parent;
                    }

                    if (parenthesisValue < 0)
                    {
                        throw new Exception("Parenthesis not properly nested");
                    }

                    remain = remain.Substring(1);
                }

                if (!isMatch)
                {
                    throw new Exception($"Could not parse function part \"{remain}\" of \"{function}\"");
                }
            }

            if (parenthesisValue != 0)
            {
                throw new Exception("Parenthesis not properly closed");
            }

            root.Restructure();
        
            return Parse(root.ChildNodes.Single());
        }

        private static Operand Parse(Node node)
        {
            if (node is Group)
            {
                return Parse(((Group)node).ChildNodes.Single());
            }
            
            if (node is BinaryNode)
            {
                var binary = (BinaryNode)node;
                var left = Parse(binary.Left);
                var right = Parse(binary.Right);

                switch(binary.Value)
                {
                    case "+":
                        return Addition.Create(left, right);
                    case "-":
                        return Subtraction.Create(left, right);
                    case "*":
                        return Multiplication.Create(left, right);
                    case "/":
                        return Division.Create(left, right);
                    case "^":
                        return Power.Create(left, right);
                    case "min":
                    case "max":
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException("Invalid operation");
                }
            }

            if (node is UnaryNode)
            {
                var unary = (UnaryNode)node;
                var inner = Parse(unary.Inner);

                switch (unary.Value)
                {
                    case "!":
                        return Factorial.Create(inner);
                    case "sin":
                        return Sine.Create(inner);
                    case "cos":
                        return Cosine.Create(inner);
                    case "tan":
                        return Tangent.Create(inner);
                    case "exp":
                        return Exponential.Create(inner);
                    case "log":
                        return LogarithmBase.Create(10, inner);
                    case "log2":
                        return LogarithmBase.Create(2, inner);
                    case "ln":
                        return Logarithm.Create(inner);
                    default:
                        throw new NotSupportedException("Invalid operation");
                }
            }

            if (node is UnstructuredNode)
            {
                return ParseSimpleNode(node);
            }

            throw new Exception("Unknown node");
        }

        private static Operand ParseSimpleNode(Node node)
        {
            if (string.Equals(node.Value, "pi", StringComparison.OrdinalIgnoreCase) || string.Equals(node.Value, "π"))
            {
                return Constant.Pi;
            }

            if (string.Equals(node.Value, "e", StringComparison.OrdinalIgnoreCase))
            {
                return Constant.e;
            }

            double value;

            if (double.TryParse(node.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                return Constant.Create(value);
            }

            if (Regex.IsMatch(node.Value, "^[a-zA-Z][a-zA-Z0-9]*$"))
            {
                return Variable.Create(node.Value);
            }

            return null;
        }
    }
}