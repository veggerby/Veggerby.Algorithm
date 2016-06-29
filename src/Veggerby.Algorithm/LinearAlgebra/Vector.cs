using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Veggerby.Algorithm.LinearAlgebra
{
    public class Vector
    {        
        public Vector(int d)
        {
            if (d <= 0) 
            {
                throw new ArgumentOutOfRangeException(nameof(d));
            }

            _values = new double[d];
        }

        public Vector(params double[] values) : this(values.AsEnumerable())
        {
        }

        public Vector(IEnumerable<double> values)
        {
            if (values == null) 
            {
                throw new ArgumentNullException(nameof(values));
            }
            
            if (!values.Any()) 
            {
                throw new ArgumentException(nameof(values), "Cannot create empty Vector");
            }

            _values = values.ToArray();
        }

        private readonly double[] _values;

        public int Size => _values.GetLength(0);

        public double this[int i]
        {
            get 
            { 
                if (i < 0 || i >= Size) 
                {
                    throw new ArgumentOutOfRangeException(nameof(i));
                }

                return _values[i]; 
            }
        }

        public double[] ToArray()
        {
            return _values.ToArray();
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            if (v1.Size != v2.Size)
            {
                throw new ArgumentException(nameof(v2), "Cannot add vectors with different dimensions");
            }

            return new Vector(v1.ToArray().Zip(v2.ToArray(), (x, y) => x + y));
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            if (v1.Size != v2.Size)
            {
                throw new ArgumentException(nameof(v2), "Cannot subtract vectors with different dimensions");
            }

            return new Vector(v1.ToArray().Zip(v2.ToArray(), (x, y) => x - y));
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            return Equals(v1, v2);
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !Equals(v1, v2);
        }

        public override string ToString()
        {
            return "(" + string.Join(", ", _values.Select(x => x.ToString(CultureInfo.InvariantCulture))) + ")";
        }

        protected bool Equals(Vector other)
        {
            return ToArray().SequenceEqual(other.ToArray());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vector)obj);
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
