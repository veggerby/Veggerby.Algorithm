namespace Veggerby.Algorithm.Calculus.Parser;

public class SyntaxParser
{
    public Node ParseTree(IEnumerable<Token> tokens)
    {
        if (tokens is null)
        {
            throw new ArgumentNullException(nameof(tokens));
        }

        var fsm = new SyntaxStateMachine();

        foreach (var token in tokens)
        {
            fsm.GetNext(token);
        }

        if (!fsm.IsClosed)
        {
            throw new ArgumentException("Parenthesis not properly closed");
        }

        return ParseGroup(fsm.Root);
    }

    private Node ParseGroupChildrenForBinary(TokenType type, string value, IEnumerable<Group> children)
    {
        // we need to find the item to the far right to properly chain operations, so that e.g. a-b-c becomes (a-b)-c and not a-(b-c)

        var childList = children.ToList();

        var groups = new List<List<Group>>();
        groups.Add(new List<Group>());
        Group previous = null;

        foreach (var group in childList)
        {
            if ((group.Token.Type == type && string.Equals(group.Token.Value, value, StringComparison.OrdinalIgnoreCase)) &&
                previous is not null && (previous.Token.Type != TokenType.Sign && previous.Token.Type != TokenType.OperatorPriority1))
            {
                groups.Add(new List<Group>());
            }

            groups.Last().Add(group);

            previous = group;
        }

        if (!groups.Where(x => x.Any()).Any())
        {
            return null;
        }

        var last = groups.Last();
        // token = first item in last group
        var tokenGroup = last.First();

        if (tokenGroup.Token.Type != type || !string.Equals(tokenGroup.Token.Value, value, StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        // left groups = all groups except the last
        var leftGroups = groups.Except([last]).SelectMany(x => x).ToList();

        // right groups = all items except first in last group
        var rightGroups = last.Skip(1).ToList();

        var left = ParseGroupChildren(leftGroups);
        var right = ParseGroupChildren(rightGroups);

        return new BinaryNode(left, tokenGroup.Token, right);
    }

    private Node ParseGroupChildrenForBinaryFunction(TokenType type, string[] values, IEnumerable<Group> children)
    {
        if (children.Count() != 3)
        {
            return null;
        }

        var function = children.First();

        if (!values.Any(x => string.Equals(x, function.Token.Value, StringComparison.OrdinalIgnoreCase)))
        {
            return null;
        }

        var leftGroup = children.Skip(1).First();
        var rightGroup = children.Last();

        var left = ParseGroup(leftGroup);
        var right = ParseGroup(rightGroup);

        return new BinaryNode(left, function.Token, right);
    }

    private Node ParseGroupChildrenForUnary(TokenType type, IEnumerable<Group> children)
    {
        if (children.Count() != 2)
        {
            return null;
        }

        var first = children.First();

        if (first.Token.Type != type)
        {
            return null;
        }

        var second = children.Last();

        var inner = ParseGroup(second);
        return new UnaryNode(first.Token, inner);
    }

    private Node ParseGroupChildren(IEnumerable<Group> children)
    {
        if (children is null || !children.Any())
        {
            return null;
        }

        if (children.Count() == 1)
        {
            return ParseGroup(children.Single());
        }

        return
            ParseGroupChildrenForBinary(TokenType.Sign, "+", children) ??
            ParseGroupChildrenForBinary(TokenType.Sign, "-", children) ??
            ParseGroupChildrenForBinary(TokenType.OperatorPriority1, "*", children) ??
            ParseGroupChildrenForBinary(TokenType.OperatorPriority1, "/", children) ??
            ParseGroupChildrenForBinary(TokenType.OperatorPriority1, "^", children) ??
            ParseGroupChildrenForBinaryFunction(TokenType.Function, ["root", "min", "max"], children) ??
            ParseGroupChildrenForUnary(TokenType.Factorial, children.Reverse()) ??
            ParseGroupChildrenForUnary(TokenType.Function, children);
    }

    private Node ParseGroup(Group group)
    {
        if (!group.Children.Any())
        {
            return new Node(group.Token);
        }

        var childNode = ParseGroupChildren(group.Children);

        switch (group.Token.Type)
        {
            case TokenType.Start:
                return childNode;
            case TokenType.Function:
                return new UnaryNode(group.Token, childNode);
        }

        return childNode;
    }
}