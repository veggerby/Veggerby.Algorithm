namespace Veggerby.Algorithm.LinearAlgebra;

public static class Extensions
{
    public static double[,] ToArray(this IEnumerable<Vector> rows)
    {
        if (rows is not null)
        {
            var rowList = rows.ToList();
            var maxSize = rowList.Max(x => x.Size);
            var result = new double[rowList.Count(), maxSize];
            var r = 0;

            foreach (var row in rowList)
            {
                for (int c = 0; c < row.Size; c++)
                {
                    result[r, c] = row[c];
                }

                r++;
            }

            return result;
        }

        return null;
    }

    public static bool IsAll(this Matrix m, Func<int, int, double, bool> predicate)
    {
        for (int r = 0; r < m.RowCount; r++)
        {
            for (var c = 0; c < m.ColCount; c++)
            {
                if (!predicate(r, c, m[r, c]))
                {
                    return false;
                }
            }
        }

        return true;
    }
}