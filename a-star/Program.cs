﻿namespace a_star;

using Kse.Algorithms.Samples;

internal class Program
{
    public static void Main(string[] args)
    {
        const int maxHeight = 11;
        const int maxWidth = 31;
        const bool addTraffic = false;
    
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
        map[goal.Column, goal.Row] = " ";
    
        var pathFinder = new PathFinder(addTraffic);
        
        var shortestPath = pathFinder.GetShortestPath(map, start, goal);
        new MapPrinter().PrintMaze(map, start, goal, shortestPath);
    }
}