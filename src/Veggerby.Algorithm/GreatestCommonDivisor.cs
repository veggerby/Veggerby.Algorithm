namespace Veggerby.Algorithm;

public class GreatestCommonDivisor
{
    public static int Euclid(int a, int b)
    {
        while (b != 0)
        {
            var t = b;
            b = a % b;
            a = t;
        }

        return a;
    }
}