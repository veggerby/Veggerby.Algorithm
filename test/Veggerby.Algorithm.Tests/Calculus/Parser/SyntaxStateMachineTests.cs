using System;

using FluentAssertions;

using Veggerby.Algorithm.Calculus.Parser;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Parser;

public class SyntaxStateMachineTests
{
    [Theory]
    [InlineData(TokenType.Number)]
    [InlineData(TokenType.Identifier)]
    [InlineData(TokenType.OperatorPriority1)]
    [InlineData(TokenType.Sign)]
    [InlineData(TokenType.Factorial)]
    [InlineData(TokenType.Function)]
    [InlineData(TokenType.StartParenthesis)]
    [InlineData(TokenType.EndParenthesis)]
    [InlineData(TokenType.Separator)]
    [InlineData(TokenType.Whitespace)]
    [InlineData(TokenType.EndOfLine)]
    public void Shoud_fail_when_first_state_is_not_start(TokenType tokenType)
    {
        // arrange
        var sfsm = new SyntaxStateMachine();
        var token = new Token(tokenType, "dummy", new TokenPosition(0, 1, 0));

        // act
        var actual = () => sfsm.GetNext(token);

        // assert
        actual.Should().Throw<Exception>().WithMessage("*Tree has to start with a start node*");
    }

    [Fact]
    public void Shoud_allow_first_state_is_not_start()
    {
        var sfsm = new SyntaxStateMachine();
        var token = new Token(TokenType.Start, "dummy", new TokenPosition(0, 1, 0));

        // act
        var actual = sfsm.GetNext(token);

        // assert
        actual.Token.Should().Be(token);
    }

    [Theory]
    [InlineData(TokenType.Number)]
    [InlineData(TokenType.Sign)]
    [InlineData(TokenType.Identifier)]
    [InlineData(TokenType.Function)]
    [InlineData(TokenType.StartParenthesis)]
    public void Should_allow_first_transition(TokenType tokenType)
    {
        // arrange
        var sfsm = new SyntaxStateMachine();
        sfsm.GetNext(new Token(TokenType.Start, "dummy", new TokenPosition(0, 1, 0)));

        var token = new Token(tokenType, "dummy", new TokenPosition(0, 1, 0));

        // act
        var actual = sfsm.GetNext(token);

        // assert
        actual.Token.Should().Be(token);
    }

    [Theory]
    [InlineData(TokenType.OperatorPriority1)]
    [InlineData(TokenType.Factorial)]
    [InlineData(TokenType.EndParenthesis)]
    public void Should_reject_first_transition(TokenType tokenType)
    {
        // arrange
        var sfsm = new SyntaxStateMachine();
        sfsm.GetNext(new Token(TokenType.Start, "dummy", new TokenPosition(0, 1, 0)));

        var token = new Token(tokenType, "dummy", new TokenPosition(0, 1, 0));

        // act
        var actual = () => sfsm.GetNext(token);

        // assert
        actual.Should().Throw<Exception>().WithMessage($"*Invalid transition from Start to {tokenType}*");
    }

    [Fact]
    public void Should_reject_invalid_transition()
    {
        // arrange
        var sfsm = new SyntaxStateMachine();
        sfsm.GetNext(new Token(TokenType.Start, "dummy", new TokenPosition(0, 1, 0)));
        sfsm.GetNext(new Token(TokenType.Identifier, "dummy", new TokenPosition(0, 1, 0)));
        sfsm.GetNext(new Token(TokenType.Sign, "dummy", new TokenPosition(0, 1, 0)));
        sfsm.GetNext(new Token(TokenType.Number, "dummy", new TokenPosition(0, 1, 0)));

        var token = new Token(TokenType.StartParenthesis, "dummy", new TokenPosition(0, 1, 0));

        // act
        var actual = () => sfsm.GetNext(token);

        // assert
        actual.Should().Throw<Exception>().WithMessage("*Invalid transition from Number to StartParenthesis*");
    }
}