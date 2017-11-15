using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Veggerby.Algorithm.Calculus.Parser
{
    public class Lexer
    {
        private Regex _endOfLineRegex = new Regex(@"\r\n|\r|\n", RegexOptions.Compiled);

        private readonly IList<TokenDefinition> _tokenDefinitions;

        public Lexer()
        {
            _tokenDefinitions = new List<TokenDefinition>();
        }

        public void AddDefinition(TokenDefinition tokenDefinition)
        {
            _tokenDefinitions.Add(tokenDefinition);
        }

        public IEnumerable<Token> Tokenize(string source)
        {
            var currentPosition = new TokenPosition(0, 1, 0);

            if (!string.IsNullOrEmpty(source))
            {
                yield return new Token(TokenType.Start, null, currentPosition);
            }

            while (currentPosition.Index < source.Length)
            {
                var match = _tokenDefinitions
                    .Select(x => x.FindMatch(source, currentPosition))
                    .Where(x => x != null)
                    .FirstOrDefault();

                if (match == null)
                {
                    throw new Exception($"Unrecognized symbol '{source[currentPosition.Index]}' at index {currentPosition.Index} (line {currentPosition.Line}, column {currentPosition.Column}).");
                }

                yield return match;

                var currentLine = currentPosition.Line;
                var currentColumn = currentPosition.Column;

                if (match.Type == TokenType.EndOfLine)
                {
                    currentLine += 1;
                    currentColumn = 0;
                }
                else
                {
                    currentColumn += match.Value.Length;
                }

                currentPosition = new TokenPosition(
                    currentPosition.Index + match.Value.Length,
                    currentLine,
                    currentColumn);
            }

            yield return new Token(TokenType.End, null, currentPosition);
        }
    }
}