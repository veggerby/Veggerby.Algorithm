namespace Veggerby.Algorithm.Calculus.Parser;

public class Group(Group parent, Token token)
{
    public Group Parent { get; } = parent;
    public Token Token { get; } = token;

    private readonly IList<Group> _children = new List<Group>();
    public IEnumerable<Group> Children => _children;

    public Group AddChildToken(Token token)
    {
        var child = new Group(this, token);
        _children.Add(child);
        return child;
    }

    public Group AddSiblingToken(Token token)
    {
        return Parent.AddChildToken(token);
    }

    public override string ToString()
    {
        var children = _children.Any() ? $"[{string.Join(", ", _children.Select(x => x.ToString()))}]" : string.Empty;
        return $"{Token}{children}";
    }
}