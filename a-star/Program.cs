namespace a_star;

using Kse.Algorithms.Samples;

internal class Program
{
    public static void Main(string[] args)
    {
        const int maxHeight = 35;
        const int maxWidth = 90;
        
        var generator = new MapGenerator(new MapGeneratorOptions()
        {
            Height = maxHeight,
            Width = maxWidth,
        });

        string[,] map = generator.Generate();
        
        PathFinder pathFinder = new PathFinder();
        var shortestPath
            = pathFinder.GetShortestPath(map, 
                new Point(0, 0), 
                new Point(maxWidth-1, maxHeight-1));
        new MapPrinter().Print(map, shortestPath);
    }
}