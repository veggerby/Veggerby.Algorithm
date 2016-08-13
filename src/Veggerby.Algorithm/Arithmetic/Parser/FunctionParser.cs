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

            System.Console.WriteLine("[");
            Print("", root);
            System.Console.WriteLine("]");
            
            return Constant.Pi;
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

        private static void Print(string indent, Node node)
        {
            if (node is Group)
            {
                Print(indent, (Group)node);
            }
            else if (node is BinaryNode)
            {
                System.Console.WriteLine($"{indent}{node.Value}(");
                Print(indent + "  ", ((BinaryNode)node).Left);
                System.Console.WriteLine($"{indent}  ,");
                Print(indent + "  ", ((BinaryNode)node).Right);
                System.Console.WriteLine($"{indent})");
            }
            else if (node is UnaryNode)
            {
                System.Console.WriteLine($"{indent}{node.Value}(");
                Print(indent + "  ", ((BinaryNode)node).Left);                
                System.Console.WriteLine($"{indent})");
            }
            else
            {
                System.Console.WriteLine($"{indent}{node.Value}");
            }
        }

        private static void Print(string indent, Group group)
        {
            foreach (var node in group.ChildNodes)
            {
                if (node is Group)
                {
                    Print(indent + "  ", (Group)node);
                }
                else 
                {
                    Print(indent, node);
                }
            }
        }
    }
}