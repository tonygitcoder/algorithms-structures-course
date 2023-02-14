namespace Kse.Algorithms.Samples
{
    public struct Point : IEquatable<Point>
    {
        public int Column { get; }
        public int Row { get; }

        public Point(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public bool Equals(Point other)
        {
            return Column == other.Column && Row == other.Row;
        }
        
        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            return obj is Point other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Column, Row);
        }
    }
}