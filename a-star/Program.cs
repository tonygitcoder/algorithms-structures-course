using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Xml;
using Kse.Algorithms.Samples;

internal class Program
{
    public static void Main(string[] args)
    {
        var generator = new MapGenerator(new MapGeneratorOptions()
        {
            Height = 35,
            Width = 90,
        });

        string[,] map = generator.Generate();

        var shortestPath = GetShortestPath(map, new Point(0, 0), new Point(89, 34));
        new MapPrinter().Print(map, shortestPath);

        List<Point> GetShortestPath(string[,] map, Point start, Point goal)
        {
            map[start.Column, start.Row] = "A";
            map[goal.Column, goal.Row] = "B";

            var visitedPoints = new List<Point>() { start };
            var distances = new Dictionary<Point, int>()
            {
                { new Point(start.Column, start.Row), 0 }
            };
            var origins = new Dictionary<Point, Point>();

            Console.WriteLine("GetNeighbours: " + GetNeighbours(5, 5, map).Count);

            // TODO: return path
            return null;


            List<Point> GetNeighbours(int column, int row, string[,] map)
            {
                var neighbours = new List<Point>();

                bool IsTraversable(Point point) => CheckPosition(point, map) == " ";


                var topNeighbour = new Point(column, row - 1);
                if (IsTraversable(topNeighbour))
                {
                    neighbours.Add(topNeighbour);
                }

                var bottomNeighbour = new Point(column, row + 1);
                if (IsTraversable(bottomNeighbour))
                {
                    neighbours.Add(bottomNeighbour);
                }

                var leftNeighbour = new Point(column - 1, row);
                if (IsTraversable(leftNeighbour))
                {
                    neighbours.Add(leftNeighbour);
                }

                var rightNeighbour = new Point(column + 1, row);
                if (IsTraversable(rightNeighbour))
                {
                    neighbours.Add(rightNeighbour);
                }

                return neighbours;
            }

            string CheckPosition(Point point, string[,] map)
            {
                var leftBorder = point.Column < 0;
                var rightBorder = point.Column >= map.GetLength(0);
                var topBorder = point.Row < 0;
                var bottomBorder = point.Row >= map.GetLength(1);

                // TODO: catch exception
                if (leftBorder || rightBorder || topBorder || bottomBorder) return "";
                return map[point.Column, point.Row];
            }
        }
    }
}