using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Trees
{
    public interface INode<T>
    {
        T Payload { get; set; }
    }
}