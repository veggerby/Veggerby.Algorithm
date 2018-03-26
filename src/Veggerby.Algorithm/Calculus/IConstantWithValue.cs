using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public interface IConstantWithValue
    {
        double Value { get; }
    }
}