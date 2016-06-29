using System;

namespace Veggerby.Algorithm.LinearAlgebra
{
    public static class MatrixExtensions
    {
        public static bool IsSquare(this Matrix m) 
        {
            return m.RowCount == m.ColCount;
        }

        public static bool IsDiagonal(this Matrix m) 
        {
            return m.IsSquare() && m.IsAll((r, c, v) => (r == c) || (Math.Abs(v) < double.Epsilon));
        }

        public static bool IsLowerTriangular(this Matrix m) 
        {
            return m.IsSquare() && m.IsAll((r, c, v) => (r >= c) || (Math.Abs(v) < double.Epsilon));
        }

        public static bool IsUpperTriangular(this Matrix m) 
        {
            return m.IsSquare() && m.IsAll((r, c, v) => (r <= c) || (Math.Abs(v) < double.Epsilon));
        }

        public static Matrix Transpose(this Matrix m)
        {
            // return a new matrix where we use current columns as rows
            return new Matrix(m.Cols);
        }
    }
}