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

            // TODO: return path
            return null;
        }
    }
}