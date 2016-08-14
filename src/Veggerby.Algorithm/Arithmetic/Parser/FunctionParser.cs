using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Veggerby.Algorithm.Arithmetic.Parser
{
    public class FunctionParser
    {
        private static readonly Regex _parserRegEx = new Regex(@"([*()\^\/]|(?<!E)[\+\-])", RegexOptions.Compiled);

        public static Operand Parse(string function) 
        {
            var split = _parserRegEx.Split(function);

            var root = new Group(null);

            var currentGroup = root;

            var parenthesis = split.Where(x => x == "(" || x == ")").ToList();

            var parenthesisValue = 0;

            foreach (var parens in parenthesis)
            {
                if (parens == "(")
                {
                    parenthesisValue++;
                }

                if (parens == ")")
                {
                    parenthesisValue--;
                }

                if (parenthesisValue < 0)
                {
                    throw new Exception("Parenthesis not properly nested");
                }
            }

            if (parenthesisValue != 0)
            {
                throw new Exception("Parenthesis not properly closed");
            }

            foreach (var value in split.Where(x => !string.IsNullOrEmpty(x)))
            {
                if (value == "(") 
                {
                    var group = new Group(currentGroup);
                    currentGroup.Add(group);
                    currentGroup = group;
                }
                else if (value == ")")
                {
                    currentGroup = (Group)currentGroup.Parent;
                }
                else
                {
                    var node = new UnstructuredNode(currentGroup, value);
                    currentGroup.Add(node);
                }
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
                        return new Addition(left, right);
                    case "-":
                        return new Subtraction(left, right);
                    case "*":
                        return new Multiplication(left, right);
                    case "/":
                        return new Division(left, right);
                    case "^":
                        return new Power(left, right);
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
                        return new Factorial(inner);
                    case "sin":
                        return new Sine(inner);
                    case "cos":
                        return new Cosine(inner);
                    case "tan":
                    case "exp":
                    case "log":
                    case "ln":
                        throw new NotImplementedException();
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
                return new Constant(value);
            }

            if (Regex.IsMatch(node.Value, "^[a-zA-Z][a-zA-Z0-9]*$"))
            {
                return new Variable(node.Value);
            }

            return null;
        }
    }
}