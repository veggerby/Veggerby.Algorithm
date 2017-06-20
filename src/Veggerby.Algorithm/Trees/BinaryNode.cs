using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Trees
{
    public class BinaryNode<T> : AbstractBinaryNode<T, BinaryNode<T>>
    {                
        public BinaryNode(T payload = default(T), BinaryNode<T> left = null, BinaryNode<T> right = null) : base(payload, left, right)
        {
        }
    }
}