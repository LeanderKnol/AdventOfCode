using System;

namespace AdventOfCode.Instrumentation.DataTypes
{
    public class Coordinate2D
    {
        //public static readonly Coordinate2D origin = new(0, 0);
        //public static readonly Coordinate2D unit_x = new(1, 0);
        //public static readonly Coordinate2D unit_y = new(0, 1);
        public readonly int X;
        public readonly int Y;

        public Coordinate2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Coordinate2D((int x, int y) coordinate)
        {
            this.X = coordinate.x;
            this.Y = coordinate.y;
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
            return new Coordinate2D(Y, -X);
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
            return new Coordinate2D(-Y, X);
        }

        public static Coordinate2D operator +(Coordinate2D a) => a;
        public static Coordinate2D operator +(Coordinate2D a, Coordinate2D b) => new(a.X + b.X, a.Y + b.Y);
        public static Coordinate2D operator -(Coordinate2D a) => new(-a.X, -a.Y);
        public static Coordinate2D operator -(Coordinate2D a, Coordinate2D b) => a + (-b);
        public static Coordinate2D operator *(int scale, Coordinate2D a) => new(scale * a.X, scale * a.Y);
        public static bool operator ==(Coordinate2D a, Coordinate2D b) => (a.X == b.X && a.Y == b.Y);
        public static bool operator !=(Coordinate2D a, Coordinate2D b) => (a.X != b.X || a.Y != b.Y);

        public static implicit operator Coordinate2D((int x, int y) a) => new(a.x, a.y);

        public static implicit operator (int x, int y)(Coordinate2D a) => (a.X, a.Y);

        public int ManDistance(Coordinate2D other)
        {
            int x = Math.Abs(this.X - other.X);
            int y = Math.Abs(this.Y - other.Y);
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
            return (100 * X + Y).GetHashCode();
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        public static Coordinate2D[] GetNeighbors()
        {
            return Neighbors2D;
        }

        private static readonly Coordinate2D[] Neighbors2D =
        {
            (-1,-1),(-1,0),(-1,1),
            (0,-1),        (0,1),
            (1,-1), (1,0), (1,1),
        };

        public static Coordinate2D[] GetTotalSquare()
        {
            return TotalSquare2D;
        }

        private static readonly Coordinate2D[] TotalSquare2D =
        {
            (-1,-1),(0,-1),(1,-1),
            (-1,0), (0,0), (1,0),
            (-1,1), (0,1), (1,1)
        };
    }

}
