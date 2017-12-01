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
        private static Compiler _compiler;
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

                        _compiler = new Compiler();

                        _isInitialized = true;
                    }
                }
            }
        }

        public static Operand Parse(string function)
        {
            var tokens = _lexer.Tokenize(function).ToList();
            var ast = _syntax.ParseTree(tokens);
            var result = _compiler.Compile(ast);
            return result;
        }
    }
}