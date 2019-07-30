using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Calculus.Parser
{
    public class Group
    {
        public Group Parent { get; }
        public Token Token { get; }

        private IList<Group> _children;
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

        public Group(Group parent, Token token)
        {
            Token = token;
            Parent = parent;
            _children = new List<Group>();
        }

        public override string ToString()
        {
            var children = _children.Any() ? $"[{string.Join(", ", _children.Select(x => x.ToString()))}]" : string.Empty;
            return $"{Token}{children}";
        }
    }
}