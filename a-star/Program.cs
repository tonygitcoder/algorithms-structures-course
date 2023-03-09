namespace a_star;

using Kse.Algorithms.Samples;

internal class Program
{
    public static void Main(string[] args)
    {
        // The map needs to have odd number of rows and columns
        const int maxHeight = 35;
        const int maxWidth = 91;
        const int seed = 123;
        
        const int carSpeed = 60;
        const bool addTraffic = true;
        
        const bool astar = true;
    
        var generator = new MapGenerator(new MapGeneratorOptions()
        {
            Height = maxHeight,
            Width = maxWidth,
            AddTraffic = addTraffic,
            Seed = seed,
        });

        string[,] map = generator.Generate();
    
        var start = new Point(0, 0);
        var goal = new Point(maxWidth-1, maxHeight-1);
        
        var pathFinder = new PathFinder(carSpeed, addTraffic, astar);
        
        var origins = pathFinder.CalculateTotalDistances(start, goal, map);
        var shortestPath = pathFinder.GetShortestPath(map, start, goal, origins);
        
        // var visited = pathFinder.GetVisitedPoints();
        new MapPrinter().PrintMaze(map, start, goal, origins, shortestPath);
    }
}