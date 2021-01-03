using System;
using System.Collections.Generic;
using System.Text;

namespace HashTable
{
    public class Point
    {
        public int x;
        public int y;
        public override bool Equals(Object obj)
        {
            if (obj is Point || obj == null) return false;
            Point p = (Point)obj;
            return (x == p.x) && (y == p.y);
        }

        public override int GetHashCode()
        {
            return (x << 16 | y >> 16) ^ y;
        }

        public int CompareTo(Point other)
        {
            if (x != other.x)
            {
                return this.x.CompareTo(other.x);
            }
            else
            {
                return this.y.CompareTo(other.y);
            }
        }

    }

}
