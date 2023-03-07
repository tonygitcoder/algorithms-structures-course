namespace a_star;

using Kse.Algorithms.Samples;

internal class Program
{
    public static void Main(string[] args)
    {
        const int maxHeight = 11;
        const int maxWidth = 33;
        const int carSpeed = 60;
        const bool addTraffic = false;
        const bool astar = true;
    
        var generator = new MapGenerator(new MapGeneratorOptions()
        {
            Height = maxHeight,
            Width = maxWidth,
            AddTraffic = addTraffic,
            Seed = 123,
        });

        string[,] map = generator.Generate();
    
        var start = new Point(0, 0);
        var goal = new Point(maxWidth-1, maxHeight-1);
        
        // To break the wall on the goal point
        // or the map needs to have odd number of rows and columns
        // map[goal.Column, goal.Row] = "0";
        // map[goal.Column, goal.Row] = "6";
    
        var pathFinder = new PathFinder(maxWidth, maxHeight, start, goal, carSpeed, addTraffic, astar);
        
        var shortestPath = pathFinder.GetShortestPath(map, start, goal);
        
        var visited = pathFinder.GetVisitedPoints();
        new MapPrinter().PrintMaze(map, start, goal, visited, shortestPath);
    }
}