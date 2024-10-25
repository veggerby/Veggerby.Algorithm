namespace Veggerby.Algorithm.Calculus.Parser;

public enum TokenType
{
    Start,
    Number,
    Identifier,
    OperatorPriority1,
    Sign,
    Factorial,
    Function,
    StartParenthesis,
    EndParenthesis,
    Separator,
    Whitespace,
    EndOfLine,
    End
}