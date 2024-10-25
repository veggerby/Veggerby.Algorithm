using System.Text.RegularExpressions;

namespace Veggerby.Algorithm.Calculus.Parser;

public class TokenDefinition(TokenType returnsToken, string regexPattern)
{
    private readonly Regex _regex = new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private readonly TokenType _returnsToken = returnsToken;

    public Token FindMatch(string inputString, TokenPosition currentPosition)
    {
        var match = _regex.Match(inputString, currentPosition.Index);

        if (match.Success && (match.Index - currentPosition.Index) == 0)
        {
            var value = inputString.Substring(currentPosition.Index, match.Length);
            return new Token(_returnsToken, value, currentPosition);
        }

        return null;
    }
}