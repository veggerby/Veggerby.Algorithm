using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.LinearAlgebra
{
    public class Matrix
    {
        public static Matrix Identity(int n)
        {
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            return new Matrix(Enumerable.Range(0, n).Select(r => new Vector(Enumerable.Range(0, n).Select(c => r == c ? 1D : 0).ToArray())));
        }

        public Matrix(int rows, int cols)
        {
            if (rows <= 0) 
            {
                throw new ArgumentOutOfRangeException(nameof(rows));    
            }
            
            if (cols <= 0) 
            {
                throw new ArgumentOutOfRangeException(nameof(cols));
            }
            
            _values = new double[rows, cols];
        }

        public Matrix(double[,] values)
        {
            if (values == null) 
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (values.GetLength(0) == 0 || values.GetLength(1) == 0) 
            {
                throw new ArgumentOutOfRangeException(nameof(values));
            }

            _values = values;
        }

        public Matrix(IEnumerable<Vector> rows)
        {
            if (rows == null)
            {
                throw new ArgumentNullException(nameof(rows));
            }

            if (!rows.Any())
            {
                throw new ArgumentException(nameof(rows));
            }

            var size = rows.First().Size;

            if (rows.Any(x => x.Size != size)) 
            {
                throw new ArgumentException(nameof(rows), "All row vectors must habe same dimension");
            }

            _values = rows.ToArray();
        }

        private readonly double[,] _values;

        public int RowCount => _values.GetLength(0);
        public int ColCount => _values.GetLength(1);
        public IEnumerable<Vector> Rows => Enumerable.Range(0, RowCount).Select(GetRow);
        public IEnumerable<Vector> Cols => Enumerable.Range(0, ColCount).Select(GetCol);

        public double this[int r, int c]
        {
            get 
            { 
                if (r < 0 || r >= RowCount) 
                {
                    throw new ArgumentOutOfRangeException(nameof(r));
                }

                if (c < 0 || c >= ColCount) 
                {
                    throw new ArgumentOutOfRangeException(nameof(c));
                }

                return _values[r, c]; 
            }
        }

        public Vector GetRow(int r)
        {
            if (r < 0 || r >= RowCount)
            {
                throw new ArgumentOutOfRangeException(nameof(r));
            }

            return new Vector(Enumerable.Range(0, ColCount).Select(c => this[r, c]));
        }

        public Vector GetCol(int c)
        {
            if (c < 0 || c >= ColCount)
            {
                throw new ArgumentOutOfRangeException(nameof(c));
            }
            
            return new Vector(Enumerable.Range(0, RowCount).Select(r => this[r, c]));
        }

        public double[,] ToArray()
        {
            return Rows.ToArray();
        }

        public double Determinant
        {
            get
            {
                if (!this.IsSquare())
                {
                    throw new IndexOutOfRangeException("Cannot calculate determinant for non-square matrix");
                }

                var v = ToArray();

                var n = ColCount - 1;
                double det = 1;

                for (var k = 0; k <= n; k++)
                {
                    if (Math.Abs(v[k, k]) < double.Epsilon)
                    {
                        var j = k;
                        while ((j < n) && (Math.Abs(v[k, j]) < double.Epsilon))
                        {
                            j = j + 1;
                        }

                        if (Math.Abs(v[k, j]) < double.Epsilon)
                        {
                            return 0;
                        }
                        
                        for (var i = k; i <= n; i++)
                        {
                            var save = v[i, j];
                            v[i, j] = v[i, k];
                            v[i, k] = save;
                        }

                        det = -det;
                    }

                    var vk = v[k, k];
                    det = det * vk;
                    if (k < n)
                    {
                        int k1 = k + 1;
                        for (var i = k1; i <= n; i++)
                        {
                            for (var j = k1; j <= n; j++)
                            {
                                v[i, j] = v[i, j] - v[i, k] * (v[k, j] / vk);
                            }
                        }
                    }
                }

                return det;
            }
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if ((m1.RowCount != m2.RowCount) || (m1.ColCount != m2.ColCount))
            {
                throw new ArgumentException(nameof(m2), "Cannot add matrices with different dimensions");
            }

            return new Matrix(m1.Rows.Zip(m2.Rows, (r1, r2) => r1 + r2));
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if ((m1.RowCount != m2.RowCount) || (m1.ColCount != m2.ColCount))
            {
                throw new ArgumentException(nameof(m2), "Cannot add matrices with different dimensions");
            }

            return new Matrix(m1.Rows.Zip(m2.Rows, (r1, r2) => r1 - r2));
        }

        public static Matrix operator *(double factor, Matrix m)
        {
            return new Matrix(m.Rows.Select(x => new Vector(x.ToArray().Select(y => factor * y))));
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.ColCount != m2.RowCount)
            {
                throw new ArgumentException(nameof(m2), "m1 must have same number of columns as m2 has rows");
            }

            int r = 0;
            var result = new double[m1.RowCount, m2.ColCount];
            foreach (var r1 in m1.Rows)
            {
                int c = 0;
                foreach (var c2 in m2.Cols)
                {
                    result[r, c] = r1.ToArray().Zip(c2.ToArray(), (rv, cv) => rv * cv).Sum();
                    c++;
                }

                r++;
            }

            return new Matrix(result);
        }

        public static bool operator ==(Matrix m1, Matrix m2)
        {
            return Equals(m1, m2);
        }

        public static bool operator !=(Matrix m1, Matrix m2)
        {
            return !Equals(m1, m2);
        }
        
        public override string ToString()
        {
            return "(" + string.Join(Environment.NewLine, Rows.Select(x => x.ToString())) + ")";
        }               

        protected bool Equals(Matrix other)
        {
            return Rows.SequenceEqual(other.Rows);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Matrix)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _values?.GetHashCode() ?? 0;
                return hashCode;
            }
        }
    }
}
