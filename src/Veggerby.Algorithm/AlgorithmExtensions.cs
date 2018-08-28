namespace Veggerby.Algorithm
{
    public static class AlgorithmExtensions
    {
        public static bool IsInfinity(this int i)
        {
            return i == int.MaxValue;
        }

        public static int InfinityAdd(this int i, int j)
        {
            return i.IsInfinity() || j.IsInfinity() ? int.MaxValue : i + j;
        }
    }
}