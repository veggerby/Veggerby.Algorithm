using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public interface IConstantWithSymbol
    {
        string Symbol { get; }
    }
}