using System;

namespace AdventOfCode.Instrumentation.DataTypes
{
    public class Coordinate3D
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Z;

        public Coordinate3D(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static implicit operator Coordinate3D((int x, int y, int z) a) => new(a.x, a.y, a.z);

        public static implicit operator (int x, int y, int z)(Coordinate3D a) => (a.X, a.Y, a.Z);
        public static Coordinate3D operator +(Coordinate3D a) => a;
        public static Coordinate3D operator +(Coordinate3D a, Coordinate3D b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Coordinate3D operator -(Coordinate3D a) => new(-a.X, -a.Y, -a.Z);
        public static Coordinate3D operator -(Coordinate3D a, Coordinate3D b) => a + (-b);
        public static bool operator ==(Coordinate3D a, Coordinate3D b) => (a.X == b.X && a.Y == b.Y && a.Z == b.Z);
        public static bool operator !=(Coordinate3D a, Coordinate3D b) => (a.X != b.X || a.Y != b.Y || a.Z != b.Z);

        public int ManhattanDistance(Coordinate3D other) => (int)(Math.Abs(X - other.X) + Math.Abs(Y - other.Y) + Math.Abs(Z - other.Z));
        public int ManhattanMagnitude() => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Coordinate3D)) return false;
            return this == (Coordinate3D)obj;
        }

        public override int GetHashCode()
        {
            return (137 * X + 149 * Y + 163 * Z);
        }

        public static Coordinate3D[] GetNeighbors()
        {
            return Neighbors3D;
        }

        private static readonly Coordinate3D[] Neighbors3D =
        {
                (-1,-1,-1),(-1,-1,0),(-1,-1,1),(-1,0,-1),(-1,0,0),(-1,0,1),(-1,1,-1),(-1,1,0),(-1,1,1),
                (0,-1,-1), (0,-1,0), (0,-1,1), (0,0,-1),          (0,0,1), (0,1,-1), (0,1,0), (0,1,1),
                (1,-1,-1), (1,-1,0), (1,-1,1), (1,0,-1), (1,0,0), (1,0,1), (1,1,-1), (1,1,0), (1,1,1)
            };

        public void Deconstruct(out int x, out int y, out int z)
        {
            x = X;
            y = Y;
            z = Z;
        }
    }
}
