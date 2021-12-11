using System;

namespace AdventOfCode.Instrumentation
{
    public class Coordinate2D
    {
        //public static readonly Coordinate2D origin = new(0, 0);
        //public static readonly Coordinate2D unit_x = new(1, 0);
        //public static readonly Coordinate2D unit_y = new(0, 1);
        public readonly int x;
        public readonly int y;

        public Coordinate2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Coordinate2D((int x, int y) coordinate)
        {
            this.x = coordinate.x;
            this.y = coordinate.y;
        }

        public Coordinate2D RotateCW(int degrees, Coordinate2D center)
        {
            Coordinate2D offset = center - this;
            return center + offset.RotateCW(degrees);
        }
        public Coordinate2D RotateCW(int degrees)
        {
            return ((degrees / 90) % 4) switch
            {
                0 => this,
                1 => RotateCW(),
                2 => -this,
                3 => RotateCCW(),
                _ => this,
            };
        }

        private Coordinate2D RotateCW()
        {
            return new Coordinate2D(y, -x);
        }

        public Coordinate2D RotateCCW(int degrees, Coordinate2D center)
        {
            Coordinate2D offset = center - this;
            return center + offset.RotateCCW(degrees);
        }
        public Coordinate2D RotateCCW(int degrees)
        {
            return ((degrees / 90) % 4) switch
            {
                0 => this,
                1 => RotateCCW(),
                2 => -this,
                3 => RotateCW(),
                _ => this,
            };
        }

        private Coordinate2D RotateCCW()
        {
            return new Coordinate2D(-y, x);
        }

        public static Coordinate2D operator +(Coordinate2D a) => a;
        public static Coordinate2D operator +(Coordinate2D a, Coordinate2D b) => new(a.x + b.x, a.y + b.y);
        public static Coordinate2D operator -(Coordinate2D a) => new(-a.x, -a.y);
        public static Coordinate2D operator -(Coordinate2D a, Coordinate2D b) => a + (-b);
        public static Coordinate2D operator *(int scale, Coordinate2D a) => new(scale * a.x, scale * a.y);
        public static bool operator ==(Coordinate2D a, Coordinate2D b) => (a.x == b.x && a.y == b.y);
        public static bool operator !=(Coordinate2D a, Coordinate2D b) => (a.x != b.x || a.y != b.y);

        public static implicit operator Coordinate2D((int x, int y) a) => new(a.x, a.y);

        public static implicit operator (int x, int y)(Coordinate2D a) => (a.x, a.y);

        public int ManDistance(Coordinate2D other)
        {
            int x = Math.Abs(this.x - other.x);
            int y = Math.Abs(this.y - other.y);
            return x + y;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Coordinate2D)) return false;
            return this == (Coordinate2D)obj;
        }

        public override int GetHashCode()
        {
            return (100 * x + y).GetHashCode();
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

    }

}
