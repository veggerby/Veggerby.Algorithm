using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Veggerby.Algorithm.Calculus.Parser
{
    public class FunctionParser
    {
        private static Lexer _lexer;
        private static SyntaxParser _syntax;
        private static bool _isInitialized = false;
        private static object _lock = new object();

        static FunctionParser()
        {
            if (!_isInitialized)
            {
                lock (_lock)
                {
                    if (!_isInitialized)
                    {
                        _lexer = new Lexer();
                        _lexer.AddDefinition(new TokenDefinition(TokenType.Function, @"sin|cos|tan|exp|ln|log|log2|sqrt|root|min|max"));
                        _lexer.AddDefinition(new TokenDefinition(TokenType.Number, @"[0-9]+(\.[0-9]+)?((e|E)[\-+]?[0-9]+)?"));
                        _lexer.AddDefinition(new TokenDefinition(TokenType.StartParenthesis, @"\("));
                        _lexer.AddDefinition(new TokenDefinition(TokenType.EndParenthesis, @"\)"));
                        _lexer.AddDefinition(new TokenDefinition(TokenType.Separator, @","));
                        _lexer.AddDefinition(new TokenDefinition(TokenType.Identifier, @"([a-zA-Z]([a-zA-Z0-9_]*))|π"));
                        _lexer.AddDefinition(new TokenDefinition(TokenType.Factorial, @"\!"));
                        _lexer.AddDefinition(new TokenDefinition(TokenType.OperatorPriority1, @"\*|\/|\^|!"));
                        _lexer.AddDefinition(new TokenDefinition(TokenType.Sign, @"\+|\-"));
                        _lexer.AddDefinition(new TokenDefinition(TokenType.Whitespace, @"[\t ]+"));
                        _lexer.AddDefinition(new TokenDefinition(TokenType.EndOfLine, @"[\f\n\r\v]+"));

                        _syntax = new SyntaxParser();

                        _isInitialized = true;
                    }
                }
            }
        }

        private static Operand CompileFunction(Token token, Operand inner)
        {
            if (token.Type != TokenType.Function)
            {
                throw new ArgumentException("Unexpected token type", nameof(token));
            }

            switch (token.Value.ToLowerInvariant())
            {
                case "sin": return Sine.Create(inner);
                case "cos": return Cosine.Create(inner);
                case "tan": return Tangent.Create(inner);
                case "exp": return Exponential.Create(inner);
                case "ln": return Logarithm.Create(inner);
                case "log": return LogarithmBase.Create(10, inner);
                case "log2": return LogarithmBase.Create(2, inner);
                case "sqrt": return Root.Create(2, inner);
            }

            return null;
        }

        private static Operand Compile(UnaryNode node)
        {
            if (node == null)
            {
                return null;
            }

            var inner = Compile(node.Inner);

            switch (node.Token.Type)
            {
                case TokenType.Factorial: return Factorial.Create(inner);
                case TokenType.Function: return CompileFunction(node.Token, inner);
            }

            return null;
        }

        private static Operand Compile(BinaryNode node)
        {
            if (node == null)
            {
                return null;
            }

            if (node.Token.Type != TokenType.Sign && node.Token.Type != TokenType.OperatorPriority1)
            {
                return null;
            }

            var left = Compile(node.Left);
            var right = Compile(node.Right);

            switch (node.Token.Value)
            {
                case "+": return left != null && right != null ? Addition.Create(left, right) : left ?? right;
                case "-": return left != null && right != null ? Subtraction.Create(left, right) : (left == null ? Negative.Create(right) : left);
                case "*": return Multiplication.Create(left, right);
                case "/": return Division.Create(left, right);
                case "^": return Power.Create(left, right);
            }

            return null;
        }

        private static Operand CompileIdentifier(Token token)
        {
            if (token.Value == "π" || token.Value == "pi")
            {
                return Constant.Pi;
            }

            if (token.Value == "e")
            {
                return Constant.e;
            }

            return Variable.Create(token.Value);
        }

        private static Operand CompileConstant(Token token)
        {
            double result;
            if (double.TryParse(token.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                return Constant.Create(result);
            }

            return null;
        }

        private static Operand CompileNode(Node node)
        {
            if (node == null)
            {
                return null;
            }

            switch (node.Token.Type)
            {
                case TokenType.Identifier: return CompileIdentifier(node.Token);;
                case TokenType.Number: return CompileConstant(node.Token);
            }

            return null;
        }

        private static Operand Compile(Node node)
        {
            return Compile(node as UnaryNode) ?? Compile(node as BinaryNode) ?? CompileNode(node);
        }

        public static Operand Parse(string function)
        {
            var tokens = _lexer.Tokenize(function).ToList();
            var ast = _syntax.ParseTree(tokens);
            var result = Compile(ast);
            return result;
        }
    }
}