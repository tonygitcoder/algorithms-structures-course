namespace a_star;

using Kse.Algorithms.Samples;

internal class Program
{
    public static void Main(string[] args)
    {
        const int maxHeight = 31;
        const int maxWidth = 91;
        const bool addTraffic = true;
    
        var generator = new MapGenerator(new MapGeneratorOptions()
        {
            Height = maxHeight,
            Width = maxWidth,
            Seed = 123,
            AddTraffic = addTraffic,
        });

        string[,] map = generator.Generate();
    
        var start = new Point(0, 0);
        var goal = new Point(maxWidth-1, maxHeight-1);
        
        // To break the wall on the goal point
        map[goal.Column, goal.Row] = "6";
    
        var pathFinder = new PathFinder(maxWidth, maxHeight, addTraffic);
        
        var shortestPath = pathFinder.GetShortestPath(map, start, goal);
        new MapPrinter().PrintMaze(map, start, goal, shortestPath);
    }
}