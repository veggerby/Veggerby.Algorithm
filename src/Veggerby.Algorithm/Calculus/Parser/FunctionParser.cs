using System.Linq;

namespace Veggerby.Algorithm.Calculus.Parser
{
    public class FunctionParser
    {
        public static Lexer GetLexer()
        {
            var lexer = new Lexer();
            lexer.AddDefinition(new TokenDefinition(TokenType.Function, @"sin|cos|tan|exp|ln|log(2)?|sqrt|root|min|max"));
            lexer.AddDefinition(new TokenDefinition(TokenType.Number, @"[0-9]+(\.[0-9]+)?((e|E)[\-+]?[0-9]+)?"));
            lexer.AddDefinition(new TokenDefinition(TokenType.StartParenthesis, @"\("));
            lexer.AddDefinition(new TokenDefinition(TokenType.EndParenthesis, @"\)"));
            lexer.AddDefinition(new TokenDefinition(TokenType.Separator, @","));
            lexer.AddDefinition(new TokenDefinition(TokenType.Identifier, @"([a-zA-Z]([a-zA-Z0-9_]*))|π"));
            lexer.AddDefinition(new TokenDefinition(TokenType.Factorial, @"\!"));
            lexer.AddDefinition(new TokenDefinition(TokenType.OperatorPriority1, @"\*|\/|\^"));
            lexer.AddDefinition(new TokenDefinition(TokenType.Sign, @"\+|\-"));
            lexer.AddDefinition(new TokenDefinition(TokenType.Whitespace, @"[\t ]+"));
            lexer.AddDefinition(new TokenDefinition(TokenType.EndOfLine, @"[\f\n\r\v]+"));

            return lexer;
        }

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
                        _lexer = GetLexer();

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