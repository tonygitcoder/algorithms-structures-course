namespace a_star;

using Kse.Algorithms.Samples;

internal class Program
{
    public static void Main(string[] args)
    {
        const int maxHeight = 11;
        const int maxWidth = 31;
    
        var generator = new MapGenerator(new MapGeneratorOptions()
        {
            Height = maxHeight,
            Width = maxWidth,
            Seed = 123,
            //AddTraffic = true,
            //TrafficSeed = 1234
        });

        string[,] map = generator.Generate();
    
        var start = new Point(0, 0);
        var goal = new Point(maxWidth-1, maxHeight-1);
    
        // map[goal.Column, goal.Row] = " ";
    
        var pathFinder = new PathFinder();
        var shortestPath = pathFinder.GetShortestPath(map, start, goal);
        new MapPrinter().Print(map, shortestPath, start, goal);
    }
}